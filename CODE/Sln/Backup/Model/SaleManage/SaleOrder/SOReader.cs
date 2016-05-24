using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 销售订单分阅
    /// </summary>
    public class SOReader
    {
        private string _soreadid;
		private string _saleorderid;
		private string _readerid;
        private string _readername;
		private DateTime _readtime;
		private string _readflag;

		/// <summary>
		/// 主键
		/// </summary>
		public string SOReadID
		{
			set{ _soreadid=value;}
			get{return _soreadid;}
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
