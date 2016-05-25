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
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal Salary { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
    }
}
