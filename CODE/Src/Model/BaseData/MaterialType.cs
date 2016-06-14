using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 物料类型
    /// </summary>
    public class MaterialType
    {
        public String MaterialTypeID{ get;set; }
        /// <summary>
        /// 物料类型编号
        /// </summary>
        public String MaterialTypeCode{ get;set; }
        /// <summary>
        /// 物料类型名称
        /// </summary>
        public String MaterialTypeName{ get;set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark{ get;set; }
    }
}