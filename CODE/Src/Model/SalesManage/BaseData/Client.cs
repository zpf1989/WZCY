using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 客户信息
    /// </summary>
    public class Client
    {
        public String ClientID { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public String ClientCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public String ClientName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public String ClientTel { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public String ClientAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public String Contactor { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public String Receiver { get; set; }
        /// <summary>
        /// 收货人联系方式
        /// </summary>
        public String ReceiverTel { get; set; }
        /// <summary>
        /// 客服开票信息
        /// </summary>
        public String BillingInfo { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public String PayTypeID { get; set; }
        /// <summary>
        /// 付款方式：名称
        /// </summary>
        public String PayType_Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
    }
}