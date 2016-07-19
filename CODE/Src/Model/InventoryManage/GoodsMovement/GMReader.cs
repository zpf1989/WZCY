using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 货物移动分阅
    /// </summary>
    public class GMReader
    {
        public string GMReadID { get; set; }

        public string GoodsMovementID { get; set; }
        /// <summary>
        /// 分阅人
        /// </summary>
        public string ReaderID { get; set; }
        public DateTime ReadTime { get; set; }
        /// <summary>
        /// 0未分阅，1已分阅
        /// </summary>
        public string ReadFlag { get; set; }
    }
}
