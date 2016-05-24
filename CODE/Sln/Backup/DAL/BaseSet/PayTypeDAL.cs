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
    public class PayTypeDAL : IPayTypeDAL
    {
        /// <summary>
        /// 新增付款方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(PayType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PayType(");
            strSql.Append("PayTypeID,PayTypeCode,PayTypeName,PayTypeRemark)");
            strSql.Append(" values (");
            strSql.Append("@PayTypeID,@PayTypeCode,@PayTypeName,@PayTypeRemark)");
            SqlParameter[] parameters = {
					new SqlParameter("@PayTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@PayTypeCode", SqlDbType.VarChar,30),
					new SqlParameter("@PayTypeName", SqlDbType.VarChar,60),
					new SqlParameter("@PayTypeRemark", SqlDbType.VarChar,1024)};
            parameters[0].Value = model.PayTypeID;
            parameters[1].Value = model.PayTypeCode;
            parameters[2].Value = model.PayTypeName;
            parameters[3].Value = model.PayTypeRemark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改付款方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(PayType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PayType set ");
            strSql.Append("PayTypeCode=@PayTypeCode,");
            strSql.Append("PayTypeName=@PayTypeName,");
            strSql.Append("PayTypeRemark=@PayTypeRemark");
            strSql.Append(" where PayTypeID=@PayTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PayTypeCode", SqlDbType.VarChar,30),
					new SqlParameter("@PayTypeName", SqlDbType.VarChar,60),
					new SqlParameter("@PayTypeRemark", SqlDbType.VarChar,1024),
					new SqlParameter("@PayTypeID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.PayTypeCode;
            parameters[1].Value = model.PayTypeName;
            parameters[2].Value = model.PayTypeRemark;
            parameters[3].Value = model.PayTypeID;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 保存时校验是否有重复数据
        /// </summary>
        /// <param name="payTypeCode">付款方式编号</param>
        /// <param name="payTypeName">付款方式名称</param>
        /// <returns></returns>
        public bool Exists(string payTypeCode, string payTypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PayType");
            strSql.Append(" where PayTypeCode=@PayTypeCode or PayTypeName=@PayTypeName");
            SqlParameter[] parameters = {
					new SqlParameter("@PayTypeCode", SqlDbType.VarChar,60),
                    new SqlParameter("@PayTypeName", SqlDbType.VarChar,255)};
            parameters[0].Value = payTypeCode;
            parameters[1].Value = payTypeName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="payTypeCode">付款方式编号</param>
        /// <param name="payTypeName">付款方式名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string payTypeCode, string payTypeName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(payTypeCode))
            {
                strSql.Append(" and PayTypeCode like'%" + payTypeCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(payTypeName))
            {
                strSql.Append(" and PayTypeName like'%" + payTypeName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by PayTypeCode desc) as RowId from PayType where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="payTypeCode">付款方式编号</param>
        /// <param name="payTypeName">付款方式名称</param>
        /// <returns></returns>
        public int GetRowCounts(string payTypeCode, string payTypeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from PayType where 1=1 ");

            if (!string.IsNullOrEmpty(payTypeCode))
            {
                strSql.Append(" and PayTypeCode like'%" + payTypeCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(payTypeName))
            {
                strSql.Append(" and PayTypeName like'%" + payTypeName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="payTypeID"></param>
        /// <returns></returns>
        public bool DelPayType(string payTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PayType ");
            strSql.Append(" where PayTypeID=@PayTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PayTypeID", SqlDbType.VarChar,36)			};
            parameters[0].Value = payTypeID;

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
        /// 获取模型
        /// </summary>
        /// <param name="payTypeID"></param>
        /// <returns></returns>
        public PayType GetModel(string payTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 PayTypeID,PayTypeCode,PayTypeName,PayTypeRemark from PayType ");
            strSql.Append(" where PayTypeID=@PayTypeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PayTypeID", SqlDbType.VarChar,36)			};
            parameters[0].Value = payTypeID;

            PayType model = new PayType();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PayTypeID"] != null && ds.Tables[0].Rows[0]["PayTypeID"].ToString() != "")
                {
                    model.PayTypeID = ds.Tables[0].Rows[0]["PayTypeID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PayTypeCode"] != null && ds.Tables[0].Rows[0]["PayTypeCode"].ToString() != "")
                {
                    model.PayTypeCode = ds.Tables[0].Rows[0]["PayTypeCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PayTypeName"] != null && ds.Tables[0].Rows[0]["PayTypeName"].ToString() != "")
                {
                    model.PayTypeName = ds.Tables[0].Rows[0]["PayTypeName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PayTypeRemark"] != null && ds.Tables[0].Rows[0]["PayTypeRemark"].ToString() != "")
                {
                    model.PayTypeRemark = ds.Tables[0].Rows[0]["PayTypeRemark"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PayTypeID,PayTypeCode,PayTypeName,PayTypeRemark from PayType ");
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }


    }
}
