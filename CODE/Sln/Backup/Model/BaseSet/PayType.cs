using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 付款方式
    /// </summary>
    public class PayType
    {
        private string _paytypeid;
        private string _paytypecode;
        private string _paytypename;
        private string _paytyperemark;

        /// <summary>
        /// 主键
        /// </summary>
        public string PayTypeID
        {
            set { _paytypeid = value; }
            get { return _paytypeid; }
        }
        /// <summary>
        /// 付款方式编号
        /// </summary>
        public string PayTypeCode
        {
            set { _paytypecode = value; }
            get { return _paytypecode; }
        }
        /// <summary>
        /// 付款方式名称
        /// </summary>
        public string PayTypeName
        {
            set { _paytypename = value; }
            get { return _paytypename; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string PayTypeRemark
        {
            set { _paytyperemark = value; }
            get { return _paytyperemark; }
        }
    }
}
