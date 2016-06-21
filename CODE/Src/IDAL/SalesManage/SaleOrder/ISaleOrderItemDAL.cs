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
        /// <param name="soId"></param>
        /// <returns></returns>
        IList<SaleOrderItem> GetOrderItems(string soId);
    }
}
