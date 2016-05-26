using GentleUtil.DB;
using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using OA.GeneralClass.Extensions;

namespace OA.DAL
{
    public class DemoEmployeeDAL : IDemoEmployeeDAL
    {
        const string TableName = "DemoEmployee";

        public bool Add(params DemoEmployee[] emps)
        {
            //1、生成ID
            emps.ToList().ForEach(e => e.EmpId = Guid.NewGuid().ToString());
            //2、组织sql插入到数据库
            //测试数据
            //DemoEmployees.AddRange(emps);
            //3、返回成功或失败的标志
            return true;
            return false;
        }

        public bool Delete(params string[] empIds)
        {
            //1、组织sql
            //2、执行sql
            //3、返回成功或失败的标志

            //测试
            foreach (string id in empIds)
            {
                //DemoEmployees.Remove(DemoEmployees.Where(e => e.EmpId == id).FirstOrDefault());
            }
            return true;

            return false;
        }

        public bool Update(params DemoEmployee[] emps)
        {
            //1、组织sql
            //2、执行sql
            //3、返回成功或失败的标志

            //测试

            return false;
        }

        public List<DemoEmployee> GetDemoEmployeesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            List<DemoEmployee> emps = new List<DemoEmployee>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "EmpId",
                Fields = "*",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    emps.Add(new DemoEmployee
                    {
                        EmpId = row["EmpId"].ToString(),
                        EmpCode = row["EmpCode"].ToString(),
                        EmpName = row["EmpName"].ToString(),
                        EmpGender = row["EmpGender"].ToString(),
                        EmpAge = Convert.ToInt32(row["EmpAge"]),
                        EmpBirthDay = Convert.ToDateTime(row["EmpBirthDay"]),
                        EmpSalary = Convert.ToDecimal(row["EmpSalary"]),
                        DeptId = row["DeptId"].ToString(),
                        DeptName = row["DeptName"].ToString()
                    });
                }
            }

            return emps;
        }
    }
}
