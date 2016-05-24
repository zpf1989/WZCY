using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using OA.Model;
using GentleUtil.DB;
using OA.IDAL;

namespace OA.DAL
{
    public class SaleOrderDAL : ISaleOrderDAL
    {
        #region 新增
        /// <summary>
        /// 新增销售订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddSaleOrder(SaleOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SaleOrder(");
            strSql.Append("SaleOrderID,SaleOrderCode,BillTypeID,MaterialID,SaleUnitID,ClientID,SaleDate,SaleQty,SalePrice,SaleCost,FinishDate,Creator,CreateTime,Editor,EditTime,FirstChecker,FirstCheckTime,FirstCheckView,RoutingID,SaleState,Remark,SecondCheckerName,ReaderName,Routing)");
            strSql.Append(" values (");
            strSql.Append("@SaleOrderID,@SaleOrderCode,@BillTypeID,@MaterialID,@SaleUnitID,@ClientID,@SaleDate,@SaleQty,@SalePrice,@SaleCost,@FinishDate,@Creator,@CreateTime,@Editor,@EditTime,@FirstChecker,@FirstCheckTime,@FirstCheckView,@RoutingID,@SaleState,@Remark,@SecondCheckerName,@ReaderName,@Routing)");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleOrderCode", SqlDbType.VarChar,60),
					new SqlParameter("@BillTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@ClientID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleDate", SqlDbType.Char,8),
					new SqlParameter("@SaleQty", SqlDbType.Decimal,13),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,13),
					new SqlParameter("@SaleCost", SqlDbType.Decimal,13),
					new SqlParameter("@FinishDate", SqlDbType.Char,8),
					new SqlParameter("@Creator", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@RoutingID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleState", SqlDbType.Char,1),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
                    new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
                    new SqlParameter("@ReaderName", SqlDbType.VarChar,255),
					new SqlParameter("@Routing", SqlDbType.VarChar,1024)};
            parameters[0].Value = model.SaleOrderID;
            parameters[1].Value = model.SaleOrderCode;
            parameters[2].Value = model.BillTypeID;
            parameters[3].Value = model.MaterialID;
            parameters[4].Value = model.SaleUnitID;
            parameters[5].Value = model.ClientID;
            parameters[6].Value = model.SaleDate;
            parameters[7].Value = model.SaleQty;
            parameters[8].Value = model.SalePrice;
            parameters[9].Value = model.SaleCost;
            parameters[10].Value = model.FinishDate;
            parameters[11].Value = model.Creator;
            parameters[12].Value = model.CreateTime;
            parameters[13].Value = model.Editor;
            parameters[14].Value = model.EditTime;
            parameters[15].Value = model.FirstChecker;
            parameters[16].Value = model.FirstCheckTime;
            parameters[17].Value = model.FirstCheckView;
            parameters[18].Value = model.RoutingID;
            parameters[19].Value = model.SaleState;
            parameters[20].Value = model.Remark;
            parameters[21].Value = model.SecondCheckerName;
            parameters[22].Value = model.ReaderName;
            parameters[23].Value = model.Routing;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                SOSecondCheckDAL soSC = new SOSecondCheckDAL();
                int socount = model.SOList.Count;
                if (socount > 0)
                {
                    for (int i = 0; i < socount;i++ )
                    {
                        SOSecondCheck sosc = model.SOList[i] as SOSecondCheck;
                        soSC.Add(sosc);
                    }
                }

                SOReaderDAL rSC = new SOReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    for (int i = 0; i < rcount; i++)
                    {
                        SOReader sor = model.RList[i] as SOReader;
                        rSC.Add(sor);
                    }
                }

                SaleOrderItemDAL item = new SaleOrderItemDAL();
                item.Save(model.SaleOrderID, model.OldSaleOrderID);

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
        /// 修改销售订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSaleOrder(SaleOrder model)
        {
            StringBuilder strSql = new StringBuilder();;
            strSql.Append("update SaleOrder set ");
            strSql.Append("BillTypeID=@BillTypeID,");
            strSql.Append("MaterialID=@MaterialID,");
            strSql.Append("SaleUnitID=@SaleUnitID,");
            strSql.Append("ClientID=@ClientID,");
            strSql.Append("SaleDate=@SaleDate,");
            strSql.Append("SaleQty=@SaleQty,");
            strSql.Append("SalePrice=@SalePrice,");
            strSql.Append("SaleCost=@SaleCost,");
            strSql.Append("FinishDate=@FinishDate,");
            strSql.Append("Editor=@Editor,");
            strSql.Append("EditTime=@EditTime,");
            strSql.Append("FirstChecker=@FirstChecker,");
            strSql.Append("RoutingID=@RoutingID,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("SecondCheckerName=@SecondCheckerName,");
            strSql.Append("ReaderName=@ReaderName,");
            strSql.Append("Routing=@Routing");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BillTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@ClientID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleDate", SqlDbType.Char,8),
					new SqlParameter("@SaleQty", SqlDbType.Decimal,13),
					new SqlParameter("@SalePrice", SqlDbType.Decimal,13),
					new SqlParameter("@SaleCost", SqlDbType.Decimal,13),
					new SqlParameter("@FinishDate", SqlDbType.Char,8),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@RoutingID", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
					new SqlParameter("@ReaderName", SqlDbType.VarChar,255),
					new SqlParameter("@Routing", SqlDbType.VarChar,1024),
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.BillTypeID;
            parameters[1].Value = model.MaterialID;
            parameters[2].Value = model.SaleUnitID;
            parameters[3].Value = model.ClientID;
            parameters[4].Value = model.SaleDate;
            parameters[5].Value = model.SaleQty;
            parameters[6].Value = model.SalePrice;
            parameters[7].Value = model.SaleCost;
            parameters[8].Value = model.FinishDate;
            parameters[9].Value = model.Editor;
            parameters[10].Value = model.EditTime;
            parameters[11].Value = model.FirstChecker;
            parameters[12].Value = model.RoutingID;
            parameters[13].Value = model.Remark;
            parameters[14].Value = model.SecondCheckerName;
            parameters[15].Value = model.ReaderName;
            parameters[16].Value = model.Routing;
            parameters[17].Value = model.SaleOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                SOSecondCheckDAL soSC = new SOSecondCheckDAL();
                int socount = model.SOList.Count;
                if (socount > 0)
                {
                    soSC.Delete(model.SaleOrderID);
                    for (int i = 0; i < socount; i++)
                    {
                        SOSecondCheck sosc = model.SOList[i] as SOSecondCheck;
                        soSC.Add(sosc);
                    }
                }

                SOReaderDAL rSC = new SOReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    rSC.Delete(model.SaleOrderID);
                    for (int i = 0; i < rcount; i++)
                    {
                        SOReader sor = model.RList[i] as SOReader;
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
        public bool FirstCheck(SaleOrder model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SaleOrder set ");
            strSql.Append("FirstCheckTime=@FirstCheckTime,");
            strSql.Append("FirstCheckView=@FirstCheckView,");
            strSql.Append("SaleState=@SaleState");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@SaleState", SqlDbType.Char,1),
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.FirstCheckTime;
            parameters[1].Value = model.FirstCheckView;
            parameters[2].Value = model.SaleState;
            parameters[3].Value = model.SaleOrderID;

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
        public bool SecondCheck(SaleOrder model,SOSecondCheck soc)
        {
            SOSecondCheckDAL socDal = new SOSecondCheckDAL();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SaleOrder set ");
            strSql.Append("SaleState=@SaleState");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleState", SqlDbType.Char,1),
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.SaleState;
            parameters[1].Value = model.SaleOrderID;


            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                socDal.Check(soc);
                if (socDal.IsCheck(model.SaleOrderID))
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
        /// 删除销售订单
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool Delete(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SaleOrder ");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = SaleOrderID;

            int rows = 0;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                //删除分录
                SaleOrderItemDAL item = new SaleOrderItemDAL();
                item.Delete(SaleOrderID);

                //删除分阅人
                SOReaderDAL sor = new SOReaderDAL();
                sor.Delete(SaleOrderID);

                //删除复核人
                SOSecondCheckDAL sos = new SOSecondCheckDAL();
                sos.Delete(SaleOrderID);

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
        /// <param name="SaleOrderID">单据ID</param>
        /// <returns></returns>
        public bool UnSubmit(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SaleOrder set ");
            strSql.Append("SaleState='1',");
            strSql.Append("FirstChecker='',");
            strSql.Append("FirstCheckView='',");
            strSql.Append("SecondCheckerName='',");
            strSql.Append("ReaderName=''");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = SaleOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                SOSecondCheckDAL soSC = new SOSecondCheckDAL();
                soSC.Delete(SaleOrderID);

                SOReaderDAL rSC = new SOReaderDAL();
                rSC.Delete(SaleOrderID);

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

        #region 所有拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SaleOrderCode, string SaleState, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SaleState))
            {
                strSql.Append(" and SaleState ='" + SaleState.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientName,row_number() OVER (order by SaleOrderCode desc) as RowId ")
                .AppendLine(" from SaleOrder A ")
                .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string SaleOrderCode, string SaleState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from SaleOrder")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SaleState))
            {
                strSql.Append(" and SaleState ='" + SaleState.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 我的拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SaleOrderCode, string SaleState, string Creator, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SaleState))
            {
                strSql.Append(" and SaleState ='" + SaleState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(Creator))
            {
                strSql.Append(" and A.Creator ='" + Creator.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientName,row_number() OVER (order by SaleOrderCode desc) as RowId ")
                .AppendLine(" from SaleOrder A ")
                .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string SaleOrderCode, string SaleState, string Creator)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from SaleOrder")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SaleState))
            {
                strSql.Append(" and SaleState ='" + SaleState.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(Creator))
            {
                strSql.Append(" and Creator ='" + Creator.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 审核拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string SaleOrderCode, string CreateUser, string FirstChecker, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(FirstChecker))
            {
                strSql.Append(" and FirstChecker ='" + FirstChecker.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientName,MU.UnitName,row_number() OVER (order by SaleOrderCode desc) as RowId ")
                .AppendLine(" from SaleOrder A ")
                .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=A.SaleUnitID) tmp ")
                .AppendLine(" where SaleState=2 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string SaleOrderCode, string CreateUser, string FirstChecker)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientName,MU.UnitName ")
            .AppendLine(" from SaleOrder A ")
            .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
            .AppendLine(" left join MeasureUnits MU on MU.UnitID=A.SaleUnitID) tmp ")
            .AppendLine(" where SaleState=2 ");

            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(FirstChecker))
            {
                strSql.Append(" and FirstChecker ='" + FirstChecker.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 批准拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string SaleOrderCode, string CreateUser, string SecondChecker, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SecondChecker))
            {
                strSql.Append(" and SecondChecker ='" + SecondChecker.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,U2.UserName as SecondCheckeName,C.ClientName,MU.UnitName,SecondChecker,SOSecondCheckID,CheckFlag,row_number() OVER (order by SaleOrderCode desc) as RowId ")
                .AppendLine(" from SaleOrder A ")
                .AppendLine(" inner join SOSecondCheck SOC on SOC.SaleOrderID = A.SaleOrderID ")
                .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join OA_User U2 on U2.UserID=SOC.SecondChecker ")
                .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=A.SaleUnitID) tmp ")
                .AppendLine(" where SaleState=3 and CheckFlag = '0' ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string SaleOrderCode, string CreateUser, string SecondChecker)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,U2.UserName as SecondCheckeName,C.ClientName,MU.UnitName,SecondChecker,CheckFlag ")
            .AppendLine(" from SaleOrder A ")
            .AppendLine(" inner join SOSecondCheck SOC on SOC.SaleOrderID = A.SaleOrderID ")
            .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join OA_User U2 on U2.UserID=SOC.SecondChecker ")
            .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
            .AppendLine(" left join MeasureUnits MU on MU.UnitID=A.SaleUnitID) tmp ")
            .AppendLine(" where SaleState=3 and CheckFlag = '0' ");

            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUser))
            {
                strSql.Append(" and CreateUser like'%" + CreateUser.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SecondChecker))
            {
                strSql.Append(" and SecondChecker ='" + SecondChecker.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 分阅拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4Read(string SaleOrderCode, string CreateUser, string Reader, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
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
                .AppendLine(" (select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientName,MU.UnitName,ReaderID,SOReadID,ReadFlag,row_number() OVER (order by SaleOrderCode desc) as RowId ")
                .AppendLine(" from SaleOrder A ")
                .AppendLine(" inner join SOReader SOR on SOR.SaleOrderID = A.SaleOrderID ")
                .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=A.SaleUnitID) tmp ")
                .AppendLine(" where ReadFlag = '0' ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        public int GetRowCounts4Read(string SaleOrderCode, string CreateUser, string Reader)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" (select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientName,MU.UnitName,ReaderID,ReadFlag ")
            .AppendLine(" from SaleOrder A ")
            .AppendLine(" inner join SOReader SOR on SOR.SaleOrderID = A.SaleOrderID ")
            .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
            .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
            .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
            .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
            .AppendLine(" left join MeasureUnits MU on MU.UnitID=A.SaleUnitID) tmp ")
            .AppendLine(" where ReadFlag = '0' ");

            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
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

        #region 获取销售订单模型
        /// <summary>
        /// 获取销售订单模型
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public DataSet GetModel(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientCode,C.ClientName,MU.UnitName,MU.UnitCode ");
            strSql.Append(" from SaleOrder A ");
            strSql.Append(" left join Materials M on M.MaterialID=A.MaterialID ");
            strSql.Append(" left join OA_User U on U.UserID=A.Creator ");
            strSql.Append(" left join OA_User U1 on U1.UserID=A.FirstChecker ");
            strSql.Append(" left join Client C on C.ClientID=A.ClientID ");
            strSql.Append(" left join MeasureUnits MU on MU.UnitID=A.SaleUnitID ");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = SaleOrderID;

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

        #region 所有拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4Group(string Creator, string FirstChecker, string SecondChecker, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(Creator))
            {
                strSql.Append(" and Creator like'%" + Creator.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(FirstChecker))
            {
                strSql.Append(" and FirstChecker ='" + FirstChecker.Trim() + "' ");
            }

            if (!string.IsNullOrEmpty(FirstChecker))
            {
                strSql.Append(" and FirstChecker ='" + FirstChecker.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select A.*,M.MaterialCode,M.MaterialName,M.Specs,U.UserName as CreateUser,U1.UserName as FirstCheckeName,C.ClientName,row_number() OVER (order by SaleOrderCode desc) as RowId ")
                .AppendLine(" from SaleOrder A ")
                .AppendLine(" left join Materials M on M.MaterialID=A.MaterialID ")
                .AppendLine(" left join OA_User U on U.UserID=A.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=A.FirstChecker ")
                .AppendLine(" left join Client C on C.ClientID=A.ClientID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <returns></returns>
        public int GetRowCountsGroup(string SaleOrderCode, string SaleState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from SaleOrder")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(SaleOrderCode))
            {
                strSql.Append(" and SaleOrderCode like'%" + SaleOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(SaleState))
            {
                strSql.Append(" and SaleState ='" + SaleState.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

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
        public int Submit(string SaleState, string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SaleOrder set ");
            strSql.Append("SaleState=@SaleState");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleState", SqlDbType.Char,1),
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = SaleState;
            parameters[1].Value = SaleOrderID;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 下达采购订单
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public int DoSend(string SaleOrderID,string UserID)
        {
            string id = System.Guid.NewGuid().ToString();
            string code = "CG" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
            string buyorderdate = DateTime.Now.ToString("yyyyMMdd");

            //表头
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("insert into BuyOrder ");
            strSql.AppendLine("(BuyOrderID,BuyOrderCode,BuyOrderDate,Creator,CreateTime,OrderState) ");
            strSql.AppendLine("values ");
            strSql.AppendLine("(@BuyOrderID,@BuyOrderCode,@BuyOrderDate,@Creator,GETDATE(),'1'); ");
            SqlParameter[] parameters = {
                    new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36),
                    new SqlParameter("@BuyOrderCode", SqlDbType.VarChar,60),
                    new SqlParameter("@Creator", SqlDbType.VarChar,36),
                    new SqlParameter("@BuyOrderDate", SqlDbType.Char,8)};
            parameters[0].Value = id;
            parameters[1].Value = code;
            parameters[2].Value = UserID;
            parameters[3].Value = buyorderdate;

            //表体
            strSql.AppendLine("insert into BuyOrderItem ");
            strSql.AppendLine("(BuyOrderItemID,BuyOrderID,MaterialID,BuyQty,BuyCost,BuyUnitID) ");
            strSql.AppendLine("select NEWID(),'" + id + "',MaterialID,PlanQty,PlanCost,PrimaryUnitID from SaleOrderItem ");
            strSql.AppendLine("where SaleOrderID = '" + SaleOrderID + "'; ");

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }

    }
}
