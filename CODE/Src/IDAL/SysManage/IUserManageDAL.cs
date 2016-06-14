using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IUserManageDAL : IBaseDAL<UserInfo>
    {
        /// <summary>
        /// 设置操作权限
        /// </summary>
        /// <param name="userIds">用户id数组</param>
        /// <param name="optValues">操作权限数组</param>
        /// <returns></returns>
        bool SetOpt(string[] userIds, string[] optValues);
    }
}
