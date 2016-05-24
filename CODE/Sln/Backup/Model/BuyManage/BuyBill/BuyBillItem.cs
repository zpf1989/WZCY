using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 采购订单从表实体类
    /// </summary>
    public class BuyBillItem
    {
        private string _buyorderitemid;
        private string _buyorderid;
        private string _materialid;
        private decimal _buyqty;
        private decimal _buycost;
        private string _buyunitid;
        private string _remark;

        /// <summary>
        /// 采购订单行ID
        /// </summary>
        public string BuyOrderItemID
        {
            set { _buyorderitemid = value; }
            get { return _buyorderitemid; }
        }
        /// <summary>
        /// 采购订单ID
        /// </summary>
        public string BuyOrderID
        {
            set { _buyorderid = value; }
            get { return _buyorderid; }
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
