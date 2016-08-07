using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 采购订单分阅
    /// </summary>
    public class BOReader
    {
        public string BOReadID { get; set; }
        public string BuyOrderID { get; set; }
        public string ReaderID { get; set; }
        public DateTime ReadTime { get; set; }
        /// <summary>
        /// 0未分阅，1已分阅
        /// </summary>
        public string ReadFlag { get; set; }
    }
}
