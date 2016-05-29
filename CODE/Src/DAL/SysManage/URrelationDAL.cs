using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class URrelationDAL : IURrelationDAL
    {
        public const string TableName = "OA_URrelation";
        /// <summary>
        /// 根据用户ID获取角色ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public URrelation GetModelOfURrelationByUserID(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select * from OA_URrelation ");
            strSql.AppendFormat(" where UserID='{0}' ", UserID);

            URrelation urRelation = new URrelation();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                urRelation.ID = ds.Tables[0].Rows[0]["ID"].ToString();
                urRelation.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                urRelation.RoleID = ds.Tables[0].Rows[0]["RoleID"].ToString();
                return urRelation;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 删除用户对应的角色
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool Delete(string UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_URrelation ");
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
        /// 添加用户对应的角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(URrelation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_URrelation(");
            strSql.Append("ID,UserID,RoleID)");
            strSql.Append(" values (");
            strSql.Append("@ID,@UserID,@RoleID)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.VarChar,36),
					new SqlParameter("@UserID", SqlDbType.VarChar,36),
					new SqlParameter("@RoleID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.RoleID;

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

    }
}
