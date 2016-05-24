using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;

namespace OA.DAL
{
    public class ApplyPublicPowerItemDAL
    {
        public bool Add(OA.Model.ApplyPublicPowerItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ApplyPublicPowerItem(");
            strSql.Append("AppItemID,AppID,ReceiverID)");
            strSql.Append(" values (");
            strSql.Append("@AppItemID,@AppID,@ReceiverID)");
            SqlParameter[] parameters = {
					new SqlParameter("@AppItemID", SqlDbType.VarChar,36),
					new SqlParameter("@AppID", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiverID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.AppItemID;
            parameters[1].Value = model.AppID;
            parameters[2].Value = model.ReceiverID;

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
    }
}
