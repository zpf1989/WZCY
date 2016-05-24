using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;

namespace OA.DAL
{
    public class AskPriceItemDAL
    {
        /// <summary>
        /// 删除分录
        /// </summary>
        /// <param name="APID"></param>
        /// <returns></returns>
        public bool Delete(string APID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AskPriceItem ");
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

    }
}
