using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.IDAL;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.Model;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.GeneralClass.Logger;

namespace OA.DAL
{
    public class UserManageDAL : BaseDAL<UserInfo>, IUserManageDAL
    {
        public const string TableName = "OA_User";
        ILogHelper<UserManageDAL> logger = LoggerFactory.GetLogger<UserManageDAL>();
        public override List<UserInfo> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (string.IsNullOrEmpty(orderBySql))
            {
                orderBySql = "UserCode";
            }

            List<UserInfo> users = new List<UserInfo>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName =
                    string.Format(" {0} as usr left join {1} as dept on usr.DeptID=dept.DeptID left join {2} as ur on usr.UserID=ur.UserID  left join {3} as role on ur.RoleID=role.RoleID ",
                    TableName, DepartmentDAL.TableName, URrelationDAL.TableName, RoleManageDAL.TableName),
                PK = "usr.UserID",
                Fields = "usr.*,dept.DeptName as Dept_Name,ur.RoleID,role.RoleName Role_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    users.Add(new UserInfo
                    {
                        UserID = row["UserID"].ToString(),
                        UserCode = row["UserCode"].ToString(),
                        UserName = row["UserName"].ToString(),
                        UserState = row["UserState"].ToString(),
                        CreateTime = Convert.ToDateTime(row["CreateTime"]),
                        DeptID = row["DeptID"].ToString(),
                        Dept_Name = row["Dept_Name"].ToString(),
                        Operator = row["Operator"].ToString(),
                        RoleID = row["RoleID"].ToString(),
                        Role_Name = row["Role_Name"].ToString()
                    });
                }
            }

            return users;
        }

        public override bool Save(params UserInfo[] users)
        {
            if (users == null || users.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            UserInfo user = null;
            for (int i = 0; i < users.Length; i++)
            {
                user = users[i];
                //1、组织sql
                if (string.IsNullOrEmpty(user.UserID))
                {
                    //新增
                    user.UserID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(UserID,UserCode,UserName,UserPwd,UserState,CreateTime,CreateUserID,DeptID)", TableName);
                    sbSql.AppendFormat(" values (@UserID{0},@UserCode{0},@UserName{0},@UserPwd{0},@UserState{0},@CreateTime{0},@CreateUserID{0},@DeptID{0});", i);
                    sbSql.AppendFormat("insert into {0}(ID,UserID,RoleID)  values (NEWID(),@UserID{1},@RoleID{1});", URrelationDAL.TableName, i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set UserCode=@UserCode{1},UserName=@UserName{1},DeptID=@DeptID{1}", TableName, i);
                    sbSql.AppendFormat(" where UserID=@UserID{0};", i);
                    sbSql.AppendFormat("delete from {0} where UserID=@UserID{1};", URrelationDAL.TableName, i);
                    sbSql.AppendFormat("insert into {0}(ID,UserID,RoleID)  values (NEWID(),@UserID{1},@RoleID{1});", URrelationDAL.TableName, i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter("@UserID"+i, SqlDbType.VarChar,36){Value=user.UserID},
                            new SqlParameter("@UserCode"+i, SqlDbType.VarChar,20){Value=user.UserCode},
                            new SqlParameter("@UserName"+i, SqlDbType.VarChar,20){Value=user.UserName},
                            new SqlParameter("@UserPwd"+i, SqlDbType.VarChar,36){Value=user.UserPwd},
                            new SqlParameter("@UserState"+i, SqlDbType.Char,1){Value=user.UserState},
                            new SqlParameter("@CreateTime"+i, SqlDbType.DateTime){Value=user.CreateTime},
                            new SqlParameter("@CreateUserID"+i, SqlDbType.VarChar,36){Value=user.CreateUserID},
                            new SqlParameter("@DeptID"+i, SqlDbType.VarChar,36){Value=user.DeptID},
                            new SqlParameter("@RoleID"+i, SqlDbType.VarChar,36){Value=user.RoleID},
                            //new SqlParameter("@Operator"+i, SqlDbType.VarChar,50){Value=user.Operator},
                                            });
            }
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public override bool Delete(params string[] userIds)
        {
            if (userIds == null || userIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql1 = new StringBuilder();//删除角色的sql
            StringBuilder sbSql2 = new StringBuilder();//删除用户的sql
            sbSql1.AppendFormat("delete from {0} where UserId in (", URrelationDAL.TableName);
            sbSql2.AppendFormat("delete from {0} where UserId in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < userIds.Length; i++)
            {
                sbSql1.AppendFormat("@UserId{0}", i);
                sbSql2.AppendFormat("@UserId{0}", i);
                sqlParams.Add(new SqlParameter("@UserId" + i, SqlDbType.VarChar, 36) { Value = userIds[i] });
                if (i < userIds.Length - 1)
                {
                    sbSql1.Append(",");
                    sbSql2.Append(",");
                }
            }
            sbSql1.Append(");");
            sbSql2.Append(");");
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql1.ToString() + sbSql2.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }


        public bool SetOpt(string[] userIds, string[] optValues)
        {
            if (userIds == null || userIds.Length < 1)
            {
                return false;
            }
            string optValString = "";
            if (optValues != null && optValues.Length > 0)
            {
                optValString = string.Join(",", optValues);
            }
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("update {0} set Operator='{1}' where UserId in (", TableName, optValString);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < userIds.Length; i++)
            {
                sbSql.AppendFormat("@UserId{0}", i);
                sqlParams.Add(new SqlParameter("@UserId" + i, SqlDbType.VarChar, 36) { Value = userIds[i] });
                if (i < userIds.Length - 1)
                {
                    sbSql.Append(",");
                }
            }
            sbSql.Append(");");
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        protected override string GetTableName()
        {
            return TableName;
        }


        public bool Exists(params string[] userCodes)
        {
            if (userCodes == null || userCodes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and UserCode in ('{0}')", string.Join("','", userCodes)));
        }
    }
}
