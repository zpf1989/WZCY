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
    public class APReaderDAL : IAPReaderDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(APReader model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into APReader(");
            strSql.Append("APReadID,APID,ReaderID,ReadFlag)");
            strSql.Append(" values (");
            strSql.Append("@APReadID,@APID,@ReaderID,@ReadFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@APReadID", SqlDbType.VarChar,36),
					new SqlParameter("@APID", SqlDbType.VarChar,36),
					new SqlParameter("@ReaderID", SqlDbType.VarChar,36),
					new SqlParameter("@ReadFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.APReadID;
            parameters[1].Value = model.APID;
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
        /// 删除
        /// </summary>
        /// <param name="APID"></param>
        /// <returns></returns>
        public bool Delete(string APID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from APReader ");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APID", SqlDbType.VarChar,36)			};
            parameters[0].Value = APID;

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
        /// 根据询价单ID获取分阅人模型
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        public List<Model.APReader> GetModel(string apID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select APReadID,APID,ReaderID,ReadTime,ReadFlag,U.UserName from APReader APR ");
            strSql.Append(" inner join OA_User U on U.UserID = APR.ReaderID ");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APID", SqlDbType.VarChar,36)			};
            parameters[0].Value = apID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.APReader> list = new List<OA.Model.APReader>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.APReader model = new Model.APReader();
                    model.APReadID = dr["APReadID"].ToString();
                    model.APID = dr["APID"].ToString();
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
