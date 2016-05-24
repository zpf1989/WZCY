using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;
using OA.Model;

namespace OA.DAL
{
    public class FunctionDAL : IFunctionDAL
    {
        /// <summary>
        /// 获取功能的DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSetOfFun()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from OA_Function order by ParentFunID,FunID ");
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }


    }
}
