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
    public class BuyOrderDAL : BaseDAL<BuyOrder>, IBuyOrderDAL
    {
        public const string TableName = "BuyOrder";
        BuyOrderItemDAL boItemDAL = new BuyOrderItemDAL();
        public override List<BuyOrder> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql, false);
        }
        public BuyOrder GetBuyOrderWithItems(string orderId)
        {
            if (ValidateUtil.isBlank(orderId))
            {
                return null;
            }
            var list = GetEntitiesByPage(pageEntity: new PageEntity(1, 20), whereSql: string.Format(" and BuyOrderID='{0}'", orderId), orderBySql: null, containItems: true);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public BuyOrder GetBuyOrder(string orderId)
        {
            if (ValidateUtil.isBlank(orderId))
            {
                return null;
            }
            var list = GetEntitiesByPage(pageEntity: new PageEntity(1, 20), whereSql: string.Format(" and BuyOrderID='{0}'", orderId), orderBySql: null, containItems: false);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public List<BuyOrder> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool containItems = false)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "b.BuyOrderCode";
            }
            List<BuyOrder> orders = new List<BuyOrder>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@" {0} b 
                left JOIN {1} s on b.SupplierID=s.SupplierID
                left join {2} ue on b.Editor=ue.UserID
                left join {2} uc on b.Creator=uc.UserID
                left join {2} uf on b.FirstChecker=uf.UserID",
                TableName, SupplierDAL.TableName, UserManageDAL.TableName),
                PK = "b.BuyOrderID",
                Fields = "b.*,s.SupplierName Supplier_Name,uc.UserName Creator_Name,ue.UserName Editor_Name,uf.UserName FirstChecker_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                BuyOrder order = null;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    order = new BuyOrder
                    {
                        BuyOrderID = row["BuyOrderID"].ToString(),
                        BuyOrderCode = row["BuyOrderCode"].ToString(),
                        SupplierID = row["SupplierID"].ToString(),
                        Supplier_Name = row["Supplier_Name"].ToString(),
                        BuyOrderDate = row["BuyOrderDate"].ToString(),
                        DeliveryDate = row["DeliveryDate"].ToString(),
                        Creator = row["Creator"].ToString(),
                        Creator_Name = row["Creator_Name"].ToString(),
                        Editor = row["Editor"].ToString(),
                        Editor_Name = row["Editor_Name"].ToString(),
                        FirstChecker = row["FirstChecker"].ToString(),
                        FirstChecker_Name = row["FirstChecker_Name"].ToString(),
                        FirstCheckView = row["FirstCheckView"].ToString(),
                        OrderState = row["OrderState"].ToString(),
                        RecCompany = row["RecCompany"].ToString(),
                        RecTel = row["RecTel"].ToString(),
                        RecFax = row["RecFax"].ToString(),
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
                    order.Items = boItemDAL.GetOrderItems(new PageEntity(1, 1000), order.BuyOrderID);
                }
            }

            return orders;
        }

        public override bool Save(params BuyOrder[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbSqlSaleOrder = new StringBuilder();
            List<Func<bool>> saveItemsFuncs = new List<Func<bool>>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            BuyOrder entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                //①采购订单
                if (ValidateUtil.isBlank(entity.BuyOrderID))
                {
                    //新增，保存部分字段
                    entity.BuyOrderID = Guid.NewGuid().ToString();
                    sbSqlSaleOrder.AppendFormat("insert into {0}(BuyOrderID,BuyOrderCode,BuyOrderDate,SupplierID,DeliveryDate,Creator,CreateTime,OrderState,Remark)", TableName);
                    sbSqlSaleOrder.AppendFormat(" values (@BuyOrderID{0},@BuyOrderCode{0},@BuyOrderDate{0},@SupplierID{0},@DeliveryDate{0},@Creator{0},@CreateTime{0},@OrderState{0},@Remark{0});", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Creator"+i, Value=entity.Creator},
                            new SqlParameter{ParameterName="@CreateTime"+i, Value=entity.CreateTime},
                            new SqlParameter{ParameterName="@OrderState"+i, Value=entity.OrderState},
                    });
                }
                else
                {
                    //修改，只更新部分字段
                    sbSqlSaleOrder.AppendFormat("update {0} set BuyOrderDate=@BuyOrderDate{1},SupplierID=@SupplierID{1},DeliveryDate=@DeliveryDate{1},Editor=@Editor{1},EditTime=@EditTime{1},Remark=@Remark{1}", TableName, i);
                    sbSqlSaleOrder.AppendFormat(" where BuyOrderID=@BuyOrderID{0};", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Editor"+i, Value=entity.Editor},
                            new SqlParameter{ParameterName="@EditTime"+i, Value=entity.EditTime},
                    });
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@BuyOrderID"+i, Value=entity.BuyOrderID},
                            new SqlParameter{ParameterName="@BuyOrderCode"+i, Value=entity.BuyOrderCode},
                            new SqlParameter{ParameterName="@BuyOrderDate"+i, Value=entity.BuyOrderDate.Replace("-","")},
                            new SqlParameter{ParameterName="@SupplierID"+i, Value=entity.SupplierID},
                            new SqlParameter{ParameterName="@DeliveryDate"+i, Value=entity.DeliveryDate.Replace("-","")},
                            new SqlParameter{ParameterName="@Remark"+i, Value=entity.Remark}
                                            });
                //②采购订单行
                if (entity.Items != null && entity.Items.Count > 0)
                {
                    foreach (var item in entity.Items)
                    {
                        item.BuyOrderID = entity.BuyOrderID;
                        saveItemsFuncs.Add(() =>
                        {
                            //调用BuyOrderItemDAL.Save
                            return boItemDAL.Save(entity.Items.ToArray());
                        });
                    }

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

        public override bool Delete(params string[] buyOrderIds)
        {
            if (buyOrderIds == null || buyOrderIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSqlBO = new StringBuilder();//删除采购订单的sql
            sbSqlBO.AppendFormat("delete from {0} where BuyOrderID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < buyOrderIds.Length; i++)
            {
                sbSqlBO.AppendFormat("@BuyOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BuyOrderID" + i, Value = buyOrderIds[i] });
                if (i < buyOrderIds.Length - 1)
                {
                    sbSqlBO.Append(",");
                }
            }
            sbSqlBO.Append(");");
            //2、执行sql
            int rst = 0;

            try
            {
                //删除采购订单
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSqlBO.ToString(), sqlParams.ToArray());
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
            return base.Exists(string.Format(" and BuyOrderCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<BuyOrder> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool SubmitToFirstChecker(string userId, params string[] boIds)
        {
            if (ValidateUtil.isBlank(userId) || boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set FirstChecker=@checker,OrderState=@OrderState where BuyOrderID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@checker", Value = userId });
            sqlParams.Add(new SqlParameter { ParameterName = "@OrderState", Value = '2' });//提交
            for (int i = 0; i < boIds.Length; i++)
            {
                sbsql.AppendFormat("@BuyOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BuyOrderID" + i, Value = boIds[i] });
                if (i < boIds.Length - 1)
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

        public bool SubmitToSecondChecker(string userId, params string[] boIds)
        {
            if (ValidateUtil.isBlank(userId) || boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set SecondCheckerName=(select UserName from {1} where UserID=@checker),OrderState=@OrderState where BuyOrderID in (", TableName, UserManageDAL.TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@checker", Value = userId });
            sqlParams.Add(new SqlParameter { ParameterName = "@OrderState", Value = '5' });//提交复审
            for (int i = 0; i < boIds.Length; i++)
            {
                sbsql.AppendFormat("@BuyOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BuyOrderID" + i, Value = boIds[i] });
                if (i < boIds.Length - 1)
                {
                    sbsql.Append(",");
                }
            }
            sbsql.Append(");");
            //2、执行sql
            int rst = 0;

            try
            {
                //更新复审人
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

        public bool SubmitToReader(string userId, params string[] boIds)
        {
            if (ValidateUtil.isBlank(userId) || boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set ReaderName=(select UserName from {1} where UserID=@reader) where BuyOrderID in (", TableName, UserManageDAL.TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@reader", Value = userId });
            for (int i = 0; i < boIds.Length; i++)
            {
                sbsql.AppendFormat("@BuyOrderID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@BuyOrderID" + i, Value = boIds[i] });
                if (i < boIds.Length - 1)
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

        public bool FirstCheck(bool result, string checkView, params string[] boIds)
        {
            if (boIds == null || boIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("update {0} set OrderState=@OrderState,FirstCheckView=@view,FirstCheckTime=@time where BuyOrderID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@OrderState", Value = result ? "3" : "4" });
            sqlParams.Add(new SqlParameter { ParameterName = "@view", Value = checkView });
            sqlParams.Add(new SqlParameter { ParameterName = "@time", Value = DateTime.Now });
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



        public bool SecondCheck(bool checkResult, params string[] boIds)
        {
            return ChangeState(boIds, checkResult ? "6" : "7");
        }


        public bool Close(string[] boIds)
        {
            return ChangeState(boIds, "8");
        }

        bool ChangeState(string[] boIds, string state)
        {
            if (boIds == null || boIds.Length < 1 || string.IsNullOrEmpty(state))
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("update {0} set OrderState=@state where BuyOrderID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = state });
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
