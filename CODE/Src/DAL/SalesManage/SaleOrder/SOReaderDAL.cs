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
    public class SOReaderDAL : BaseDAL<SOReader>, ISOReaderDAL
    {
        public const string TableName = "SOReader";
        public override List<SOReader> GetEntitiesByPage(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override List<SOReader> GetEntitiesByPageForHelp(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override bool Save(params SOReader[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbsql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            SOReader entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //①销售订单分阅
                entity.SOReadID = Guid.NewGuid().ToString();

                //新增
                sbsql.AppendFormat("insert into {0}(SOReadID,SaleOrderID,ReaderID,ReadTime,ReadFlag)", TableName);
                sbsql.AppendFormat(" values (@SOReadID{0},@SaleOrderID{0},@ReaderID{0},@ReadTime{0},@ReadFlag{0});", i);

                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@SOReadID"+i, Value=entity.SOReadID},
                            new SqlParameter{ParameterName="@SaleOrderID"+i, Value=entity.SaleOrderID},
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


        public bool DeleteBySOIds(params string[] soIds)
        {
            if (soIds == null || soIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where SaleOrderID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < soIds.Length; i++)
            {
                sbSql.AppendFormat("@SaleOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@SaleOrderID" + i, Value = soIds[i] });
                if (i < soIds.Length - 1)
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
