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
    public class AskPriceItemDAL : BaseDAL<AskPriceItem>, IAskPriceItemDAL
    {
        public const string TableName = "AskPriceItem";
        public override List<AskPriceItem> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "ai.APID";
            }
            List<AskPriceItem> items = new List<AskPriceItem>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@"{0} ai left join {1} m on ai.MaterialID=m.MaterialID left join {2} mu on ai.UnitID=mu.UnitID", TableName, MaterialsDAL.TableName, MeasureUnitsDAL.TableName),
                PK = "ai.AskPriceItemID",
                Fields = "ai.*,m.MaterialCode Material_Code, m.MaterialName Material_Name,m.Specs Material_Specs,mu.UnitName Unit_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    items.Add(new AskPriceItem
                    {
                        APItemID = row["APItemID"].ToString(),
                        APID = row["APID"].ToString(),
                        MaterialID = row["MaterialID"].ToString(),
                        Material_Code = row["Material_Code"].ToString(),
                        Material_Name = row["Material_Name"].ToString(),
                        Material_Specs = row["Material_Specs"].ToString(),
                        Routing = row["Routing"].ToString(),
                        PlanPrice = Convert.ToDecimal(row["PlanPrice"]),
                        Qty = Convert.ToDecimal(row["Qty"]),
                        ActualPrice = Convert.ToDecimal(row["ActualPrice"]),
                        UnitID = row["UnitID"].ToString(),
                        Unit_Name = row["Unit_Name"].ToString(),
                        IsTax = row["IsTax"].ToString(),
                        IsShipping = row["IsShipping"].ToString(),
                    });
                }
            }

            return items;
        }

        public override bool Save(params AskPriceItem[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            AskPriceItem entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.APItemID))
                {
                    //新增
                    entity.APItemID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(APItemID,APID,MaterialID,UnitID,Routing,PlanPrice,Qty,ActualPrice,IsTax,IsShipping)", TableName);
                    sbSql.AppendFormat(" values (@APItemID{0},@APID{0},@MaterialID{0},@UnitID{0},@Routing{0},@PlanPrice{0},@Qty{0},@ActualPrice{0},@IsTax{0},@IsShipping{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set MaterialID=@MaterialID{1},Routing=@Routing{1},PlanPrice=@PlanPrice{1},Qty=@Qty{1},UnitID=@UnitID{1},ActualPrice=@ActualPrice{1},IsTax=@IsTax{1},IsShipping=@IsShipping{1}", TableName, i);
                    sbSql.AppendFormat(" where APItemID=@APItemID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@APItemID"+i, Value=entity.APItemID},
                            new SqlParameter{ParameterName="@APID"+i, Value=entity.APID},
                            new SqlParameter{ParameterName="@MaterialID"+i, Value=entity.MaterialID},
                            new SqlParameter{ParameterName="@Routing"+i, Value=entity.Routing},
                            new SqlParameter{ParameterName="@PlanPrice"+i, Value=entity.PlanPrice},
                            new SqlParameter{ParameterName="@Qty"+i, Value=entity.Qty},
                            new SqlParameter{ParameterName="@UnitID"+i, Value=entity.UnitID},
                            new SqlParameter{ParameterName="@ActualPrice"+i, Value=entity.ActualPrice},
                            new SqlParameter{ParameterName="@IsTax"+i, Value=entity.IsTax},
                            new SqlParameter{ParameterName="@IsShipping"+i, Value=entity.IsShipping},
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

        public override bool Delete(params string[] itemIds)
        {
            if (itemIds == null || itemIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//sql
            sbSql.AppendFormat("delete from {0} where APItemID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < itemIds.Length; i++)
            {
                sbSql.AppendFormat("@APItemID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@APItemID" + i, Value = itemIds[i] });
                if (i < itemIds.Length - 1)
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

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<AskPriceItem> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public IList<AskPriceItem> GetAPItems(PageEntity pageEntity, string apId)
        {
            if (ValidateUtil.isBlank(apId))
            {
                return null;
            }
            return GetEntitiesByPage(pageEntity: pageEntity, whereSql: string.Format(" and APID='{0}'", apId), orderBySql: null);
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
