using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 采购申请订单主表实体类
    /// </summary>
    public class BuyApplyBill
    {
        private string _buyapplyorderid;
        private string _buyapplyordercode;
        private string _buyapplyorderdate;
        private string _supplierid;
        private string _deliverydate;
		private string _creator;
		private DateTime _createtime;
		private string _editor;
		private DateTime _edittime;
		private string _firstchecker;
		private DateTime _firstchecktime;
		private string _firstcheckview;
        private string _reccompany;
        private string _rectel;
        private string _recfax;
		private string _orderstate;
		private string _remark;
        private string _oldbuyorderid;
        private string _secondcheckername;
        private string _readername;
        private List<BOASecondCheck> _clist;
        private List<BOAReader> _rlist;

		/// <summary>
        /// 采购申请订单ID
		/// </summary>
        public string BuyApplyOrderID
		{
            set { _buyapplyorderid = value; }
            get { return _buyapplyorderid; }
		}
		/// <summary>
        /// 采购申请订单号
		/// </summary>
        public string BuyApplyOrderCode
		{
            set { _buyapplyordercode = value; }
            get { return _buyapplyordercode; }
		}
		/// <summary>
        /// 采购申请日期
		/// </summary>
        public string BuyApplyOrderDate
		{
            set { _buyapplyorderdate = value; }
            get { return _buyapplyorderdate; }
		}
		/// <summary>
		/// 供应商ID
		/// </summary>
		public string SupplierID
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
        }
        /// <summary>
        /// 到货日期
        /// </summary>
        public string DeliveryDate
        {
            set { _deliverydate = value; }
            get { return _deliverydate; }
        }
		/// <summary>
		/// 创建人ID
		/// </summary>
		public string Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 修改人ID
		/// </summary>
		public string Editor
		{
			set{ _editor=value;}
			get{return _editor;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime EditTime
		{
			set{ _edittime=value;}
			get{return _edittime;}
		}
		/// <summary>
		/// 初审人ID
		/// </summary>
		public string FirstChecker
		{
			set{ _firstchecker=value;}
			get{return _firstchecker;}
		}
		/// <summary>
		/// 初审时间
		/// </summary>
		public DateTime FirstCheckTime
		{
			set{ _firstchecktime=value;}
			get{return _firstchecktime;}
		}
		/// <summary>
		/// 初审意见
		/// </summary>
		public string FirstCheckView
		{
			set{ _firstcheckview=value;}
			get{return _firstcheckview;}
        }
        /// <summary>
        /// 接收单位
        /// </summary>
        public string RecCompany
        {
            set { _reccompany = value; }
            get { return _reccompany; }
        }
        /// <summary>
        /// 接收电话
        /// </summary>
        public string RecTel
        {
            set { _rectel = value; }
            get { return _rectel; }
        }
        /// <summary>
        /// 接收传真
        /// </summary>
        public string RecFax
        {
            set { _recfax = value; }
            get { return _recfax; }
        }
		/// <summary>
		/// 1:编制
        /// 单据状态
        ///2:提交
        ///3:初审通过
        ///4:初审不通过
        ///5:复审通过
        ///6:复审不通过
        ///7:关闭
		/// </summary>
		public string OrderState
		{
			set{ _orderstate=value;}
			get{return _orderstate;}
		}
		/// <summary>
        /// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// 采购订单ID
        /// </summary>
        public string OldBuyOrderID
        {
            set { _oldbuyorderid = value; }
            get { return _oldbuyorderid; }
        }
        /// <summary>
        /// 批准人姓名
        /// </summary>
        public string SecondCheckerName
        {
            set { _secondcheckername = value; }
            get { return _secondcheckername; }
        }
        /// <summary>
        /// 分阅人姓名
        /// </summary>
        public string ReaderName
        {
            set { _readername = value; }
            get { return _readername; }
        }
        /// <summary>
        /// 复审人
        /// </summary>
        public List<BOASecondCheck> CList
        {
            set { _clist = value; }
            get { return _clist; }
        }
        /// <summary>
        /// 分阅人
        /// </summary>
        public List<BOAReader> RList
        {
            set { _rlist = value; }
            get { return _rlist; }
        }


    }
}
