using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 客户分级设置
    /// </summary>
    public class ClientLevel
    {
        public String LevelId { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public String LevelName { get; set; }
        /// <summary>
        /// 等级分类
        /// </summary>
        public String LevelType { get; set; }
        /// <summary>
        /// 上限
        /// </summary>
        public decimal LevelMax { get; set; }
        /// <summary>
        /// 下限
        /// </summary>
        public decimal LevelMin { get; set; }
    }
}