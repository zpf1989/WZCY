using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// demo-职员实体类
    /// </summary>
    public class DemoEmployee
    {
        public string EmpId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string EmpGender { get; set; }
        public int EmpAge { get; set; }
        public DateTime EmpBirthDay { get; set; }
        public decimal EmpSalary { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 部门名称：冗余（测试用）
        /// </summary>
        public string DeptName { get; set; }
    }
}
