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
    public class GMSecondCheckDAL : IGMSecondCheckDAL
    {
        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(GMSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GMSecondCheck(");
            strSql.Append("GMSecondCheckID,GoodsMovementID,SecondChecker,SecondCheckView,SecondCheckTime,CheckFlag)");
            strSql.Append(" values (");
            strSql.Append("@GMSecondCheckID,@GoodsMovementID,@SecondChecker,@SecondCheckView,@SecondCheckTime,@CheckFlag)");
            SqlParameter[] parameters = {
					new SqlParameter("@GMSecondCheckID", SqlDbType.VarChar,36),
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36),
					new SqlParameter("@SecondChecker", SqlDbType.VarChar,36),
					new SqlParameter("@SecondCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@SecondCheckTime", SqlDbType.DateTime),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1)};
            parameters[0].Value = model.GMSecondCheckID;
            parameters[1].Value = model.GoodsMovementID;
            parameters[2].Value = model.SecondChecker;
            parameters[3].Value = model.SecondCheckView;
            parameters[4].Value = model.SecondCheckTime;
            parameters[5].Value = model.CheckFlag;

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
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public bool Delete(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GMSecondCheck ");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)			};
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
        /// 根据货物移动ID获取复审人
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public List<GMSecondCheck> GetModel(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GMSecondCheckID,GoodsMovementID,SecondChecker,SecondCheckView,SecondCheckTime,CheckFlag,U.UserName from GMSecondCheck GMC ");
            strSql.Append(" inner join OA_User U on U.UserID = GMC.SecondChecker ");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)			};
            parameters[0].Value = GoodsMovementID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            List<GMSecondCheck> list = new List<GMSecondCheck>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMSecondCheck model = new GMSecondCheck();
                    if (dr["GMSecondCheckID"] != null && dr["GMSecondCheckID"].ToString() != "")
                    {
                        model.GMSecondCheckID = dr["GMSecondCheckID"].ToString();
                    }
                    if (dr["GoodsMovementID"] != null && dr["GoodsMovementID"].ToString() != "")
                    {
                        model.GoodsMovementID = dr["GoodsMovementID"].ToString();
                    }
                    if (dr["SecondChecker"] != null && dr["SecondChecker"].ToString() != "")
                    {
                        model.SecondChecker = dr["SecondChecker"].ToString();
                    }
                    if (dr["SecondCheckView"] != null && dr["SecondCheckView"].ToString() != "")
                    {
                        model.SecondCheckView = dr["SecondCheckView"].ToString();
                    }
                    if (dr["SecondCheckTime"] != null && dr["SecondCheckTime"].ToString() != "")
                    {
                        model.SecondCheckTime = DateTime.Parse(dr["SecondCheckTime"].ToString());
                    }
                    if (dr["CheckFlag"] != null && dr["CheckFlag"].ToString() != "")
                    {
                        model.CheckFlag = dr["CheckFlag"].ToString();
                    }
                    if (dr["UserName"] != null && dr["UserName"].ToString() != "")
                    {
                        model.SecondCheckerName = dr["UserName"].ToString();
                    }
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
        public bool Check(GMSecondCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GMSecondCheck set ");
            strSql.Append("SecondCheckTime=@SecondCheckTime,");
            strSql.Append("SecondCheckView=@SecondCheckView,");
            strSql.Append("CheckFlag=@CheckFlag");
            strSql.Append(" where GMSecondCheckID=@GMSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SecondCheckTime", SqlDbType.DateTime),
					new SqlParameter("@SecondCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@CheckFlag", SqlDbType.Char,1),
					new SqlParameter("@GMSecondCheckID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.SecondCheckTime;
            parameters[1].Value = model.SecondCheckView;
            parameters[2].Value = model.CheckFlag;
            parameters[3].Value = model.GMSecondCheckID;

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
        /// <param name="GMSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string GMSecondCheckID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GMSecondCheckID,GoodsMovementID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag,U.UserName from GMSecondCheck GMC ");
            strSql.Append(" inner join OA_User U on U.UserID = GMC.SecondChecker ");
            strSql.Append(" where GMSecondCheckID=@GMSecondCheckID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GMSecondCheckID", SqlDbType.VarChar,36)			};
            parameters[0].Value = GMSecondCheckID;

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            return ds;
        }
        /// <summary>
        /// 判断是否批准完成
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public bool IsCheck(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from GMSecondCheck where CheckFlag ='0' ");
            strSql.Append(" and GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)			};
            parameters[0].Value = GoodsMovementID;

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
