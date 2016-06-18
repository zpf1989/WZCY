using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;
using OA.Model;
using System.Reflection;
using OA.GeneralClass;

namespace OA.DAL
{
    public class UserInfoDAL : IUserInfoDAL
    {
        #region 添加
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">用户实体类</param>
        /// <returns>用户ID</returns>
        public bool Add(OA.Model.UserInfo userInfo)
        {
            URrelationDAL urDal = new URrelationDAL();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_User(");
            strSql.Append("UserID,UserCode,UserName,UserPwd,UserState,CreateTime,CreateUserID,DeptID,Operator)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@UserCode,@UserName,@UserPwd,@UserState,@CreateTime,@CreateUserID,@DeptID,@Operator)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,36),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@UserPwd", SqlDbType.VarChar,36),
					new SqlParameter("@UserState", SqlDbType.Char,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.VarChar,36),
					new SqlParameter("@DeptID", SqlDbType.VarChar,36),
					new SqlParameter("@Operator", SqlDbType.NVarChar,30)};
            parameters[0].Value = userInfo.UserID;
            parameters[1].Value = userInfo.UserCode;
            parameters[2].Value = userInfo.UserName;
            parameters[3].Value = userInfo.UserPwd;
            parameters[4].Value = userInfo.UserState;
            parameters[5].Value = userInfo.CreateTime;
            parameters[6].Value = userInfo.CreateUserID;
            parameters[7].Value = userInfo.DeptID;
            parameters[8].Value = userInfo.Operator;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                int obj = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                if (obj > 0)
                {
                    urDal.Delete(userInfo.UserID);

                    URrelation ur = new URrelation();
                    ur.ID = System.Guid.NewGuid().ToString();
                    ur.UserID = userInfo.UserID;
                    ur.RoleID = userInfo.RoleID;
                    urDal.Add(ur);

                    transaction.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            return false;
        }
        #endregion
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(UserInfo model)
        {
            URrelationDAL urDal = new URrelationDAL();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_User set ");
            strSql.Append("UserCode=@UserCode,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("UserState=@UserState,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("Operator=@Operator");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@UserState", SqlDbType.Char,1),
					new SqlParameter("@DeptID", SqlDbType.VarChar,36),
					new SqlParameter("@Operator", SqlDbType.NVarChar,30),
					new SqlParameter("@UserID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.UserCode;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.UserState;
            parameters[3].Value = model.DeptID;
            parameters[4].Value = model.Operator;
            parameters[5].Value = model.UserID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                int rows = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                if (rows > 0)
                {
                    urDal.Delete(model.UserID);

                    URrelation ur = new URrelation();
                    ur.ID = System.Guid.NewGuid().ToString();
                    ur.UserID = model.UserID;
                    ur.RoleID = model.RoleID;
                    urDal.Add(ur);

                    transaction.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            return false;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool Delete(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_User ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,36)};
            parameters[0].Value = UserID;

            int rows = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检验登录账号
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <returns></returns>
        public int CheckUserCode(string UserCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select count(*) from OA_User where UserState='1' and UserCode='{0}' ", UserCode);
            return Convert.ToInt32(DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null));
        }
        /// <summary>
        /// 检验登录账号和密码
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserPwd">用户密码</param>
        /// <returns></returns>
        public int CheckUserCodeAndPwd(string UserCode, string UserPwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select count(*) from OA_User ");
            strSql.AppendFormat(" where UserState='1' and UserCode='{0}' and UserPwd='{1}' ", UserCode, UserPwd);
            return Convert.ToInt32(DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null));
        }
        /// <summary>
        /// 根据用户账号和密码获取用户模型
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserPwd">用户密码</param>
        /// <returns></returns>
        public UserInfo GetModelOfUserByUserCodeAndPwd(string UserCode, string UserPwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select * from OA_User ");
            strSql.AppendFormat(" where UserCode='{0}' and UserPwd='{1}' ", UserCode, UserPwd);

            UserInfo userInfo = new UserInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                userInfo.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                userInfo.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                userInfo.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                userInfo.UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                userInfo.UserState = ds.Tables[0].Rows[0]["UserState"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    userInfo.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                userInfo.CreateUserID = ds.Tables[0].Rows[0]["CreateUserID"].ToString();
                return userInfo;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户ID获取用户模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserInfo GetModel(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 U.UserID,UserCode,UserName,UserPwd,UserState,CreateTime,CreateUserID,DeptID,Operator,UR.RoleID from OA_User U ");
            strSql.Append(" left join OA_URrelation UR on UR.UserID=U.UserID ");
            strSql.Append(" where U.UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.VarChar,36)};
            parameters[0].Value = UserID;

            UserInfo model = new UserInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserCode"] != null && ds.Tables[0].Rows[0]["UserCode"].ToString() != "")
                {
                    model.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserPwd"] != null && ds.Tables[0].Rows[0]["UserPwd"].ToString() != "")
                {
                    model.UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserState"] != null && ds.Tables[0].Rows[0]["UserState"].ToString() != "")
                {
                    model.UserState = ds.Tables[0].Rows[0]["UserState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateUserID"] != null && ds.Tables[0].Rows[0]["CreateUserID"].ToString() != "")
                {
                    model.CreateUserID = ds.Tables[0].Rows[0]["CreateUserID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeptID"] != null && ds.Tables[0].Rows[0]["DeptID"].ToString() != "")
                {
                    model.DeptID = ds.Tables[0].Rows[0]["DeptID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Operator"] != null && ds.Tables[0].Rows[0]["Operator"].ToString() != "")
                {
                    model.Operator = ds.Tables[0].Rows[0]["Operator"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RoleID"] != null && ds.Tables[0].Rows[0]["RoleID"].ToString() != "")
                {
                    model.RoleID = ds.Tables[0].Rows[0]["RoleID"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserNewPwd">用户新密码</param>
        /// <returns></returns>
        public int ChangePwd(string UserCode, string UserNewPwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" update OA_User ");
            strSql.AppendLine(" set UserPwd=@UserPwd ");
            strSql.AppendLine(" where UserCode=@UserCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@UserPwd", SqlDbType.VarChar,36)
            };
            parameters[0].Value = UserCode;
            parameters[1].Value = UserNewPwd;

            int count = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count <= 0)
            {
                return 0;
            }
            else
            {
                return count;
            }
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
            StringBuilder strSql = new StringBuilder();
            if (!ValidateUtil.isBlank(UserCode))
            {
                strSql.Append(" and UserCode like'%" + UserCode.Trim() + "%' ");
            }
            if (!ValidateUtil.isBlank(UserName))
            {
                strSql.Append(" and UserName like'%" + UserName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select U.*,(select max(UserName) ")
                .AppendLine(" from OA_User ")
                .AppendLine(" where UserID=U.CreateUserID) Name,UR.RoleID,R.RoleName,R.RoleCode,row_number() OVER (order by UserCode desc) as RowId ")
                .AppendLine(" from OA_User U ")
                .AppendLine(" left join OA_URrelation UR on UR.UserID=U.UserID ")
                .AppendLine(" left join OA_Role R on R.RoleID=UR.RoleID ")
                .AppendLine(" where UserState=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UserCode">用户账号</param>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public int GetRowCounts(string UserCode, string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from OA_User ")
            .AppendLine(" where UserState=1 ");

            if (!ValidateUtil.isBlank(UserCode))
            {
                strSql.Append(" and UserCode like'%" + UserCode.Trim() + "%' ");
            }
            if (!ValidateUtil.isBlank(UserName))
            {
                strSql.Append(" and UserName like'%" + UserName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取用户List
        /// </summary>
        /// <param name="deptID">部门ID</param>
        /// <returns></returns>
        public IList<UserInfo> GetUserList(string deptID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" select * from OA_User where DeptID='{0}' ", deptID);
                IList<UserInfo> list = new List<UserInfo>();

                DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.UserID = dr["UserID"].ToString();
                        userInfo.UserCode = dr["UserCode"].ToString();
                        userInfo.UserName = dr["UserName"].ToString();
                        userInfo.UserPwd = dr["UserPwd"].ToString();
                        userInfo.UserState = dr["UserState"].ToString();
                        if (dr["CreateTime"].ToString() != "")
                        {
                            userInfo.CreateTime = DateTime.Parse(dr["CreateTime"].ToString());
                        }
                        userInfo.CreateUserID = dr["CreateUserID"].ToString();
                        list.Add(userInfo);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //
            }
        }

    }
}
