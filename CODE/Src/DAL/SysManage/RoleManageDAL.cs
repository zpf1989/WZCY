using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GentleUtil.DB;
using System.Data;
using OA.IDAL;
using System.Data.SqlClient;
using OA.Model;
using OA.GeneralClass;
using OA.GeneralClass.Extensions;

namespace OA.DAL
{
    public class RoleManageDAL : IRoleManageDAL
    {
        public const string TableName = "OA_Role";
        /// <summary>
        /// 检验添加的角色是否存在
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="RoleName">角色名称</param>
        /// <returns></returns>
        public int CheckRole(string RoleCode, string RoleName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select count(*) from OA_Role where RoleCode='{0}' or RoleName='{1}' ", RoleCode, RoleName);
            return Convert.ToInt32(DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null));
        }
        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="roleInfo">角色实例</param>
        /// <returns></returns>
        public int AddRole(OA.Model.RoleInfo roleInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" insert into OA_Role( ");
            strSql.AppendLine(" RoleID,RoleCode,RoleName) ");
            strSql.AppendLine(" values ( ");
            strSql.AppendLine(" @RoleID,@RoleCode,@RoleName) ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.VarChar,36),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,100),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,100)
            };
            parameters[0].Value = roleInfo.RoleID;
            parameters[1].Value = roleInfo.RoleCode;
            parameters[2].Value = roleInfo.RoleName;

            int obj = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == 0)
            {
                return 0;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// 修改用户组
        /// </summary>
        /// <param name="roleInfo">角色实例</param>
        /// <returns></returns>
        public int UpdateRole(OA.Model.RoleInfo roleInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" update OA_Role ");
            strSql.AppendLine(" set RoleCode=@RoleCode,RoleName=@RoleName ");
            strSql.AppendLine(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.VarChar,36),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,100),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,100)
            };
            parameters[0].Value = roleInfo.RoleID;
            parameters[1].Value = roleInfo.RoleCode;
            parameters[2].Value = roleInfo.RoleName;

            int obj = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == 0)
            {
                return 0;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="roleInfo">角色ID</param>
        /// <returns></returns>
        public int DeleteRole(string RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" delete OA_Role ");
            strSql.AppendLine(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = RoleID;

            int obj = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == 0)
            {
                return 0;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// 根据RoleID获取模型
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
        public RoleInfo GetModelByRoleID(string RoleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select * from OA_Role where RoleID=@RoleID ");
            SqlParameter[] parameters = {
				new SqlParameter("@RoleID", SqlDbType.VarChar,36)};
            parameters[0].Value = RoleID;

            RoleInfo model = new RoleInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.RoleID = ds.Tables[0].Rows[0]["RoleID"].ToString();
                model.RoleCode = ds.Tables[0].Rows[0]["RoleCode"].ToString();
                model.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取角色的DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSetOfRole()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine(" select distinct * from OA_Role order by RoleCode ");
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, sqlStr.ToString(), null);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="RoleName">角色名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string RoleCode, string RoleName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(RoleCode))
            {
                strSql.Append(" and RoleCode like'%" + RoleCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(RoleName))
            {
                strSql.Append(" and RoleName like'%" + RoleName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by RoleCode asc) as RowId from OA_Role ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="RoleName">角色名称</param>
        /// <returns></returns>
        public int GetRowCounts(string RoleCode, string RoleName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from OA_Role ")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(RoleCode))
            {
                strSql.Append(" and RoleCode like'%" + RoleCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(RoleName))
            {
                strSql.Append(" and RoleName like'%" + RoleName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }



        public List<RoleInfo> GetRoleList()
        {
            List<RoleInfo> roles = new List<RoleInfo>();
            DataSet ds = GetDataSetOfRole();
            if (!ds.HasRow())
            {
                return roles;
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                roles.Add(new RoleInfo
                {
                    RoleID = row["RoleID"].ToString(),
                    RoleCode = row["RoleCode"].ToString(),
                    RoleName = row["RoleName"].ToString()
                });
            }
            return roles;
        }
    }
}
