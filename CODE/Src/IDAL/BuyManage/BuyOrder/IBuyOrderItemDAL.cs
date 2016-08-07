using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.GeneralClass;
using OA.Model;

namespace OA.IDAL
{
    public interface IBuyOrderItemDAL : IBaseDAL<BuyOrderItem>
    {
        /// <summary>
        /// 获取指定采购订单的行
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="soId"></param>
        /// <returns></returns>
        IList<BuyOrderItem> GetOrderItems(PageEntity pageEntity, string boId);

        /// <summary>
        /// 根据删除指定订单下的所有行
        /// </summary>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool DeleteByBOIds(params string[] boIds);
    }
}
