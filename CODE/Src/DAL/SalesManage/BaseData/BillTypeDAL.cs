using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.IDAL;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.Model;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.GeneralClass.Logger;

namespace OA.DAL
{
    public class BillTypeDAL : BaseDAL<BillType>, IBillTypeDAL
    {
        public const string TableName = "BillType";
        public override List<BillType> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "BillCode";
            }
            List<BillType> types = new List<BillType>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "BillID",
                Fields = "BillID,BillCode,BillName,BillType,Remark",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    types.Add(new BillType
                    {
                        BillID = row["BillID"].ToString(),
                        BillCode = row["BillCode"].ToString(),
                        BillName = row["BillName"].ToString(),
                        Type = row["BillType"].ToString(),
                        Remark = row["Remark"].ToString()
                    });
                }
            }

            return types;
        }

        public override bool Save(params BillType[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            BillType entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.BillID))
                {
                    //新增
                    entity.BillID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(BillID,BillCode,BillName,BillType,Remark)", TableName);
                    sbSql.AppendFormat(" values (@BillID{0},@BillCode{0},@BillName{0},@BillType{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set BillName=@BillName{1},BillType=@BillType{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where BillID=@BillID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@BillID"+i, Value=entity.BillID},
                            new SqlParameter{ParameterName="@BillCode"+i, Value=entity.BillCode},
                            new SqlParameter{ParameterName="@BillName"+i, Value=entity.BillName},
                            new SqlParameter{ParameterName="@BillType"+i, Value=entity.Type},
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
            sbSql.AppendFormat("delete from {0} where BillID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < classIds.Length; i++)
            {
                sbSql.AppendFormat("@BillID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BillID" + i, Value = classIds[i] });
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
            return base.Exists(string.Format(" and BillCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<BillType> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }
    }
}
