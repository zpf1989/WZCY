using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class BOAReader
    {
        private string _boareadid;
		private string _buyapplyorderid;
        private string _readerid;
        private string _readername;
		private DateTime _readtime;
		private string _readflag;

		/// <summary>
		/// 主键ID
		/// </summary>
		public string BOAReadID
		{
            set { _boareadid = value; }
            get { return _boareadid; }
		}
		/// <summary>
        /// 采购申请订单ID
		/// </summary>
		public string BuyApplyOrderID
		{
            set { _buyapplyorderid = value; }
            get { return _buyapplyorderid; }
		}
		/// <summary>
		/// 分阅人ID
		/// </summary>
		public string ReaderID
		{
			set{ _readerid=value;}
			get{return _readerid;}
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
		/// 分阅时间
		/// </summary>
		public DateTime ReadTime
		{
			set{ _readtime=value;}
			get{return _readtime;}
		}
		/// <summary>
        /// 分阅标识
		/// 0 未分阅
        /// 1 已分阅
		/// </summary>
		public string ReadFlag
		{
			set{ _readflag=value;}
			get{return _readflag;}
		}
    }
}
