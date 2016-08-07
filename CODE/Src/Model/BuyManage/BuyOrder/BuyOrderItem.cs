using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 采购订单分录
    /// </summary>
    public class BuyOrderItem
    {
        /// <summary>
        /// 采购订单行ID
        /// </summary>
        public string BuyOrderItemID { get; set; }
        /// <summary>
        /// 采购订单ID
        /// </summary>
        public string BuyOrderID { get; set; }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Material_Name { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public Decimal BuyQty { get; set; }
        /// <summary>
        /// 采购金额
        /// </summary>
        public Decimal BuyCost { get; set; }
        /// <summary>
        /// 采购计量单位ID
        /// </summary>
        public string BuyUnitID { get; set; }
        /// <summary>
        /// 采购计量单位
        /// </summary>
        public string BuyUnitID_Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
