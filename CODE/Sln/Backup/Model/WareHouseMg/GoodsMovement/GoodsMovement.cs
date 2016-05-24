using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 货物移动
    /// </summary>
    public class GoodsMovement
    {
        private string _goodsmovementid;
        private string _oldgoodsmovementid;
		private string _createdate;
		private string _goodsmovementcode;
		private string _businesstype;
		private string _movetypecode;
		private string _sourcebilltype;
		private string _purcompanyid;
		private string _purdeptid;
		private string _purempid;
		private string _supplierid;
		private string _receiptdate;
		private string _reccompanyid;
        private string _recdeptid;
		private string _rechandler;
		private string _recwhid;
		private string _recwhempid;
		private string _salescompanyid;
		private string _salesdepid;
		private string _salesempid;
		private string _customerid;
		private string _issdate;
		private string _isscompanyid;
		private string _issdeptid;
		private string _isshandler;
		private string _isswhid;
		private string _isswhempid;
		private string _procompanyid;
		private string _prodepid;
		private string _proempid;
		private string _concompanyid;
		private string _condepid;
		private string _conempid;
		private string _creator;
		private DateTime _createtime;
		private string _editor;
		private DateTime _edittime;
		private string _billstate;
		private string _firstchecker;
		private DateTime _firstchecktime;
		private string _firstcheckview;
		private string _isred;
        private string _remark;
        private List<GMSecondCheck> _sclist;
        private List<GMReader> _rlist;
        private string _secondcheckname;
        private string _readername;

		/// <summary>
		/// 货物移动ID
		/// </summary>
		public string GoodsMovementID
		{
			set{ _goodsmovementid=value;}
			get{return _goodsmovementid;}
		}
        /// <summary>
        /// 货物移动ID
        /// </summary>
        public string OldGoodsMovementID
        {
            set { _oldgoodsmovementid = value; }
            get { return _oldgoodsmovementid; }
        }
		/// <summary>
		/// 单据日期
		/// </summary>
		public string CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 单据编号
		/// </summary>
		public string GoodsMovementCode
		{
			set{ _goodsmovementcode=value;}
			get{return _goodsmovementcode;}
		}
		/// <summary>
        /// 业务类别
		/// 1   入库业务
        /// 2   出库业务
		/// </summary>
		public string BusinessType
		{
			set{ _businesstype=value;}
			get{return _businesstype;}
		}
		/// <summary>
        /// 移动类型
		/// 100          采购入库
        /// 101          生产入库
        /// 102          领料出库
        /// 103          销售出库
        /// 104          其他出库
		/// </summary>
		public string MoveTypeCode
		{
			set{ _movetypecode=value;}
			get{return _movetypecode;}
		}
		/// <summary>
        /// 来源单据类型
		/// CG：采购单
        /// SC：生产订单
        /// XS：销售订单
        /// LL：领料单
		/// </summary>
		public string SourceBillType
		{
			set{ _sourcebilltype=value;}
			get{return _sourcebilltype;}
		}
		/// <summary>
		/// 采购公司ID
		/// </summary>
		public string PurCompanyID
		{
			set{ _purcompanyid=value;}
			get{return _purcompanyid;}
		}
		/// <summary>
		/// 采购部门ID
		/// </summary>
		public string PurDeptID
		{
			set{ _purdeptid=value;}
			get{return _purdeptid;}
		}
		/// <summary>
		/// 采购人员ID
		/// </summary>
		public string PurEmpID
		{
			set{ _purempid=value;}
			get{return _purempid;}
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
		/// 接收日期
		/// </summary>
		public string ReceiptDate
		{
			set{ _receiptdate=value;}
			get{return _receiptdate;}
		}
		/// <summary>
		/// 接收公司ID
		/// </summary>
		public string RecCompanyID
		{
			set{ _reccompanyid=value;}
			get{return _reccompanyid;}
        }
        /// <summary>
        /// 接收部门ID
        /// </summary>
        public string RecDeptID
        {
            set { _recdeptid = value; }
            get { return _recdeptid; }
        }
		/// <summary>
		/// 接收经办人
		/// </summary>
		public string RecHandler
		{
			set{ _rechandler=value;}
			get{return _rechandler;}
		}
		/// <summary>
		/// 接收仓库ID
		/// </summary>
		public string RecWHID
		{
			set{ _recwhid=value;}
			get{return _recwhid;}
		}
		/// <summary>
		/// 接收仓库保管员ID
		/// </summary>
		public string RecWHEmpID
		{
			set{ _recwhempid=value;}
			get{return _recwhempid;}
		}
		/// <summary>
		/// 销售公司ID
		/// </summary>
		public string SalesCompanyID
		{
			set{ _salescompanyid=value;}
			get{return _salescompanyid;}
		}
		/// <summary>
		/// 销售部门ID
		/// </summary>
		public string SalesDepID
		{
			set{ _salesdepid=value;}
			get{return _salesdepid;}
		}
		/// <summary>
		/// 销售人员ID
		/// </summary>
		public string SalesEmpID
		{
			set{ _salesempid=value;}
			get{return _salesempid;}
		}
		/// <summary>
		/// 销售客户
		/// </summary>
		public string CustomerID
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 发出日期
		/// </summary>
		public string IssDate
		{
			set{ _issdate=value;}
			get{return _issdate;}
		}
		/// <summary>
		/// 发出公司ID
		/// </summary>
		public string IssCompanyID
		{
			set{ _isscompanyid=value;}
			get{return _isscompanyid;}
		}
		/// <summary>
		/// 发出部门ID
		/// </summary>
		public string IssDeptID
		{
			set{ _issdeptid=value;}
			get{return _issdeptid;}
		}
		/// <summary>
		/// 发出经办人
		/// </summary>
		public string IssHandler
		{
			set{ _isshandler=value;}
			get{return _isshandler;}
		}
		/// <summary>
		/// 发出仓库ID
		/// </summary>
		public string IssWHID
		{
			set{ _isswhid=value;}
			get{return _isswhid;}
		}
		/// <summary>
		/// 发出仓库保管员ID
		/// </summary>
		public string IssWHEmpID
		{
			set{ _isswhempid=value;}
			get{return _isswhempid;}
		}
		/// <summary>
		/// 生产公司ID
		/// </summary>
		public string ProCompanyID
		{
			set{ _procompanyid=value;}
			get{return _procompanyid;}
		}
		/// <summary>
		/// 生产部门ID
		/// </summary>
		public string ProDepID
		{
			set{ _prodepid=value;}
			get{return _prodepid;}
		}
		/// <summary>
		/// 生产人ID
		/// </summary>
		public string ProEmpID
		{
			set{ _proempid=value;}
			get{return _proempid;}
		}
		/// <summary>
		/// 领用公司ID
		/// </summary>
		public string ConCompanyID
		{
			set{ _concompanyid=value;}
			get{return _concompanyid;}
		}
		/// <summary>
		/// 领用部门ID
		/// </summary>
		public string ConDepID
		{
			set{ _condepid=value;}
			get{return _condepid;}
		}
		/// <summary>
		/// 领用人ID
		/// </summary>
		public string ConEmpID
		{
			set{ _conempid=value;}
			get{return _conempid;}
		}
		/// <summary>
		/// 制单人
		/// </summary>
		public string Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 制单时间
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
        /// 单据状态
		/// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭
		/// </summary>
		public string BillState
		{
			set{ _billstate=value;}
			get{return _billstate;}
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
        /// 红单标识    
        /// 0 蓝单
        /// 1 红单
		/// </summary>
		public string IsRed
		{
			set{ _isred=value;}
			get{return _isred;}
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
        /// 复审人
        /// </summary>
        public List<GMSecondCheck> SCList
        {
            set { _sclist = value; }
            get { return _sclist; }
        }
        /// <summary>
        /// 分阅人
        /// </summary>
        public List<GMReader> RList
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

    }
}
