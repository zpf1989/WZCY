using GentleUtil.DB;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OA.GeneralClass.Extensions;

namespace OA.DAL
{
    public class DemoDepartmentDAL : IDemoDepartmentDAL
    {
        const string TableName = "DemoDepartment";
        public List<DemoDepartment> GetAllDepartmentsForGridHelp()
        {
            List<DemoDepartment> depts = new List<DemoDepartment>();
            //1、从数据库查询数据
            string sql = string.Format("select * from {0}", TableName);
            //2、DataSet转换成List<DemoDepartment>
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, sql, null);
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    depts.Add(new DemoDepartment()
                    {
                        DeptId = row["DeptId"].ToString(),
                        DeptCode = row["DeptCode"].ToString(),
                        DeptName = row["DeptName"].ToString()
                    });
                }
            }
            return depts;
        }
    }
}
