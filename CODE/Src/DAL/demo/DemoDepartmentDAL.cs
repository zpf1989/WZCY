using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.DAL
{
    public class DemoDepartmentDAL:IDemoDepartmentDAL
    {
        public List<DemoDepartment> GetAllDepartmentsForGridHelp()
        {
            //1、从数据库查询数据
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select * from OA_Function order by ParentFunID,FunID ");
            //return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            //2、DataSet转换成List<DemoDepartment>
            //3、返回List<DemoDepartment>

            //先用测试数据
            List<DemoDepartment> depts = new List<DemoDepartment>
            {
                new DemoDepartment{Id="01",Code="HR",Name="人事部"},
                new DemoDepartment{Id="02",Code="Dev",Name="研发部"},
                new DemoDepartment{Id="03",Code="Purchase",Name="采购部"},
            };
            return depts;
        }
    }
}
