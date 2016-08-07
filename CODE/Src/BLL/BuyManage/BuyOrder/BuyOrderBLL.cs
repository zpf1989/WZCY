using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.GeneralClass;
using OA.IDAL;
using OA.Model;

namespace OA.BLL
{
    public class BuyOrderBLL : BaseBLL<BuyOrder>
    {
        private readonly IBuyOrderDAL idal = DALFactory.Helper.GetIBuyOrderDAL();
        private readonly IBuyOrderItemDAL idalBOItem = DALFactory.Helper.GetIBuyOrderItemDAL();
        private readonly IBOSecondCheckDAL idalSecondCheck = DALFactory.Helper.GetIBOSecondCheckDAL();
        private readonly IBOReaderDAL idalReader = DALFactory.Helper.GetIBOReaderDAL();
        public List<BuyOrder> GetSaleOrdersByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {

            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public List<BuyOrder> GetSaleOrdersByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public BuyOrder GetBuyOrderWithItems(string buyId)
        {
            return idal.GetBuyOrderWithItems(buyId);
        }
        public BuyOrder GetBuyOrder(string buyId)
        {
            return idal.GetBuyOrder(buyId);
        }

        public bool Save(params BuyOrder[] entites)
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
            idalBOItem.DeleteByBOIds(entityIds);
            idalReader.DeleteByBOIds(entityIds);
            idalSecondCheck.DeleteByBOIds(entityIds);
            return idal.Delete(entityIds);
        }
        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(BuyOrder[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.BuyOrderCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from e in entities
                            where ValidateUtil.isBlank(e.BuyOrderID)
                            select e.BuyOrderCode).ToArray();
            if (newCodes == null || newCodes.Length < 1)
            {
                return true;
            }
            return !idal.Exists(newCodes);
        }

        public bool SubmitToFirstChecker(string checker, params string[] soIds)
        {
            if (ValidateUtil.isBlank(checker) || soIds.Length < 1)
            {
                return false;
            }
            return idal.SubmitToFirstChecker(checker, soIds);
        }

        public bool SubmitToSecondChcker(string checker, params string[] boIds)
        {
            if (ValidateUtil.isBlank(checker) || boIds.Length < 1)
            {
                return false;
            }
            //1、插入复审记录
            bool rst = false;
            List<BOSecondCheck> list = new List<BOSecondCheck>();
            foreach (string boId in boIds)
            {
                list.Add(new BOSecondCheck
                {
                    BOSecondCheckID = Guid.NewGuid().ToString(),
                    BuyOrderID = boId,
                    SecondChecker = checker,
                    SecondCheckTime = DateTime.Now,
                    SecondCheckView = "",
                    CheckFlag = "0"
                });
            }
            rst = idalSecondCheck.Save(list.ToArray());
            if (rst)
            {
                //设置销售订单状态和复审人
                rst = idal.SubmitToSecondChecker(checker, boIds);
            }
            return rst;
        }

        public bool SubmitToReader(string reader, params string[] boIds)
        {
            if (ValidateUtil.isBlank(reader) || boIds.Length < 1)
            {
                return false;
            }
            //1、新增分阅记录
            bool rst = false;
            List<BOReader> list = new List<BOReader>();
            foreach (string soId in boIds)
            {
                list.Add(new BOReader
                {
                    BuyOrderID = soId,
                    BOReadID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "0"
                });
            }
            rst = idalReader.Save(list.ToArray());
            if (rst)
            {
                //2、设置销售订单分阅人
                rst = idal.SubmitToReader(reader, boIds);
            }
            return rst;
        }

        public bool FirstCheck(bool result, string checkView, params string[] boIds)
        {
            if (boIds == null || boIds.Length < 1)
            {
                return false;
            }
            return idal.FirstCheck(result, checkView, boIds);
        }

        public bool SecondCheck(bool result, string checker, string checkView, params string[] boIds)
        {
            if (ValidateUtil.isBlank(checker) || boIds == null || boIds.Length < 1)
            {
                return false;
            }
            if (ValidateUtil.isBlank(checker) || boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、更改销售订单状态
            bool rst = false;
            rst = idal.SecondCheck(result, boIds);
            if (rst)
            {
                List<BOSecondCheck> list = new List<BOSecondCheck>();
                foreach (string soId in boIds)
                {
                    list.Add(new BOSecondCheck
                    {
                        BOSecondCheckID = Guid.NewGuid().ToString(),
                        BuyOrderID = soId,
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

        public bool Read(string reader, params string[] boIds)
        {
            if (ValidateUtil.isBlank(reader) || boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、插入分阅记录
            List<BOReader> list = new List<BOReader>();
            foreach (string id in boIds)
            {
                list.Add(new BOReader
                {
                    BOReadID = Guid.NewGuid().ToString(),
                    BuyOrderID = id,
                    ReaderID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "1"//已分阅
                });
            }
            return idalReader.Save(list.ToArray());
        }

        public bool Close(string[] boIds)
        {
            if (boIds == null || boIds.Length < 1)
            {
                return false;
            }

            return idal.Close(boIds);
        }
    }
}
