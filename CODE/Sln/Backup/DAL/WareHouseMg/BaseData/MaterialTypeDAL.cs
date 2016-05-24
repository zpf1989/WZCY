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
    public class MaterialTypeDAL : IMaterialTypeDAL
    {
        /// <summary>
        /// 添加物料类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMaterialType(MaterialType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MaterialType(");
            strSql.Append("MaterialTypeID,MaterialTypeCode,MaterialTypeName,Remark)");
            strSql.Append(" values (");
            strSql.Append("@MaterialTypeID,@MaterialTypeCode,@MaterialTypeName,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialTypeCode", SqlDbType.VarChar,30),
					new SqlParameter("@MaterialTypeName", SqlDbType.VarChar,60),
					new SqlParameter("@Remark", SqlDbType.VarChar,255)};
            parameters[0].Value = model.MaterialTypeID;
            parameters[1].Value = model.MaterialTypeCode;
            parameters[2].Value = model.MaterialTypeName;
            parameters[3].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改物料类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMaterialType(MaterialType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MaterialType set ");
            strSql.Append("MaterialTypeCode=@MaterialTypeCode,");
            strSql.Append("MaterialTypeName=@MaterialTypeName,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where MaterialTypeID=@MaterialTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialTypeCode", SqlDbType.VarChar,30),
					new SqlParameter("@MaterialTypeName", SqlDbType.VarChar,60),
					new SqlParameter("@Remark", SqlDbType.VarChar,255),
					new SqlParameter("@MaterialTypeID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.MaterialTypeCode;
            parameters[1].Value = model.MaterialTypeName;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.MaterialTypeID;

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
        /// 删除物料类型
        /// </summary>
        /// <param name="MaterialTypeID"></param>
        /// <returns></returns>
        public bool DelMaterialType(string MaterialTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MaterialType ");
            strSql.Append(" where MaterialTypeID=@MaterialTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialTypeID", SqlDbType.VarChar,36)};
            parameters[0].Value = MaterialTypeID;

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
        /// 根据物料类型编号和名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        public bool Exists(string MaterialTypeCode, string MaterialTypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MaterialType");
            strSql.Append(" where MaterialTypeCode=@MaterialTypeCode or MaterialTypeName=@MaterialTypeName");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialTypeCode", SqlDbType.VarChar,30),
                    new SqlParameter("@MaterialTypeName", SqlDbType.VarChar,60)};
            parameters[0].Value = MaterialTypeCode;
            parameters[1].Value = MaterialTypeName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据计量单位名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        public bool Exists4Update(string MaterialTypeCode, string MaterialTypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MaterialType");
            strSql.Append(" where MaterialTypeName=@MaterialTypeName and MaterialTypeCode != @MaterialTypeCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@MaterialTypeName", SqlDbType.VarChar,60),
                    new SqlParameter("@MaterialTypeCode", SqlDbType.VarChar,30)};
            parameters[0].Value = MaterialTypeName;
            parameters[1].Value = MaterialTypeCode;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string MaterialTypeCode, string MaterialTypeName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(MaterialTypeCode))
            {
                strSql.Append(" and MaterialTypeCode like'%" + MaterialTypeCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialTypeName))
            {
                strSql.Append(" and MaterialTypeName like'%" + MaterialTypeName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by MaterialTypeCode asc) as RowId from MaterialType where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        public int GetRowCounts(string MaterialTypeCode, string MaterialTypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from MaterialType where 1=1 ");

            if (!string.IsNullOrEmpty(MaterialTypeCode))
            {
                strSql.Append(" and MaterialTypeCode like'%" + MaterialTypeCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialTypeName))
            {
                strSql.Append(" and MaterialTypeName like'%" + MaterialTypeName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取物料类型模型
        /// </summary>
        /// <param name="MaterialTypeID"></param>
        /// <returns></returns>
        public MaterialType GetModel(string MaterialTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MaterialTypeID,MaterialTypeCode,MaterialTypeName,Remark from MaterialType ");
            strSql.Append(" where MaterialTypeID=@MaterialTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialTypeID", SqlDbType.VarChar,36)};
            parameters[0].Value = MaterialTypeID;

            MaterialType model = new MaterialType();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MaterialTypeID"] != null && ds.Tables[0].Rows[0]["MaterialTypeID"].ToString() != "")
                {
                    model.MaterialTypeID = ds.Tables[0].Rows[0]["MaterialTypeID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialTypeCode"] != null && ds.Tables[0].Rows[0]["MaterialTypeCode"].ToString() != "")
                {
                    model.MaterialTypeCode = ds.Tables[0].Rows[0]["MaterialTypeCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialTypeName"] != null && ds.Tables[0].Rows[0]["MaterialTypeName"].ToString() != "")
                {
                    model.MaterialTypeName = ds.Tables[0].Rows[0]["MaterialTypeName"].ToString();
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
        /// 获取物料类型DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MaterialTypeID,MaterialTypeCode,MaterialTypeName,Remark ");
            strSql.Append(" FROM MaterialType ");
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }

    }
}
