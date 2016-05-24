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
    public class GMReaderDAL : IGMReaderDAL
    {
        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(GMReader model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GMReader(");
            strSql.Append("GMReadID,GoodsMovementID,ReaderID,ReadTime,ReadFlag)");
            strSql.Append(" values (");
            strSql.Append("@GMReadID,@GoodsMovementID,@ReaderID,@ReadTime,@ReadFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@GMReadID", SqlDbType.VarChar,36),
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36),
					new SqlParameter("@ReaderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReadTime", SqlDbType.DateTime),
					new SqlParameter("@ReadFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.GMReadID;
            parameters[1].Value = model.GoodsMovementID;
            parameters[2].Value = model.ReaderID;
            parameters[3].Value = model.ReadTime;
            parameters[4].Value = model.ReadFlag;

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
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public bool Delete(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GMReader ");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = GoodsMovementID;

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
        /// 根据货物移动ID获取分阅人模型
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public List<GMReader> GetModel(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GMReadID,GoodsMovementID,ReaderID,ReadTime,ReadFlag,U.UserName from GMReader GMR ");
            strSql.Append(" inner join OA_User U on U.UserID = GMR.ReaderID ");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)			};
            parameters[0].Value = GoodsMovementID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<GMReader> list = new List<GMReader>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMReader model = new GMReader();
                    if (dr["GMReadID"] != null && dr["GMReadID"].ToString() != "")
                    {
                        model.GMReadID = dr["GMReadID"].ToString();
                    }
                    if (dr["GoodsMovementID"] != null && dr["GoodsMovementID"].ToString() != "")
                    {
                        model.GoodsMovementID = dr["GoodsMovementID"].ToString();
                    }
                    if (dr["ReaderID"] != null && dr["ReaderID"].ToString() != "")
                    {
                        model.ReaderID = dr["ReaderID"].ToString();
                    }
                    if (dr["ReadTime"] != null && dr["ReadTime"].ToString() != "")
                    {
                        model.ReadTime = DateTime.Parse(dr["ReadTime"].ToString());
                    }
                    if (dr["ReadFlag"] != null && dr["ReadFlag"].ToString() != "")
                    {
                        model.ReadFlag = dr["ReadFlag"].ToString();
                    }
                    if (dr["UserName"] != null && dr["UserName"].ToString() != "")
                    {
                        model.ReaderName = dr["UserName"].ToString();
                    }
                    list.Add(model);
                }
            }
            return list;
        }

    }
}
