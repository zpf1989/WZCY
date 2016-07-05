using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 询价单从表
    /// </summary>
    public class AskPriceItem
    {
        public string APItemID { get; set; }
        /// <summary>
        /// 询价单id
        /// </summary>
        public string APID { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string Material_Code { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Material_Name { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Material_Specs { get; set; }
        /// <summary>
        /// 工艺
        /// </summary>
        public string Routing { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal PlanPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 计量单位id
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 计量单位名称
        /// </summary>
        public string Unit_Name { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal ActualPrice { get; set; }

        /// <summary>
        /// 是否含税
        /// </summary>
        public string IsTax { get; set; }
        /// <summary>
        /// 是否含运费
        /// </summary>
        public string IsShipping { get; set; }
    }
}