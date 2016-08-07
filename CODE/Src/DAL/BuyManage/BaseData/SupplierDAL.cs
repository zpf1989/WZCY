using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GentleUtil.DB;
using OA.Model;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.GeneralClass.Logger;
using OA.IDAL;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class SupplierDAL : BaseDAL<Supplier>, ISupplierDAL
    {
        public const string TableName = "Supplier";
        public override List<Supplier> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "SupplierCode";
            }
            List<Supplier> classes = new List<Supplier>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(" {0} ", TableName),
                PK = "SupplierID",
                Fields = "SupplierID,SupplierCode,SupplierName,Contactor,Tel,Fax,Remark",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    classes.Add(new Supplier
                    {
                        SupplierID = row["SupplierID"].ToString(),
                        SupplierCode = row["SupplierCode"].ToString(),
                        SupplierName = row["SupplierName"].ToString(),
                        Contactor = row["Contactor"].ToString(),
                        Tel = row["Tel"].ToString(),
                        Fax = row["Fax"].ToString(),
                        Remark = row["Remark"].ToString()
                    });
                }
            }

            return classes;
        }

        public override bool Save(params Supplier[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Supplier entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.SupplierID))
                {
                    //新增
                    entity.SupplierID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(SupplierID,SupplierCode,SupplierName,Contactor,Tel,Fax,Remark)", TableName);
                    sbSql.AppendFormat(" values (@SupplierID{0},@SupplierCode{0},@SupplierName{0},@Contactor{0},@Tel{0},@Fax{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set SupplierCode=@SupplierCode{1},SupplierName=@SupplierName{1},Contactor=@Contactor{1},Tel=@Tel{1},Fax=@Fax{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where SupplierID=@SupplierID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@SupplierID"+i, Value=entity.SupplierID},
                            new SqlParameter{ParameterName="@SupplierCode"+i, Value=entity.SupplierCode},
                            new SqlParameter{ParameterName="@SupplierName"+i, Value=entity.SupplierName},
                            new SqlParameter{ParameterName="@Contactor"+i, Value=entity.Contactor},
                            new SqlParameter{ParameterName="@Tel"+i, Value=entity.Tel},
                            new SqlParameter{ParameterName="@Fax"+i, Value=entity.Fax},
                             new SqlParameter{ParameterName="@Remark"+i, Value=entity.Remark},
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
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public override bool Delete(params string[] classIds)
        {
            if (classIds == null || classIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where SupplierID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < classIds.Length; i++)
            {
                sbSql.AppendFormat("@SupplierID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@SupplierID" + i, Value = classIds[i] });
                if (i < classIds.Length - 1)
                {
                    sbSql.Append(",");
                }
            }
            sbSql.Append(");");
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }


        public bool Exists(params string[] codes)
        {
            if (codes == null || codes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and SupplierCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<Supplier> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }
    }
}
