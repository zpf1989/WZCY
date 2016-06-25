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
    public class ClientLevelDAL : BaseDAL<ClientLevel>, IClientLevelDAL
    {
        public const string TableName = "ClientLevel";
        public override List<ClientLevel> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "LevelMax desc";
            }
            List<ClientLevel> entities = new List<ClientLevel>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "LevelId",
                Fields = "LevelId,LevelName,LevelType,LevelMax,LevelMin",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entities.Add(new ClientLevel
                    {
                        LevelId = row["LevelID"].ToString(),
                        LevelName = row["LevelName"].ToString(),
                        LevelType = row["LevelType"].ToString(),
                        LevelMax = Convert.ToDecimal(row["LevelMax"]),
                        LevelMin = Convert.ToDecimal(row["LevelMin"]),
                    });
                }
            }

            return entities;
        }

        public override bool Save(params ClientLevel[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            ClientLevel entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.LevelId))
                {
                    //新增
                    entity.LevelId = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(LevelId,LevelName,LevelType,LevelMax,LevelMin)", TableName);
                    sbSql.AppendFormat(" values (@LevelId{0},@LevelName{0},@LevelType{0},@LevelMax{0},@LevelMin{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set LevelType=@LevelType{1},LevelMax=@LevelMax{1},LevelMin=@LevelMin{1}", TableName, i);
                    sbSql.AppendFormat(" where LevelId=@LevelId{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@LevelId"+i, Value=entity.LevelId},
                            new SqlParameter{ParameterName="@LevelName"+i, Value=entity.LevelName},
                            new SqlParameter{ParameterName="@LevelType"+i, Value=entity.LevelType},
                            new SqlParameter{ParameterName="@LevelMax"+i, Value=entity.LevelMax},
                            new SqlParameter{ParameterName="@LevelMin"+i, Value=entity.LevelMin},
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
            sbSql.AppendFormat("delete from {0} where LevelId in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < entityIds.Length; i++)
            {
                sbSql.AppendFormat("@LevelId{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@LevelId" + i, Value = entityIds[i] });
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

        public bool Exists(params string[] names)
        {
            if (names == null || names.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and LevelName in ('{0}')", string.Join("','", names)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<ClientLevel> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }
    }
}
