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
    public class SaleOrderDAL : BaseDAL<SaleOrder>, ISaleOrderDAL
    {
        public const string TableName = "SaleOrder";
        SaleOrderItemDAL soItemDAL = new SaleOrderItemDAL();
        public override List<SaleOrder> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql, false);
        }
        public SaleOrder GetSaleOrderWithItems(string orderId)
        {
            if (ValidateUtil.isBlank(orderId))
            {
                return null;
            }
            var list = GetEntitiesByPage(pageEntity: new PageEntity(1, 20), whereSql: string.Format(" and SaleOrderID='{0}'", orderId), orderBySql: null, containItems: true);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public List<SaleOrder> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool containItems = false)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "s.SaleOrderCode";
            }
            List<SaleOrder> orders = new List<SaleOrder>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@" {0} s 
left join {1} t on s.BillTypeID=t.BillID
LEFT JOIN {2} m on s.MaterialID=m.MaterialID
LEFT JOIN {3} mu on s.SaleUnitID=mu.UnitID
left JOIN {4} c on s.ClientID=c.ClientID
left join {5} ue on s.Creator=ue.UserID
left join {5} uc on s.Creator=uc.UserID
left join {5} uf on s.FirstChecker=uf.UserID",
TableName, BillTypeDAL.TableName, MaterialsDAL.TableName, MeasureUnitsDAL.TableName, ClientDAL.TableName, UserManageDAL.TableName),
                PK = "s.SaleOrderID",
                Fields = "s.*,t.BillName BillType_Name,m.MaterialName Material_Name,mu.UnitName SaleUnit_Name,c.ClientName Client_Name,uc.UserName Creator_Name,ue.UserName Editor_Name,uf.UserName FirstChecker_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                SaleOrder order = null;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    order = new SaleOrder
                    {
                        SaleOrderID = row["SaleOrderID"].ToString(),
                        SaleOrderCode = row["SaleOrderCode"].ToString(),
                        BillTypeID = row["BillTypeID"].ToString(),
                        BillType_Name = row["BillType_Name"].ToString(),
                        MaterialID = row["MaterialID"].ToString(),
                        Material_Name = row["Material_Name"].ToString(),
                        SaleUnitID = row["SaleUnitID"].ToString(),
                        SaleUnit_Name = row["SaleUnit_Name"].ToString(),
                        ClientID = row["ClientID"].ToString(),
                        Client_Name = row["Client_Name"].ToString(),
                        SaleDate = row["SaleDate"].ToString(),
                        SaleQty = Convert.ToDecimal(row["SaleQty"]),
                        SalePrice = Convert.ToDecimal(row["SalePrice"]),
                        SaleCost = Convert.ToDecimal(row["SaleCost"]),
                        FinishDate = row["FinishDate"].ToString(),
                        Creator = row["Creator"].ToString(),
                        Creator_Name = row["Creator_Name"].ToString(),
                        Editor = row["Editor"].ToString(),
                        Editor_Name = row["Editor_Name"].ToString(),
                        FirstChecker = row["FirstChecker"].ToString(),
                        FirstChecker_Name = row["FirstChecker_Name"].ToString(),
                        FirstCheckView = row["FirstCheckView"].ToString(),
                        RoutingID = row["RoutingID"].ToString(),
                        Routing = row["Routing"].ToString(),
                        SaleState = row["SaleState"].ToString(),
                        Remark = row["Remark"].ToString(),
                        SecondCheckerName = row["SecondCheckerName"].ToString(),
                        ReaderName = row["ReaderName"].ToString()
                    };
                    DateTime dtTemp;
                    if (DateTime.TryParse(row["CreateTime"].ToString(), out dtTemp))
                    {
                        order.CreateTime = dtTemp;
                    }
                    if (DateTime.TryParse(row["EditTime"].ToString(), out dtTemp))
                    {
                        order.EditTime = dtTemp;
                    }
                    if (DateTime.TryParse(row["FirstCheckTime"].ToString(), out dtTemp))
                    {
                        order.FirstCheckTime = dtTemp;
                    }

                    orders.Add(order);
                }
            }
            if (containItems && orders != null && orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    order.Items = soItemDAL.GetOrderItems(new PageEntity(1, 1000), order.SaleOrderID);
                }
            }

            return orders;
        }

        public override bool Save(params SaleOrder[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbSqlSaleOrder = new StringBuilder();
            List<Func<bool>> saveItemsFuncs = new List<Func<bool>>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            SaleOrder entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                //①销售订单
                if (ValidateUtil.isBlank(entity.SaleOrderID))
                {
                    //新增，保存部分字段
                    entity.SaleOrderID = Guid.NewGuid().ToString();
                    sbSqlSaleOrder.AppendFormat("insert into {0}(SaleOrderID,SaleOrderCode,BillTypeID,MaterialID,SaleUnitID,ClientID,SaleDate,SaleQty,SalePrice,SaleCost,FinishDate,Creator,CreateTime,SaleState,Remark,Routing)", TableName);
                    sbSqlSaleOrder.AppendFormat(" values (@SaleOrderID{0},@SaleOrderCode{0},@BillTypeID{0},@MaterialID{0},@SaleUnitID{0},@ClientID{0},@SaleDate{0},@SaleQty{0},@SalePrice{0},@SaleCost{0},@FinishDate{0},@Creator{0},@CreateTime{0},@SaleState{0},@Remark{0},@Routing{0});", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Creator"+i, Value=entity.Creator},
                            new SqlParameter{ParameterName="@CreateTime"+i, Value=entity.CreateTime},
                            new SqlParameter{ParameterName="@SaleState"+i, Value=entity.SaleState},
                    });
                }
                else
                {
                    //修改，只更新部分字段
                    sbSqlSaleOrder.AppendFormat("update {0} set BillTypeID=@BillTypeID{1},MaterialID=@MaterialID{1},SaleUnitID=@SaleUnitID{1},ClientID=@ClientID{1},SaleDate=@SaleDate{1},SaleQty=@SaleQty{1},SalePrice=@SalePrice{1},SaleCost=@SaleCost{1},FinishDate=@FinishDate{1},Editor=@Editor{1},EditTime=@EditTime{1},Remark=@Remark{1},Routing=@Routing{1}", TableName, i);
                    sbSqlSaleOrder.AppendFormat(" where SaleOrderID=@SaleOrderID{0};", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Editor"+i, Value=entity.Editor},
                            new SqlParameter{ParameterName="@EditTime"+i, Value=entity.EditTime},
                    });
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@SaleOrderID"+i, Value=entity.SaleOrderID},
                            new SqlParameter{ParameterName="@SaleOrderCode"+i, Value=entity.SaleOrderCode},
                            new SqlParameter{ParameterName="@BillTypeID"+i, Value=entity.BillTypeID},
                            new SqlParameter{ParameterName="@MaterialID"+i, Value=entity.MaterialID},
                            new SqlParameter{ParameterName="@SaleUnitID"+i, Value=entity.SaleUnitID},
                            new SqlParameter{ParameterName="@ClientID"+i, Value=entity.ClientID},
                            new SqlParameter{ParameterName="@SaleDate"+i, Value=entity.SaleDate.Replace("-","")},
                            new SqlParameter{ParameterName="@SaleQty"+i, Value=entity.SaleQty},
                            new SqlParameter{ParameterName="@SalePrice"+i, Value=entity.SalePrice},
                            new SqlParameter{ParameterName="@SaleCost"+i, Value=entity.SaleCost},
                            new SqlParameter{ParameterName="@FinishDate"+i, Value=entity.FinishDate.Replace("-","")},
                            new SqlParameter{ParameterName="@Remark"+i, Value=entity.Remark},
                            new SqlParameter{ParameterName="@Routing"+i, Value=entity.Routing}
                                            });
                //②销售订单行
                if (entity.Items != null && entity.Items.Count > 0)
                {
                    foreach (var item in entity.Items)
                    {
                        item.SaleOrderID = entity.SaleOrderID;
                    }
                    saveItemsFuncs.Add(() =>
                    {
                        //调用SalesOrderItemDAL.Save
                        return soItemDAL.Save(entity.Items.ToArray());
                    });
                }
            }
            //2、执行sql
            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSqlSaleOrder.ToString(), sqlParams.ToArray());
                //保存销售订单行
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

        public override bool Delete(params string[] saleOrderIds)
        {
            if (saleOrderIds == null || saleOrderIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSqlSO = new StringBuilder();//删除销售订单的sql
            sbSqlSO.AppendFormat("delete from {0} where SaleOrderID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < saleOrderIds.Length; i++)
            {
                sbSqlSO.AppendFormat("@SaleOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@SaleOrderID" + i, Value = saleOrderIds[i] });
                if (i < saleOrderIds.Length - 1)
                {
                    sbSqlSO.Append(",");
                }
            }
            sbSqlSO.Append(");");
            //2、执行sql
            int rst = 0;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                //先删除订单行
                bool result = soItemDAL.DeleteBySOIds(saleOrderIds);
                if (!result)
                {
                    throw new Exception("删除销售订单行失败");
                }
                //最后删除销售订单
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSqlSO.ToString(), sqlParams.ToArray());
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

        public bool Exists(params string[] codes)
        {
            if (codes == null || codes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and SaleOrderCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<SaleOrder> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

    }
}
