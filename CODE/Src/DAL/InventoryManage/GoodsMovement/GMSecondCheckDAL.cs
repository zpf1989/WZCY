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
    public class GMSecondCheckDAL : BaseDAL<GMSecondCheck>, IGMSecondCheckDAL
    {
        public const string TableName = "GMSecondCheck";
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

        public override List<GMSecondCheck> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override List<GMSecondCheck> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override bool Save(params GMSecondCheck[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbsql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            GMSecondCheck entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //①货物移动复审
                //新增
                entity.GMSecondCheckID = Guid.NewGuid().ToString();
                sbsql.AppendFormat("insert into {0}(GMSecondCheckID,GoodsMovementID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag)", TableName);
                sbsql.AppendFormat(" values (@id{0},@gmId{0},@SecondChecker{0},@SecondCheckTime{0},@SecondCheckView{0},@CheckFlag{0});", i);

                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@id"+i, Value=entity.GMSecondCheckID},
                            new SqlParameter{ParameterName="@gmId"+i, Value=entity.GoodsMovementID},
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
    }
}
