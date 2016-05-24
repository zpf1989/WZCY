using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 用户角色关联类
    /// </summary>
    public class URrelation
    {
        private string _id;
        private string _userID;
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
        public string UserID
        {
            set { _userID = value; }
            get { return _userID; }
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
