using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 询价单分阅
    /// </summary>
    public class APReader
    {
        private string _apreadid;
		private string _apid;
		private string _readerid;
		private DateTime _readtime;
        private string _readflag;
        private string _readername;

		/// <summary>
		/// 主键
		/// </summary>
		public string APReadID
		{
			set{ _apreadid=value;}
			get{return _apreadid;}
		}
		/// <summary>
		/// 询价单ID
		/// </summary>
		public string APID
		{
			set{ _apid=value;}
			get{return _apid;}
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
		/// 分阅时间
		/// </summary>
		public DateTime ReadTime
		{
			set{ _readtime=value;}
			get{return _readtime;}
		}
		/// <summary>
        /// 分阅标示
		/// 0 未分阅
        /// 1 已分阅
		/// </summary>
		public string ReadFlag
		{
			set{ _readflag=value;}
			get{return _readflag;}
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
