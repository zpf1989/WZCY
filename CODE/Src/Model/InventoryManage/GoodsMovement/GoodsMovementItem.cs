using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 货物移动子表
    /// </summary>
    public class GoodsMovementItem
    {
        public string GoodsMovementItemID { get; set; }
        /// <summary>
        /// 货物移动id
        /// </summary>
        public string GoodsMovementID { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        public string Material_Name { get; set; }
        /// <summary>
        /// 应收数量
        /// </summary>
        public Decimal TargInpQty { get; set; }
        /// <summary>
        /// 实收数量
        /// </summary>
        public Decimal ActInpQty { get; set; }
        /// <summary>
        /// 接收计量单位id
        /// </summary>
        public string RecUnitID { get; set; }
        /// <summary>
        /// 接收计量单位
        /// </summary>
        public string RecUnit_Name { get; set; }
        /// <summary>
        /// 应发数量
        /// </summary>
        public Decimal TargOutQty { get; set; }
        /// <summary>
        /// 实发数量
        /// </summary>
        public Decimal ActOutQty { get; set; }
        /// <summary>
        /// 发出计量单位id
        /// </summary>
        public string IssUnitID { get; set; }
        /// <summary>
        /// 发出计量单位
        /// </summary>
        public string IssUnit_Name { get; set; }
        /// <summary>
        /// 接收计划单价
        /// </summary>
        public Decimal InpPlaPrice { get; set; }
        /// <summary>
        /// 接收计划金额
        /// </summary>
        public Decimal InpPlaValue { get; set; }
        /// <summary>
        /// 接收实际单价
        /// </summary>
        public Decimal InpActPrice { get; set; }
        /// <summary>
        /// 接收实际金额
        /// </summary>
        public Decimal InpActValue { get; set; }
        /// <summary>
        /// 发出计划单价
        /// </summary>
        public Decimal OutPlaPrice { get; set; }
        /// <summary>
        /// 发出计划金额
        /// </summary>
        public Decimal OutPlaValue { get; set; }
        /// <summary>
        /// 发出实际单价
        /// </summary>
        public Decimal OutActPrice { get; set; }
        /// <summary>
        /// 发出实际金额
        /// </summary>
        public Decimal OutActValue { get; set; }
        /// <summary>
        /// 退回数量
        /// </summary>
        public Decimal ReturnQuantity { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}