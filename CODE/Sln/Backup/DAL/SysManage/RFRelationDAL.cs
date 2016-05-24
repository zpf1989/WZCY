using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.IDAL;
using System.Reflection;
using System.Data;
using OA.Model;
using GentleUtil.DB;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class RFRelationDAL : IRFRelationDAL
    {
        /// <summary>
        /// 根据用户ID获取功能List
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<FunctionInfo> GetFunList(string userid)
        {
            IDataReader reader = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" select * from OA_Function ");
                strSql.AppendFormat(" where FunID in (select FunID from OA_RFRelation where RoleID='{0}') ", userid);
                List<FunctionInfo> list = new List<FunctionInfo>();
                reader = DBAccess.ExecuteReader(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                while (reader.Read())
                {
                    FunctionInfo info = new FunctionInfo();
                    this.ReaderToObject(reader, info);
                    list.Add(info);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
        }
        /// <summary>
        /// 数据映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="targetObj"></param>
        private void ReaderToObject(IDataReader reader, object targetObj)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                PropertyInfo property = targetObj.GetType().GetProperty(reader.GetName(i));
                if ((property != null) && (reader.GetValue(i) != DBNull.Value))
                {
                    if (reader.GetFieldType(i).ToString().ToUpper() == "SYSTEM.DATETIME")
                    {
                        property.SetValue(targetObj, ((DateTime)reader.GetValue(i)).ToString("yyyy年MM月dd日 hh:mm"), null);
                    }
                    else if (reader.GetFieldType(i).ToString().ToUpper() == "SYSTEM.DOUBLE")
                    {
                        property.SetValue(targetObj, (float)((double)reader.GetValue(i)), null);
                    }
                    else
                    {
                        property.SetValue(targetObj, reader.GetValue(i), null);
                    }
                }
            }
        }
        /// <summary>
        /// 根据角色获取该角色的权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public DataSet GetDataSetOfRFRelationByRoleID(string roleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select * from OA_RFRelation where RoleID = '{0}' ", roleID);
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 根据角色ID删除相应的功能权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public int DelByRoleID(string roleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" delete OA_RFRelation where RoleID='{0}' ", roleID);
            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null); 
        }
        /// <summary>
        /// 给角色添加权限
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddRole(OA.Model.RFRelation info)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" insert into OA_RFRelation( ");
            strSql.AppendLine(" ID,RoleID,FunID) ");
            strSql.AppendLine(" values ( ");
            strSql.AppendLine(" @ID,@RoleID,@FunID) ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.VarChar,36),
					new SqlParameter("@RoleID", SqlDbType.VarChar,36),
					new SqlParameter("@FunID", SqlDbType.Int)
            };
            parameters[0].Value = info.ID;
            parameters[1].Value = info.RoleID;
            parameters[2].Value = info.FunID;

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
    }
}
