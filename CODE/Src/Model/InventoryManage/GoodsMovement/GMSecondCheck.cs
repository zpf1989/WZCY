using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 货物移动复审
    /// </summary>
    public class GMSecondCheck
    {
        public string GMSecondCheckID { get; set; }
        /// <summary>
        /// 货物移动id
        /// </summary>
        public string GoodsMovementID { get; set; }
        /// <summary>
        /// 复审人
        /// </summary>
        public string SecondChecker { get; set; }
        /// <summary>
        /// 复审意见
        /// </summary>
        public string SecondCheckView { get; set; }

        /// <summary>
        /// 复审时间
        /// </summary>
        public DateTime SecondCheckTime { get; set; }
        /// <summary>
        /// 0 未批准 1 批准不通过 2 批准通过
        /// </summary>
        public string CheckFlag { get; set; }
    }
}
