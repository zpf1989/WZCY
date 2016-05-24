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
    public class BOAReaderDAL : IBOAReaderDAL
    {
        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.BOAReader model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BOAReader(");
            strSql.Append("BOAReadID,BuyApplyOrderID,ReaderID,ReadFlag)");
            strSql.Append(" values (");
            strSql.Append("@BOAReadID,@BuyApplyOrderID,@ReaderID,@ReadFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@BOAReadID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReaderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReadFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.BOAReadID;
            parameters[1].Value = model.BuyApplyOrderID;
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
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Delete(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BOAReader ");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyApplyOrderID;

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
        /// 根据采购订单ID获取分阅人模型
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public List<Model.BOAReader> GetModel(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BOAReadID,BuyApplyOrderID,ReaderID,ReadTime,ReadFlag,U.UserName from BOAReader BOR ");
            strSql.Append(" inner join OA_User U on U.UserID = BOR.ReaderID ");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BuyApplyOrderID;
            
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.BOAReader> list = new List<OA.Model.BOAReader>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.BOAReader model = new Model.BOAReader();
                    model.BOAReadID = dr["BOAReadID"].ToString();
                    model.BuyApplyOrderID = dr["BuyApplyOrderID"].ToString();
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
