using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 询价单
    /// </summary>
    public class AskPrice
    {
        private string _apid;
        private string _apcode;
        private string _askdate;
        private string _clientname;
        private string _clientcontact;
        private string _clienttel;
        private string _clientaddress;
        private string _materialcode;
        private string _materialname;
        private string _specs;
        private string _routing;
        private decimal _planprice;
        private string _issued;
        private int _issuedcount;
        private decimal _actualprice;
        private string _paytypeid;
        private string _trackdescription;
        private string _clientsurvey;
        private string _apremark;
        private string _creator;
        private DateTime _createTime;
        private string _editor;
        private DateTime _edittime;
        private string _firstchecker;
        private DateTime _firstchecktime;
        private string _firstcheckview;
        private string _state;
        private string _secondcheckername;
        private string _readername;
        private decimal _qty;
        private string _unitid;
        private string _istax;
        private string _isshipping;
        private List<APSecondCheck> _sclist;
        private List<APReader> _rlist;

        /// <summary>
        /// 主键
        /// </summary>
        public string APID
        {
            set { _apid = value; }
            get { return _apid; }
        }
        /// <summary>
        /// 询价单编号
        /// </summary>
        public string APCode
        {
            set { _apcode = value; }
            get { return _apcode; }
        }
        /// <summary>
        /// 询价日期
        /// </summary>
        public string AskDate
        {
            set { _askdate = value; }
            get { return _askdate; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string ClientContact
        {
            set { _clientcontact = value; }
            get { return _clientcontact; }
        }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string ClientTel
        {
            set { _clienttel = value; }
            get { return _clienttel; }
        }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string ClientAddress
        {
            set { _clientaddress = value; }
            get { return _clientaddress; }
        }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string MaterialCode
        {
            set { _materialcode = value; }
            get { return _materialcode; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string MaterialName
        {
            set { _materialname = value; }
            get { return _materialname; }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specs
        {
            set { _specs = value; }
            get { return _specs; }
        }
        /// <summary>
        /// 工艺
        /// </summary>
        public string Routing
        {
            set { _routing = value; }
            get { return _routing; }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal PlanPrice
        {
            set { _planprice = value; }
            get { return _planprice; }
        }
        /// <summary>
        /// 有无下达单据
        /// 0：没下达
        /// 1：已下达
        /// </summary>
        public string Issued
        {
            set { _issued = value; }
            get { return _issued; }
        }
        /// <summary>
        /// 下达数量
        /// </summary>
        public int IssuedCount
        {
            set { _issuedcount = value; }
            get { return _issuedcount; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal ActualPrice
        {
            set { _actualprice = value; }
            get { return _actualprice; }
        }
        /// <summary>
        /// 付款方式ID
        /// </summary>
        public string PayTypeID
        {
            set { _paytypeid = value; }
            get { return _paytypeid; }
        }
        /// <summary>
        /// 跟踪情况
        /// </summary>
        public string TrackDescription
        {
            set { _trackdescription = value; }
            get { return _trackdescription; }
        }
        /// <summary>
        /// 客户调查
        /// </summary>
        public string ClientSurvey
        {
            set { _clientsurvey = value; }
            get { return _clientsurvey; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string APRemark
        {
            set { _apremark = value; }
            get { return _apremark; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createTime = value; }
            get { return _createTime; }
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
        /// 单据状态
		/// 1:编制
        /// 2:提交
        /// 3:审核通过
        /// 4:审核不通过
        /// 5:批准通过
        /// 6:批准不通过
        /// 7:关闭
		/// </summary>
		public string State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
        /// 复核人姓名
		/// </summary>
		public string SecondCheckerName
		{
			set{ _secondcheckername=value;}
			get{return _secondcheckername;}
		}
		/// <summary>
        /// 分阅人姓名
		/// </summary>
		public string ReaderName
		{
			set{ _readername=value;}
			get{return _readername;}
		}
        /// <summary>
        /// 复审人
        /// </summary>
        public List<APSecondCheck> SCList
        {
            set { _sclist = value; }
            get { return _sclist; }
        }
        /// <summary>
        /// 分阅人
        /// </summary>
        public List<APReader> RList
        {
            set { _rlist = value; }
            get { return _rlist; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty
        {
            set { _qty = value; }
            get { return _qty; }
        }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 是否含税
        /// </summary>
        public string IsTax
        {
            set { _istax = value; }
            get { return _istax; }
        }
        /// <summary>
        /// 是否含运费
        /// </summary>
        public string IsShipping
        {
            set { _isshipping = value; }
            get { return _isshipping; }
        }
    }
}
