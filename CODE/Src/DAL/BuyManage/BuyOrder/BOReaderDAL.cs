using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using GentleUtil.DB;
using OA.IDAL;
using OA.Model;

namespace OA.DAL
{
    public class BOReaderDAL : BaseDAL<BOReader>, IBOReaderDAL
    {
        public const string TableName = "BOReader";
        public override List<BOReader> GetEntitiesByPage(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override List<BOReader> GetEntitiesByPageForHelp(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override bool Save(params BOReader[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbsql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            BOReader entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //①采购订单分阅
                entity.BOReadID = Guid.NewGuid().ToString();

                //新增
                sbsql.AppendFormat("insert into {0}(BOReadID,BuyOrderID,ReaderID,ReadTime,ReadFlag)", TableName);
                sbsql.AppendFormat(" values (@BOReadID{0},@BuyOrderID{0},@ReaderID{0},@ReadTime{0},@ReadFlag{0});", i);

                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@BOReadID"+i, Value=entity.BOReadID},
                            new SqlParameter{ParameterName="@BuyOrderID"+i, Value=entity.BuyOrderID},
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


        public bool DeleteByBOIds(params string[] boIds)
        {
            if (boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where BuyOrderID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < boIds.Length; i++)
            {
                sbSql.AppendFormat("@BuyOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BuyOrderID" + i, Value = boIds[i] });
                if (i < boIds.Length - 1)
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
    }
}
