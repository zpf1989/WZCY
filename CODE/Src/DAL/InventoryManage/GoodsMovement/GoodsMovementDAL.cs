using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OA.GeneralClass.Extensions;
using System.Data.SqlClient;
using GentleUtil.DB;

namespace OA.DAL
{
    public class GoodsMovementDAL : BaseDAL<GoodsMovement>, IGoodsMovementDAL
    {
        public const string TableName = "GoodsMovement";
        GoodsMovementItemDAL gmItemDAL = new GoodsMovementItemDAL();
        public bool Exists(params string[] codes)
        {
            if (codes == null || codes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and GoodsMovementCode in ('{0}')", string.Join("','", codes)));
        }

        public GoodsMovement GetGMWithItems(string gmId)
        {
            if (ValidateUtil.isBlank(gmId))
            {
                return null;
            }
            var list = GetEntitiesByPage(pageEntity: new PageEntity(1, 20), whereSql: string.Format(" and GoodsMovementID='{0}'", gmId), orderBySql: null, containItems: true);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public GoodsMovement GetGoodsMovement(string gmId)
        {
            if (ValidateUtil.isBlank(gmId))
            {
                return null;
            }
            var list = GetEntitiesByPage(pageEntity: new PageEntity(1, 20), whereSql: string.Format(" and GoodsMovementID='{0}'", gmId), orderBySql: null, containItems: false);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public bool SubmitToFirstChecker(string userId, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(userId) || gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set FirstChecker=@checker,BillState=@state where GoodsMovementID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@checker", Value = userId });
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = '2' });//提交
            for (int i = 0; i < gmIds.Length; i++)
            {
                sbsql.AppendFormat("@gmId{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@gmId" + i, Value = gmIds[i] });
                if (i < gmIds.Length - 1)
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

        public bool SubmitToSecondChecker(string userId, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(userId) || gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set SecondCheckerName=(select UserName from {1} where UserID=@checker),BillState=@state where GoodsMovementID in (", TableName, UserManageDAL.TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@checker", Value = userId });
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = '5' });//提交复审
            for (int i = 0; i < gmIds.Length; i++)
            {
                sbsql.AppendFormat("@gmId{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@gmId" + i, Value = gmIds[i] });
                if (i < gmIds.Length - 1)
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

        public bool SubmitToReader(string userId, params string[] gmIds)
        {
            if (ValidateUtil.isBlank(userId) || gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbsql = new StringBuilder();//
            sbsql.AppendFormat("update {0} set ReaderName=(select UserName from {1} where UserID=@reader) where GoodsMovementID in (", TableName, UserManageDAL.TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@reader", Value = userId });
            for (int i = 0; i < gmIds.Length; i++)
            {
                sbsql.AppendFormat("@gmId{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@gmId" + i, Value = gmIds[i] });
                if (i < gmIds.Length - 1)
                {
                    sbsql.Append(",");
                }
            }
            sbsql.Append(");");
            //2、执行sql
            int rst = 0;

            try
            {
                //更新
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

        public bool FirstCheck(bool result, string checkView, params string[] gmIds)
        {
            if (gmIds == null || gmIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("update {0} set BillState=@state,FirstCheckView=@view,FirstCheckTime=@time where GoodsMovementID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = result ? "3" : "4" });
            sqlParams.Add(new SqlParameter { ParameterName = "@view", Value = checkView });
            sqlParams.Add(new SqlParameter { ParameterName = "@time", Value = DateTime.Now });
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

        public bool SecondCheck(bool checkResult, params string[] gmIds)
        {
            return ChangeState(gmIds, checkResult ? "6" : "7");
        }

        public bool Close(string[] gmIds)
        {
            return ChangeState(gmIds, "8");
        }

        bool ChangeState(string[] gmIds, string state)
        {
            if (gmIds == null || gmIds.Length < 1 || string.IsNullOrEmpty(state))
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("update {0} set BillState=@state where GoodsMovementID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@state", Value = state });
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

        public override List<GoodsMovement> GetEntitiesByPage(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql, false);
        }

        public List<GoodsMovement> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool containItems = false)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "gm.CreateTime desc";
            }
            List<GoodsMovement> list = new List<GoodsMovement>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(@" {0} gm 
left join {3} deptRec on gm.RecDeptID = deptRec.DeptID 
left join {2} usrRec on gm.RecHandler = usrRec.UserID 
left join {4} whRec on gm.RecWHID = whRec.WareHouseID 
left join {2} usrRecWH on gm.RecWHEmpID = usrRecWH.UserID 
left join {3} deptIss on gm.IssDeptID = deptIss.DeptID 
left join {2} usrIss on gm.IssHandler = usrIss.UserID 
left join {4} whIss on gm.IssWHID = whIss.WareHouseID 
left join {2} usrIssWH on gm.IssWHEmpID = usrIssWH.UserID 
left join {3} deptPur on gm.PurDeptID = deptPur.DeptID 
left join {2} usrPur on gm.PurEmpID = usrPur.UserID 
left join {3} deptSale on gm.SalesDepID = deptSale.DeptID 
left join {2} usrSale on gm.SalesEmpID = usrSale.UserID 
left join {1} client on gm.CustomerID = client.ClientID 
left join {3} deptPro on gm.ProDepID = deptPro.DeptID 
left join {2} usrPro on gm.ProEmpID = usrPro.UserID 
left join {3} deptCon on gm.ConDepID = deptCon.DeptID 
left join {2} usrCon on gm.ConEmpID = usrCon.UserID 
left join {2} usrCreate on gm.Creator = usrCreate.UserID 
left join {2} usrEdit on gm.Editor = usrEdit.UserID 
left join {2} usrFirst on gm.FirstChecker = usrFirst.UserID ",
TableName, ClientDAL.TableName, UserManageDAL.TableName, DepartmentDAL.TableName, WareHouseDAL.TableName),
                PK = "gm.GoodsMovementID",
                Fields = @"gm.*,deptRec.DeptName RecDept_Name,usrRec.UserName RecHandler_Name,whRec.WareHouseName RecWH_Name,
usrRecWH.UserName RecWHEmp_Name,deptIss.DeptName IssDept_Name,usrIss.UserName IssHandler_Name, 
whIss.WareHouseName IssWH_Name,usrIssWH.UserName IssWHEmp_Name,deptPur.DeptName PurDept_Name,usrPur.UserName PurEmp_Name,
deptSale.DeptName SalesDep_Name,usrSale.UserName SalesEmp_Name,client.ClientName Customer_Name,
deptPro.DeptName ProDep_Name,usrPro.UserName ProEmp_Name,deptCon.DeptName ConDep_Name,usrCon.UserName ConEmp_Name,
usrCreate.UserName Creator_Name,usrEdit.UserName Editor_Name,usrFirst.UserName FirstChecker_Name ",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                GoodsMovement order = null;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    order = new GoodsMovement
                    {
                        GoodsMovementID = row["GoodsMovementID"].ToString(),
                        BusinessType = row["BusinessType"].ToString(),
                        MoveTypeCode = row["MoveTypeCode"].ToString(),
                        GoodsMovementCode = row["GoodsMovementCode"].ToString(),
                        BillState = row["BillState"].ToString(),
                        CreateDate = row["CreateDate"].ToString(),
                        ReceiptDate = row["ReceiptDate"].ToString(),
                        RecDeptID = row["RecDeptID"].ToString(),
                        RecDept_Name = row["RecDept_Name"].ToString(),
                        RecHandler = row["RecHandler"].ToString(),
                        RecHandler_Name = row["RecHandler_Name"].ToString(),
                        RecWHID = row["RecWHID"].ToString(),
                        RecWH_Name = row["RecWH_Name"].ToString(),
                        RecWHEmpID = row["RecWHEmpID"].ToString(),
                        RecWHEmp_Name = row["RecWHEmp_Name"].ToString(),
                        IssDate = row["IssDate"].ToString(),
                        IssDeptID = row["IssDeptID"].ToString(),
                        IssDept_Name = row["IssDept_Name"].ToString(),
                        IssHandler = row["IssHandler"].ToString(),
                        IssHandler_Name = row["IssHandler_Name"].ToString(),
                        IssWHID = row["IssWHID"].ToString(),
                        IssWH_Name = row["IssWH_Name"].ToString(),
                        IssWHEmpID = row["IssWHEmpID"].ToString(),
                        IssWHEmp_Name = row["IssWHEmp_Name"].ToString(),
                        PurDeptID = row["PurDeptID"].ToString(),
                        PurDept_Name = row["PurDept_Name"].ToString(),
                        PurEmpID = row["PurEmpID"].ToString(),
                        PurEmp_Name = row["PurEmp_Name"].ToString(),
                        SupplierID = row["SupplierID"].ToString(),
                        //Supplier_Name = row[""].ToString(),
                        SalesDepID = row["SalesDepID"].ToString(),
                        SalesDep_Name = row["SalesDep_Name"].ToString(),
                        SalesEmpID = row["SalesEmpID"].ToString(),
                        SalesEmp_Name = row["SalesEmp_Name"].ToString(),
                        CustomerID = row["CustomerID"].ToString(),
                        Customer_Name = row["Customer_Name"].ToString(),
                        ProDepID = row["ProDepID"].ToString(),
                        ProDep_Name = row["ProDep_Name"].ToString(),
                        ProEmpID = row["ProEmpID"].ToString(),
                        ProEmp_Name = row["ProEmp_Name"].ToString(),
                        ConDepID = row["ConDepID"].ToString(),
                        ConDep_Name = row["ConDep_Name"].ToString(),
                        ConEmpID = row["ConEmpID"].ToString(),
                        ConEmp_Name = row["ConEmp_Name"].ToString(),
                        Creator = row["Creator"].ToString(),
                        Creator_Name = row["Creator_Name"].ToString(),
                        Editor = row["Editor"].ToString(),
                        Editor_Name = row["Editor_Name"].ToString(),
                        FirstChecker = row["FirstChecker"].ToString(),
                        FirstChecker_Name = row["FirstChecker_Name"].ToString(),
                        FirstCheckView = row["FirstCheckView"].ToString(),
                        IsRed = row["IsRed"].ToString(),
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

                    list.Add(order);
                }
            }
            if (containItems && list != null && list.Count > 0)
            {
                foreach (var order in list)
                {
                    order.Items = gmItemDAL.GetGMItems(new PageEntity(1, 1000), order.GoodsMovementID);
                }
            }

            return list;
        }

        public override List<GoodsMovement> GetEntitiesByPageForHelp(GeneralClass.PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public override bool Save(params GoodsMovement[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            //Logger.LogInfo(entities.SerializeToJson());
            StringBuilder sbSqlGM = new StringBuilder();
            List<Func<bool>> saveItemsFuncs = new List<Func<bool>>();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            GoodsMovement entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                //①货物移动
                if (ValidateUtil.isBlank(entity.GoodsMovementID))
                {
                    //新增，保存部分字段
                    entity.GoodsMovementID = Guid.NewGuid().ToString();
                    sbSqlGM.AppendFormat("insert into {0}(GoodsMovementID,BusinessType,MoveTypeCode,GoodsMovementCode,CreateDate,ReceiptDate,RecDeptID,RecHandler,RecWHID,RecWHEmpID,IssDate,IssDeptID,IssHandler,IssWHID,IssWHEmpID,PurDeptID,PurEmpID,SupplierID,SalesDepID,SalesEmpID,CustomerID,ProDepID,ProEmpID,ConDepID,ConEmpID,Creator,CreateTime,BillState,IsRed,Remark)", TableName);
                    sbSqlGM.AppendFormat(" values (@GoodsMovementID{0},@BusinessType{0},@MoveTypeCode{0},@GoodsMovementCode{0},@CreateDate{0},@ReceiptDate{0},@RecDeptID{0},@RecHandler{0},@RecWHID{0},@RecWHEmpID{0},@IssDate{0},@IssDeptID{0},@IssHandler{0},@IssWHID{0},@IssWHEmpID{0},@PurDeptID{0},@PurEmpID{0},@SupplierID{0},@SalesDepID{0},@SalesEmpID{0},@CustomerID{0},@ProDepID{0},@ProEmpID{0},@ConDepID{0},@ConEmpID{0},@Creator{0},@CreateTime{0},@BillState{0},@IsRed{0},@Remark{0});", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Creator"+i, Value=entity.Creator},
                            new SqlParameter{ParameterName="@CreateTime"+i, Value=entity.CreateTime},
                            new SqlParameter{ParameterName="@BillState"+i, Value=entity.BillState},
                    });
                }
                else
                {
                    //修改，只更新部分字段
                    sbSqlGM.AppendFormat("update {0} set BusinessType=@BusinessType{1},MoveTypeCode=@MoveTypeCode{1},CreateDate=@CreateDate{1},ReceiptDate=@ReceiptDate{1},RecDeptID=@RecDeptID{1},RecHandler=@RecHandler{1},RecWHID=@RecWHID{1},RecWHEmpID=@RecWHEmpID{1},IssDate=@IssDate{1},IssDeptID=@IssDeptID{1},IssHandler=@IssHandler{1},IssWHID=@IssWHID{1},IssWHEmpID=@IssWHEmpID{1},PurDeptID=@PurDeptID{1},PurEmpID=@PurEmpID{1},SupplierID=@SupplierID{1},SalesDepID=@SalesDepID{1},SalesEmpID=@SalesEmpID{1},CustomerID=@CustomerID{1},ProDepID=@ProDepID{1},ProEmpID=@ProEmpID{1},ConDepID=@ConDepID{1},ConEmpID=@ConEmpID{1},IsRed=@IsRed{1},Remark=@Remark{1},Editor=@Editor{1},EditTime=@EditTime{1}", TableName, i);
                    sbSqlGM.AppendFormat(" where GoodsMovementID=@GoodsMovementID{0};", i);
                    sqlParams.AddRange(new SqlParameter[]{
                            new SqlParameter{ParameterName="@Editor"+i, Value=entity.Editor},
                            new SqlParameter{ParameterName="@EditTime"+i, Value=entity.EditTime},
                    });
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@GoodsMovementID"+i, Value=entity.GoodsMovementID},
                            new SqlParameter{ParameterName="@BusinessType"+i, Value=entity.BusinessType},
                            new SqlParameter{ParameterName="@MoveTypeCode"+i, Value=entity.MoveTypeCode},
                            new SqlParameter{ParameterName="@GoodsMovementCode"+i, Value=entity.GoodsMovementCode},
                            new SqlParameter{ParameterName="@CreateDate"+i, Value=string.IsNullOrEmpty(entity.CreateDate)?null:entity.CreateDate.Replace("-","")},
                            new SqlParameter{ParameterName="@ReceiptDate"+i, Value=string.IsNullOrEmpty(entity.ReceiptDate)?null:entity.ReceiptDate.Replace("-","")},
                            new SqlParameter{ParameterName="@RecDeptID"+i, Value=entity.RecDeptID},
                            new SqlParameter{ParameterName="@RecHandler"+i, Value=entity.RecHandler},
                            new SqlParameter{ParameterName="@RecWHID"+i, Value=entity.RecWHID},
                            new SqlParameter{ParameterName="@RecWHEmpID"+i, Value=entity.RecWHEmpID},
                            new SqlParameter{ParameterName="@IssDate"+i, Value=string.IsNullOrEmpty(entity.IssDate)?null:entity.IssDate.Replace("-","")},
                            new SqlParameter{ParameterName="@IssDeptID"+i, Value=entity.IssDeptID},
                            new SqlParameter{ParameterName="@IssHandler"+i, Value=entity.IssHandler},
                            new SqlParameter{ParameterName="@IssWHID"+i, Value=entity.IssWHID},
                            new SqlParameter{ParameterName="@IssWHEmpID"+i, Value=entity.IssWHEmpID},
                            new SqlParameter{ParameterName="@PurDeptID"+i, Value=entity.PurDeptID},
                            new SqlParameter{ParameterName="@PurEmpID"+i, Value=entity.PurEmpID},
                            new SqlParameter{ParameterName="@SupplierID"+i, Value=entity.SupplierID},
                            new SqlParameter{ParameterName="@SalesDepID"+i, Value=entity.SalesDepID},
                            new SqlParameter{ParameterName="@SalesEmpID"+i, Value=entity.SalesEmpID},
                            new SqlParameter{ParameterName="@CustomerID"+i, Value=entity.CustomerID},
                            new SqlParameter{ParameterName="@ProDepID"+i, Value=entity.ProDepID},
                            new SqlParameter{ParameterName="@ProEmpID"+i, Value=entity.ProEmpID},
                            new SqlParameter{ParameterName="@ConDepID"+i, Value=entity.ConDepID},
                            new SqlParameter{ParameterName="@ConEmpID"+i, Value=entity.ConEmpID},
                            new SqlParameter{ParameterName="@IsRed"+i, Value=entity.IsRed},
                            new SqlParameter{ParameterName="@Remark"+i, Value=entity.Remark},
                                            });
                //②货物移动行
                if (entity.Items != null && entity.Items.Count > 0)
                {
                    foreach (var item in entity.Items)
                    {
                        item.GoodsMovementID = entity.GoodsMovementID;
                        saveItemsFuncs.Add(() =>
                        {
                            //调用GoodsMovementItemDAL.Save
                            return gmItemDAL.Save(entity.Items.ToArray());
                        });
                    }
                }
            }
            //2、执行sql
            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSqlGM.ToString(), sqlParams.ToArray());
                //保存货物移动行
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

        public override bool Delete(params string[] ids)
        {
            if (ids == null || ids.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where GoodsMovementID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < ids.Length; i++)
            {
                sbSql.AppendFormat("@gmId{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@gmId" + i, Value = ids[i] });
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
                //删除
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
