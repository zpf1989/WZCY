using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.Model;
using OA.IDAL;

namespace OA.DAL
{
    public class BuyBillDAL : IBuyBillDAL
    {
        #region 新增
        /// <summary>
        /// 新增采购单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddBuyBill(BuyBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BuyOrder(");
            strSql.Append("BuyOrderID,BuyOrderCode,BuyOrderDate,SupplierID,DeliveryDate,Creator,CreateTime,Editor,EditTime,FirstChecker,RecCompany,RecTel,RecFax,OrderState,Remark,SecondCheckerName,ReaderName)");
            strSql.Append(" values (");
            strSql.Append("@BuyOrderID,@BuyOrderCode,@BuyOrderDate,@SupplierID,@DeliveryDate,@Creator,@CreateTime,@Editor,@EditTime,@FirstChecker,@RecCompany,@RecTel,@RecFax,@OrderState,@Remark,@SecondCheckerName,@ReaderName)");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyOrderCode", SqlDbType.VarChar,60),
					new SqlParameter("@BuyOrderDate", SqlDbType.Char,8),
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36),
					new SqlParameter("@DeliveryDate", SqlDbType.Char,8),
					new SqlParameter("@Creator", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@RecCompany", SqlDbType.VarChar,255),
					new SqlParameter("@RecTel", SqlDbType.VarChar,30),
					new SqlParameter("@RecFax", SqlDbType.VarChar,30),
					new SqlParameter("@OrderState", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
					new SqlParameter("@ReaderName", SqlDbType.VarChar,255)};
            parameters[0].Value = model.BuyOrderID;
            parameters[1].Value = model.BuyOrderCode;
            parameters[2].Value = model.BuyOrderDate;
            parameters[3].Value = model.SupplierID;
            parameters[4].Value = model.DeliveryDate;
            parameters[5].Value = model.Creator;
            parameters[6].Value = model.CreateTime;
            parameters[7].Value = model.Editor;
            parameters[8].Value = model.EditTime;
            parameters[9].Value = model.FirstChecker;
            parameters[10].Value = model.RecCompany;
            parameters[11].Value = model.RecTel;
            parameters[12].Value = model.RecFax;
            parameters[13].Value = model.OrderState;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.SecondCheckerName;
            parameters[16].Value = model.ReaderName;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                BOSecondCheckDAL boSC = new BOSecondCheckDAL();
                int ccount = model.CList.Count;
                if (ccount > 0)
                {
                    for (int i = 0; i < ccount; i++)
                    {
                        BOSecondCheck sosc = model.CList[i] as BOSecondCheck;
                        boSC.Add(sosc);
                    }
                }

                BOReaderDAL rSC = new BOReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    for (int i = 0; i < rcount; i++)
                    {
                        BOReader bor = model.RList[i] as BOReader;
                        rSC.Add(bor);
                    }
                }

                BuyBillItemDAL item = new BuyBillItemDAL();
                item.Save(model.BuyOrderID, model.OldBuyOrderID);

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
        /// 修改采购单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateBuyBill(BuyBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyOrder set ");
            strSql.Append("BuyOrderCode=@BuyOrderCode,");
            strSql.Append("BuyOrderDate=@BuyOrderDate,");
            strSql.Append("SupplierID=@SupplierID,");
            strSql.Append("DeliveryDate=@DeliveryDate,");
            strSql.Append("Editor=@Editor,");
            strSql.Append("EditTime=@EditTime,");
            strSql.Append("FirstChecker=@FirstChecker,");
            strSql.Append("FirstCheckTime=@FirstCheckTime,");
            strSql.Append("FirstCheckView=@FirstCheckView,");
            strSql.Append("RecCompany=@RecCompany,");
            strSql.Append("RecTel=@RecTel,");
            strSql.Append("RecFax=@RecFax,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("SecondCheckerName=@SecondCheckerName,");
            strSql.Append("ReaderName=@ReaderName");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderCode", SqlDbType.VarChar,60),
					new SqlParameter("@BuyOrderDate", SqlDbType.Char,8),
					new SqlParameter("@SupplierID", SqlDbType.VarChar,36),
					new SqlParameter("@DeliveryDate", SqlDbType.Char,8),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@RecCompany", SqlDbType.VarChar,255),
					new SqlParameter("@RecTel", SqlDbType.VarChar,30),
					new SqlParameter("@RecFax", SqlDbType.VarChar,30),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
					new SqlParameter("@ReaderName", SqlDbType.VarChar,255),
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.BuyOrderCode;
            parameters[1].Value = model.BuyOrderDate;
            parameters[2].Value = model.SupplierID;
            parameters[3].Value = model.DeliveryDate;
            parameters[4].Value = model.Editor;
            parameters[5].Value = model.EditTime;
            parameters[6].Value = model.FirstChecker;
            parameters[7].Value = model.FirstCheckTime;
            parameters[8].Value = model.FirstCheckView;
            parameters[9].Value = model.RecCompany;
            parameters[10].Value = model.RecTel;
            parameters[11].Value = model.RecFax;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.SecondCheckerName;
            parameters[14].Value = model.ReaderName;
            parameters[15].Value = model.BuyOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                BOSecondCheckDAL boSC = new BOSecondCheckDAL();
                int socount = model.CList.Count;
                if (socount > 0)
                {
                    boSC.Delete(model.BuyOrderID);
                    for (int i = 0; i < socount; i++)
                    {
                        BOSecondCheck bosc = model.CList[i] as BOSecondCheck;
                        boSC.Add(bosc);
                    }
                }

                BOReaderDAL rSC = new BOReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    rSC.Delete(model.BuyOrderID);
                    for (int i = 0; i < rcount; i++)
                    {
                        BOReader sor = model.RList[i] as BOReader;
                        rSC.Add(sor);
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
        public bool UpdateCheckF(BuyBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyOrder set ");
            strSql.Append("FirstCheckTime=@FirstCheckTime,");
            strSql.Append("FirstCheckView=@FirstCheckView,");
            strSql.Append("OrderState=@OrderState");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@OrderState", SqlDbType.Char,1),
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.FirstCheckTime;
            parameters[1].Value = model.FirstCheckView;
            parameters[2].Value = model.OrderState;
            parameters[3].Value = model.BuyOrderID;

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
        public bool UpdateCheckS(BuyBill model, BOSecondCheck boc)
        {
            BOSecondCheckDAL bocDal = new BOSecondCheckDAL();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyOrder set ");
            strSql.Append("OrderState=@OrderState");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderState", SqlDbType.Char,1),
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.OrderState;
            parameters[1].Value = model.BuyOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                bocDal.Check(boc);
                if (bocDal.IsCheck(model.BuyOrderID))
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
        /// 删除采购订单
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Delete(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BuyOrder ");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyOrderID;

            int rows = 0;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                //删除分录
                BuyBillItemDAL item = new BuyBillItemDAL();
                item.Delete(BuyOrderID);

                //删除分阅人
                BOReaderDAL bor = new BOReaderDAL();
                bor.Delete(BuyOrderID);

                //删除复核人
                BOSecondCheckDAL bos = new BOSecondCheckDAL();
                bos.Delete(BuyOrderID);

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

        #region 取消提交
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        public bool UnSubmit(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyOrder set ");
            strSql.Append("OrderState='1',");
            strSql.Append("FirstChecker='',");
            strSql.Append("FirstCheckView='',");
            strSql.Append("SecondCheckerName='',");
            strSql.Append("ReaderName=''");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                BOSecondCheckDAL boSC = new BOSecondCheckDAL();
                boSC.Delete(BuyOrderID);

                BOReaderDAL bSC = new BOReaderDAL();
                bSC.Delete(BuyOrderID);

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

        #region 所有采购单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string BuyOrderCode, string OrderState, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(OrderState))
            {
                strSql.Append(" and OrderState ='" + OrderState.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,row_number() OVER (order by BuyOrderCode desc) as RowId ")
                .AppendLine(" from BuyOrder A ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string BuyOrderCode, string OrderState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from BuyOrder")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(OrderState))
            {
                strSql.Append(" and OrderState ='" + OrderState.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 我的采购单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="UserId">创建单据UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string BuyOrderCode, string OrderState, string UserId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(OrderState))
            {
                strSql.Append(" and OrderState ='" + OrderState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and A.Creator ='" + UserId.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,row_number() OVER (order by BuyOrderCode desc) as RowId ")
                .AppendLine(" from BuyOrder A ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="UserId">创建单据UserID</param>
        /// <returns></returns>
        public int GetRowCounts(string BuyOrderCode, string OrderState, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from BuyOrder")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(OrderState))
            {
                strSql.Append(" and OrderState ='" + OrderState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and Creator ='" + UserId.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 审核采购单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">初审UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string BuyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and FirstChecker ='" + UserId.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,row_number() OVER (order by BuyOrderCode desc) as RowId ")
                .AppendLine(" from BuyOrder A ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID) tmp ")
                .AppendLine(" where OrderState=2 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">初审UserID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string BuyOrderCode, string CreateUser, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax ")
            .AppendLine(" from BuyOrder A ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID) tmp ")
            .AppendLine(" where OrderState=2 ");

            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and FirstChecker ='" + UserId.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 批准采购单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">复审UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string BuyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and SecondChecker ='" + UserId.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,U2.UserName as SecondCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,SecondChecker,BOSecondCheckID,CheckFlag,row_number() OVER (order by BuyOrderCode desc) as RowId ")
                .AppendLine(" from BuyOrder A ")
                .AppendLine(" inner join BOSecondCheck BOC on BOC.BuyOrderID = A.BuyOrderID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join OA_User U2 on U2.UserID=BOC.SecondChecker ")
                .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID) tmp ")
                .AppendLine(" where OrderState=3 and CheckFlag = '0' ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">复审UserID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string BuyOrderCode, string CreateUser, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,U2.UserName as SecondCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,SecondChecker,BOSecondCheckID,CheckFlag ")
            .AppendLine(" from BuyOrder A ")
            .AppendLine(" inner join BOSecondCheck BOC on BOC.BuyOrderID = A.BuyOrderID ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join OA_User U2 on U2.UserID=BOC.SecondChecker ")
            .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID) tmp ")
            .AppendLine(" where OrderState=3 ");

            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and SecondChecker ='" + UserId.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 分阅采购单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4Read(string BuyOrderCode, string CreateUser, string Reader, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(Reader))
            {
                strSql.Append(" and ReaderID ='" + Reader.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,ReaderID,BOReadID,ReadFlag,row_number() OVER (order by BuyOrderCode desc) as RowId ")
                .AppendLine(" from BuyOrder A ")
                .AppendLine(" inner join BOReader BOR on BOR.BuyOrderID = A.BuyOrderID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID ) tmp ")
                .AppendLine(" where ReadFlag = '0' ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        public int GetRowCounts4Read(string BuyOrderCode, string CreateUser, string Reader)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,ReaderID,ReadFlag ")
            .AppendLine(" from BuyOrder A ")
            .AppendLine(" inner join BOReader BOR on BOR.BuyOrderID = A.BuyOrderID ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID ) tmp ")
            .AppendLine(" where ReadFlag = '0' ");

            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(Reader))
            {
                strSql.Append(" and ReaderID ='" + Reader.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 获取采购订单实体类
        /// <summary>
        /// 获取采购订单实体类
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public DataSet GetModel(string BuyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax ");
            strSql.AppendLine(" from BuyOrder A ");
            strSql.AppendLine(" left join OA_User U on U.UserID=A.Creator ");
            strSql.AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ");
            strSql.AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID ");
            strSql.AppendLine(" where A.BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyOrderID;

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

        /// <summary>
        /// 检查是否已经复审了
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        public int CheckedS(string BuyOrderID,out string Mes)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from OA_BuyOrder ");
            strSql.AppendLine(" where BuyOrderID=@BuyOrderID and OrderState='3' and SecondCheckView is null ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = BuyOrderID;

            int obj = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj <= 0)
            {
                Mes = "请确认单据是否通过了复审！";
                return 0;
            }
            else
            {
                Mes = "单据已经通过了复审！是否要关闭？";
                return (int)obj;
            }
        }
        /// <summary>
        /// 修改单据状态
        /// </summary>
        /// <param name="SaleState">单据状态
        /// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭</param>
        /// <param name="SaleOrderID">单据ID</param>
        /// <returns></returns>
        public int Submit(string OrderState, string BuyBillID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyOrder set ");
            strSql.Append("OrderState=@OrderState");
            strSql.Append(" where BuyOrderID=@BuyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderState", SqlDbType.Char,1),
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = OrderState;
            parameters[1].Value = BuyBillID;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


    }
}
