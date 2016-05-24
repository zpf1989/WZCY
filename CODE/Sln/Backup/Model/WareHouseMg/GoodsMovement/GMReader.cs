using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 出入库单分阅
    /// </summary>
    public class GMReader
    {
        private string _gmreadid;
		private string _goodsmovementid;
		private string _readerid;
		private DateTime _readtime;
        private string _readflag;
        private string _readername;

		/// <summary>
		/// 主键
		/// </summary>
		public string GMReadID
		{
			set{ _gmreadid=value;}
			get{return _gmreadid;}
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
