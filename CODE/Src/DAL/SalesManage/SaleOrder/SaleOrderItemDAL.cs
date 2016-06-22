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
    public class SaleOrderItemDAL : BaseDAL<SaleOrderItem>, ISaleOrderItemDAL
    {
        public const string TableName = "SaleOrderItem";
        public override List<SaleOrderItem> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "si.SaleOrderID";
            }
            List<SaleOrderItem> items = new List<SaleOrderItem>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@"{0} si left join {1} m on si.MaterialID=m.MaterialID left join {2} mu on si.PrimaryUnitID=mu.UnitID", TableName, MaterialsDAL.TableName, MeasureUnitsDAL.TableName),
                PK = "si.SaleOrderItemID",
                Fields = "si.*,m.MaterialName Material_Name,mu.UnitName PrimaryUnit_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    items.Add(new SaleOrderItem
                    {
                        SaleOrderItemID = row["SaleOrderItemID"].ToString(),
                        SaleOrderID = row["SaleOrderID"].ToString(),
                        MaterialID = row["MaterialID"].ToString(),
                        Material_Name = row["Material_Name"].ToString(),
                        PlanQty = Convert.ToDecimal(row["PlanQty"]),
                        ActualQty = Convert.ToDecimal(row["ActualQty"]),
                        PlanCost = Convert.ToDecimal(row["PlanCost"]),
                        PrimaryUnitID = row["PrimaryUnitID"].ToString(),
                        PrimaryUnit_Name = row["PrimaryUnit_Name"].ToString(),
                        Remark = row["Remark"].ToString(),
                    });
                }
            }

            return items;
        }

        public override bool Save(params SaleOrderItem[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            SaleOrderItem entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.SaleOrderItemID))
                {
                    //新增
                    entity.SaleOrderItemID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(SaleOrderItemID,SaleOrderID,MaterialID,PlanQty,ActualQty,PlanCost,PrimaryUnitID,Remark)", TableName);
                    sbSql.AppendFormat(" values (@SaleOrderItemID{0},@SaleOrderID{0},@MaterialID{0},@PlanQty{0},@ActualQty{0},@PlanCost{0},@PrimaryUnitID{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set MaterialID=@MaterialID{1},PlanQty=@PlanQty{1},ActualQty=@ActualQty{1},PlanCost=@PlanCost{1},PrimaryUnitID=@PrimaryUnitID{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where SaleOrderItemID=@SaleOrderItemID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@SaleOrderItemID"+i, Value=entity.SaleOrderItemID},
                            new SqlParameter{ParameterName="@SaleOrderID"+i, Value=entity.SaleOrderID},
                            new SqlParameter{ParameterName="@MaterialID"+i, Value=entity.MaterialID},
                            new SqlParameter{ParameterName="@PlanQty"+i, Value=entity.PlanQty},
                            new SqlParameter{ParameterName="@ActualQty"+i, Value=entity.ActualQty},
                            new SqlParameter{ParameterName="@PlanCost"+i, Value=entity.PlanCost},
                            new SqlParameter{ParameterName="@PrimaryUnitID"+i, Value=entity.PrimaryUnitID},
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

        public override bool Delete(params string[] itemIds)
        {
            if (itemIds == null || itemIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where SaleOrderItemID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < itemIds.Length; i++)
            {
                sbSql.AppendFormat("@SaleOrderItemID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@SaleOrderItemID" + i, Value = itemIds[i] });
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

        public override List<SaleOrderItem> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public IList<SaleOrderItem> GetOrderItems(PageEntity pageEntity, string soId)
        {
            if (ValidateUtil.isBlank(soId))
            {
                return null;
            }
            return GetEntitiesByPage(pageEntity: pageEntity, whereSql: string.Format(" and SaleOrderID='{0}'", soId), orderBySql: null);
        }


        public bool DeleteBySOIds(params string[] soIds)
        {
            if (soIds == null || soIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
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
