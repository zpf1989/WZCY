using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface ISaleOrderItemDAL : IBaseDAL<SaleOrderItem>
    {
        /// <summary>
        /// 获取指定销售订单的行
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="soId"></param>
        /// <returns></returns>
        IList<SaleOrderItem> GetOrderItems(PageEntity pageEntity, string soId);

        /// <summary>
        /// 根据删除指定订单下的所有行
        /// </summary>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool DeleteBySOIds(params string[] soIds);
    }
}
