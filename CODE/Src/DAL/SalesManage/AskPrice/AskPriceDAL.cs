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
    public class AskPriceDAL : BaseDAL<AskPrice>, IAskPriceDAL
    {
        public const string TableName = "AskPrice";
        AskPriceItemDAL apItemDAL = new AskPriceItemDAL();
        public override List<AskPrice> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql, false);
        }
        public AskPrice GetAskPriceWithItems(string apId)
        {
            if (ValidateUtil.isBlank(apId))
            {
                return null;
            }
            var list = GetEntitiesByPage(pageEntity: new PageEntity(1, 20), whereSql: string.Format(" and APID='{0}'", apId), orderBySql: null, containItems: true);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public List<AskPrice> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool containItems = false)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "ap.APCode";
            }
            List<AskPrice> aps = new List<AskPrice>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@" {0} ap 
left JOIN {4} c on ap.ClientID=c.ClientID
left JOIN {4} pt on ap.PayTypeID=c.PayTypeID
left join {5} ue on ap.Editor=ue.UserID
left join {5} uc on ap.Creator=uc.UserID
left join {5} uf on ap.FirstChecker=uf.UserID",
TableName, ClientDAL.TableName, PayTypeDAL.TableName, UserManageDAL.TableName),
                PK = "ap.APID",
                Fields = "ap.*,c.ClientName Client_Name,pt.PayTypeName PayType_Name,uc.UserName Creator_Name,ue.UserName Editor_Name,uf.UserName FirstChecker_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                AskPrice ap = null;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ap = new AskPrice
                    {
                        APID = row["APID"].ToString(),
                        APCode = row["APCode"].ToString(),
                        APType = row["APType"].ToString(),
                        ClientID = row["ClientID"].ToString(),
                        Client_Name = row["Client_Name"].ToString(),
                        Client_Contact = row["Client_Contact"].ToString(),
                        Client_Tel = row["Client_Tel"].ToString(),
                        Client_Address = row["Client_Address"].ToString(),
                        PayTypeID = row["PayTypeID"].ToString(),
                        PayType_Name = row["PayType_Name"].ToString(),
                        TrackDescription = row["TrackDescription"].ToString(),
                        ClientSurvey = row["ClientSurvey"].ToString(),
                        APRemark = row["APRemark"].ToString(),
                        Creator = row["Creator"].ToString(),
                        Creator_Name = row["Creator_Name"].ToString(),
                        Editor = row["Editor"].ToString(),
                        Editor_Name = row["Editor_Name"].ToString(),
                        FirstChecker = row["FirstChecker"].ToString(),
                        FirstChecker_Name = row["FirstChecker_Name"].ToString(),
                        FirstCheckView = row["FirstCheckView"].ToString(),
                        SecondCheckerName = row["SecondCheckerName"].ToString(),
                        ReaderName = row["ReaderName"].ToString(),
                        State = row["State"].ToString(),
                    };
                    DateTime dtTemp;
                    if (DateTime.TryParse(row["AskDate"].ToString(), out dtTemp))
                    {
                        ap.AskDate = dtTemp;
                    }
                    if (DateTime.TryParse(row["CreateTime"].ToString(), out dtTemp))
                    {
                        ap.CreateTime = dtTemp;
                    }
                    if (DateTime.TryParse(row["EditTime"].ToString(), out dtTemp))
                    {
                        ap.EditTime = dtTemp;
                    }
                    if (DateTime.TryParse(row["FirstCheckTime"].ToString(), out dtTemp))
                    {
                        ap.FirstCheckTime = dtTemp;
                    }

                    aps.Add(ap);
                }
            }
            if (containItems && aps != null && aps.Count > 0)
            {
                foreach (var ap in aps)
                {
                    ap.Items = apItemDAL.GetAPItems(new PageEntity(1, 1000), ap.APID);
                }
            }

            return aps;
        }

        public override bool Save(params AskPrice[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbSqlAskPrice = new StringBuilder();
            List<Func<bool>> saveItemsFuncs = new List<Func<bool>>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            AskPrice entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                //①询价单
                if (ValidateUtil.isBlank(entity.APID))
                {
                    //新增，保存部分字段
                    entity.APID = Guid.NewGuid().ToString();
                    sbSqlAskPrice.AppendFormat("insert into {0}(APID,APCode,APType,AskDate,ClientID,PayTypeID,TrackDescription,ClientSurvey,APRemark,Creator,CreateTime,State)", TableName);
                    sbSqlAskPrice.AppendFormat(" values (@APID{0},@APCode{0},@APType{0},@AskDate{0},,@ClientID{0},@PayTypeID{0},@TrackDescription{0},@ClientSurvey{0},@APRemark{0},@Creator{0},@CreateTime{0},@State{0});", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Creator"+i, Value=entity.Creator},
                            new SqlParameter{ParameterName="@CreateTime"+i, Value=entity.CreateTime},
                            new SqlParameter{ParameterName="@State"+i, Value=entity.State},
                    });
                }
                else
                {
                    //修改，只更新部分字段
                    sbSqlAskPrice.AppendFormat("update {0} set APType=@APType{1},AskDate=@AskDate{1},ClientID=@ClientID{1},PayTypeID=@PayTypeID{1},TrackDescription=@TrackDescription{1},ClientSurvey=@ClientSurvey{1},APRemark=@APRemark{1},Editor=@Editor{1},EditTime=@EditTime{1},APRemark=@APRemark{1}", TableName, i);
                    sbSqlAskPrice.AppendFormat(" where APID=@APID{0};", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Editor"+i, Value=entity.Editor},
                            new SqlParameter{ParameterName="@EditTime"+i, Value=entity.EditTime},
                    });
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@APID"+i, Value=entity.APID},
                            new SqlParameter{ParameterName="@APCode"+i, Value=entity.APCode},
                            new SqlParameter{ParameterName="@APType"+i, Value=entity.APType},
                            new SqlParameter{ParameterName="@AskDate"+i, Value=entity.AskDate},
                            new SqlParameter{ParameterName="@ClientID"+i, Value=entity.ClientID},
                            new SqlParameter{ParameterName="@PayTypeID"+i, Value=entity.PayTypeID},
                            new SqlParameter{ParameterName="@TrackDescription"+i, Value=entity.TrackDescription},
                            new SqlParameter{ParameterName="@ClientSurvey"+i, Value=entity.ClientSurvey},
                            new SqlParameter{ParameterName="@APRemark"+i, Value=entity.APRemark},
                                            });
                //②询价单行
                if (entity.Items != null && entity.Items.Count > 0)
                {
                    foreach (var item in entity.Items)
                    {
                        item.APID = entity.APID;
                    }
                    saveItemsFuncs.Add(() =>
                    {
                        //调用AskPriceItemDAL.Save
                        return apItemDAL.Save(entity.Items.ToArray());
                    });
                }
            }
            //2、执行sql
            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSqlAskPrice.ToString(), sqlParams.ToArray());
                //保存询价单行
                if (saveItemsFuncs.Count > 0)
                {
                    foreach (var func in saveItemsFuncs)
                    {
                        func();
                    }
                }
                //提交
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public override bool Delete(params string[] apIDs)
        {
            if (apIDs == null || apIDs.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSqlSO = new StringBuilder();//删除询价单的sql
            sbSqlSO.AppendFormat("delete from {0} where APID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < apIDs.Length; i++)
            {
                sbSqlSO.AppendFormat("@APID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@APID" + i, Value = apIDs[i] });
                if (i < apIDs.Length - 1)
                {
                    sbSqlSO.Append(",");
                }
            }
            sbSqlSO.Append(");");
            //2、执行sql
            int rst = 0;

            try
            {
                //删除询价单
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSqlSO.ToString(), sqlParams.ToArray());
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
            return base.Exists(string.Format(" and APCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<AskPrice> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool SubmitToFirstChecker(string userId, params string[] apIds)
        {
            if (ValidateUtil.isBlank(userId) || apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set FirstChecker=@checker,State=@state where APID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@checker", Value = userId });
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = '2' });//提交
            for (int i = 0; i < apIds.Length; i++)
            {
                sbsql.AppendFormat("@APID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@APID" + i, Value = apIds[i] });
                if (i < apIds.Length - 1)
                {
                    sbsql.Append(",");
                }
            }
            sbsql.Append(");");
            //2、执行sql
            int rst = 0;

            try
            {
                //更新初审人
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbsql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public bool SubmitToSecondChecker(string userId, params string[] apIds)
        {
            if (ValidateUtil.isBlank(userId) || apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set SecondCheckerName=(select UserName from {1} where UserID=@checker),State=@state where APID in (", TableName, UserManageDAL.TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@checker", Value = userId });
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = '5' });//提交复审
            for (int i = 0; i < apIds.Length; i++)
            {
                sbsql.AppendFormat("@APID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@APID" + i, Value = apIds[i] });
                if (i < apIds.Length - 1)
                {
                    sbsql.Append(",");
                }
            }
            sbsql.Append(");");
            //2、执行sql
            int rst = 0;

            try
            {
                //更新初审人
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbsql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public bool SubmitToReader(string userId, params string[] apIds)
        {
            if (ValidateUtil.isBlank(userId) || apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set ReaderName=(select UserName from {1} where UserID=@reader) where APID in (", TableName, UserManageDAL.TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@reader", Value = userId });
            for (int i = 0; i < apIds.Length; i++)
            {
                sbsql.AppendFormat("@APID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@APID" + i, Value = apIds[i] });
                if (i < apIds.Length - 1)
                {
                    sbsql.Append(",");
                }
            }
            sbsql.Append(");");
            //2、执行sql
            int rst = 0;

            try
            {
                //更新初审人
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbsql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public bool FirstCheck(bool result, string checkView, params string[] apIds)
        {
            if (apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("update {0} set State=@state,FirstCheckView=@view,FirstCheckTime=@time where APID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = result ? "3" : "4" });
            sqlParams.Add(new SqlParameter { ParameterName = "@view", Value = checkView });
            sqlParams.Add(new SqlParameter { ParameterName = "@time", Value = DateTime.Now });
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



        public bool SecondCheck(bool checkResult, params string[] apIds)
        {
            if (apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("update {0} set State=@state where APID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = checkResult ? "6" : "7" });
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


        public bool Close(string[] apIds)
        {
            if (apIds == null || apIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("update {0} set SaleState=@state where APID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = "8" });
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
