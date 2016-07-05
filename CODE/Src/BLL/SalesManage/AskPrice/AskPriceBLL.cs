using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.GeneralClass.Extensions;

namespace OA.BLL
{
    public class AskPriceBLL : BaseBLL<AskPrice>
    {
        private readonly IAskPriceDAL idal = DALFactory.Helper.GetIAskPriceDAL();
        private readonly IAskPriceItemDAL idalSOItem = DALFactory.Helper.GetIAskPriceItemDAL();
        private readonly IAPSecondCheckDAL idalSecondCheck = DALFactory.Helper.GetIAPSecondCheckDAL();
        private readonly IAPReaderDAL idalReader = DALFactory.Helper.GetIAPReaderDAL();
        public List<AskPrice> GetAskPricesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {

            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public List<AskPrice> GetAskPricesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public AskPrice GetAskPriceWithItems(string orderId)
        {
            return idal.GetAskPriceWithItems(orderId);
        }

        public bool Save(params AskPrice[] entites)
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
            idalSOItem.DeleteByAPIds(entityIds);
            idalReader.DeleteByAPIds(entityIds);
            idalSecondCheck.DeleteByAPIds(entityIds);
            return idal.Delete(entityIds);
        }
        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(AskPrice[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.APCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from e in entities
                            where ValidateUtil.isBlank(e.APID)
                            select e.APCode).ToArray();
            if (newCodes == null || newCodes.Length < 1)
            {
                return true;
            }
            return !idal.Exists(newCodes);
        }

        public bool SubmitToFirstChecker(string checker, params string[] apIds)
        {
            if (ValidateUtil.isBlank(checker) || apIds.Length < 1)
            {
                return false;
            }
            return idal.SubmitToFirstChecker(checker, apIds);
        }

        public bool SubmitToSecondChcker(string checker, params string[] apIds)
        {
            if (ValidateUtil.isBlank(checker) || apIds.Length < 1)
            {
                return false;
            }
            //1、插入复审记录
            bool rst = false;
            List<APSecondCheck> list = new List<APSecondCheck>();
            foreach (string soId in apIds)
            {
                list.Add(new APSecondCheck
                {
                    APSecondCheckID = Guid.NewGuid().ToString(),
                    APID = soId,
                    SecondChecker = checker,
                    SecondCheckTime = DateTime.Now,
                    SecondCheckView = "",
                    CheckFlag = "0"
                });
            }
            rst = idalSecondCheck.Save(list.ToArray());
            if (rst)
            {
                //设置询价单状态和复审人
                rst = idal.SubmitToSecondChecker(checker, apIds);
            }
            return rst;
        }

        public bool SubmitToReader(string reader, params string[] apIds)
        {
            if (ValidateUtil.isBlank(reader) || apIds.Length < 1)
            {
                return false;
            }
            //1、新增分阅记录
            bool rst = false;
            List<APReader> list = new List<APReader>();
            foreach (string apId in apIds)
            {
                list.Add(new APReader
                {
                    APID = apId,
                    APReadID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "0"
                });
            }
            rst = idalReader.Save(list.ToArray());
            if (rst)
            {
                //2、设置询价单分阅人
                rst = idal.SubmitToReader(reader, apIds);
            }
            return rst;
        }

        public bool FirstCheck(bool result, string checkView, params string[] apIds)
        {
            if (apIds == null || apIds.Length < 1)
            {
                return false;
            }
            return idal.FirstCheck(result, checkView, apIds);
        }

        public bool SecondCheck(bool result, string checker, string checkView, params string[] apIds)
        {
            if (ValidateUtil.isBlank(checker) || apIds == null || apIds.Length < 1)
            {
                return false;
            }
            if (ValidateUtil.isBlank(checker) || apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、更改询价单状态
            bool rst = false;
            rst = idal.SecondCheck(result, apIds);
            if (rst)
            {
                List<APSecondCheck> list = new List<APSecondCheck>();
                foreach (string apId in apIds)
                {
                    list.Add(new APSecondCheck
                    {
                        APSecondCheckID = Guid.NewGuid().ToString(),
                        APID = apId,
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

        public bool Read(string reader, params string[] apIds)
        {
            if (ValidateUtil.isBlank(reader) || apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、插入分阅记录
            List<APReader> list = new List<APReader>();
            foreach (string id in apIds)
            {
                list.Add(new APReader
                {
                    APReadID = Guid.NewGuid().ToString(),
                    APID = id,
                    ReaderID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "1"//已分阅
                });
            }
            return idalReader.Save(list.ToArray());
        }

        public bool Close(string[] apIds)
        {
            if (apIds == null || apIds.Length < 1)
            {
                return false;
            }

            return idal.Close(apIds);
        }
    }
}
