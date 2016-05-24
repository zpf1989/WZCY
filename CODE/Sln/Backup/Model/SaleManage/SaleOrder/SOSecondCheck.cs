using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 销售订单复审
    /// </summary>
    public class SOSecondCheck
    {
        private string _sosecondcheckid;
		private string _saleorderid;
		private string _secondchecker;
		private DateTime _secondchecktime;
		private string _secondcheckview;
		private string _checkflag;
        private string _secondcheckername;

		/// <summary>
		/// 主键
		/// </summary>
		public string SOSecondCheckID
		{
			set{ _sosecondcheckid=value;}
			get{return _sosecondcheckid;}
		}
		/// <summary>
		/// 销售订单ID
		/// </summary>
		public string SaleOrderID
		{
			set{ _saleorderid=value;}
			get{return _saleorderid;}
		}
		/// <summary>
		/// 复核人ID
		/// </summary>
		public string SecondChecker
		{
			set{ _secondchecker=value;}
			get{return _secondchecker;}
        }
		/// <summary>
		/// 复核时间
		/// </summary>
		public DateTime SecondCheckTime
		{
			set{ _secondchecktime=value;}
			get{return _secondchecktime;}
		}
		/// <summary>
		/// 复核意见
		/// </summary>
		public string SecondCheckView
		{
			set{ _secondcheckview=value;}
			get{return _secondcheckview;}
		}
		/// <summary>
		/// 0 未批准
        /// 1 批准不通过
        /// 2 批准通过
		/// </summary>
		public string CheckFlag
		{
			set{ _checkflag=value;}
			get{return _checkflag;}
        }
        /// <summary>
        /// 复核人姓名
        /// </summary>
        public string SecondCheckerName
        {
            set { _secondcheckername = value; }
            get { return _secondcheckername; }
        }
    }
}
