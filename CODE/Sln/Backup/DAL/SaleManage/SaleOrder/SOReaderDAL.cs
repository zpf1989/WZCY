using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;

namespace OA.DAL
{
    public class SOReaderDAL : ISOReaderDAL
    {
        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.SOReader model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SOReader(");
            strSql.Append("SOReadID,SaleOrderID,ReaderID,ReadFlag)");
            strSql.Append(" values (");
            strSql.Append("@SOReadID,@SaleOrderID,@ReaderID,@ReadFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@SOReadID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReaderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReadFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.SOReadID;
            parameters[1].Value = model.SaleOrderID;
            parameters[2].Value = model.ReaderID;
            parameters[3].Value = model.ReadFlag;

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
        /// 删除分阅人
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool Delete(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SOReader ");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = SaleOrderID;

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
        /// 根据销售订单ID获取分阅人模型
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public List<Model.SOReader> GetModel(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SOReadID,SaleOrderID,ReaderID,ReadTime,ReadFlag,U.UserName from SOReader SOR ");
            strSql.Append(" inner join OA_User U on U.UserID = SOR.ReaderID ");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = SaleOrderID;
            
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.SOReader> list = new List<OA.Model.SOReader>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.SOReader model = new Model.SOReader();
                    model.SOReadID = dr["SOReadID"].ToString();
                    model.SaleOrderID = dr["SaleOrderID"].ToString();
                    model.ReaderID = dr["ReaderID"].ToString();
                    if (dr["ReadTime"] != null && dr["ReadTime"].ToString() != "")
                    {
                        model.ReadTime = DateTime.Parse(dr["ReadTime"].ToString());
                    }
                    model.ReadFlag = dr["ReadFlag"].ToString();
                    model.ReaderName = dr["UserName"].ToString();

                    list.Add(model);
                }
            }
            return list;
        }


    }
}
