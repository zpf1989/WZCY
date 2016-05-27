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
            if (emps == null || emps.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            DemoEmployee emp = null;
            for (int i = 0; i < emps.Length; i++)
            {
                emp = emps[i];
                //1、组织sql
                sbSql.AppendFormat("insert into {0}(EmpId,EmpCode,EmpName,EmpGender,EmpAge,EmpBirthDay,EmpSalary,DeptId,DeptName)", TableName);
                sbSql.AppendFormat(" values (@EmpId{0},@EmpCode{0},@EmpName{0},@EmpGender{0},@EmpAge{0},@EmpBirthDay{0},@EmpSalary{0},@DeptId{0},@DeptName{0});");
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter("@EmpId"+i, SqlDbType.VarChar,36){Value=Guid.NewGuid().ToString()},
                            new SqlParameter("@EmpCode"+i, SqlDbType.VarChar,50){Value=emp.EmpCode},
                            new SqlParameter("@EmpName"+i, SqlDbType.VarChar,100){Value=emp.EmpName},
                            new SqlParameter("@EmpGender"+i, SqlDbType.Char,1){Value=emp.EmpGender},
                            new SqlParameter("@EmpAge"+i, SqlDbType.Int){Value=emp.EmpAge},
                            new SqlParameter("@EmpBirthDay"+i, SqlDbType.Date){Value=emp.EmpBirthDay},
                            new SqlParameter("@EmpSalary"+i, SqlDbType.Decimal){Value=emp.EmpSalary},
                            new SqlParameter("@DeptId"+i, SqlDbType.VarChar,36){Value=emp.DeptId},
                            new SqlParameter("@DeptName"+i, SqlDbType.VarChar,50){Value=emp.DeptName},
                                            });
            }
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //3、返回成功或失败的标志
            return rst > 0;
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
            if (emps == null || emps.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            DemoEmployee emp = null;
            for (int i = 0; i < emps.Length; i++)
            {
                emp = emps[i];
                //1、组织sql
                sbSql.AppendFormat("update {0} set EmpCode=@EmpCode{1},EmpName=@EmpName{1},EmpGender=@EmpGender{1},EmpAge=@EmpAge{1},EmpBirthDay=@EmpBirthDay{1},EmpSalary=@EmpSalary{1},DeptId=@DeptId{1},DeptName=@DeptName{1}", TableName, i);
                sbSql.AppendFormat(" where EmpId=@EmpId{0};", i);
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter("@EmpId"+i, SqlDbType.VarChar,36){Value=emp.EmpId},
                            new SqlParameter("@EmpCode"+i, SqlDbType.VarChar,50){Value=emp.EmpCode},
                            new SqlParameter("@EmpName"+i, SqlDbType.VarChar,100){Value=emp.EmpName},
                            new SqlParameter("@EmpGender"+i, SqlDbType.Char,1){Value=emp.EmpGender},
                            new SqlParameter("@EmpAge"+i, SqlDbType.Int){Value=emp.EmpAge},
                            new SqlParameter("@EmpBirthDay"+i, SqlDbType.Date){Value=emp.EmpBirthDay},
                            new SqlParameter("@EmpSalary"+i, SqlDbType.Decimal){Value=emp.EmpSalary},
                            new SqlParameter("@DeptId"+i, SqlDbType.VarChar,36){Value=emp.DeptId},
                            new SqlParameter("@DeptName"+i, SqlDbType.VarChar,50){Value=emp.DeptName},
                                            });
            }
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //3、返回成功或失败的标志
            return rst > 0;
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
