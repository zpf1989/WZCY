using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class ApplyPublicPowerItem
    {
        private string _appitemid;
        private string _appid;
        private string _receiverid;
        private DateTime _operatetime;

        /// <summary>
        /// 主键
        /// </summary>
        public string AppItemID
        {
            set { _appitemid = value; }
            get { return _appitemid; }
        }
        /// <summary>
        /// 公章申请单ID
        /// </summary>
        public string AppID
        {
            set { _appid = value; }
            get { return _appid; }
        }
        /// <summary>
        /// 分阅人
        /// </summary>
        public string ReceiverID
        {
            set { _receiverid = value; }
            get { return _receiverid; }
        }
        /// <summary>
        /// 分阅时间
        /// </summary>
        public DateTime OperateTime
        {
            set { _operatetime = value; }
            get { return _operatetime; }
        }
    }
}
