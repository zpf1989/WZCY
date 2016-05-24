using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class NewsInfo
    {
        private string _newid;
        private string _title;
        private string _subtitle;
        private string _contents;
        private string _creatorid;
        private string _writer;
        private DateTime _publishtime;

        /// <summary>
        /// 新闻ID
        /// </summary>
        public string NewID
        {
            set { _newid = value; }
            get { return _newid; }
        }
        /// <summary>
        /// 主标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle
        {
            set { _subtitle = value; }
            get { return _subtitle; }
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
        /// 创建人ID
        /// </summary>
        public string CreatorID
        {
            set { _creatorid = value; }
            get { return _creatorid; }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string Writer
        {
            set { _writer = value; }
            get { return _writer; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime
        {
            set { _publishtime = value; }
            get { return _publishtime; }
        }
    }
}
