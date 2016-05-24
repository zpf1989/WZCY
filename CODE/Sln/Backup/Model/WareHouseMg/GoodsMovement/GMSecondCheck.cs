using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 出入库单复审
    /// </summary>
    public class GMSecondCheck
    {
        private string _gmsecondcheckid;
		private string _goodsmovementid;
		private string _secondchecker;
		private string _secondcheckview;
		private DateTime _secondchecktime;
        private string _checkflag;
        private string _secondcheckername;

		/// <summary>
		/// 主键
		/// </summary>
		public string GMSecondCheckID
		{
			set{ _gmsecondcheckid=value;}
			get{return _gmsecondcheckid;}
		}
		/// <summary>
		/// 货物移动ID
		/// </summary>
		public string GoodsMovementID
		{
			set{ _goodsmovementid=value;}
			get{return _goodsmovementid;}
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
		/// 复核人意见
		/// </summary>
		public string SecondCheckView
		{
			set{ _secondcheckview=value;}
			get{return _secondcheckview;}
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
        /// 复审标志
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
