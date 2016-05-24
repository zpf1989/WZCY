using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 角色权限关联类
    /// </summary>
    public class RFRelation
    {
        private string _id;
        private int _funID;
        private string _roleID;

        /// <summary>
        /// 关联ID
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int FunID
        {
            set { _funID = value; }
            get { return _funID; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID
        {
            get { return _roleID; }
            set { this._roleID = value; }
        }
    }
}
