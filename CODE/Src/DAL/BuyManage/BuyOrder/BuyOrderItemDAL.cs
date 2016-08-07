using System;
using System.Collections.Generic;
using System.Data;
using GentleUtil.DB;
using OA.Model;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.GeneralClass.Logger;
using System.Text;
using System.Data.SqlClient;
using OA.IDAL;

namespace OA.DAL
{
    public class BuyOrderItemDAL : BaseDAL<BuyOrderItem>, IBuyOrderItemDAL
    {
        public const string TableName = "BuyOrderItem";
        public override List<BuyOrderItem> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "bi.BuyOrderID";
            }
            List<BuyOrderItem> items = new List<BuyOrderItem>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@"{0} bi left join {1} m on bi.MaterialID=m.MaterialID left join {2} mu on bi.BuyUnitID=mu.UnitID", TableName, MaterialsDAL.TableName, MeasureUnitsDAL.TableName),
                PK = "bi.BuyOrderItemID",
                Fields = "bi.*,m.MaterialName Material_Name,mu.UnitName BuyUnit_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    items.Add(new BuyOrderItem
                    {
                        BuyOrderItemID = row["BuyOrderItemID"].ToString(),
                        BuyOrderID = row["BuyOrderID"].ToString(),
                        MaterialID = row["MaterialID"].ToString(),
                        Material_Name = row["Material_Name"].ToString(),
                        BuyQty = Convert.ToDecimal(row["BuyQty"]),
                        BuyCost = Convert.ToDecimal(row["BuyCost"]),
                        BuyUnitID = row["BuyUnitID"].ToString(),
                        BuyUnitID_Name = row["BuyUnit_Name"].ToString(),
                        Remark = row["Remark"].ToString(),
                    });
                }
            }

            return items;
        }

        public override bool Save(params BuyOrderItem[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            BuyOrderItem entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.BuyOrderItemID))
                {
                    //新增
                    entity.BuyOrderItemID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(BuyOrderItemID,BuyOrderID,MaterialID,BuyQty,BuyCost,BuyUnitID,Remark)", TableName);
                    sbSql.AppendFormat(" values (@BuyOrderItemID{0},@BuyOrderID{0},@MaterialID{0},@BuyQty{0},@BuyCost{0},@BuyUnitID{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set MaterialID=@MaterialID{1},BuyQty=@BuyQty{1},BuyCost=@BuyCost{1},BuyUnitID=@BuyUnitID{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where BuyOrderItemID=@BuyOrderItemID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@BuyOrderItemID"+i, Value=entity.BuyOrderItemID},
                            new SqlParameter{ParameterName="@BuyOrderID"+i, Value=entity.BuyOrderID},
                            new SqlParameter{ParameterName="@MaterialID"+i, Value=entity.MaterialID},
                            new SqlParameter{ParameterName="@BuyQty"+i, Value=entity.BuyQty},
                            new SqlParameter{ParameterName="@BuyCost"+i, Value=entity.BuyCost},
                            new SqlParameter{ParameterName="@BuyUnitID"+i, Value=entity.BuyUnitID},
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
            StringBuilder sbSql = new StringBuilder();//sql
            sbSql.AppendFormat("delete from {0} where BuyOrderItemID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < itemIds.Length; i++)
            {
                sbSql.AppendFormat("@BuyOrderItemID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BuyOrderItemID" + i, Value = itemIds[i] });
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

        public override List<BuyOrderItem> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public IList<BuyOrderItem> GetOrderItems(PageEntity pageEntity, string boId)
        {
            if (ValidateUtil.isBlank(boId))
            {
                return null;
            }
            return GetEntitiesByPage(pageEntity: pageEntity, whereSql: string.Format(" and BuyOrderID='{0}'", boId), orderBySql: null);
        }


        public bool DeleteByBOIds(params string[] boIds)
        {
            if (boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where BuyOrderItemID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < boIds.Length; i++)
            {
                sbSql.AppendFormat("@BuyOrderItemID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BuyOrderItemID" + i, Value = boIds[i] });
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
