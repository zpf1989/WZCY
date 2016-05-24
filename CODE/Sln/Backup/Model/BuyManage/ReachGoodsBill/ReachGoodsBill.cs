using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 到货通知单
    /// </summary>
    public class ReachGoodsBill
    {
        private string _reachgoodsbillid;
        private string _reachgoodsbillcode;
        private string _buyordercode;
        private string _reachgoodsdate;
        private DateTime _createbilltime;
        private string _createuserid;
        private string _inforperson;
        private string _remark;

        private string _buyorderid;

        /// <summary>
        /// 到货通知单ID
        /// </summary>
        public string ReachGoodsBillID
        {
            set { _reachgoodsbillid = value; }
            get { return _reachgoodsbillid; }
        }
        /// <summary>
        /// 到货通知单编号
        /// </summary>
        public string ReachGoodsBillCode
        {
            set { _reachgoodsbillcode = value; }
            get { return _reachgoodsbillcode; }
        }
        /// <summary>
        /// 采购单据号
        /// </summary>
        public string BuyOrderCode
        {
            set { _buyordercode = value; }
            get { return _buyordercode; }
        }
        /// <summary>
        /// 到货时间
        /// </summary>
        public string ReachGoodsDate
        {
            set { _reachgoodsdate = value; }
            get { return _reachgoodsdate; }
        }
        /// <summary>
        /// 创单时间
        /// </summary>
        public DateTime CreateBillTime
        {
            set { _createbilltime = value; }
            get { return _createbilltime; }
        }
        /// <summary>
        /// 创单人
        /// </summary>
        public string CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 通知人
        /// </summary>
        public string InforPerson
        {
            set { _inforperson = value; }
            get { return _inforperson; }
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
        /// 采购订单ID
        /// </summary>
        public string BuyOrderID
        {
            set { _buyorderid = value; }
            get { return _buyorderid; }
        }
    }
}
