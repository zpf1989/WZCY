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
    public class DepartmentDAL : IDepartmentDAL
    {
        public const string TableName = "OA_Dept";
        public List<DepartmentInfo> GetAllDepartmentsForGridHelp()
        {
            List<DepartmentInfo> depts = new List<DepartmentInfo>();
            //1、从数据库查询数据
            string sql = string.Format("select * from {0}", TableName);
            //2、DataSet转换成List<DepartmentInfo>
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, sql, null);
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    depts.Add(new DepartmentInfo()
                    {
                        DeptID = row["DeptId"].ToString(),
                        DeptCode = row["DeptCode"].ToString(),
                        DeptName = row["DeptName"].ToString(),
                        Remark = row["Remark"].ToString(),
                    });
                }
            }
            return depts;
        }
    }
}
