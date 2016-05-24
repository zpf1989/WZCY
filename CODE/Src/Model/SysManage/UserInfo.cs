using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class UserInfo
    {
        private string _userID;
        private string _userCode;
        private string _userName;
        private string _userPwd;
        private string _userState;
        private DateTime _createTime;
        private string _createUserID;
        private string _deptid;
        private string _operator;
        private string _roleID;

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID
        {
            set { _userID = value; }
            get { return _userID; }
        }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserCode
        {
            set { _userCode = value; }
            get { return _userCode; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd
        {
            get { return _userPwd; }
            set { this._userPwd = value; }
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        public string UserState
        {
            set { _userState = value; }
            get { return _userState; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createTime = value; }
            get { return _createTime; }
        }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreateUserID
        {
            set { _createUserID = value; }
            get { return _createUserID; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 操作权限
        /// </summary>
        public string Operator
        {
            set { _operator = value; }
            get { return _operator; }
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
