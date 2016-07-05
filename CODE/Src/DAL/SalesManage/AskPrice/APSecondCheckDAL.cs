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
    public class APSecondCheckDAL : BaseDAL<APSecondCheck>, IAPSecondCheckDAL
    {
        public const string TableName = "APSecondCheck";
        public override List<APSecondCheck> GetEntitiesByPage(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override List<APSecondCheck> GetEntitiesByPageForHelp(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            throw new NotImplementedException();
        }

        public override bool Save(params APSecondCheck[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbsql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            APSecondCheck entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //①询价单复审
                //新增
                entity.APSecondCheckID = Guid.NewGuid().ToString();
                sbsql.AppendFormat("insert into {0}(APSecondCheckID,APID,SecondChecker,SecondCheckTime,SecondCheckView,CheckFlag)", TableName);
                sbsql.AppendFormat(" values (@APSecondCheckID{0},@APID{0},@SecondChecker{0},@SecondCheckTime{0},@SecondCheckView{0},@CheckFlag{0});", i);

                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@APSecondCheckID"+i, Value=entity.APSecondCheckID},
                            new SqlParameter{ParameterName="@APID"+i, Value=entity.APID},
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


        public bool DeleteByAPIds(params string[] apIds)
        {
            if (apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where APID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < apIds.Length; i++)
            {
                sbSql.AppendFormat("@APID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@APID" + i, Value = apIds[i] });
                if (i < apIds.Length - 1)
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
