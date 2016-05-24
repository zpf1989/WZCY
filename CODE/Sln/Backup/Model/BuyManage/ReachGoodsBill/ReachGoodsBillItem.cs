using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class ReachGoodsBillItem
    {
        private string _reachgoodsbillitemid;
        private string _reachgoodsbillid;
        private string _materialsid;
        private int _reachqty;
        private int _buyqty;
        private string _remark;
        private decimal _reachcost;

        /// <summary>
        /// 到货通知单从表ID
        /// </summary>
        public string ReachGoodsBillItemID
        {
            set { _reachgoodsbillitemid = value; }
            get { return _reachgoodsbillitemid; }
        }
        /// <summary>
        /// 到货通知单ID
        /// </summary>
        public string ReachGoodsBillID
        {
            set { _reachgoodsbillid = value; }
            get { return _reachgoodsbillid; }
        }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterialsID
        {
            set { _materialsid = value; }
            get { return _materialsid; }
        }
        /// <summary>
        /// 到货数量
        /// </summary>
        public int ReachQty
        {
            set { _reachqty = value; }
            get { return _reachqty; }
        }
        /// <summary>
        /// 采购数量
        /// </summary>
        public int BuyQty
        {
            set { _buyqty = value; }
            get { return _buyqty; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 到货金额
        /// </summary>
        public decimal ReachCost
        {
            set { _reachcost = value; }
            get { return _reachcost; }
        }
    }
}
