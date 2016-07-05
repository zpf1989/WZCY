using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class AskPriceItemBLL : BaseBLL<AskPriceItem>
    {
        private readonly IAskPriceItemDAL idal = DALFactory.Helper.GetIAskPriceItemDAL();

        public List<AskPriceItem> GetAskPricesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {

            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public List<AskPriceItem> GetAskPricesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public IList<AskPriceItem> GetOrderItems(PageEntity pageEntity, string apId)
        {
            if (ValidateUtil.isBlank(apId))
            {
                return null;
            }
            return idal.GetAPItems(pageEntity, apId);
        }

        public bool Save(params AskPriceItem[] entites)
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
        public override bool RepeatCheck(AskPriceItem[] entities)
        {
            return true;
        }
    }
}
