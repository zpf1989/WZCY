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
        static List<DemoEmployee> DemoEmployees = new List<DemoEmployee>()
            {
                new DemoEmployee{EmpId="001",EmpCode="zs001",EmpName="张三001",EmpGender="1",EmpBirthDay=DateTime.Now,EmpAge=24,DeptId="01",DeptName="人事部",EmpSalary=4500.23M},
                new DemoEmployee{EmpId="002",EmpCode="ls002",EmpName="李四002",EmpGender="0",EmpBirthDay=DateTime.Now,EmpAge=34,DeptId="02",DeptName="研发部",EmpSalary=8500M},
                new DemoEmployee{EmpId="003",EmpCode="zs003",EmpName="张三003",EmpGender="1",EmpBirthDay=DateTime.Now,EmpAge=24,DeptId="01",DeptName="人事部",EmpSalary=4500.23M},
                new DemoEmployee{EmpId="004",EmpCode="ls004",EmpName="李四004",EmpGender="0",EmpBirthDay=DateTime.Now,EmpAge=34,DeptId="02",DeptName="研发部",EmpSalary=8500M},
                new DemoEmployee{EmpId="005",EmpCode="zs005",EmpName="张三005",EmpGender="1",EmpBirthDay=DateTime.Now,EmpAge=24,DeptId="01",DeptName="人事部",EmpSalary=4500.23M},
                new DemoEmployee{EmpId="006",EmpCode="ls006",EmpName="李四006",EmpGender="0",EmpBirthDay=DateTime.Now,EmpAge=34,DeptId="02",DeptName="研发部",EmpSalary=8500M},
                new DemoEmployee{EmpId="007",EmpCode="zs007",EmpName="张三007",EmpGender="1",EmpBirthDay=DateTime.Now,EmpAge=24,DeptId="01",DeptName="人事部",EmpSalary=4500.23M},
                new DemoEmployee{EmpId="008",EmpCode="ls008",EmpName="李四008",EmpGender="0",EmpBirthDay=DateTime.Now,EmpAge=34,DeptId="02",DeptName="研发部",EmpSalary=8500M},
                new DemoEmployee{EmpId="009",EmpCode="zs009",EmpName="张三009",EmpGender="1",EmpBirthDay=DateTime.Now,EmpAge=24,DeptId="01",DeptName="人事部",EmpSalary=4500.23M},
                new DemoEmployee{EmpId="010",EmpCode="ls010",EmpName="李四010",EmpGender="0",EmpBirthDay=DateTime.Now,EmpAge=34,DeptId="02",DeptName="研发部",EmpSalary=8500M},
                new DemoEmployee{EmpId="011",EmpCode="zs011",EmpName="张三011",EmpGender="1",EmpBirthDay=DateTime.Now,EmpAge=24,DeptId="01",DeptName="人事部",EmpSalary=4500.23M},
                new DemoEmployee{EmpId="012",EmpCode="ls012",EmpName="李四012",EmpGender="0",EmpBirthDay=DateTime.Now,EmpAge=34,DeptId="02",DeptName="研发部",EmpSalary=8500M},
            };
        public List<DemoEmployee> GetDemoEmployeesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetDemoEmployeesByPageFromDb(pageEntity, whereSql, orderBySql);

            //1、调用数据库存储过程，获取DataSet
            //2、DataSet—>List<DemoEmployee>
            //3、返回List<DemoEmployee>
            //测试数据
            if (pageEntity == null)
            {
                return null;
            }
            pageEntity.TotalRecords = DemoEmployees.Count;//更新总记录数
            return DemoEmployees.Skip(pageEntity.QueryRange.Start - 1).Take(pageEntity.PageSize).ToList();
        }


        public bool Add(params DemoEmployee[] emps)
        {
            //1、生成ID
            emps.ToList().ForEach(e => e.EmpId = Guid.NewGuid().ToString());
            //2、组织sql插入到数据库
            //测试数据
            DemoEmployees.AddRange(emps);
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
                DemoEmployees.Remove(DemoEmployees.Where(e => e.EmpId == id).FirstOrDefault());
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

        private List<DemoEmployee> GetDemoEmployeesByPageFromDb(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            List<DemoEmployee> emps = new List<DemoEmployee>();
            //组织查询参数
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("TableName", SqlDbType.NVarChar, 100) { Value = TableName });
            sqlParams.Add(new SqlParameter("Fields", SqlDbType.NVarChar, 1000) { Value = "*" });
            sqlParams.Add(new SqlParameter("OrderField", SqlDbType.NVarChar, 1000) { Value = orderBySql });
            sqlParams.Add(new SqlParameter("sqlWhere", SqlDbType.NVarChar, 1000) { Value = whereSql });
            sqlParams.Add(new SqlParameter("pageSize", SqlDbType.Int) { Value = pageEntity.PageSize });
            SqlParameter sqlParamPageIndex = new SqlParameter("pageIndex", SqlDbType.Int) { Value = pageEntity.PageIndex, Direction = ParameterDirection.InputOutput };
            sqlParams.Add(sqlParamPageIndex);
            SqlParameter sqlParamTotalPage = new SqlParameter("TotalPage", SqlDbType.Int) { Direction = ParameterDirection.Output };
            sqlParams.Add(sqlParamTotalPage);
            SqlParameter sqlParamTotalRecords = new SqlParameter("TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };
            sqlParams.Add(sqlParamTotalRecords);

            DataSet ds = DBAccess.ExecuteDataset(CommandType.StoredProcedure, "Proc_GetDataByPage", sqlParams.ToArray());
            pageEntity.PageIndex = Convert.ToInt32(sqlParamPageIndex.Value);
            pageEntity.TotalRecords = Convert.ToInt32(sqlParamTotalRecords.Value);
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
