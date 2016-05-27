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

        public bool Delete(params string[] empIds)
        {
            if (empIds == null || empIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("delete from {0} where EmpId in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < empIds.Length; i++)
            {
                sbSql.AppendFormat("@EmpId{0}", i);
                sqlParams.Add(new SqlParameter("@EmpId" + i, SqlDbType.VarChar, 36) { Value = empIds[i] });
                if (i < empIds.Length - 1)
                {
                    sbSql.Append(",");
                }
            }
            sbSql.Append(")");
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

        public bool Save(params DemoEmployee[] emps)
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
                if (string.IsNullOrEmpty(emp.EmpId))
                {
                    //新增
                    emp.EmpId = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(EmpId,EmpCode,EmpName,EmpGender,EmpAge,EmpBirthDay,EmpSalary,DeptId,DeptName)", TableName);
                    sbSql.AppendFormat(" values (@EmpId{0},@EmpCode{0},@EmpName{0},@EmpGender{0},@EmpAge{0},@EmpBirthDay{0},@EmpSalary{0},@DeptId{0},@DeptName{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set EmpCode=@EmpCode{1},EmpName=@EmpName{1},EmpGender=@EmpGender{1},EmpAge=@EmpAge{1},EmpBirthDay=@EmpBirthDay{1},EmpSalary=@EmpSalary{1},DeptId=@DeptId{1},DeptName=@DeptName{1}", TableName, i);
                    sbSql.AppendFormat(" where EmpId=@EmpId{0};", i);
                }
                //不管新增或修改， 参数都一样
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
