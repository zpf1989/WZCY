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
using OA.GeneralClass.Extensions;

namespace OA.DAL
{
    public class GoodsMovementItemDAL : BaseDAL<GoodsMovementItem>, IGoodsMovementItemDAL
    {
        public const string TableName = "GoodsMovementItem";
        public IList<GoodsMovementItem> GetGMItems(PageEntity pageEntity, string gmId)
        {
            if (ValidateUtil.isBlank(gmId))
            {
                return null;
            }
            return GetEntitiesByPage(pageEntity: pageEntity, whereSql: string.Format(" and GoodsMovementID='{0}'", gmId), orderBySql: null);
        }

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

        public override List<GoodsMovementItem> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "gmi.GoodsMovementID";
            }
            List<GoodsMovementItem> items = new List<GoodsMovementItem>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@"{0} gmi left join {1} m on gmi.MaterialID = m.MaterialID left join {2} muRec on gmi.RecUnitID = muRec.UnitID left join {2} muIss on gmi.IssUnitID = muIss.UnitID", TableName, MaterialsDAL.TableName, MeasureUnitsDAL.TableName),
                PK = "gmi.GoodsMovementID",
                Fields = "gmi.*,m.MaterialName Material_Name,muRec.UnitName RecUnit_Name,muIss.UnitName IssUnit_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    items.Add(new GoodsMovementItem
                    {
                        GoodsMovementItemID = row["GoodsMovementItemID"].ToString(),
                        GoodsMovementID = row["GoodsMovementID"].ToString(),

                        MaterialID = row["MaterialID"].ToString(),
                        Material_Name = row["Material_Name"].ToString(),

                        TargInpQty = Convert.ToDecimal(row["TargInpQty"]),
                        ActInpQty = Convert.ToDecimal(row["ActInpQty"]),

                        RecUnitID = row["RecUnitID"].ToString(),
                        RecUnit_Name = row["RecUnit_Name"].ToString(),

                        TargOutQty = Convert.ToDecimal(row["TargOutQty"]),
                        ActOutQty = Convert.ToDecimal(row["ActOutQty"]),

                        IssUnitID = row["IssUnitID"].ToString(),
                        IssUnit_Name = row["IssUnit_Name"].ToString(),

                        InpPlaPrice = Convert.ToDecimal(row["InpPlaPrice"]),
                        InpPlaValue = Convert.ToDecimal(row["InpPlaValue"]),
                        InpActPrice = Convert.ToDecimal(row["InpActPrice"]),
                        InpActValue = Convert.ToDecimal(row["InpActValue"]),

                        OutPlaPrice = Convert.ToDecimal(row["OutPlaPrice"]),
                        OutPlaValue = Convert.ToDecimal(row["OutPlaValue"]),
                        OutActPrice = Convert.ToDecimal(row["OutActPrice"]),
                        OutActValue = Convert.ToDecimal(row["OutActValue"]),

                        ReturnQuantity = Convert.ToDecimal(row["ReturnQuantity"]),

                        Remark = row["Remark"].ToString(),
                    });
                }
            }

            return items;
        }

        public override List<GoodsMovementItem> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public override bool Save(params GoodsMovementItem[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            GoodsMovementItem entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.GoodsMovementItemID))
                {
                    //新增
                    entity.GoodsMovementItemID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(GoodsMovementItemID,GoodsMovementID,MaterialID,TargInpQty,ActInpQty,RecUnitID,TargOutQty,ActOutQty,IssUnitID,InpPlaPrice,InpPlaValue,InpActPrice,InpActValue,OutPlaPrice,OutPlaValue,OutActPrice,OutActValue,ReturnQuantity,Remark)", TableName);
                    sbSql.AppendFormat(" values (@gmItemID{0},@gmId{0},@MaterialID{0},@TargInpQty{0},@ActInpQty{0},@RecUnitID{0},@TargOutQty{0},@ActOutQty{0},@IssUnitID{0},@InpPlaPrice{0},@InpPlaValue{0},@InpActPrice{0},@InpActValue{0},@OutPlaPrice{0},@OutPlaValue{0},@OutActPrice{0},@OutActValue{0},@ReturnQuantity{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set MaterialID=@MaterialID{1},TargInpQty=@TargInpQty{1},ActInpQty=@ActInpQty{1},RecUnitID=@RecUnitID{1},TargOutQty=@TargOutQty{1},ActOutQty=@ActOutQty{1},IssUnitID=@IssUnitID{1},InpPlaPrice=@InpPlaPrice{1},InpPlaValue=@InpPlaValue{1},InpActPrice=@InpActPrice{1},InpActValue=@InpActValue{1},OutPlaPrice=@OutPlaPrice{1},OutPlaValue=@OutPlaValue{1},OutActPrice=@OutActPrice{1},OutActValue=@OutActValue{1},ReturnQuantity=@ReturnQuantity{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where GoodsMovementItemID=@GoodsMovementItemID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@gmItemID"+i, Value=entity.GoodsMovementItemID},
                            new SqlParameter{ParameterName="@gmId"+i, Value=entity.GoodsMovementID},
                            new SqlParameter{ParameterName="@MaterialID"+i, Value=entity.MaterialID},
                            new SqlParameter{ParameterName="@TargInpQty"+i, Value=entity.TargInpQty},
                            new SqlParameter{ParameterName="@ActInpQty"+i, Value=entity.ActInpQty},
                            new SqlParameter{ParameterName="@RecUnitID"+i, Value=entity.RecUnitID},
                            new SqlParameter{ParameterName="@TargOutQty"+i, Value=entity.TargOutQty},
                            new SqlParameter{ParameterName="@ActOutQty"+i, Value=entity.ActOutQty},
                            new SqlParameter{ParameterName="@IssUnitID"+i, Value=entity.IssUnitID},
                            new SqlParameter{ParameterName="@InpPlaPrice"+i, Value=entity.InpPlaPrice},
                            new SqlParameter{ParameterName="@InpPlaValue"+i, Value=entity.InpPlaValue},
                            new SqlParameter{ParameterName="@InpActPrice"+i, Value=entity.InpActPrice},
                            new SqlParameter{ParameterName="@InpActValue"+i, Value=entity.InpActValue},
                            new SqlParameter{ParameterName="@OutPlaPrice"+i, Value=entity.OutPlaPrice},
                            new SqlParameter{ParameterName="@OutPlaValue"+i, Value=entity.OutPlaValue},
                            new SqlParameter{ParameterName="@OutActPrice"+i, Value=entity.OutActPrice},
                            new SqlParameter{ParameterName="@OutActValue"+i, Value=entity.OutActValue},
                            new SqlParameter{ParameterName="@ReturnQuantity"+i, Value=entity.ReturnQuantity},
                            new SqlParameter{ParameterName="@Remark"+i, Value=entity.Remark},
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

        public override bool Delete(params string[] ids)
        {
            if (ids == null || ids.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//sql
            sbSql.AppendFormat("delete from {0} where GoodsMovementItemID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < ids.Length; i++)
            {
                sbSql.AppendFormat("@gmItemId{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@gmItemId" + i, Value = ids[i] });
                if (i < ids.Length - 1)
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
    }
}
