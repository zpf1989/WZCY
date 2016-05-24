using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;
using OA.Model;

namespace OA.DAL
{
    public class BOSecondCheckDAL : IBOSecondCheckDAL
    {
        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(BOSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BOSecondCheck(");
            strSql.Append("BOSecondCheckID,BuyOrderID,SecondChecker,CheckFlag)");
            strSql.Append(" values (");
            strSql.Append("@BOSecondCheckID,@BuyOrderID,@SecondChecker,@CheckFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@BOSecondCheckID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@SecondChecker", SqlDbType.VarChar,36),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.BOSecondCheckID;
            parameters[1].Value = model.BuyOrderID;
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
        /// 删除复审人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Delete(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BOSecondCheck ");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)			};
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
        /// 根据采购订单ID获取复审人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public List<BOSecondCheck> GetModel(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BOSecondCheckID,BuyOrderID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from BOSecondCheck BOC ");
            strSql.Append(" inner join OA_User U on U.UserID = BOC.SecondChecker ");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BuyOrderID;
            
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.BOSecondCheck> list = new List<OA.Model.BOSecondCheck>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BOSecondCheck model = new BOSecondCheck();
                    model.BOSecondCheckID = dr["BOSecondCheckID"].ToString();
                    model.BuyOrderID = dr["BuyOrderID"].ToString();
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
        public bool Check(BOSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BOSecondCheck set ");
            strSql.Append("SecondCheckTime=@SecondCheckTime,");
            strSql.Append("SecondCheckView=@SecondCheckView,");
            strSql.Append("CheckFlag=@CheckFlag");
            strSql.Append(" where BOSecondCheckID=@BOSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SecondCheckTime", SqlDbType.DateTime),
					new SqlParameter("@SecondCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1),
					new SqlParameter("@BOSecondCheckID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.SecondCheckTime;
            parameters[1].Value = model.SecondCheckView;
            parameters[2].Value = model.CheckFlag;
            parameters[3].Value = model.BOSecondCheckID;

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
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="BOSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string BOSecondCheckID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BOSecondCheckID,BuyOrderID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from BOSecondCheck BOC ");
            strSql.Append(" inner join OA_User U on U.UserID = BOC.SecondChecker ");
            strSql.Append(" where BOSecondCheckID=@BOSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BOSecondCheckID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BOSecondCheckID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            return ds;
        }
        /// <summary>
        /// 判断是否批准完成
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool IsCheck(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from BOSecondCheck where CheckFlag ='0' ");
            strSql.Append(" and BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BuyOrderID;

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

    }
}
