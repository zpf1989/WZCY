using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 销售订单分阅记录
    /// </summary>
    public class SOReader
    {
        public string SOReadID{ get;set; }
        public string SaleOrderID{ get;set; }
        public string ReaderID{ get;set; }
        public DateTime ReadTime{ get;set; }
        /// <summary>
        /// 0未分阅，1已分阅
        /// </summary>
        public string ReadFlag{ get;set; }
    }
}