using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GentleUtil.DB;
using System.Data.SqlClient;
using OA.IDAL;

namespace OA.DAL
{
    public class BillTypeDAL : IBillTypeDAL
    {
        /// <summary>
        /// 获取单据类型
        /// </summary>
        /// <param name="BillType"></param>
        /// <returns></returns>
        public DataSet GetList(string BillType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BillID,BillCode,BillName,BillType,Remark ");
            strSql.Append(" FROM BillType where BillType = @BillType");
            SqlParameter[] parameters = {
					new SqlParameter("@BillType", SqlDbType.VarChar,20)};
            parameters[0].Value = BillType;

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
    }
}
