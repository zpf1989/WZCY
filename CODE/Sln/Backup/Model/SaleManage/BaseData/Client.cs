using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class Client
    {
        private string _clientid;
        private string _clientcode;
        private string _clientname;
        private string _clienttel;
        private string _clientaddress;
        private string _contactor;
        private string _remark;

        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientID
        {
            set { _clientid = value; }
            get { return _clientid; }
        }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string ClientCode
        {
            set { _clientcode = value; }
            get { return _clientcode; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ClientTel
        {
            set { _clienttel = value; }
            get { return _clienttel; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ClientAddress
        {
            set { _clientaddress = value; }
            get { return _clientaddress; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contactor
        {
            set { _contactor = value; }
            get { return _contactor; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
