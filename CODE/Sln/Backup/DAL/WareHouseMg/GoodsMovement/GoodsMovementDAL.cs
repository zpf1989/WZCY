using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;

namespace OA.DAL
{
    public class GoodsMovementDAL : IGoodsMovementDAL
    {
        #region 新增
        /// <summary>
        /// 新增货物移动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddGoodsMovement(GoodsMovement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GoodsMovement(");
            strSql.Append("GoodsMovementID,CreateDate,GoodsMovementCode,BusinessType,MoveTypeCode,SourceBillType,PurCompanyID,PurDeptID,PurEmpID,SupplierID,ReceiptDate,RecCompanyID,RecHandler,RecWHID,RecWHEmpID,SalesCompanyID,SalesDepID,SalesEmpID,CustomerID,IssDate,IssCompanyID,IssDeptID,IssHandler,IssWHID,IssWHEmpID,ProCompanyID,ProDepID,ProEmpID,ConCompanyID,ConDepID,ConEmpID,Creator,CreateTime,Editor,EditTime,BillState,FirstChecker,FirstCheckTime,FirstCheckView,IsRed,Remark,SecondCheckerName,ReaderName,RecDeptID)");
            strSql.Append(" values (");
            strSql.Append("@GoodsMovementID,@CreateDate,@GoodsMovementCode,@BusinessType,@MoveTypeCode,@SourceBillType,@PurCompanyID,@PurDeptID,@PurEmpID,@SupplierID,@ReceiptDate,@RecCompanyID,@RecHandler,@RecWHID,@RecWHEmpID,@SalesCompanyID,@SalesDepID,@SalesEmpID,@CustomerID,@IssDate,@IssCompanyID,@IssDeptID,@IssHandler,@IssWHID,@IssWHEmpID,@ProCompanyID,@ProDepID,@ProEmpID,@ConCompanyID,@ConDepID,@ConEmpID,@Creator,@CreateTime,@Editor,@EditTime,@BillState,@FirstChecker,@FirstCheckTime,@FirstCheckView,@IsRed,@Remark,@SecondCheckerName,@ReaderName,@RecDeptID)");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36),
					new SqlParameter("@CreateDate", SqlDbType.Char,8),
					new SqlParameter("@GoodsMovementCode", SqlDbType.VarChar,60),
					new SqlParameter("@BusinessType", SqlDbType.Char,1),
					new SqlParameter("@MoveTypeCode", SqlDbType.Char,3),
					new SqlParameter("@SourceBillType", SqlDbType.Char,6),
					new SqlParameter("@PurCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@PurDeptID", SqlDbType.VarChar,36),
					new SqlParameter("@PurEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiptDate", SqlDbType.Char,8),
					new SqlParameter("@RecCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@RecHandler", SqlDbType.VarChar,50),
					new SqlParameter("@RecWHID", SqlDbType.VarChar,36),
					new SqlParameter("@RecWHEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@SalesCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@SalesDepID", SqlDbType.VarChar,36),
					new SqlParameter("@SalesEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@CustomerID", SqlDbType.VarChar,36),
					new SqlParameter("@IssDate", SqlDbType.Char,8),
					new SqlParameter("@IssCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@IssDeptID", SqlDbType.VarChar,36),
					new SqlParameter("@IssHandler", SqlDbType.VarChar,50),
					new SqlParameter("@IssWHID", SqlDbType.VarChar,36),
					new SqlParameter("@IssWHEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@ProCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@ProDepID", SqlDbType.VarChar,36),
					new SqlParameter("@ProEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@ConCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@ConDepID", SqlDbType.VarChar,36),
					new SqlParameter("@ConEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@Creator", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@BillState", SqlDbType.Char,1),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@IsRed", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
                    new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
                    new SqlParameter("@ReaderName", SqlDbType.VarChar,255),
                    new SqlParameter("@RecDeptID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.GoodsMovementID;
            parameters[1].Value = model.CreateDate;
            parameters[2].Value = model.GoodsMovementCode;
            parameters[3].Value = model.BusinessType;
            parameters[4].Value = model.MoveTypeCode;
            parameters[5].Value = model.SourceBillType;
            parameters[6].Value = model.PurCompanyID;
            parameters[7].Value = model.PurDeptID;
            parameters[8].Value = model.PurEmpID;
            parameters[9].Value = model.SupplierID;
            parameters[10].Value = model.ReceiptDate;
            parameters[11].Value = model.RecCompanyID;
            parameters[12].Value = model.RecHandler;
            parameters[13].Value = model.RecWHID;
            parameters[14].Value = model.RecWHEmpID;
            parameters[15].Value = model.SalesCompanyID;
            parameters[16].Value = model.SalesDepID;
            parameters[17].Value = model.SalesEmpID;
            parameters[18].Value = model.CustomerID;
            parameters[19].Value = model.IssDate;
            parameters[20].Value = model.IssCompanyID;
            parameters[21].Value = model.IssDeptID;
            parameters[22].Value = model.IssHandler;
            parameters[23].Value = model.IssWHID;
            parameters[24].Value = model.IssWHEmpID;
            parameters[25].Value = model.ProCompanyID;
            parameters[26].Value = model.ProDepID;
            parameters[27].Value = model.ProEmpID;
            parameters[28].Value = model.ConCompanyID;
            parameters[29].Value = model.ConDepID;
            parameters[30].Value = model.ConEmpID;
            parameters[31].Value = model.Creator;
            parameters[32].Value = model.CreateTime;
            parameters[33].Value = model.Editor;
            parameters[34].Value = model.EditTime;
            parameters[35].Value = model.BillState;
            parameters[36].Value = model.FirstChecker;
            parameters[37].Value = model.FirstCheckTime;
            parameters[38].Value = model.FirstCheckView;
            parameters[39].Value = model.IsRed;
            parameters[40].Value = model.Remark;
            parameters[41].Value = model.SecondCheckerName;
            parameters[42].Value = model.ReaderName;
            parameters[43].Value = model.RecDeptID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                GMSecondCheckDAL gmSC = new GMSecondCheckDAL();
                int socount = model.SCList.Count;
                if (socount > 0)
                {
                    for (int i = 0; i < socount;i++ )
                    {
                        GMSecondCheck gmsc = model.SCList[i] as GMSecondCheck;
                        gmSC.Add(gmsc);
                    }
                }

                GMReaderDAL gmr = new GMReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    for (int i = 0; i < rcount; i++)
                    {
                        GMReader sor = model.RList[i] as GMReader;
                        gmr.Add(sor);
                    }
                }

                GoodsMovementItemDAL item = new GoodsMovementItemDAL();
                item.Save(model.GoodsMovementID, model.OldGoodsMovementID);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            return 1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改入库单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGoodsMovement(GoodsMovement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsMovement set ");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("GoodsMovementCode=@GoodsMovementCode,");
            strSql.Append("BusinessType=@BusinessType,");
            strSql.Append("MoveTypeCode=@MoveTypeCode,");
            strSql.Append("SourceBillType=@SourceBillType,");
            strSql.Append("PurCompanyID=@PurCompanyID,");
            strSql.Append("PurDeptID=@PurDeptID,");
            strSql.Append("PurEmpID=@PurEmpID,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("ReceiptDate=@ReceiptDate,");
            strSql.Append("RecCompanyID=@RecCompanyID,");
            strSql.Append("RecHandler=@RecHandler,");
            strSql.Append("RecWHID=@RecWHID,");
            strSql.Append("RecWHEmpID=@RecWHEmpID,");
            strSql.Append("SalesCompanyID=@SalesCompanyID,");
            strSql.Append("SalesDepID=@SalesDepID,");
            strSql.Append("SalesEmpID=@SalesEmpID,");
            strSql.Append("CustomerID=@CustomerID,");
            strSql.Append("IssDate=@IssDate,");
            strSql.Append("IssCompanyID=@IssCompanyID,");
            strSql.Append("IssDeptID=@IssDeptID,");
            strSql.Append("IssHandler=@IssHandler,");
            strSql.Append("IssWHID=@IssWHID,");
            strSql.Append("IssWHEmpID=@IssWHEmpID,");
            strSql.Append("ProCompanyID=@ProCompanyID,");
            strSql.Append("ProDepID=@ProDepID,");
            strSql.Append("ProEmpID=@ProEmpID,");
            strSql.Append("ConCompanyID=@ConCompanyID,");
            strSql.Append("ConDepID=@ConDepID,");
            strSql.Append("ConEmpID=@ConEmpID,");
            strSql.Append("Editor=@Editor,");
            strSql.Append("EditTime=@EditTime,");
            strSql.Append("BillState=@BillState,");
            strSql.Append("FirstChecker=@FirstChecker,");
            strSql.Append("FirstCheckTime=@FirstCheckTime,");
            strSql.Append("FirstCheckView=@FirstCheckView,");
            strSql.Append("IsRed=@IsRed,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("SecondCheckerName=@SecondCheckerName,");
            strSql.Append("ReaderName=@ReaderName,");
            strSql.Append("RecDeptID=@RecDeptID");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CreateDate", SqlDbType.Char,8),
					new SqlParameter("@GoodsMovementCode", SqlDbType.VarChar,60),
					new SqlParameter("@BusinessType", SqlDbType.Char,1),
					new SqlParameter("@MoveTypeCode", SqlDbType.Char,3),
					new SqlParameter("@SourceBillType", SqlDbType.Char,6),
					new SqlParameter("@PurCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@PurDeptID", SqlDbType.VarChar,36),
					new SqlParameter("@PurEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiptDate", SqlDbType.Char,8),
					new SqlParameter("@RecCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@RecHandler", SqlDbType.VarChar,50),
					new SqlParameter("@RecWHID", SqlDbType.VarChar,36),
					new SqlParameter("@RecWHEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@SalesCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@SalesDepID", SqlDbType.VarChar,36),
					new SqlParameter("@SalesEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@CustomerID", SqlDbType.VarChar,36),
					new SqlParameter("@IssDate", SqlDbType.Char,8),
					new SqlParameter("@IssCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@IssDeptID", SqlDbType.VarChar,36),
					new SqlParameter("@IssHandler", SqlDbType.VarChar,50),
					new SqlParameter("@IssWHID", SqlDbType.VarChar,36),
					new SqlParameter("@IssWHEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@ProCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@ProDepID", SqlDbType.VarChar,36),
					new SqlParameter("@ProEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@ConCompanyID", SqlDbType.VarChar,36),
					new SqlParameter("@ConDepID", SqlDbType.VarChar,36),
					new SqlParameter("@ConEmpID", SqlDbType.VarChar,36),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@BillState", SqlDbType.Char,1),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@IsRed", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
					new SqlParameter("@ReaderName", SqlDbType.VarChar,255),
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36),
                    new SqlParameter("@RecDeptID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.CreateDate;
            parameters[1].Value = model.GoodsMovementCode;
            parameters[2].Value = model.BusinessType;
            parameters[3].Value = model.MoveTypeCode;
            parameters[4].Value = model.SourceBillType;
            parameters[5].Value = model.PurCompanyID;
            parameters[6].Value = model.PurDeptID;
            parameters[7].Value = model.PurEmpID;
            parameters[8].Value = model.SupplierID;
            parameters[9].Value = model.ReceiptDate;
            parameters[10].Value = model.RecCompanyID;
            parameters[11].Value = model.RecHandler;
            parameters[12].Value = model.RecWHID;
            parameters[13].Value = model.RecWHEmpID;
            parameters[14].Value = model.SalesCompanyID;
            parameters[15].Value = model.SalesDepID;
            parameters[16].Value = model.SalesEmpID;
            parameters[17].Value = model.CustomerID;
            parameters[18].Value = model.IssDate;
            parameters[19].Value = model.IssCompanyID;
            parameters[20].Value = model.IssDeptID;
            parameters[21].Value = model.IssHandler;
            parameters[22].Value = model.IssWHID;
            parameters[23].Value = model.IssWHEmpID;
            parameters[24].Value = model.ProCompanyID;
            parameters[25].Value = model.ProDepID;
            parameters[26].Value = model.ProEmpID;
            parameters[27].Value = model.ConCompanyID;
            parameters[28].Value = model.ConDepID;
            parameters[29].Value = model.ConEmpID;
            parameters[30].Value = model.Editor;
            parameters[31].Value = model.EditTime;
            parameters[32].Value = model.BillState;
            parameters[33].Value = model.FirstChecker;
            parameters[34].Value = model.FirstCheckTime;
            parameters[35].Value = model.FirstCheckView;
            parameters[36].Value = model.IsRed;
            parameters[37].Value = model.Remark;
            parameters[38].Value = model.SecondCheckerName;
            parameters[39].Value = model.ReaderName;
            parameters[40].Value = model.GoodsMovementID;
            parameters[41].Value = model.RecDeptID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                GMSecondCheckDAL gmSC = new GMSecondCheckDAL();
                int socount = model.SCList.Count;
                if (socount > 0)
                {
                    gmSC.Delete(model.GoodsMovementID);
                    for (int i = 0; i < socount; i++)
                    {
                        GMSecondCheck gmsc = model.SCList[i] as GMSecondCheck;
                        gmSC.Add(gmsc);
                    }
                }

                GMReaderDAL rSC = new GMReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    rSC.Delete(model.GoodsMovementID);
                    for (int i = 0; i < rcount; i++)
                    {
                        GMReader gmr = model.RList[i] as GMReader;
                        rSC.Add(gmr);
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            return true;
        }
        #endregion

        #region 审核
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool FirstCheck(GoodsMovement model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsMovement set ");
            strSql.Append("FirstCheckTime=@FirstCheckTime,");
            strSql.Append("FirstCheckView=@FirstCheckView,");
            strSql.Append("BillState=@BillState");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@BillState", SqlDbType.Char,1),
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.FirstCheckTime;
            parameters[1].Value = model.FirstCheckView;
            parameters[2].Value = model.BillState;
            parameters[3].Value = model.GoodsMovementID;

            int rows = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 批准
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SecondCheck(GoodsMovement model, GMSecondCheck gmsc)
        {
            GMSecondCheckDAL socDal = new GMSecondCheckDAL();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsMovement set ");
            strSql.Append("BillState=@BillState");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BillState", SqlDbType.Char,1),
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.BillState;
            parameters[1].Value = model.GoodsMovementID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                socDal.Check(gmsc);
                if (socDal.IsCheck(model.GoodsMovementID))
                {
                    DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return true;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public bool Delete(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GoodsMovement ");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = GoodsMovementID;

            int rows = 0;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                GoodsMovementItemDAL item = new GoodsMovementItemDAL();
                item.Delete(GoodsMovementID);

                //删除分阅人
                GMReaderDAL gmr = new GMReaderDAL();
                gmr.Delete(GoodsMovementID);

                //删除复核人
                GMSecondCheckDAL gms = new GMSecondCheckDAL();
                gms.Delete(GoodsMovementID);

                rows = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 全部货物移动列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string GoodsMovementCode, string BillState, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                strSql.Append(" and BillState ='" + BillState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select G.*,U.UserName as CreateUser,dept.DeptName as SalesDeptName ")
                .AppendLine(" ,U1.UserName as SalesEmpName,U2.UserName as PurEmpName ")
                .AppendLine(" ,U3.UserName as ProEmpName,U4.UserName as ConEmpName,U5.UserName as FirstCheckerName ")
                .AppendLine(" ,dept1.DeptName as PurDeptName,dept2.DeptName as ProDeptName ")
                .AppendLine(" ,dept3.DeptName as ConDeptName,row_number() OVER (order by CreateDate desc) as RowId ")
                .AppendLine(" from GoodsMovement G ")
                .AppendLine(" left join OA_User U on U.UserID=G.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=G.SalesEmpID ")
                .AppendLine(" left join OA_User U2 on U2.UserID=G.PurEmpID ")
                .AppendLine(" left join OA_User U3 on U3.UserID=G.ProEmpID ")
                .AppendLine(" left join OA_User U4 on U4.UserID=G.ConEmpID ")
                .AppendLine(" left join OA_User U5 on U5.UserID=G.FirstChecker ")
                .AppendLine(" left join OA_Dept dept1 on dept1.DeptID=G.PurDeptID ")
                .AppendLine(" left join OA_Dept dept2 on dept2.DeptID=G.ProDepID ")
                .AppendLine(" left join OA_Dept dept3 on dept3.DeptID=G.ConDepID ")
                .AppendLine(" left join OA_Dept dept on dept.DeptID=G.SalesDepID) tmp ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string GoodsMovementCode, string BillState, string MoveTypeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from GoodsMovement")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                strSql.Append(" and BillState ='" + BillState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 我的货物移动列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string GoodsMovementCode, string BillState, string Creator, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                strSql.Append(" and BillState ='" + BillState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(Creator))
            {
                strSql.Append(" and Creator ='" + Creator.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select G.*,U.UserName as CreateUser,dept.DeptName as SalesDeptName ")
                .AppendLine(" ,U1.UserName as SalesEmpName,U2.UserName as PurEmpName ")
                .AppendLine(" ,U3.UserName as ProEmpName,U4.UserName as ConEmpName,U5.UserName as FirstCheckerName ")
                .AppendLine(" ,dept1.DeptName as PurDeptName,dept2.DeptName as ProDeptName ")
                .AppendLine(" ,dept3.DeptName as ConDeptName,row_number() OVER (order by CreateDate desc) as RowId ")
                .AppendLine(" from GoodsMovement G ")
                .AppendLine(" left join OA_User U on U.UserID=G.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=G.SalesEmpID ")
                .AppendLine(" left join OA_User U2 on U2.UserID=G.PurEmpID ")
                .AppendLine(" left join OA_User U3 on U3.UserID=G.ProEmpID ")
                .AppendLine(" left join OA_User U4 on U4.UserID=G.ConEmpID ")
                .AppendLine(" left join OA_User U5 on U5.UserID=G.FirstChecker ")
                .AppendLine(" left join OA_Dept dept1 on dept1.DeptID=G.PurDeptID ")
                .AppendLine(" left join OA_Dept dept2 on dept2.DeptID=G.ProDepID ")
                .AppendLine(" left join OA_Dept dept3 on dept3.DeptID=G.ConDepID ")
                .AppendLine(" left join OA_Dept dept on dept.DeptID=G.SalesDepID) tmp ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string GoodsMovementCode, string BillState, string Creator, string MoveTypeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from GoodsMovement")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(BillState))
            {
                strSql.Append(" and BillState ='" + BillState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(Creator))
            {
                strSql.Append(" and Creator ='" + Creator.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 审核货物移动列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string GoodsMovementCode, string CreateUser, string FirstChecker, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(FirstChecker))
            {
                strSql.Append(" and FirstChecker ='" + FirstChecker.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select G.*,U.UserName as CreateUser,dept.DeptName as SalesDeptName ")
                .AppendLine(" ,U1.UserName as SalesEmpName,U2.UserName as PurEmpName ")
                .AppendLine(" ,U3.UserName as ProEmpName,U4.UserName as ConEmpName,U5.UserName as FirstCheckerName ")
                .AppendLine(" ,dept1.DeptName as PurDeptName,dept2.DeptName as ProDeptName ")
                .AppendLine(" ,dept3.DeptName as ConDeptName,row_number() OVER (order by CreateDate desc) as RowId ")
                .AppendLine(" from GoodsMovement G ")
                .AppendLine(" left join OA_User U on U.UserID=G.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=G.SalesEmpID ")
                .AppendLine(" left join OA_User U2 on U2.UserID=G.PurEmpID ")
                .AppendLine(" left join OA_User U3 on U3.UserID=G.ProEmpID ")
                .AppendLine(" left join OA_User U4 on U4.UserID=G.ConEmpID ")
                .AppendLine(" left join OA_User U5 on U5.UserID=G.FirstChecker ")
                .AppendLine(" left join OA_Dept dept1 on dept1.DeptID=G.PurDeptID ")
                .AppendLine(" left join OA_Dept dept2 on dept2.DeptID=G.ProDepID ")
                .AppendLine(" left join OA_Dept dept3 on dept3.DeptID=G.ConDepID ")
                .AppendLine(" left join OA_Dept dept on dept.DeptID=G.SalesDepID) tmp ")
                .AppendLine(" where BillState=2 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string GoodsMovementCode, string CreateUser, string FirstChecker, string MoveTypeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from GoodsMovement")
            .AppendLine(" where BillState=2 ");

            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(FirstChecker))
            {
                strSql.Append(" and FirstChecker ='" + FirstChecker.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 批准货物移动列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string GoodsMovementCode, string CreateUser, string SecondChecker, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SecondChecker))
            {
                strSql.Append(" and SecondChecker ='" + SecondChecker.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select G.*,U.UserName as CreateUser,dept.DeptName as SalesDeptName ")
                .AppendLine(" ,U1.UserName as SalesEmpName,U2.UserName as PurEmpName ")
                .AppendLine(" ,U3.UserName as ProEmpName,U4.UserName as ConEmpName,U5.UserName as FirstCheckerName ")
                .AppendLine(" ,dept1.DeptName as PurDeptName,dept2.DeptName as ProDeptName,GSC.GMSecondCheckID,GSC.CheckFlag,GSC.SecondChecker ")
                .AppendLine(" ,dept3.DeptName as ConDeptName,row_number() OVER (order by CreateDate desc) as RowId ")
                .AppendLine(" from GoodsMovement G ")
                .AppendLine(" inner join GMSecondCheck GSC on GSC.GoodsMovementID = G.GoodsMovementID ")
                .AppendLine(" left join OA_User U on U.UserID=G.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=G.SalesEmpID ")
                .AppendLine(" left join OA_User U2 on U2.UserID=G.PurEmpID ")
                .AppendLine(" left join OA_User U3 on U3.UserID=G.ProEmpID ")
                .AppendLine(" left join OA_User U4 on U4.UserID=G.ConEmpID ")
                .AppendLine(" left join OA_User U5 on U5.UserID=G.FirstChecker ")
                .AppendLine(" left join OA_Dept dept1 on dept1.DeptID=G.PurDeptID ")
                .AppendLine(" left join OA_Dept dept2 on dept2.DeptID=G.ProDepID ")
                .AppendLine(" left join OA_Dept dept3 on dept3.DeptID=G.ConDepID ")
                .AppendLine(" left join OA_Dept dept on dept.DeptID=G.SalesDepID) tmp ")
                .AppendLine(" where BillState=3 and CheckFlag = '0' ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string GoodsMovementCode, string CreateUser, string SecondChecker, string MoveTypeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from GoodsMovement G ")
                .AppendLine(" inner join GMSecondCheck GSC on GSC.GoodsMovementID = G.GoodsMovementID ")
                .AppendLine(" where BillState=3 and CheckFlag = '0' ");

            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SecondChecker))
            {
                strSql.Append(" and SecondChecker ='" + SecondChecker.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 分阅货物移动列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4Read(string GoodsMovementCode, string CreateUser, string Reader, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(Reader))
            {
                strSql.Append(" and ReaderID ='" + Reader.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select G.*,U.UserName as CreateUser,dept.DeptName as SalesDeptName,GMR.ReaderID,GMR.ReadFlag ")
                .AppendLine(" ,U1.UserName as SalesEmpName,U2.UserName as PurEmpName ")
                .AppendLine(" ,U3.UserName as ProEmpName,U4.UserName as ConEmpName,U5.UserName as FirstCheckerName ")
                .AppendLine(" ,dept1.DeptName as PurDeptName,dept2.DeptName as ProDeptName ")
                .AppendLine(" ,dept3.DeptName as ConDeptName,row_number() OVER (order by CreateDate desc) as RowId ")
                .AppendLine(" from GoodsMovement G ")
                .AppendLine(" inner join GMReader GMR on GMR.GoodsMovementID = G.GoodsMovementID ")
                .AppendLine(" left join OA_User U on U.UserID=G.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=G.SalesEmpID ")
                .AppendLine(" left join OA_User U2 on U2.UserID=G.PurEmpID ")
                .AppendLine(" left join OA_User U3 on U3.UserID=G.ProEmpID ")
                .AppendLine(" left join OA_User U4 on U4.UserID=G.ConEmpID ")
                .AppendLine(" left join OA_User U5 on U5.UserID=G.FirstChecker ")
                .AppendLine(" left join OA_Dept dept1 on dept1.DeptID=G.PurDeptID ")
                .AppendLine(" left join OA_Dept dept2 on dept2.DeptID=G.ProDepID ")
                .AppendLine(" left join OA_Dept dept3 on dept3.DeptID=G.ConDepID ")
                .AppendLine(" left join OA_Dept dept on dept.DeptID=G.SalesDepID) tmp ")
                .AppendLine(" where ReadFlag = '0' ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        public int GetRowCounts4Read(string GoodsMovementCode, string CreateUser, string Reader, string MoveTypeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from GoodsMovement G ")
                .AppendLine(" inner join GMReader GMR on GMR.GoodsMovementID = G.GoodsMovementID ")
                .AppendLine(" where ReadFlag = '0' ");

            if (!string.IsNullOrEmpty(GoodsMovementCode))
            {
                strSql.Append(" and GoodsMovementCode like'%" + GoodsMovementCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(Reader))
            {
                strSql.Append(" and ReaderID ='" + Reader.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MoveTypeCode))
            {
                strSql.Append(" and MoveTypeCode ='" + MoveTypeCode.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 获取模型
        /// <summary>
        /// 获取入库单模型
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public DataSet GetModel(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select G.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,U2.UserName as SaleEmpName ");
            strSql.Append(" ,U3.UserName as IssWHEmpName,U4.UserName as PurEmpName,U5.UserName as RecWHEmpName ");
            strSql.Append(" ,U6.UserName as ProEmpName,U7.UserName as ConEmpName,C.ClientName,D1.DeptName as SaleDeptName ");
            strSql.Append(" ,D2.DeptName as IssDeptName,D3.DeptName as PurDeptName,D4.DeptName as RecDeptName,S.SupplierName as SupplierName ");
            strSql.Append(" ,D5.DeptName as ProDeptName,D6.DeptName as ConDeptName,WH.WareHouseName as IssWHName,WH2.WareHouseName as RecWHName ");
            strSql.Append(" ,row_number() OVER (order by CreateDate desc) as RowId ");
            strSql.Append(" from GoodsMovement G ");
            strSql.Append(" left join OA_User U on U.UserID=G.Creator ");
            strSql.Append(" left join OA_User U1 on U1.UserID=G.FirstChecker ");
            strSql.Append(" left join OA_User U2 on U2.UserID=G.SalesEmpID ");
            strSql.Append(" left join OA_User U3 on U3.UserID=G.IssWHEmpID "); 
            strSql.Append(" left join OA_User U4 on U4.UserID=G.PurEmpID ");
            strSql.Append(" left join OA_User U5 on U5.UserID=G.RecWHEmpID ");
            strSql.Append(" left join OA_User U6 on U6.UserID=G.ProEmpID ");
            strSql.Append(" left join OA_User U7 on U7.UserID=G.ConEmpID ");
            strSql.Append(" left join OA_Dept D1 on D1.DeptID = G.SalesDepID ");
            strSql.Append(" left join OA_Dept D2 on D2.DeptID = G.IssDeptID ");
            strSql.Append(" left join OA_Dept D3 on D3.DeptID = G.PurDeptID ");
            strSql.Append(" left join OA_Dept D4 on D4.DeptID = G.RecDeptID ");
            strSql.Append(" left join OA_Dept D5 on D5.DeptID = G.ProDepID ");
            strSql.Append(" left join OA_Dept D6 on D6.DeptID = G.ConDepID ");
            strSql.Append(" left join Client C on C.ClientID = G.CustomerID ");
            strSql.Append(" left join Supplier S on S.SupplierID = G.SupplierID ");
            strSql.Append(" left join WareHouse WH on WH.WareHouseID = G.IssWHID ");
            strSql.Append(" left join WareHouse WH2 on WH2.WareHouseID = G.RecWHID ");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = GoodsMovementID;

            SaleOrder model = new SaleOrder();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 取消提交
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="GoodsMovementID">单据ID</param>
        /// <returns></returns>
        public bool UnSubmit(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsMovement set ");
            strSql.Append("BillState='1',");
            strSql.Append("FirstChecker='',");
            strSql.Append("FirstCheckView='',");
            strSql.Append("SecondCheckerName='',");
            strSql.Append("ReaderName=''");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = GoodsMovementID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                GMSecondCheckDAL gmSC = new GMSecondCheckDAL();
                gmSC.Delete(GoodsMovementID);

                GMReaderDAL rSC = new GMReaderDAL();
                rSC.Delete(GoodsMovementID);

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 修改单据状态
        /// </summary>
        /// <param name="BillState">单据状态
        /// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭</param>
        /// <param name="GoodsMovementID">单据ID</param>
        /// <returns></returns>
        public int Submit(string BillState, string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsMovement set ");
            strSql.Append("BillState=@BillState");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BillState", SqlDbType.Char,1),
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = BillState;
            parameters[1].Value = GoodsMovementID;

            int flag = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            if (BillState != "7")
                return flag;
            DataSet ds = GetModel(GoodsMovementID);
            if(ds == null || ds.Tables[0].Rows.Count <= 0)
                return flag;
            InvBalRealAccountDAL iDal = new InvBalRealAccountDAL();
            GoodsMovementItemDAL itemDal = new GoodsMovementItemDAL();
            string BusinessType = ds.Tables[0].Rows[0]["BusinessType"].ToString();
            string IsRed = ds.Tables[0].Rows[0]["IsRed"].ToString();
            DataSet dsItem = itemDal.GetModel(GoodsMovementID);
            if (BusinessType == "1")
            {
                string WareHouseID = ds.Tables[0].Rows[0]["RecWHID"].ToString();
                foreach (DataRow dr in dsItem.Tables[0].Rows)
                {
                    InvBalRealAccount info = new InvBalRealAccount();
                    info.MaterialID = dr["MaterialID"].ToString();
                    info.WareHouseID = WareHouseID;
                    info.CurQtyBalance = Convert.ToDecimal(dr["ActInpQty"]);
                    iDal.InWH(info);
                }
            }
            else
            {
                if (IsRed == "1")
                {
                    string WareHouseID = ds.Tables[0].Rows[0]["IssWHID"].ToString();
                    foreach (DataRow dr in dsItem.Tables[0].Rows)
                    {
                        InvBalRealAccount info = new InvBalRealAccount();
                        info.MaterialID = dr["MaterialID"].ToString();
                        info.WareHouseID = WareHouseID;
                        info.CurQtyBalance = Convert.ToDecimal(dr["ActOutQty"]);
                        iDal.InWH(info);
                    }
                }
                else
                {
                    string WareHouseID = ds.Tables[0].Rows[0]["IssWHID"].ToString();
                    foreach (DataRow dr in dsItem.Tables[0].Rows)
                    {
                        InvBalRealAccount info = new InvBalRealAccount();
                        info.MaterialID = dr["MaterialID"].ToString();
                        info.WareHouseID = WareHouseID;
                        info.CurQtyBalance = Convert.ToDecimal(dr["ActOutQty"]);
                        iDal.OutWH(info);
                    }
                }
            }
            return flag;
        }
    }
}
