using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// 供应商ID
        /// </summary>
        public String SupplierID { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public String SupplierCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public String SupplierName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public String Contactor { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public String Tel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public String Fax { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }
    }
}
