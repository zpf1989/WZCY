using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class SaleOrder
    {
        private string _saleorderid;
		private string _saleordercode;
		private string _materialid;
		private string _saleunitid;
		private string _clientid;
		private string _saledate;
		private decimal _saleqty;
		private decimal _saleprice;
		private decimal _salecost;
		private string _creator;
		private DateTime _createtime;
		private string _editor;
		private DateTime _edittime;
		private string _firstchecker;
		private DateTime _firstchecktime;
		private string _firstcheckview;
		private string _routingid;
		private string _salestate;
        private string _remark;
        private string _billtypeid;
        private string _oldsaleorderid;
        private string _finishdate;
        private List<SOSecondCheck> _solist;
        private List<SOReader> _rlist;
        private string _secondcheckname;
        private string _readername;
        private string _routing;

		/// <summary>
		/// 销售订单ID
		/// </summary>
		public string SaleOrderID
		{
			set{ _saleorderid=value;}
			get{return _saleorderid;}
		}
		/// <summary>
		/// 销售订单号
		/// </summary>
		public string SaleOrderCode
		{
			set{ _saleordercode=value;}
			get{return _saleordercode;}
		}
		/// <summary>
		/// 物料ID
		/// </summary>
		public string MaterialID
		{
			set{ _materialid=value;}
			get{return _materialid;}
		}
		/// <summary>
		/// 销售计量单位ID
		/// </summary>
		public string SaleUnitID
		{
			set{ _saleunitid=value;}
			get{return _saleunitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClientID
		{
			set{ _clientid=value;}
			get{return _clientid;}
		}
		/// <summary>
		/// 客户ID
		/// </summary>
		public string SaleDate
		{
			set{ _saledate=value;}
			get{return _saledate;}
		}
		/// <summary>
		/// 销售数量
		/// </summary>
		public decimal SaleQty
		{
			set{ _saleqty=value;}
			get{return _saleqty;}
		}
		/// <summary>
		/// 销售单价
		/// </summary>
		public decimal SalePrice
		{
			set{ _saleprice=value;}
			get{return _saleprice;}
		}
		/// <summary>
		/// 销售金额
		/// </summary>
		public decimal SaleCost
		{
			set{ _salecost=value;}
			get{return _salecost;}
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
		/// 工艺路线ID
		/// </summary>
		public string RoutingID
		{
			set{ _routingid=value;}
			get{return _routingid;}
		}
		/// <summary>
        /// 单据状态
		/// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭
		/// </summary>
		public string SaleState
		{
			set{ _salestate=value;}
			get{return _salestate;}
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
        /// 单据类型
        /// DYPLD 打样拼料单
        /// PLPLD  批量拼料单
        /// </summary>
        public string BillTypeID
        {
            set { _billtypeid = value; }
            get { return _billtypeid; }
        }
        /// <summary>
        /// 销售订单ID
        /// </summary>
        public string OldSaleOrderID
        {
            set { _oldsaleorderid = value; }
            get { return _oldsaleorderid; }
        }
        /// <summary>
        /// 交货日期
        /// </summary>
        public string FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
        }
        /// <summary>
        /// 复审人
        /// </summary>
        public List<SOSecondCheck> SOList
        {
            set { _solist = value; }
            get { return _solist; }
        }
        /// <summary>
        /// 分阅人
        /// </summary>
        public List<SOReader> RList
        {
            set { _rlist = value; }
            get { return _rlist; }
        }
        /// <summary>
        /// 复核人姓名
        /// </summary>
        public string SecondCheckerName
        {
            set { _secondcheckname = value; }
            get { return _secondcheckname; }
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
        /// 生产工艺
        /// </summary>
        public string Routing
        {
            set { _routing = value; }
            get { return _routing; }
        }

    }
}
