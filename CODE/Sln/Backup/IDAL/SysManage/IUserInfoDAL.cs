using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IUserInfoDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        bool Add(OA.Model.UserInfo userInfo);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(UserInfo model);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool Delete(string UserID);
        /// <summary>
        /// 检验登录账号
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <returns></returns>
        int CheckUserCode(string UserCode);
        /// <summary>
        /// 检验登录账号和密码
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserPwd">用户密码</param>
        /// <returns></returns>
        int CheckUserCodeAndPwd(string UserCode, string UserPwd);
        /// <summary>
        /// 根据用户账号和密码获取用户模型
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserPwd">用户密码</param>
        /// <returns></returns>
        UserInfo GetModelOfUserByUserCodeAndPwd(string UserCode, string UserPwd);
        /// <summary>
        /// 根据用户ID获取用户模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        UserInfo GetModel(string UserID);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserNewPwd">用户新密码</param>
        /// <returns></returns>
        int ChangePwd(string UserCode, string UserNewPwd);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserName">用户名</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string UserCode, string UserName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        int GetRowCounts(string UserCode, string UserName);
        /// <summary>
        /// 获取用户List
        /// </summary>
        /// <param name="deptID">部门ID</param>
        /// <returns></returns>
        IList<UserInfo> GetUserList(string deptID);
    }
}
