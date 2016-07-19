using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class GoodsMovementBLL : BaseBLL<GoodsMovement>
    {
        private readonly IGoodsMovementDAL idal = DALFactory.Helper.GetIGoodsMovementDAL();
        private readonly IGoodsMovementItemDAL idalGMItem = DALFactory.Helper.GetIGoodsMovementItemDAL();
        private readonly IGMSecondCheckDAL idalSecondCheck = DALFactory.Helper.GetIGMSecondCheckDAL();
        private readonly IGMReaderDAL idalReader = DALFactory.Helper.GetIGMReaderDAL();

        public List<GoodsMovement> GetGoodsMovementsByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {
            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public List<GoodsMovement> GetGoodsMovementsByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public GoodsMovement GetGMWithItems(string gmId)
        {
            return idal.GetGMWithItems(gmId);
        }

        public GoodsMovement GetGoodsMovement(string gmId)
        {
            return idal.GetGoodsMovement(gmId);
        }

        public bool Save(params GoodsMovement[] entites)
        {
            if (entites == null || entites.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entites.SerializeToJson());
            if (!RepeatCheck(entites))
            {
                return false;
            }
            return idal.Save(entites);
        }

        public bool Delete(params string[] entityIds)
        {
            idalGMItem.DeleteByGMIds(entityIds);
            idalReader.DeleteByGMIds(entityIds);
            idalSecondCheck.DeleteByGMIds(entityIds);
            return idal.Delete(entityIds);
        }

        public override bool RepeatCheck(GoodsMovement[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.GoodsMovementCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from e in entities
                            where ValidateUtil.isBlank(e.GoodsMovementID)
                            select e.GoodsMovementCode).ToArray();
            if (newCodes == null || newCodes.Length < 1)
            {
                return true;
            }
            return !idal.Exists(newCodes);
        }

        public bool SubmitToFirstChecker(string checker, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(checker) || gmIds.Length < 1)
            {
                return false;
            }
            return idal.SubmitToFirstChecker(checker, gmIds);
        }

        public bool SubmitToSecondChcker(string checker, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(checker) || gmIds.Length < 1)
            {
                return false;
            }
            //1、插入复审记录
            bool rst = false;
            List<GMSecondCheck> list = new List<GMSecondCheck>();
            foreach (string gmId in gmIds)
            {
                list.Add(new GMSecondCheck
                {
                    GMSecondCheckID = Guid.NewGuid().ToString(),
                    GoodsMovementID = gmId,
                    SecondChecker = checker,
                    SecondCheckTime = DateTime.Now,
                    SecondCheckView = "",
                    CheckFlag = "0"
                });
            }
            rst = idalSecondCheck.Save(list.ToArray());
            if (rst)
            {
                //设置货物移动状态和复审人
                rst = idal.SubmitToSecondChecker(checker, gmIds);
            }
            return rst;
        }

        public bool SubmitToReader(string reader, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(reader) || gmIds.Length < 1)
            {
                return false;
            }
            //1、新增分阅记录
            bool rst = false;
            List<GMReader> list = new List<GMReader>();
            foreach (string gmId in gmIds)
            {
                list.Add(new GMReader
                {
                    GoodsMovementID = gmId,
                    GMReadID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "0"
                });
            }
            rst = idalReader.Save(list.ToArray());
            if (rst)
            {
                //2、设置货物移动分阅人
                rst = idal.SubmitToReader(reader, gmIds);
            }
            return rst;
        }

        public bool FirstCheck(bool result, string checkView, params string[] gmIds)
        {
            if (gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            return idal.FirstCheck(result, checkView, gmIds);
        }

        public bool SecondCheck(bool result, string checker, string checkView, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(checker) || gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            if (ValidateUtil.isBlank(checker) || gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            //1、更改货物移动状态
            bool rst = false;
            rst = idal.SecondCheck(result, gmIds);
            if (rst)
            {
                List<GMSecondCheck> list = new List<GMSecondCheck>();
                foreach (string gmId in gmIds)
                {
                    list.Add(new GMSecondCheck
                    {
                        GMSecondCheckID = Guid.NewGuid().ToString(),
                        GoodsMovementID = gmId,
                        SecondChecker = checker,
                        SecondCheckTime = DateTime.Now,
                        SecondCheckView = checkView,
                        CheckFlag = result ? "2" : "1"
                    });
                }
                //2、插入复审记录
                rst = idalSecondCheck.Save(list.ToArray());
            }
            return rst;
        }

        public bool Read(string reader, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(reader) || gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            //1、插入分阅记录
            List<GMReader> list = new List<GMReader>();
            foreach (string id in gmIds)
            {
                list.Add(new GMReader
                {
                    GMReadID = Guid.NewGuid().ToString(),
                    GoodsMovementID = id,
                    ReaderID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "1"//已分阅
                });
            }
            return idalReader.Save(list.ToArray());
        }

        public bool Close(string[] gmIds)
        {
            if (gmIds == null || gmIds.Length < 1)
            {
                return false;
            }

            return idal.Close(gmIds);
        }
    }
}
