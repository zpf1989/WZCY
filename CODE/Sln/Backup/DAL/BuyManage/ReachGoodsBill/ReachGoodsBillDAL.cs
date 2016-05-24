using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GentleUtil.DB;
using System.Data;
using OA.IDAL;
using OA.Model;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class ReachGoodsBillDAL : IReachGoodsBillDAL
    {
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="ReachGoodsBillCode">单据编号</param>
        /// <param name="CreateUserID">创建单据UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string ReachGoodsBillCode, string CreateUserID, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(ReachGoodsBillCode))
            {
                strSql.Append(" and ReachGoodsBillCode like'%" + ReachGoodsBillCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUserID))
            {
                strSql.Append(" and CreateUserID ='" + CreateUserID.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by ReachGoodsBillCode desc) as RowId ")
                .AppendLine(" from OA_ReachGoodsBill ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="ReachGoodsBillCode">单据编号</param>
        /// <param name="CreateUserID">创建单据UserID</param>
        /// <returns></returns>
        public int GetRowCounts(string ReachGoodsBillCode, string CreateUserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from OA_ReachGoodsBill ")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(ReachGoodsBillCode))
            {
                strSql.Append(" and ReachGoodsBillCode like'%" + ReachGoodsBillCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(CreateUserID))
            {
                strSql.Append(" and CreateUserID ='" + CreateUserID.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 新增到货单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddReachGoodsBill(ReachGoodsBill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_ReachGoodsBill(");
            strSql.Append("ReachGoodsBillID,ReachGoodsBillCode,BuyOrderCode,ReachGoodsDate,CreateBillTime,CreateUserID,InforPerson,Remark,BuyOrderID)");
            strSql.Append(" values (");
            strSql.Append("@ReachGoodsBillID,@ReachGoodsBillCode,@BuyOrderCode,@ReachGoodsDate,@CreateBillTime,@CreateUserID,@InforPerson,@Remark,@BuyOrderID)");
            SqlParameter[] parameters = {
					new SqlParameter("@ReachGoodsBillID", SqlDbType.VarChar,36),
					new SqlParameter("@ReachGoodsBillCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BuyOrderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReachGoodsDate", SqlDbType.Char,8),
					new SqlParameter("@CreateBillTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.VarChar,36),
					new SqlParameter("@InforPerson", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@BuyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.ReachGoodsBillID;
            parameters[1].Value = model.ReachGoodsBillCode;
            parameters[2].Value = model.BuyOrderCode;
            parameters[3].Value = model.ReachGoodsDate;
            parameters[4].Value = model.CreateBillTime;
            parameters[5].Value = model.CreateUserID;
            parameters[6].Value = model.InforPerson;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.BuyOrderID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                ReachGoodsBillItemDAL item = new ReachGoodsBillItemDAL();
                item.Save(model.ReachGoodsBillID);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            return 1;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">采购单据编号</param>
        /// <param name="InforPersonId">通知人UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4InfoPerson(string BuyOrderCode, string InforPersonId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like '%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(InforPersonId))
            {
                strSql.Append(" and InforPerson ='" + InforPersonId.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select R.*,B.BuyOrderDate,U.UserCode,U.UserName,row_number() OVER (order by R.BuyOrderCode desc) as RowId ")
                .AppendLine(" from OA_ReachGoodsBill R ")
                .AppendLine(" left join OA_BuyOrder B on B.BuyOrderID=R.BuyOrderID ")
                .AppendLine(" left join OA_User U on U.UserID=B.CreateUserID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">采购单据编号</param>
        /// <param name="InforPersonId">通知人UserID</param>
        /// <returns></returns>
        public int GetRowCounts4InfoPerson(string BuyOrderCode, string InforPersonId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from OA_ReachGoodsBill ")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(BuyOrderCode))
            {
                strSql.Append(" and BuyOrderCode like '%" + BuyOrderCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(InforPersonId))
            {
                strSql.Append(" and InforPerson ='" + InforPersonId.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
    }
}
