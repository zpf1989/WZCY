using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class OfficeDocItem
    {
        private string _officedocitemid;
        private string _officedocid;
        private string _receiverid;
        private string _opinion;
        private string _operatetype;
        private string _state;
        private DateTime _operatetime;
        private string _title;
        private string _contents;
        private string _writerid;
        private DateTime _writetime;
        private string _userName;

        /// <summary>
        /// 主键ID
        /// </summary>
        public string OfficeDocItemID
        {
            set { _officedocitemid = value; }
            get { return _officedocitemid; }
        }
        /// <summary>
        /// 公文ID
        /// </summary>
        public string OfficeDocID
        {
            set { _officedocid = value; }
            get { return _officedocid; }
        }
        /// <summary>
        /// 办理人
        /// </summary>
        public string ReceiverID
        {
            set { _receiverid = value; }
            get { return _receiverid; }
        }
        /// <summary>
        /// 办理意见
        /// </summary>
        public string Opinion
        {
            set { _opinion = value; }
            get { return _opinion; }
        }
        /// <summary>
        /// 操作类型(1：办理，2：分阅)
        /// </summary>
        public string OperateType
        {
            set { _operatetype = value; }
            get { return _operatetype; }
        }
        /// <summary>
        /// 办理状态(1：等待办理，2：已办理，3：未办理，4：等待查阅，5：已查阅，6：未查阅)
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            set { _operatetime = value; }
            get { return _operatetime; }
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
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
        }
    }
}
