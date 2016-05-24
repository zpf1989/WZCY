using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GentleUtil.DB;
using System.Data;
using OA.IDAL;
using OA.Model;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class SupplierDAL : ISupplierDAL
    {
        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddSupplier(SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Supplier(");
            strSql.Append("SupplierID,SupplierCode,SupplierName,Contactor,Tel,Fax,Remark)");
            strSql.Append(" values (");
            strSql.Append("@SupplierID,@SupplierCode,@SupplierName,@Contactor,@Tel,@Fax,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36),
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,60),
					new SqlParameter("@SupplierName", SqlDbType.VarChar,255),
					new SqlParameter("@Contactor", SqlDbType.VarChar,60),
					new SqlParameter("@Tel", SqlDbType.VarChar,30),
					new SqlParameter("@Fax", SqlDbType.VarChar,30),
					new SqlParameter("@Remark", SqlDbType.VarChar,255)};
            parameters[0].Value = model.SupplierID;
            parameters[1].Value = model.SupplierCode;
            parameters[2].Value = model.SupplierName;
            parameters[3].Value = model.Contactor;
            parameters[4].Value = model.Tel;
            parameters[5].Value = model.Fax;
            parameters[6].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSupplier(SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Supplier set ");
            strSql.Append("SupplierCode=@SupplierCode,");
            strSql.Append("SupplierName=@SupplierName,");
            strSql.Append("Contactor=@Contactor,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Fax=@Fax,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where SupplierID=@SupplierID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,60),
					new SqlParameter("@SupplierName", SqlDbType.VarChar,255),
					new SqlParameter("@Contactor", SqlDbType.VarChar,60),
					new SqlParameter("@Tel", SqlDbType.VarChar,30),
					new SqlParameter("@Fax", SqlDbType.VarChar,30),
					new SqlParameter("@Remark", SqlDbType.VarChar,255),
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.SupplierCode;
            parameters[1].Value = model.SupplierName;
            parameters[2].Value = model.Contactor;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.Fax;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.SupplierID;

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
        /// 删除供应商
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public bool DelSupplier(string SupplierID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Supplier ");
            strSql.Append(" where SupplierID=@SupplierID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36)};
            parameters[0].Value = SupplierID;

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
        /// 根据供应商编号和名称判断是否已经存在
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <returns></returns>
        public bool Exists(string SupplierCode, string SupplierName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Supplier");
            strSql.Append(" where SupplierCode=@SupplierCode or SupplierName=@SupplierName");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,60),
                    new SqlParameter("@SupplierName", SqlDbType.VarChar,255)};
            parameters[0].Value = SupplierCode;
            parameters[1].Value = SupplierName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据供应商名称判断是否已经存在
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <returns></returns>
        public bool Exists4Update(string SupplierCode, string SupplierName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Supplier");
            strSql.Append(" where SupplierCode=@SupplierCode and SupplierName != @SupplierName");
            SqlParameter[] parameters = {
                    new SqlParameter("@SupplierCode", SqlDbType.VarChar,60),
                    new SqlParameter("@SupplierName", SqlDbType.VarChar,255)};
            parameters[0].Value = SupplierCode;
            parameters[1].Value = SupplierName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取供应商模型
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public SupplierInfo GetModel(string SupplierID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupplierID,SupplierCode,SupplierName,Contactor,Tel,Fax,Remark from Supplier ");
            strSql.Append(" where SupplierID=@SupplierID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36)};
            parameters[0].Value = SupplierID;

            SupplierInfo model = new SupplierInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SupplierID"] != null && ds.Tables[0].Rows[0]["SupplierID"].ToString() != "")
                {
                    model.SupplierID = ds.Tables[0].Rows[0]["SupplierID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SupplierCode"] != null && ds.Tables[0].Rows[0]["SupplierCode"].ToString() != "")
                {
                    model.SupplierCode = ds.Tables[0].Rows[0]["SupplierCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SupplierName"] != null && ds.Tables[0].Rows[0]["SupplierName"].ToString() != "")
                {
                    model.SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Contactor"] != null && ds.Tables[0].Rows[0]["Contactor"].ToString() != "")
                {
                    model.Contactor = ds.Tables[0].Rows[0]["Contactor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Tel"] != null && ds.Tables[0].Rows[0]["Tel"].ToString() != "")
                {
                    model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Fax"] != null && ds.Tables[0].Rows[0]["Fax"].ToString() != "")
                {
                    model.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
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
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UserCode">供应商编号</param>
        /// <param name="UserName">供应商名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SupplierCode, string SupplierName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(SupplierCode))
            {
                strSql.Append(" and SupplierCode like'%" + SupplierCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SupplierName))
            {
                strSql.Append(" and SupplierName like'%" + SupplierName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by SupplierCode desc) as RowId from Supplier ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UserCode">供应商编号</param>
        /// <param name="UserName">供应商名称</param>
        /// <returns></returns>
        public int GetRowCounts(string SupplierCode, string SupplierName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from Supplier ")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(SupplierCode))
            {
                strSql.Append(" and SupplierCode like'%" + SupplierCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SupplierName))
            {
                strSql.Append(" and SupplierName like'%" + SupplierName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取供应商List
        /// </summary>
        /// <returns></returns>
        public IList<SupplierInfo> GetSupplierList()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select * from Supplier ");
                IList<SupplierInfo> list = new List<SupplierInfo>();

                DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SupplierInfo supplierInfo = new SupplierInfo();
                        if (dr["SupplierID"] != null && dr["SupplierID"].ToString() != "")
                        {
                            supplierInfo.SupplierID = dr["SupplierID"].ToString();
                        }
                        if (dr["SupplierCode"] != null && dr["SupplierCode"].ToString() != "")
                        {
                            supplierInfo.SupplierCode = dr["SupplierCode"].ToString();
                        }
                        if (dr["SupplierName"] != null && dr["SupplierName"].ToString() != "")
                        {
                            supplierInfo.SupplierName = dr["SupplierName"].ToString();
                        }
                        if (dr["Contactor"] != null && dr["Contactor"].ToString() != "")
                        {
                            supplierInfo.Contactor = dr["Contactor"].ToString();
                        }
                        if (dr["Tel"] != null && dr["Tel"].ToString() != "")
                        {
                            supplierInfo.Tel = dr["Tel"].ToString();
                        }
                        if (dr["Fax"] != null && dr["Fax"].ToString() != "")
                        {
                            supplierInfo.Fax = dr["Fax"].ToString();
                        }
                        if (dr["Remark"] != null && dr["Remark"].ToString() != "")
                        {
                            supplierInfo.Remark = dr["Remark"].ToString();
                        }
                        list.Add(supplierInfo);
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
