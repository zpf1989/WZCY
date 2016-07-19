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

namespace OA.DAL
{
    public class GMReaderDAL : BaseDAL<GMReader>, IGMReaderDAL
    {
        public const string TableName = "GMReader";
        public bool DeleteByGMIds(params string[] gmIds)
        {
            if (gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where GoodsMovementID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < gmIds.Length; i++)
            {
                sbSql.AppendFormat("@gmId{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@gmId" + i, Value = gmIds[i] });
                if (i < gmIds.Length - 1)
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

        public override List<GMReader> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override List<GMReader> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override bool Save(params GMReader[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbsql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            GMReader entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //①货物移动分阅
                entity.GMReadID = Guid.NewGuid().ToString();

                //新增
                sbsql.AppendFormat("insert into {0}(GMReadID,GoodsMovementID,ReaderID,ReadTime,ReadFlag)", TableName);
                sbsql.AppendFormat(" values (@id{0},@gmId{0},@ReaderID{0},@ReadTime{0},@ReadFlag{0});", i);

                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@id"+i, Value=entity.GMReadID},
                            new SqlParameter{ParameterName="@gmId"+i, Value=entity.GoodsMovementID},
                            new SqlParameter{ParameterName="@ReaderID"+i, Value=entity.ReaderID},
                            new SqlParameter{ParameterName="@ReadTime"+i, Value=entity.ReadTime},
                            new SqlParameter{ParameterName="@ReadFlag"+i, Value=entity.ReadFlag},
                                            });
            }
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbsql.ToString(), sqlParams.ToArray());
                return rst > 0;
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex);
                return false;
            }
        }

        public override bool Delete(params string[] ids)
        {
            throw new NotImplementedException();
        }

        protected override string GetTableName()
        {
            return TableName;
        }
    }
}
