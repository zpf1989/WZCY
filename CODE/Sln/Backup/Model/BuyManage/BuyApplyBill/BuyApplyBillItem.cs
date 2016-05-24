using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 采购申请订单从表实体类
    /// </summary>
    public class BuyApplyBillItem
    {
        private string _buyapplyorderitemid;
        private string _buyapplyorderid;
        private string _materialid;
        private decimal _buyqty;
        private decimal _buycost;
        private string _buyunitid;
        private string _remark;

        /// <summary>
        /// 采购申请订单行ID
        /// </summary>
        public string BuyApplyOrderItemID
        {
            set { _buyapplyorderitemid = value; }
            get { return _buyapplyorderitemid; }
        }
        /// <summary>
        /// 采购申请订单ID
        /// </summary>
        public string BuyApplyOrderID
        {
            set { _buyapplyorderid = value; }
            get { return _buyapplyorderid; }
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
        /// 采购数量
        /// </summary>
        public decimal BuyQty
        {
            set { _buyqty = value; }
            get { return _buyqty; }
        }
        /// <summary>
        /// 采购金额
        /// </summary>
        public decimal BuyCost
        {
            set { _buycost = value; }
            get { return _buycost; }
        }
        /// <summary>
        /// 采购计量单位ID
        /// </summary>
        public string BuyUnitID
        {
            set { _buyunitid = value; }
            get { return _buyunitid; }
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
