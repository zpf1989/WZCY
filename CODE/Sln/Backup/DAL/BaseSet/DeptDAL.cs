using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;
using GentleUtil.DB;
using System.Reflection;
using OA.IDAL;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class DeptDAL : IDeptDAL
    {
        /// <summary>
        /// 获取部门List
        /// </summary>
        /// <returns></returns>
        public IList<DeptInfo> GetDeptList()
        {
            IDataReader reader = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" select * from OA_Dept ");
                IList<DeptInfo> list = new List<DeptInfo>();
                reader = DBAccess.ExecuteReader(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                while (reader.Read())
                {
                    DeptInfo info = new DeptInfo();
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
        /// 新增部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddDept(DeptInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_Dept(");
            strSql.Append("DeptID,DeptCode,DeptName,ParentDeptID,Remark)");
            strSql.Append(" values (");
            strSql.Append("@DeptID,@DeptCode,@DeptName,@ParentDeptID,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@DeptID", SqlDbType.VarChar,36),
					new SqlParameter("@DeptCode", SqlDbType.NVarChar,50),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentDeptID", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.DeptID;
            parameters[1].Value = model.DeptCode;
            parameters[2].Value = model.DeptName;
            parameters[3].Value = model.ParentDeptID;
            parameters[4].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDept(DeptInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_Dept set ");
            strSql.Append("DeptCode=@DeptCode,");
            strSql.Append("DeptName=@DeptName,");
            strSql.Append("ParentDeptID=@ParentDeptID,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where DeptID=@DeptID ");
            SqlParameter[] parameters = {
					new SqlParameter("@DeptID", SqlDbType.VarChar,36),
					new SqlParameter("@DeptCode", SqlDbType.NVarChar,50),
					new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentDeptID", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.DeptID;
            parameters[1].Value = model.DeptCode;
            parameters[2].Value = model.DeptName;
            parameters[3].Value = model.ParentDeptID;
            parameters[4].Value = model.Remark;

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
        /// 删除部门
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public bool DeleteDept(string deptID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("delete from OA_Dept where DeptID='{0}' ", deptID);

            int rows = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
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
        /// 返回分页列表集合
        /// </summary>
        /// <param name="DeptCode">部门编号</param>
        /// <param name="DeptName">部门名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string DeptCode, string DeptName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(DeptCode))
            {
                strSql.Append(" and DeptCode like'%" + DeptCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(DeptName))
            {
                strSql.Append(" and DeptName like'%" + DeptName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by DeptCode desc) as RowId from OA_Dept where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="DeptCode">部门编号</param>
        /// <param name="DeptName">部门名称</param>
        /// <returns></returns>
        public int GetRowCounts(string DeptCode, string DeptName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from OA_Dept where 1=1 ");

            if (!string.IsNullOrEmpty(DeptCode))
            {
                strSql.Append(" and DeptCode like'%" + DeptCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(DeptName))
            {
                strSql.Append(" and DeptName like'%" + DeptName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 根据部门主键获取模型
        /// </summary>
        /// <param name="DeptID">部门ID</param>
        /// <returns></returns>
        public DeptInfo GetModel(string DeptID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select  top 1 DeptID,DeptCode,DeptName,ParentDeptID,Remark from OA_Dept where DeptID='{0}' ", DeptID);

            DeptInfo model = new DeptInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DeptID"] != null && ds.Tables[0].Rows[0]["DeptID"].ToString() != "")
                {
                    model.DeptID = ds.Tables[0].Rows[0]["DeptID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeptCode"] != null && ds.Tables[0].Rows[0]["DeptCode"].ToString() != "")
                {
                    model.DeptCode = ds.Tables[0].Rows[0]["DeptCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeptName"] != null && ds.Tables[0].Rows[0]["DeptName"].ToString() != "")
                {
                    model.DeptName = ds.Tables[0].Rows[0]["DeptName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentDeptID"] != null && ds.Tables[0].Rows[0]["ParentDeptID"].ToString() != "")
                {
                    model.ParentDeptID = ds.Tables[0].Rows[0]["ParentDeptID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取部门DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDeptDataSet()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select * from OA_Dept ");

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }

    }
}
