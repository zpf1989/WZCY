using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.GeneralClass;
using OA.IDAL;
using OA.Model;

namespace OA.BLL
{
    public class BuyOrderItemBLL : BaseBLL<BuyOrderItem>
    {
        private readonly IBuyOrderItemDAL idal = DALFactory.Helper.GetIBuyOrderItemDAL();

        public IList<BuyOrderItem> GetOrderItems(PageEntity pageEntity, string boId)
        {
            if (ValidateUtil.isBlank(boId))
            {
                return null;
            }
            return idal.GetOrderItems(pageEntity, boId);
        }

        public bool Save(params BuyOrderItem[] entites)
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
        public override bool RepeatCheck(BuyOrderItem[] entities)
        {
            return true;
        }
    }
}
