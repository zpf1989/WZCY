using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class DepartmentInfo
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DeptID { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DeptCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 上级部门id
        /// </summary>
        public string ParentDeptID { get; set; }
        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string ParentDeptName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
