using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;

namespace OA.DAL
{
    public class MaterialClassDAL : IMaterialClassDAL
    {
        /// <summary>
        /// 添加物料分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMaterialClass(MaterialClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MaterialClass(");
            strSql.Append("MaterialClassID,MaterialClassCode,MaterialClassName,Remark)");
            strSql.Append(" values (");
            strSql.Append("@MaterialClassID,@MaterialClassCode,@MaterialClassName,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialClassID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialClassCode", SqlDbType.VarChar,30),
					new SqlParameter("@MaterialClassName", SqlDbType.VarChar,60),
					new SqlParameter("@Remark", SqlDbType.VarChar,255)};
            parameters[0].Value = model.MaterialClassID;
            parameters[1].Value = model.MaterialClassCode;
            parameters[2].Value = model.MaterialClassName;
            parameters[3].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改物料分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMaterialClass(MaterialClass model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MaterialClass set ");
            strSql.Append("MaterialClassCode=@MaterialClassCode,");
            strSql.Append("MaterialClassName=@MaterialClassName,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where MaterialClassID=@MaterialClassID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialClassCode", SqlDbType.VarChar,30),
					new SqlParameter("@MaterialClassName", SqlDbType.VarChar,60),
					new SqlParameter("@Remark", SqlDbType.VarChar,255),
					new SqlParameter("@MaterialClassID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.MaterialClassCode;
            parameters[1].Value = model.MaterialClassName;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.MaterialClassID;

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
        /// 删除物料分类
        /// </summary>
        /// <param name="MaterialClassID"></param>
        /// <returns></returns>
        public bool DelMaterialClass(string MaterialClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MaterialClass ");
            strSql.Append(" where MaterialClassID=@MaterialClassID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialClassID", SqlDbType.VarChar,36)};
            parameters[0].Value = MaterialClassID;

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
        /// 根据物料分类编号和名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        public bool Exists(string MaterialClassCode, string MaterialClassName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MaterialClass");
            strSql.Append(" where MaterialClassCode=@MaterialClassCode or MaterialClassName=@MaterialClassName");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialClassCode", SqlDbType.VarChar,30),
                    new SqlParameter("@MaterialClassName", SqlDbType.VarChar,60)};
            parameters[0].Value = MaterialClassCode;
            parameters[1].Value = MaterialClassName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据物料分类名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        public bool Exists4Update(string MaterialClassCode, string MaterialClassName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MaterialClass");
            strSql.Append(" where MaterialClassName=@MaterialClassName and MaterialClassCode != @MaterialClassCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@MaterialClassName", SqlDbType.VarChar,60),
                    new SqlParameter("@MaterialClassCode", SqlDbType.VarChar,30)};
            parameters[0].Value = MaterialClassName;
            parameters[1].Value = MaterialClassCode;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string MaterialClassCode, string MaterialClassName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(MaterialClassCode))
            {
                strSql.Append(" and MaterialClassCode like'%" + MaterialClassCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialClassName))
            {
                strSql.Append(" and MaterialClassName like'%" + MaterialClassName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by MaterialClassCode asc) as RowId from MaterialClass where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        public int GetRowCounts(string MaterialClassCode, string MaterialClassName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from MaterialClass where 1=1 ");

            if (!string.IsNullOrEmpty(MaterialClassCode))
            {
                strSql.Append(" and MaterialClassCode like'%" + MaterialClassCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialClassName))
            {
                strSql.Append(" and MaterialClassName like'%" + MaterialClassName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取物料分类模型
        /// </summary>
        /// <param name="MaterialClassID"></param>
        /// <returns></returns>
        public MaterialClass GetModel(string MaterialClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MaterialClassID,MaterialClassCode,MaterialClassName,Remark from MaterialClass ");
            strSql.Append(" where MaterialClassID=@MaterialClassID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialClassID", SqlDbType.VarChar,36)};
            parameters[0].Value = MaterialClassID;

            MaterialClass model = new MaterialClass();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MaterialClassID"] != null && ds.Tables[0].Rows[0]["MaterialClassID"].ToString() != "")
                {
                    model.MaterialClassID = ds.Tables[0].Rows[0]["MaterialClassID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialClassCode"] != null && ds.Tables[0].Rows[0]["MaterialClassCode"].ToString() != "")
                {
                    model.MaterialClassCode = ds.Tables[0].Rows[0]["MaterialClassCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialClassName"] != null && ds.Tables[0].Rows[0]["MaterialClassName"].ToString() != "")
                {
                    model.MaterialClassName = ds.Tables[0].Rows[0]["MaterialClassName"].ToString();
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
        /// 获取物料分类DataSet
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MaterialClassID,MaterialClassCode,MaterialClassName,Remark ");
            strSql.Append(" FROM MaterialClass ");
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 获取物料分类List
        /// </summary>
        /// <returns></returns>
        public IList<MaterialClass> GetMaterialClass()
        { 
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select * from MaterialClass ");
                IList<MaterialClass> list = new List<MaterialClass>();

                DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MaterialClass materialClass = new MaterialClass();
                        if(dr["MaterialClassID"]!=null && dr["MaterialClassID"].ToString()!="")
				        {
					        materialClass.MaterialClassID=dr["MaterialClassID"].ToString();
				        }
				        if(dr["MaterialClassCode"]!=null && dr["MaterialClassCode"].ToString()!="")
				        {
					        materialClass.MaterialClassCode=dr["MaterialClassCode"].ToString();
				        }
				        if(dr["MaterialClassName"]!=null && dr["MaterialClassName"].ToString()!="")
				        {
					        materialClass.MaterialClassName=dr["MaterialClassName"].ToString();
				        }
				        if(dr["Remark"]!=null && dr["Remark"].ToString()!="")
				        {
					        materialClass.Remark=dr["Remark"].ToString();
				        }
                        list.Add(materialClass);
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
