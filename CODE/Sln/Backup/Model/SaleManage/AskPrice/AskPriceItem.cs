using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 询价单下达子表
    /// </summary>
    public class AskPriceItem
    {
        private string _apitemid;
        private string _apid;
        private string _saleorderid;

        /// <summary>
        /// 主键
        /// </summary>
        public string APItemID
        {
            set { _apitemid = value; }
            get { return _apitemid; }
        }
        /// <summary>
        /// 询价单ID
        /// </summary>
        public string APID
        {
            set { _apid = value; }
            get { return _apid; }
        }
        /// <summary>
        /// 销售订单ID
        /// </summary>
        public string SaleOrderID
        {
            set { _saleorderid = value; }
            get { return _saleorderid; }
        }
    }
}
