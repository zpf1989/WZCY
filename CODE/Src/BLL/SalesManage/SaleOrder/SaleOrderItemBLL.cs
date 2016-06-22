using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class SaleOrderItemBLL : BaseBLL<SaleOrderItem>
    {
        private readonly ISaleOrderItemDAL idal = DALFactory.Helper.GetISaleOrderItemDAL();

        public List<SaleOrderItem> GetSaleOrdersByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {

            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public List<SaleOrderItem> GetSaleOrdersByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public IList<SaleOrderItem> GetOrderItems(PageEntity pageEntity, string soId)
        {
            if (ValidateUtil.isBlank(soId))
            {
                return null;
            }
            return idal.GetOrderItems(pageEntity, soId);
        }

        public bool Save(params SaleOrderItem[] entites)
        {
            if (!RepeatCheck(entites))
            {
                return false;
            }
            return idal.Save(entites);
        }

        public bool Delete(params string[] entityIds)
        {
            return idal.Delete(entityIds);
        }
        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(SaleOrderItem[] entities)
        {
            return true;
        }
    }
}
