using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 单据类型
    /// </summary>
    public class BillType
    {
        public string BillID { get; set; }
        public string BillCode { get; set; }
        public string BillName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        public string Remark { get; set; }
    }
}