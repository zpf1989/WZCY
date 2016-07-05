using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 询价单复审表
    /// </summary>
    public class APSecondCheck
    {
        public string APSecondCheckID { get; set; }
        public string APID { get; set; }
        public string SecondChecker { get; set; }
        /// <summary>
        /// 复审意见
        /// </summary>
        public string SecondCheckView { get; set; }
        public DateTime SecondCheckTime { get; set; }
        /// <summary>
        /// 0 未批准   1 批准不通过   2 批准通过
        /// </summary>
        public string CheckFlag { get; set; }
    }
}