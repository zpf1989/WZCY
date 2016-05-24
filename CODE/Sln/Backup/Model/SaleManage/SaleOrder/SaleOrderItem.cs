using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class SaleOrderItem
    {
        private string _saleorderitemid;
        private string _saleorderid;
        private string _materialid;
        private decimal _planqty;
        private decimal _actualqty;
        private decimal _plancost;
        private string _primaryunitid;
        private string _remark;

        /// <summary>
        /// 销售订单行ID
        /// </summary>
        public string SaleOrderItemID
        {
            set { _saleorderitemid = value; }
            get { return _saleorderitemid; }
        }
        /// <summary>
        /// 销售订单ID
        /// </summary>
        public string SaleOrderID
        {
            set { _saleorderid = value; }
            get { return _saleorderid; }
        }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterialID
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        /// <summary>
        /// 计划数量
        /// </summary>
        public decimal PlanQty
        {
            set { _planqty = value; }
            get { return _planqty; }
        }
        /// <summary>
        /// 实际数量
        /// </summary>
        public decimal ActualQty
        {
            set { _actualqty = value; }
            get { return _actualqty; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal PlanCost
        {
            set { _plancost = value; }
            get { return _plancost; }
        }
        /// <summary>
        /// 基本计量单位ID
        /// </summary>
        public string PrimaryUnitID
        {
            set { _primaryunitid = value; }
            get { return _primaryunitid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
