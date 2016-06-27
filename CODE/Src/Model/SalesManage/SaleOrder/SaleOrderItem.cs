using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 销售订单行
    /// </summary>
    public class SaleOrderItem
    {
        public string SaleOrderItemID { get; set; }
        /// <summary>
        /// 销售订单id
        /// </summary>
        public string SaleOrderID { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string Material_Name { get; set; }
        /// <summary>
        /// 计划数量
        /// </summary>
        public Decimal PlanQty { get; set; }
        /// <summary>
        /// 实际数量
        /// </summary>
        public Decimal ActualQty { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public Decimal PlanCost { get; set; }
        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string PrimaryUnitID { get; set; }
        public string PrimaryUnit_Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}