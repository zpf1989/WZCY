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
    public class BOSecondCheckDAL : BaseDAL<BOSecondCheck>, IBOSecondCheckDAL
    {
        public const string TableName = "BOSecondCheck";
        public override List<BOSecondCheck> GetEntitiesByPage(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override List<BOSecondCheck> GetEntitiesByPageForHelp(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override bool Save(params BOSecondCheck[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbsql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            BOSecondCheck entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //①销售订单复审
                //新增
                entity.BOSecondCheckID = Guid.NewGuid().ToString();
                sbsql.AppendFormat("insert into {0}(BOSecondCheckID,BuyOrderID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag)", TableName);
                sbsql.AppendFormat(" values (@BOSecondCheckID{0},@BuyOrderID{0},@SecondChecker{0},@SecondCheckTime{0},@SecondCheckView{0},@CheckFlag{0});", i);

                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@BOSecondCheckID"+i, Value=entity.BOSecondCheckID},
                            new SqlParameter{ParameterName="@BuyOrderID"+i, Value=entity.BuyOrderID},
                            new SqlParameter{ParameterName="@SecondChecker"+i, Value=entity.SecondChecker},
                            new SqlParameter{ParameterName="@SecondCheckTime"+i, Value=entity.SecondCheckTime},
                            new SqlParameter{ParameterName="@SecondCheckView"+i, Value=entity.SecondCheckView},
                            new SqlParameter{ParameterName="@CheckFlag"+i, Value=entity.CheckFlag},
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
