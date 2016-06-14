using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 物料
    /// </summary>
    public class Materials
    {
        public String MaterialID { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        public String MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public String MaterialName { get; set; }
        /// <summary>
        /// 物料规格
        /// </summary>
        public String Specs { get; set; }
        /// <summary>
        /// 物料分类id
        /// </summary>
        public String MaterialClassID { get; set; }
        /// <summary>
        /// 实体类冗余字段：物料分类名称
        /// </summary>
        public String MaterialClass_Name { get; set; }
        /// <summary>
        /// 物料类型id
        /// </summary>
        public String MaterialTypeID { get; set; }
        /// <summary>
        /// 实体类冗余字段：物料类型名称
        /// </summary>
        public String MaterialType_Name { get; set; }
        /// <summary>
        /// 物料基本计量单位id
        /// </summary>
        public String PrimaryUnitID { get; set; }
        /// <summary>
        /// 实体类冗余字段：计量单位名称
        /// </summary>
        public String PrimaryUnit_Name { get; set; }
        /// <summary>
        /// 物料价格
        /// </summary>
        public Decimal Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public String Creator { get; set; }
        /// <summary>
        /// 实体类冗余字段：创建人名称
        /// </summary>
        public String Creator_Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 废品率
        /// </summary>
        public Decimal WasterRate { get; set; }
    }
}