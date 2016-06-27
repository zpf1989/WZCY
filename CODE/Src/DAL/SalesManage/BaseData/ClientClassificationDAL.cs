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
    public class ClientClassificationDAL : BaseDAL<ClientClassification>, IClientClassificationDAL
    {
        public const string TableName = "ClientClassification";
        public override List<ClientClassification> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "Amount desc";
            }
            List<ClientClassification> entities = new List<ClientClassification>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "Id",
                Fields = "Id,Amount,ClientName,LevelName,LevelTypeName",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entities.Add(new ClientClassification
                    {
                        Id = row["Id"].ToString(),
                        Amount = Convert.ToDecimal(row["Amount"]),
                        ClientName = row["ClientName"].ToString(),
                        LevelName = row["LevelName"].ToString(),
                        LevelTypeName = row["LevelTypeName"].ToString()
                    });
                }
            }

            return entities;
        }

        public override bool Save(params ClientClassification[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            ClientClassification entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.Id))
                {
                    //新增
                    entity.Id = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(Id,Amount,ClientName,LevelName,LevelTypeName)", TableName);
                    sbSql.AppendFormat(" values (@Id{0},@Amount{0},@ClientName{0},@LevelName{0},@LevelTypeName{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set ClientName=@ClientName{1},LevelName=@LevelName{1},LevelTypeName=@LevelTypeName{1}", TableName, i);
                    sbSql.AppendFormat(" where Id=@Id{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@Id"+i, Value=entity.Id},
                            new SqlParameter{ParameterName="@Amount"+i, Value=entity.Amount},
                            new SqlParameter{ParameterName="@ClientName"+i, Value=entity.ClientName},
                            new SqlParameter{ParameterName="@LevelName"+i, Value=entity.LevelName},
                            new SqlParameter{ParameterName="@LevelTypeName"+i, Value=entity.LevelTypeName}
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

        public override bool Delete(params string[] entityIds)
        {
            if (entityIds == null || entityIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where Id in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < entityIds.Length; i++)
            {
                sbSql.AppendFormat("@Id{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@Id" + i, Value = entityIds[i] });
                if (i < entityIds.Length - 1)
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
        public bool Exists(string[] clientNames)
        {
            if (clientNames == null || clientNames.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and ClientName in ('{0}')", string.Join("','", clientNames)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<ClientClassification> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Classify()
        {
            int rst = 0;
            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                //清空表当前数据
                Clear();
                //归集数据，插入表
                string sql = @"insert into ClientClassification(Id,Amount,ClientName,LevelName,LevelTypeName) 
select NEWID() Id,t1.*,cl.LevelName,case cl.LevelType when 'SOTotalAmmount' then '销售订单总额' end LevelTypeName
from (select SUM(so.SaleCost) Amount,c.ClientName
from SaleOrder so 
left join Client c on so.ClientID=c.ClientID
where so.ClientID is not null and so.ClientID <> '' 
GROUP by so.ClientID,c.ClientName) t1,ClientLevel cl
where t1.Amount >= cl.LevelMin and t1.Amount < cl.LevelMax";
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sql);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                base.Logger.LogError(ex);
                return false;
            }
            return rst > 0;
        }

        public bool Clear()
        {
            string sql = "delete from " + TableName;
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }
    }
}
