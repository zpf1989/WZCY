using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class WareHouse
    {
        public string WareHouseID { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WareHouseCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WareHouseName { get; set; }
        /// <summary>
        /// 仓库管理员
        /// </summary>
        public string WareHouseMan { get; set; }
        /// <summary>
        /// 仓库管理员名称
        /// </summary>
        public string WareHouseMan_Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}