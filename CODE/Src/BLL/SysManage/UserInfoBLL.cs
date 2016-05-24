using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class UserInfoBLL
    {
        private readonly OA.IDAL.IUserInfoDAL iUserInfoDAL = DALFactory.Helper.GetIUserInfoDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(UserInfo userInfo)
        {
            return iUserInfoDAL.Add(userInfo);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(UserInfo model)
        {
            return iUserInfoDAL.Update(model);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool Delete(string UserID)
        {
            return iUserInfoDAL.Delete(UserID);
        }
        /// <summary>
        /// 检验登录账号
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <returns></returns>
        public int CheckUserCode(string UserCode)
        {
            return iUserInfoDAL.CheckUserCode(UserCode);
        }
        /// <summary>
        /// 检验登录账号和密码
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserPwd">用户密码</param>
        /// <returns></returns>
        public int CheckUserCodeAndPwd(string UserCode, string UserPwd)
        {
            return iUserInfoDAL.CheckUserCodeAndPwd(UserCode, UserPwd);
        }
        /// <summary>
        /// 根据用户账号和密码获取用户模型
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserPwd">用户密码</param>
        /// <returns></returns>
        public UserInfo GetModelOfUserByUserCodeAndPwd(string UserCode, string UserPwd)
        {
            return iUserInfoDAL.GetModelOfUserByUserCodeAndPwd(UserCode, UserPwd);
        }
        /// <summary>
        /// 根据用户ID获取用户模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserInfo GetModel(string UserID)
        {
            return iUserInfoDAL.GetModel(UserID);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserNewPwd">用户新密码</param>
        /// <returns></returns>
        public int ChangePwd(string UserCode, string UserNewPwd)
        {
            return iUserInfoDAL.ChangePwd(UserCode, UserNewPwd);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserName">用户名</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string UserCode, string UserName, int pageSize, int startRowIndex)
        {
            return iUserInfoDAL.GetPageList(UserCode, UserName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public int GetRowCounts(string UserCode, string UserName)
        {
            return iUserInfoDAL.GetRowCounts(UserCode, UserName);
        }
        /// <summary>
        /// 获取用户List
        /// </summary>
        /// <param name="deptID">部门ID</param>
        /// <returns></returns>
        public IList<UserInfo> GetUserList(string deptID)
        {
            return iUserInfoDAL.GetUserList(deptID);
        }

    }
}
