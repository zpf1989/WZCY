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
    public class APSecondCheckDAL : IAPSecondCheckDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(APSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into APSecondCheck(");
            strSql.Append("APSecondCheckID,APID,SecondChecker,CheckFlag)");
            strSql.Append(" values (");
            strSql.Append("@APSecondCheckID,@APID,@SecondChecker,@CheckFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@APSecondCheckID", SqlDbType.VarChar,36),
					new SqlParameter("@APID", SqlDbType.VarChar,36),
					new SqlParameter("@SecondChecker", SqlDbType.VarChar,36),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.APSecondCheckID;
            parameters[1].Value = model.APID;
            parameters[2].Value = model.SecondChecker;
            parameters[3].Value = model.CheckFlag;

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
            strSql.Append("delete from APSecondCheck ");
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
        /// 根据询价单ID获取复审人
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        public List<APSecondCheck> GetModel(string apID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select APSecondCheckID,APID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from APSecondCheck APSC ");
            strSql.Append(" inner join OA_User U on U.UserID = APSC.SecondChecker ");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APID", SqlDbType.VarChar,36)			};
            parameters[0].Value = apID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.APSecondCheck> list = new List<OA.Model.APSecondCheck>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.APSecondCheck model = new Model.APSecondCheck();
                    model.APSecondCheckID = dr["APSecondCheckID"].ToString();
                    model.APID = dr["APID"].ToString();
                    model.SecondChecker = dr["SecondChecker"].ToString();
                    if (dr["SecondCheckTime"] != null && dr["SecondCheckTime"].ToString() != "")
                    {
                        model.SecondCheckTime = DateTime.Parse(dr["SecondCheckTime"].ToString());
                    }
                    model.SecondCheckView = dr["SecondCheckView"].ToString();
                    model.CheckFlag = dr["CheckFlag"].ToString();
                    model.SecondCheckerName = dr["UserName"].ToString();

                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 批准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Check(APSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update APSecondCheck set ");
            strSql.Append("SecondCheckTime=@SecondCheckTime,");
            strSql.Append("SecondCheckView=@SecondCheckView,");
            strSql.Append("CheckFlag=@CheckFlag");
            strSql.Append(" where APSecondCheckID=@APSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SecondCheckTime", SqlDbType.DateTime),
					new SqlParameter("@SecondCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1),
					new SqlParameter("@APSecondCheckID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.SecondCheckTime;
            parameters[1].Value = model.SecondCheckView;
            parameters[2].Value = model.CheckFlag;
            parameters[3].Value = model.APSecondCheckID;

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
        /// 判断是否批准完成
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool IsCheck(string apID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from APSecondCheck where CheckFlag ='0' ");
            strSql.Append(" and APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APID", SqlDbType.VarChar,36)			};
            parameters[0].Value = apID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="APSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string APSecondCheckID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select APSecondCheckID,APID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from APSecondCheck APSC ");
            strSql.Append(" inner join OA_User U on U.UserID = APSC.SecondChecker ");
            strSql.Append(" where APSecondCheckID=@APSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APSecondCheckID", SqlDbType.VarChar,36)			};
            parameters[0].Value = APSecondCheckID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            return ds;
        }


    }
}
