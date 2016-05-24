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
    public class ApplyPublicPowerDAL : IApplyPublicPowerDAL
    {
        /// <summary>
        /// 新增公章申请
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Add(OA.Model.ApplyPublicPower model, IList<ApplyPublicPowerItem> list)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ApplyPublicPower(");
            strSql.Append("AppID,DeptID,ApplyDate,Creator,CreateTime,FirstChecker,SecondChecker)");
            strSql.Append(" values (");
            strSql.Append("@AppID,@DeptID,@ApplyDate,@Creator,@CreateTime,@FirstChecker,@SecondChecker)");
            SqlParameter[] parameters = {
					new SqlParameter("@AppID", SqlDbType.VarChar,36),
					new SqlParameter("@DeptID", SqlDbType.VarChar,36),
					new SqlParameter("@ApplyDate", SqlDbType.Char,8),
					new SqlParameter("@Creator", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@SecondChecker", SqlDbType.VarChar,36)};
            parameters[0].Value = model.AppID;
            parameters[1].Value = model.DeptID;
            parameters[2].Value = model.ApplyDate;
            parameters[3].Value = model.Creator;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.FirstChecker;
            parameters[6].Value = model.SecondChecker;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                ApplyPublicPowerItemDAL itemBll = new ApplyPublicPowerItemDAL();

                for (int i = 0; i < list.Count; i++)
                {
                    ApplyPublicPowerItem item = list[i] as ApplyPublicPowerItem;
                    itemBll.Add(item);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return true;
        }
    }
}
