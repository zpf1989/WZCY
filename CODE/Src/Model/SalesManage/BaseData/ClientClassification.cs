using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 客户分级结果
    /// </summary>
    public class ClientClassification
    {
        public string Id { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 分级名称
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 分级类别
        /// </summary>
        public string LevelTypeName { get; set; }
        /// <summary>
        /// 总额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
