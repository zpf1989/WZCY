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
    public class BOReaderDAL : IBOReaderDAL
    {
        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.BOReader model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BOReader(");
            strSql.Append("BOReadID,BuyOrderID,ReaderID,ReadFlag)");
            strSql.Append(" values (");
            strSql.Append("@BOReadID,@BuyOrderID,@ReaderID,@ReadFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@BOReadID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReaderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReadFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.BOReadID;
            parameters[1].Value = model.BuyOrderID;
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
        public bool Delete(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BOReader ");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyOrderID;

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
        public List<Model.BOReader> GetModel(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BOReadID,BuyOrderID,ReaderID,ReadTime,ReadFlag,U.UserName from BOReader BOR ");
            strSql.Append(" inner join OA_User U on U.UserID = BOR.ReaderID ");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BuyOrderID;
            
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.BOReader> list = new List<OA.Model.BOReader>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.BOReader model = new Model.BOReader();
                    model.BOReadID = dr["BOReadID"].ToString();
                    model.BuyOrderID = dr["BuyOrderID"].ToString();
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
