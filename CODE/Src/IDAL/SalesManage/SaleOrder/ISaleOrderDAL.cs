using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface ISaleOrderDAL : IBaseDAL<SaleOrder>
    {
        /// <summary>
        /// 编号是否已存在
        /// </summary>
        /// <param name="codes"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(params string[] codes);

        /// <summary>
        /// 获取销售订单，包含销售订单行
        /// </summary>
        /// <param name="orderId">销售订单id</param>
        /// <returns></returns>
        SaleOrder GetSaleOrderWithItems(string orderId);
    }
}
