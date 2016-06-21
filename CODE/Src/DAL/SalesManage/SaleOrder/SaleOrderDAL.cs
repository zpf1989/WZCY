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
                Fields = "s.*,t.BillName BillType_Name,m.MaterialName Material_Name,mu.UnitID SaleUnit_Name,c.ClientName Client_Name,uc.UserName Creator_Name,ue.UserName Editor_Name,uf.UserName FirstChecker_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    orders.Add(new SaleOrder
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
                        CreateTime = Convert.ToDateTime(row["CreateTime"]),
                        Editor = row["Editor"].ToString(),
                        Editor_Name = row["Editor_Name"].ToString(),
                        EditTime = Convert.ToDateTime(row["EditTime"]),
                        FirstChecker = row["FirstChecker"].ToString(),
                        FirstChecker_Name = row["FirstChecker_Name"].ToString(),
                        FirstCheckView = row["FirstCheckView"].ToString(),
                        FirstCheckTime = Convert.ToDateTime(row["FirstCheckTime"]),
                        RoutingID = row["RoutingID"].ToString(),
                        Routing = row["Routing"].ToString(),
                        SaleState = row["SaleState"].ToString(),
                        Remark = row["Remark"].ToString(),
                        SecondCheckerName = row["SecondCheckerName"].ToString(),
                        ReaderName = row["ReaderName"].ToString()
                    });
                }
            }
            if (containItems && orders != null && orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    order.Items = soItemDAL.GetOrderItems(order.SaleOrderID);
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
                    //新增
                    entity.SaleOrderID = Guid.NewGuid().ToString();
                    sbSqlSaleOrder.AppendFormat("insert into {0}(SaleOrderID,SaleOrderCode,BillTypeID,MaterialID,SaleUnitID,ClientID,SaleDate,SaleQty,SalePrice,SaleCost,FinishDate,Creator,CreateTime,Editor,EditTime,FirstChecker,FirstCheckTime,FirstCheckView,RoutingID,SaleState,Remark,Routing,SecondCheckerName,ReaderName)", TableName);
                    sbSqlSaleOrder.AppendFormat(" values (@SaleOrderID{0},@SaleOrderCode{0},@BillTypeID{0},@MaterialID{0},@SaleUnitID{0},@ClientID{0},@SaleDate{0},@SaleQty{0},@SalePrice{0},@SaleCost{0},@FinishDate{0},@Creator{0},@CreateTime{0},@Editor{0},@EditTime{0},@FirstChecker{0},@FirstCheckTime{0},@FirstCheckView{0},@RoutingID{0},@SaleState{0},@Remark{0},@Routing{0},@SecondCheckerName{0},@ReaderName{0};", i);
                }
                else
                {
                    //修改
                    sbSqlSaleOrder.AppendFormat("update {0} set SaleOrderCode=@SaleOrderCode{0},BillTypeID=@BillTypeID{0},MaterialID=@MaterialID{0},SaleUnitID=@SaleUnitID{0},ClientID=@ClientID{0},SaleDate=@SaleDate{0},SaleQty=@SaleQty{0},SalePrice=@SalePrice{0},SaleCost=@SaleCost{0},FinishDate=@FinishDate{0},Editor=@Editor{0},EditTime=@EditTime{0},FirstChecker=@FirstChecker{0},FirstCheckTime=@FirstCheckTime{0},FirstCheckView=@FirstCheckView{0},RoutingID=@RoutingID{0},SaleState=@SaleState{0},Remark=@Remark{0},Routing=@Routing{0},SecondCheckerName=@SecondCheckerName{0},ReaderName=@ReaderName{0};", TableName, i);
                    sbSqlSaleOrder.AppendFormat(" where SaleOrderID=@SaleOrderID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@SaleOrderID"+i, Value=entity.SaleOrderID},
                            new SqlParameter{ParameterName="@SaleOrderCode"+i, Value=entity.SaleOrderCode},
                            new SqlParameter{ParameterName="@BillTypeID"+i, Value=entity.BillTypeID},
                            new SqlParameter{ParameterName="@MaterialID"+i, Value=entity.MaterialID},
                            new SqlParameter{ParameterName="@SaleUnitID"+i, Value=entity.SaleUnitID},
                            new SqlParameter{ParameterName="@ClientID"+i, Value=entity.ClientID},
                            new SqlParameter{ParameterName="@SaleDate"+i, Value=entity.SaleDate},
                            new SqlParameter{ParameterName="@SaleQty"+i, Value=entity.SaleQty},
                            new SqlParameter{ParameterName="@SalePrice"+i, Value=entity.SalePrice},
                            new SqlParameter{ParameterName="@SaleCost"+i, Value=entity.SaleCost},
                            new SqlParameter{ParameterName="@FinishDate"+i, Value=entity.Remark},
                            new SqlParameter{ParameterName="@Creator"+i, Value=entity.Remark},
                            new SqlParameter{ParameterName="@CreateTime"+i, Value=entity.CreateTime},
                            new SqlParameter{ParameterName="@Editor"+i, Value=entity.Editor},
                            new SqlParameter{ParameterName="@EditTime"+i, Value=entity.EditTime},
                            new SqlParameter{ParameterName="@FirstChecker"+i, Value=entity.FirstChecker},
                            new SqlParameter{ParameterName="@FirstCheckTime"+i, Value=entity.FirstCheckTime},
                            new SqlParameter{ParameterName="@FirstCheckView"+i, Value=entity.FirstCheckView},
                            new SqlParameter{ParameterName="@RoutingID"+i, Value=entity.RoutingID},
                            new SqlParameter{ParameterName="@SaleState"+i, Value=entity.SaleState},
                            new SqlParameter{ParameterName="@Remark"+i, Value=entity.Remark},
                            new SqlParameter{ParameterName="@Routing"+i, Value=entity.Routing},
                            new SqlParameter{ParameterName="@SecondCheckerName"+i, Value=entity.SecondCheckerName},
                            new SqlParameter{ParameterName="@ReaderName"+i, Value=entity.ReaderName},
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
            StringBuilder sbSqlSOItem = new StringBuilder();//删除销售订单行的sql
            sbSqlSO.AppendFormat("delete from {0} where SaleOrderID in (", TableName);
            sbSqlSOItem.AppendFormat("delete from {1} where SaleOrderID in (", SaleOrderItemDAL.TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < saleOrderIds.Length; i++)
            {
                sbSqlSO.AppendFormat("@SaleOrderID{0}", i);
                sbSqlSOItem.AppendFormat("@SaleOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@SaleOrderID" + i, Value = saleOrderIds[i] });
                if (i < saleOrderIds.Length - 1)
                {
                    sbSqlSO.Append(",");
                    sbSqlSOItem.Append(",");
                }
            }
            sbSqlSO.Append(");");
            sbSqlSOItem.Append(");");
            //2、执行sql
            int rst = 0;
            try
            {
                //先删除订单行
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSqlSOItem.ToString(), sqlParams.ToArray());
                //最后删除销售订单
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
