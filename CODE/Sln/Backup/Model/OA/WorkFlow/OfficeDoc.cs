using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class OfficeDoc
    {
        private string _officedocid;
        private string _title;
        private string _contents;
        private string _writerid;
        private DateTime _writetime;

        /// <summary>
        /// 主键ID
        /// </summary>
        public string OfficeDocID
        {
            set { _officedocid = value; }
            get { return _officedocid; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }
        /// <summary>
        /// 起草人
        /// </summary>
        public string WriterID
        {
            set { _writerid = value; }
            get { return _writerid; }
        }
        /// <summary>
        /// 起草时间
        /// </summary>
        public DateTime WriteTime
        {
            set { _writetime = value; }
            get { return _writetime; }
        }
    }
}
