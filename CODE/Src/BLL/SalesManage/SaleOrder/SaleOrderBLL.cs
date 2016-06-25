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
    public class SaleOrderBLL : BaseBLL<SaleOrder>
    {
        private readonly ISaleOrderDAL idal = DALFactory.Helper.GetISaleOrderDAL();
        private readonly ISaleOrderItemDAL idalSOItem = DALFactory.Helper.GetISaleOrderItemDAL();
        private readonly ISOSecondCheckDAL idalSecondCheck = DALFactory.Helper.GetISOSecondCheckDAL();
        private readonly ISOReaderDAL idalReader = DALFactory.Helper.GetISOReaderDAL();
        public List<SaleOrder> GetSaleOrdersByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {

            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public List<SaleOrder> GetSaleOrdersByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public SaleOrder GetSaleOrderWithItems(string orderId)
        {
            return idal.GetSaleOrderWithItems(orderId);
        }

        public bool Save(params SaleOrder[] entites)
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
            idalSOItem.DeleteBySOIds(entityIds);
            idalReader.DeleteBySOIds(entityIds);
            idalSecondCheck.DeleteBySOIds(entityIds);
            return idal.Delete(entityIds);
        }
        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(SaleOrder[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.SaleOrderCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from e in entities
                            where ValidateUtil.isBlank(e.SaleOrderID)
                            select e.SaleOrderCode).ToArray();
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

        public bool SubmitToSecondChcker(string checker, params string[] soIds)
        {
            if (ValidateUtil.isBlank(checker) || soIds.Length < 1)
            {
                return false;
            }
            //1、插入复审记录
            bool rst = false;
            List<SOSecondCheck> list = new List<SOSecondCheck>();
            foreach (string soId in soIds)
            {
                list.Add(new SOSecondCheck
                {
                    SOSecondCheckID = Guid.NewGuid().ToString(),
                    SaleOrderID = soId,
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
                rst = idal.SubmitToSecondChecker(checker, soIds);
            }
            return rst;
        }

        public bool SubmitToReader(string reader, params string[] soIds)
        {
            if (ValidateUtil.isBlank(reader) || soIds.Length < 1)
            {
                return false;
            }
            //1、新增分阅记录
            bool rst = false;
            List<SOReader> list = new List<SOReader>();
            foreach (string soId in soIds)
            {
                list.Add(new SOReader
                {
                    SaleOrderID = soId,
                    SOReadID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "0"
                });
            }
            rst = idalReader.Save(list.ToArray());
            if (rst)
            {
                //2、设置销售订单分阅人
                rst = idal.SubmitToReader(reader, soIds);
            }
            return rst;
        }

        public bool FirstCheck(bool result, string checkView, params string[] soIds)
        {
            if (soIds == null || soIds.Length < 1)
            {
                return false;
            }
            return idal.FirstCheck(result, checkView, soIds);
        }

        public bool SecondCheck(bool result, string checker, string checkView, params string[] soIds)
        {
            if (ValidateUtil.isBlank(checker) || soIds == null || soIds.Length < 1)
            {
                return false;
            }
            if (ValidateUtil.isBlank(checker) || soIds == null || soIds.Length < 1)
            {
                return false;
            }
            //1、更改销售订单状态
            bool rst = false;
            rst = idal.SecondCheck(result, soIds);
            if (rst)
            {
                List<SOSecondCheck> list = new List<SOSecondCheck>();
                foreach (string soId in soIds)
                {
                    list.Add(new SOSecondCheck
                    {
                        SOSecondCheckID = Guid.NewGuid().ToString(),
                        SaleOrderID = soId,
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

        public bool Read(string reader, params string[] soIds)
        {
            if (ValidateUtil.isBlank(reader) || soIds == null || soIds.Length < 1)
            {
                return false;
            }
            //1、插入分阅记录
            List<SOReader> list = new List<SOReader>();
            foreach (string id in soIds)
            {
                list.Add(new SOReader
                {
                    SOReadID = Guid.NewGuid().ToString(),
                    SaleOrderID = id,
                    ReaderID = reader,
                    ReadTime = DateTime.Now,
                    ReadFlag = "1"//已分阅
                });
            }
            return idalReader.Save(list.ToArray());
        }

        public bool Close(string[] soIds)
        {
            if (soIds == null || soIds.Length < 1)
            {
                return false;
            }

            return idal.Close(soIds);
        }
    }
}
