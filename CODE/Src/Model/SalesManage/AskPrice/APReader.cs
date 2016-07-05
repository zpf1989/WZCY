using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 询价单分阅表
    /// </summary>
    public class APReader
    {
        public string APReadID{ get;set; }
        public string APID{ get;set; }
        public string ReaderID{ get;set; }
        public DateTime ReadTime{ get;set; }
        /// <summary>
        /// 0 未分阅   1 已分阅
        /// </summary>
        public string ReadFlag{ get;set; }
    }
}