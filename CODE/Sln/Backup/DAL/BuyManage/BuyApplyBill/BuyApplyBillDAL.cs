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
    public class BuyApplyBillDAL : IBuyApplyBillDAL
    {
        #region 新增
        /// <summary>
        /// 新增采购单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddBuyApplyBill(BuyApplyBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BuyApplyOrder(");
            strSql.Append("BuyApplyOrderID,BuyApplyOrderCode,BuyApplyOrderDate,SupplierID,DeliveryDate,Creator,CreateTime,Editor,EditTime,FirstChecker,RecCompany,RecTel,RecFax,OrderState,Remark,SecondCheckerName,ReaderName)");
            strSql.Append(" values (");
            strSql.Append("@BuyApplyOrderID,@BuyApplyOrderCode,@BuyApplyOrderDate,@SupplierID,@DeliveryDate,@Creator,@CreateTime,@Editor,@EditTime,@FirstChecker,@RecCompany,@RecTel,@RecFax,@OrderState,@Remark,@SecondCheckerName,@ReaderName)");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyApplyOrderCode", SqlDbType.VarChar,60),
					new SqlParameter("@BuyApplyOrderDate", SqlDbType.Char,8),
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
            parameters[0].Value = model.BuyApplyOrderID;
            parameters[1].Value = model.BuyApplyOrderCode;
            parameters[2].Value = model.BuyApplyOrderDate;
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

                BOASecondCheckDAL boSC = new BOASecondCheckDAL();
                int ccount = model.CList.Count;
                if (ccount > 0)
                {
                    for (int i = 0; i < ccount; i++)
                    {
                        BOASecondCheck sosc = model.CList[i] as BOASecondCheck;
                        boSC.Add(sosc);
                    }
                }

                BOAReaderDAL rSC = new BOAReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    for (int i = 0; i < rcount; i++)
                    {
                        BOAReader bor = model.RList[i] as BOAReader;
                        rSC.Add(bor);
                    }
                }

                BuyApplyBillItemDAL item = new BuyApplyBillItemDAL();
                item.Save(model.BuyApplyOrderID, model.OldBuyOrderID);

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
        public bool UpdateBuyApplyBill(BuyApplyBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyApplyOrder set ");
            strSql.Append("BuyApplyOrderCode=@BuyApplyOrderCode,");
            strSql.Append("BuyApplyOrderDate=@BuyApplyOrderDate,");
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
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderCode", SqlDbType.VarChar,60),
					new SqlParameter("@BuyApplyOrderDate", SqlDbType.Char,8),
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
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.BuyApplyOrderCode;
            parameters[1].Value = model.BuyApplyOrderDate;
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
            parameters[15].Value = model.BuyApplyOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                BOASecondCheckDAL boSC = new BOASecondCheckDAL();
                int socount = model.CList.Count;
                if (socount > 0)
                {
                    boSC.Delete(model.BuyApplyOrderID);
                    for (int i = 0; i < socount; i++)
                    {
                        BOASecondCheck bosc = model.CList[i] as BOASecondCheck;
                        boSC.Add(bosc);
                    }
                }

                BOAReaderDAL rSC = new BOAReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    rSC.Delete(model.BuyApplyOrderID);
                    for (int i = 0; i < rcount; i++)
                    {
                        BOAReader sor = model.RList[i] as BOAReader;
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
        public bool UpdateCheckF(BuyApplyBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyApplyOrder set ");
            strSql.Append("FirstCheckTime=@FirstCheckTime,");
            strSql.Append("FirstCheckView=@FirstCheckView,");
            strSql.Append("OrderState=@OrderState");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@OrderState", SqlDbType.Char,1),
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.FirstCheckTime;
            parameters[1].Value = model.FirstCheckView;
            parameters[2].Value = model.OrderState;
            parameters[3].Value = model.BuyApplyOrderID;

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
        public bool UpdateCheckS(BuyApplyBill model, BOASecondCheck boc)
        {
            BOASecondCheckDAL bocDal = new BOASecondCheckDAL();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyApplyOrder set ");
            strSql.Append("OrderState=@OrderState");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderState", SqlDbType.Char,1),
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.OrderState;
            parameters[1].Value = model.BuyApplyOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                bocDal.Check(boc);
                if (bocDal.IsCheck(model.BuyApplyOrderID))
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
        public bool Delete(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BuyApplyOrder ");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyApplyOrderID;

            int rows = 0;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                //删除分录
                BuyApplyBillItemDAL item = new BuyApplyBillItemDAL();
                item.Delete(BuyApplyOrderID);

                //删除分阅人
                BOAReaderDAL bor = new BOAReaderDAL();
                bor.Delete(BuyApplyOrderID);

                //删除复核人
                BOASecondCheckDAL bos = new BOASecondCheckDAL();
                bos.Delete(BuyApplyOrderID);

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
        public bool UnSubmit(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyApplyOrder set ");
            strSql.Append("OrderState='1',");
            strSql.Append("FirstChecker='',");
            strSql.Append("FirstCheckView='',");
            strSql.Append("SecondCheckerName='',");
            strSql.Append("ReaderName=''");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyApplyOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                BOASecondCheckDAL boSC = new BOASecondCheckDAL();
                boSC.Delete(BuyApplyOrderID);

                BOAReaderDAL bSC = new BOAReaderDAL();
                bSC.Delete(BuyApplyOrderID);

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
        public DataSet GetPageList(string BuyApplyOrderCode, string OrderState, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(OrderState))
            {
                strSql.Append(" and OrderState ='" + OrderState.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,row_number() OVER (order by BuyApplyOrderCode desc) as RowId ")
                .AppendLine(" from BuyApplyOrder A ")
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
        public int GetRowCounts(string BuyApplyOrderCode, string OrderState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from BuyApplyOrder")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
        public DataSet GetPageList(string BuyApplyOrderCode, string OrderState, string UserId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
            tmpSQL.AppendLine(" with m as(select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,row_number() OVER (order by BuyApplyOrderCode desc) as RowId ")
                .AppendLine(" from BuyApplyOrder A ")
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
        public int GetRowCounts(string BuyApplyOrderCode, string OrderState, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from BuyApplyOrder")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
        public DataSet GetPageList4FirstCheck(string BuyApplyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
                .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,row_number() OVER (order by BuyApplyOrderCode desc) as RowId ")
                .AppendLine(" from BuyApplyOrder A ")
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
        public int GetRowCounts4FirstCheck(string BuyApplyOrderCode, string CreateUser, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax ")
            .AppendLine(" from BuyApplyOrder A ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID) tmp ")
            .AppendLine(" where OrderState=2 ");

            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
        public DataSet GetPageList4SecondCheck(string BuyApplyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
                .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,U2.UserName as SecondCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,SecondChecker,BOASecondCheckID,CheckFlag,row_number() OVER (order by BuyApplyOrderCode desc) as RowId ")
                .AppendLine(" from BuyApplyOrder A ")
                .AppendLine(" inner join BOASecondCheck BOC on BOC.BuyApplyOrderID = A.BuyApplyOrderID ")
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
        public int GetRowCounts4SecondCheck(string BuyApplyOrderCode, string CreateUser, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,U2.UserName as SecondCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,SecondChecker,BOASecondCheckID,CheckFlag ")
            .AppendLine(" from BuyApplyOrder A ")
            .AppendLine(" inner join BOASecondCheck BOC on BOC.BuyApplyOrderID = A.BuyApplyOrderID ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join OA_User U2 on U2.UserID=BOC.SecondChecker ")
            .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID) tmp ")
            .AppendLine(" where OrderState=3 ");

            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
        public DataSet GetPageList4Read(string BuyApplyOrderCode, string CreateUser, string Reader, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
                .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax,ReaderID,BOAReadID,ReadFlag,row_number() OVER (order by BuyApplyOrderCode desc) as RowId ")
                .AppendLine(" from BuyApplyOrder A ")
                .AppendLine(" inner join BOAReader BOR on BOR.BuyApplyOrderID = A.BuyApplyOrderID ")
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
        public int GetRowCounts4Read(string BuyApplyOrderCode, string CreateUser, string Reader)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,ReaderID,ReadFlag ")
            .AppendLine(" from BuyApplyOrder A ")
            .AppendLine(" inner join BOAReader BOR on BOR.BuyApplyOrderID = A.BuyApplyOrderID ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID ) tmp ")
            .AppendLine(" where ReadFlag = '0' ");

            if (!string.IsNullOrEmpty(BuyApplyOrderCode))
            {
                strSql.Append(" and BuyApplyOrderCode like'%" + BuyApplyOrderCode.Trim() + "%' ");
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
        public DataSet GetModel(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select A.*,U.UserName as CreateUser,U1.UserName as FirstCheckeName,S.SupplierCode,S.SupplierName,S.Contactor,S.Tel,S.Fax ");
            strSql.AppendLine(" from BuyApplyOrder A ");
            strSql.AppendLine(" left join OA_User U on U.UserID=A.Creator ");
            strSql.AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ");
            strSql.AppendLine(" left join Supplier S on S.SupplierID=A.SupplierID ");
            strSql.AppendLine(" where A.BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyApplyOrderID;

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
        public int CheckedS(string BuyApplyOrderID, out string Mes)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from OA_BuyOrder ");
            strSql.AppendLine(" where BuyOrderID=@BuyOrderID and OrderState='3' and SecondCheckView is null ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = BuyApplyOrderID;

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
        public int Submit(string OrderState, string BuyApplyBillID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyApplyOrder set ");
            strSql.Append("OrderState=@OrderState");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderState", SqlDbType.Char,1),
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = OrderState;
            parameters[1].Value = BuyApplyBillID;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }


    }
}
