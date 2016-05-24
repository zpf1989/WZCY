using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 部门实体类
    /// </summary>
    public class DeptInfo
    {
        private string _deptid;
        private string _deptcode;
        private string _deptname;
        private string _parentdeptid;
        private string _remark;

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DeptCode
        {
            set { _deptcode = value; }
            get { return _deptcode; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }
        }
        /// <summary>
        /// 父级节点
        /// </summary>
        public string ParentDeptID
        {
            set { _parentdeptid = value; }
            get { return _parentdeptid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
