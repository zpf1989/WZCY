using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 角色实体类
    /// </summary>
    public class RoleInfo
    {
        private string _roleID;
        private string _roleCode;
        private string _roleName;

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID
        {
            set { _roleID = value; }
            get { return _roleID; }
        }
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleCode
        {
            set { _roleCode = value; }
            get { return _roleCode; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get { return _roleName; }
            set { this._roleName = value; }
        }
    }
}
