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
    public class SOSecondCheckDAL : ISOSecondCheckDAL
    {
        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.SOSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SOSecondCheck(");
            strSql.Append("SOSecondCheckID,SaleOrderID,SecondChecker,CheckFlag)");
            strSql.Append(" values (");
            strSql.Append("@SOSecondCheckID,@SaleOrderID,@SecondChecker,@CheckFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@SOSecondCheckID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@SecondChecker", SqlDbType.VarChar,36),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.SOSecondCheckID;
            parameters[1].Value = model.SaleOrderID;
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
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool Delete(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SOSecondCheck ");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)			};
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
        /// 根据销售订单ID获取复审人
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public List<SOSecondCheck> GetModel(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SOSecondCheckID,SaleOrderID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from SOSecondCheck SOC ");
            strSql.Append(" inner join OA_User U on U.UserID = SOC.SecondChecker ");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = SaleOrderID;
            
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<Model.SOSecondCheck> list = new List<OA.Model.SOSecondCheck>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Model.SOSecondCheck model = new Model.SOSecondCheck();
                    model.SOSecondCheckID = dr["SOSecondCheckID"].ToString();
                    model.SaleOrderID = dr["SaleOrderID"].ToString();
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
        public bool Check(SOSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SOSecondCheck set ");
            strSql.Append("SecondCheckTime=@SecondCheckTime,");
            strSql.Append("SecondCheckView=@SecondCheckView,");
            strSql.Append("CheckFlag=@CheckFlag");
            strSql.Append(" where SOSecondCheckID=@SOSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SecondCheckTime", SqlDbType.DateTime),
					new SqlParameter("@SecondCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1),
					new SqlParameter("@SOSecondCheckID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.SecondCheckTime;
            parameters[1].Value = model.SecondCheckView;
            parameters[2].Value = model.CheckFlag;
            parameters[3].Value = model.SOSecondCheckID;

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
        /// <param name="SOSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string SOSecondCheckID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SOSecondCheckID,SaleOrderID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from SOSecondCheck SOC ");
            strSql.Append(" inner join OA_User U on U.UserID = SOC.SecondChecker ");
            strSql.Append(" where SOSecondCheckID=@SOSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SOSecondCheckID", SqlDbType.VarChar,36)			};
            parameters[0].Value = SOSecondCheckID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            return ds;
        }
        /// <summary>
        /// 判断是否批准完成
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool IsCheck(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SOSecondCheck where CheckFlag ='0' ");
            strSql.Append(" and SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)			};
            parameters[0].Value = SaleOrderID;

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
