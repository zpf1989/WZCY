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

namespace OA.DAL
{
    public class DepartmentDAL : BaseDAL<DepartmentInfo>, IDepartmentDAL
    {
        public const string TableName = "OA_Dept";

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

        public override bool Save(params DepartmentInfo[] entities)
        {
            throw new NotImplementedException("尚未实现");

        }

        public override bool Delete(params string[] ids)
        {
            throw new NotImplementedException("尚未实现");

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
