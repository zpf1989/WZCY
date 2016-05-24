using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class BOSecondCheck
    {
        private string _bosecondcheckid;
		private string _buyorderid;
		private string _secondchecker;
		private string _secondcheckername;
        private DateTime _secondchecktime;
        private string _secondcheckview;
		private string _checkflag;

		/// <summary>
		/// 主键ID
		/// </summary>
		public string BOSecondCheckID
		{
			set{ _bosecondcheckid=value;}
			get{return _bosecondcheckid;}
		}
		/// <summary>
		/// 采购订单ID
		/// </summary>
		public string BuyOrderID
		{
			set{ _buyorderid=value;}
			get{return _buyorderid;}
		}
		/// <summary>
		/// 批准人ID
		/// </summary>
		public string SecondChecker
		{
			set{ _secondchecker=value;}
			get{return _secondchecker;}
		}
		/// <summary>
		/// 批准人姓名
		/// </summary>
		public string SecondCheckerName
		{
			set{ _secondcheckername=value;}
			get{return _secondcheckername;}
		}
		/// <summary>
		/// 批准时间
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
            set { _secondcheckview = value; }
            get { return _secondcheckview; }
        }
		/// <summary>
        /// 批准标识
		/// 0 未批准
        /// 1 批准不通过
        /// 2 批准通过
		/// </summary>
		public string CheckFlag
		{
			set{ _checkflag=value;}
			get{return _checkflag;}
		}
    }
}
