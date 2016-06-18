using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 物料分类
    /// </summary>
    public class MaterialClass
    {
        public String MaterialClassID { get; set; }
        /// <summary>
        /// 物料分类编号
        /// </summary>
        public String MaterialClassCode { get; set; }
        /// <summary>
        /// 物料分类名称
        /// </summary>
        public String MaterialClassName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
    }
}