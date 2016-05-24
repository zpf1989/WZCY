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
    public class BOASecondCheckDAL : IBOASecondCheckDAL
    {
        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(BOASecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BOASecondCheck(");
            strSql.Append("BOASecondCheckID,BuyApplyOrderID,SecondChecker,CheckFlag)");
            strSql.Append(" values (");
            strSql.Append("@BOASecondCheckID,@BuyApplyOrderID,@SecondChecker,@CheckFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@BOASecondCheckID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@SecondChecker", SqlDbType.VarChar,36),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.BOASecondCheckID;
            parameters[1].Value = model.BuyApplyOrderID;
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
        public bool Delete(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BOASecondCheck ");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)			};
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
        /// 根据采购订单ID获取复审人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public List<BOASecondCheck> GetModel(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BOASecondCheckID,BuyApplyOrderID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from BOASecondCheck BOC ");
            strSql.Append(" inner join OA_User U on U.UserID = BOC.SecondChecker ");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BuyApplyOrderID;
            
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.BOASecondCheck> list = new List<OA.Model.BOASecondCheck>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BOASecondCheck model = new BOASecondCheck();
                    model.BOASecondCheckID = dr["BOASecondCheckID"].ToString();
                    model.BuyApplyOrderID = dr["BuyApplyOrderID"].ToString();
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
        public bool Check(BOASecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BOASecondCheck set ");
            strSql.Append("SecondCheckTime=@SecondCheckTime,");
            strSql.Append("SecondCheckView=@SecondCheckView,");
            strSql.Append("CheckFlag=@CheckFlag");
            strSql.Append(" where BOASecondCheckID=@BOASecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SecondCheckTime", SqlDbType.DateTime),
					new SqlParameter("@SecondCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1),
					new SqlParameter("@BOASecondCheckID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.SecondCheckTime;
            parameters[1].Value = model.SecondCheckView;
            parameters[2].Value = model.CheckFlag;
            parameters[3].Value = model.BOASecondCheckID;

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
        public DataSet GetDataSet(string BOASecondCheckID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BOASecondCheckID,BuyApplyOrderID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from BOASecondCheck BOC ");
            strSql.Append(" inner join OA_User U on U.UserID = BOC.SecondChecker ");
            strSql.Append(" where BOASecondCheckID=@BOASecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BOASecondCheckID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BOASecondCheckID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            return ds;
        }
        /// <summary>
        /// 判断是否批准完成
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool IsCheck(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from BOASecondCheck where CheckFlag ='0' ");
            strSql.Append(" and BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = BuyApplyOrderID;

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
