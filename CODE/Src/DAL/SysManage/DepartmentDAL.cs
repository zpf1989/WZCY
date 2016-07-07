using GentleUtil.DB;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using System.Data.SqlClient;
using OA.GeneralClass.Logger;

namespace OA.DAL
{
    public class DepartmentDAL : BaseDAL<DepartmentInfo>, IDepartmentDAL
    {
        public const string TableName = "OA_Dept";
        ILogHelper<DepartmentDAL> logger = LoggerFactory.GetLogger<DepartmentDAL>();

        public override List<DepartmentInfo> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "d1.DeptCode";
            }
            List<DepartmentInfo> depts = new List<DepartmentInfo>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(" {0} d1 left join {1} d2 on d1.ParentDeptID=d2.DeptID ", TableName, TableName),
                PK = "d1.DeptID",
                Fields = "d1.DeptID,d1.DeptCode,d1.DeptName,d1.ParentDeptID,d1.Remark,d2.DeptName ParentDeptName",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    depts.Add(new DepartmentInfo
                    {
                        DeptID = row["DeptID"].ToString(),
                        DeptCode = row["DeptCode"].ToString(),
                        DeptName = row["DeptName"].ToString(),
                        ParentDeptID = row["ParentDeptID"].ToString(),
                        ParentDeptName = row["ParentDeptName"].ToString(),
                        Remark = row["Remark"].ToString()
                    });
                }
            }

            return depts;
        }

        public override List<DepartmentInfo> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="deptinfo"></param>
        /// <returns></returns>
        public override bool Save(params DepartmentInfo[] deptinfo)
        {
            if (deptinfo == null || deptinfo.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            DepartmentInfo dept = null;
            for (int i = 0; i < deptinfo.Length; i++)
            {
                dept = deptinfo[i];
                //1、组织sql
                if (ValidateUtil.isBlank(dept.DeptID))
                {
                    //新增
                    dept.DeptID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(DeptID,DeptCode,DeptName,ParentDeptID,Remark)", TableName);
                    sbSql.AppendFormat(" values (@DeptID{0},@DeptCode{0},@DeptName{0},@ParentDeptID{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set DeptCode=@DeptCode{1},DeptName=@DeptName{1},Remark=@Remark{1},ParentDeptID=@ParentDeptID{1}", TableName, i);
                    sbSql.AppendFormat(" where DeptID=@DeptID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter("@DeptID"+i, SqlDbType.VarChar,36){Value=dept.DeptID},
                            new SqlParameter("@DeptCode"+i, SqlDbType.VarChar,100){Value=dept.DeptCode},
                            new SqlParameter("@DeptName"+i, SqlDbType.VarChar,100){Value=dept.DeptName},
                            new SqlParameter("@ParentDeptID"+i, SqlDbType.VarChar,36){Value=dept.ParentDeptID},
                            new SqlParameter("@Remark"+i, SqlDbType.VarChar,1000){Value=dept.Remark},
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
                logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deptIds"></param>
        /// <returns></returns>
        public override bool Delete(params string[] deptIds)
        {
            if (deptIds == null || deptIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql2 = new StringBuilder();//删除用户的sql
            sbSql2.AppendFormat("delete from {0} where DeptID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < deptIds.Length; i++)
            {
                sbSql2.AppendFormat("@DeptID{0}", i);
                sqlParams.Add(new SqlParameter("@DeptID" + i, SqlDbType.VarChar, 36) { Value = deptIds[i] });
                if (i < deptIds.Length - 1)
                {
                    sbSql2.Append(",");
                }
            }
            sbSql2.Append(");");
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql2.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        protected override string GetTableName()
        {
            return TableName;
        }


        public bool Exists(params string[] deptCodes)
        {
            if (deptCodes == null || deptCodes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and DeptCode in ('{0}')", string.Join("','", deptCodes)));
        }

    }
}
