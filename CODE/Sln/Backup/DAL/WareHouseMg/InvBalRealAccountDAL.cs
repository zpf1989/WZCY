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
    public class InvBalRealAccountDAL : IInvBalRealAccountDAL
    {
        /// <summary>
        /// 导入新入库物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int NewIn(InvBalRealAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into InvBalRealAccount(");
            strSql.Append("ID,WareHouseID,MaterialID,CurQtyBalance)");
            strSql.Append(" values (");
            strSql.Append("@ID,@WareHouseID,@MaterialID,@CurQtyBalance)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.VarChar,36),
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@CurQtyBalance", SqlDbType.Decimal,13)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.WareHouseID;
            parameters[2].Value = model.MaterialID;
            parameters[3].Value = model.CurQtyBalance;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool OutWH(InvBalRealAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update InvBalRealAccount set ");
            strSql.Append("CurQtyBalance=CurQtyBalance - @CurQtyBalance");
            strSql.Append(" where WareHouseID=@WareHouseID and MaterialID=@MaterialID");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@CurQtyBalance", SqlDbType.Decimal,13)};
            parameters[0].Value = model.WareHouseID;
            parameters[1].Value = model.MaterialID;
            parameters[2].Value = model.CurQtyBalance;

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
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InWH(InvBalRealAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update InvBalRealAccount set ");
            strSql.Append("CurQtyBalance=CurQtyBalance + @CurQtyBalance");
            strSql.Append(" where WareHouseID=@WareHouseID and MaterialID=@MaterialID");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@CurQtyBalance", SqlDbType.Decimal,13)};
            parameters[0].Value = model.WareHouseID;
            parameters[1].Value = model.MaterialID;
            parameters[2].Value = model.CurQtyBalance;

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
        /// <summary>
        /// 修改入库数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCurQtyBalance(InvBalRealAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update InvBalRealAccount set ");
            strSql.Append("CurQtyBalance=@CurQtyBalance");
            strSql.Append(" where MaterialID=@MaterialID");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@CurQtyBalance", SqlDbType.Decimal,13)};
            parameters[0].Value = model.MaterialID;
            parameters[1].Value = model.CurQtyBalance;

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
        /// <summary>
        /// 判断是否在库存里次物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsNewMaterials(InvBalRealAccount model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from InvBalRealAccount ");
            strSql.Append(" where WareHouseID=@WareHouseID and MaterialID=@MaterialID");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.WareHouseID;
            parameters[1].Value = model.MaterialID;

            int rows = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            //if (!string.IsNullOrEmpty(BuyOrderCode))
            //{
            //    strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            //}
            //if (!string.IsNullOrEmpty(OrderState))
            //{
            //    strSql.Append(" and OrderState ='" + OrderState.Trim() + "' ");
            //}
            //if (!string.IsNullOrEmpty(UserId))
            //{
            //    strSql.Append(" and CreateUserID ='" + UserId.Trim() + "' ");
            //}
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select I.*,M.MaterialCode,M.MaterialName,M.Price,M.Specs,MU.UnitCode,MU.UnitName,MC.MaterialClassName,W.WareHouseCode,W.WareHouseName,row_number() OVER (order by M.MaterialCode desc) as RowId ")
                .AppendLine(" from InvBalRealAccount I ")
                .AppendLine(" left join Materials M on M.MaterialID=I.MaterialID ")
                .AppendLine(" left join WareHouse W on W.WareHouseID = I.WareHouseID ")
                .AppendLine(" left join MaterialClass MC on MC.MaterialClassID=M.MaterialClassID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=M.PrimaryUnitID ")
                .AppendLine(" where 1=1 ")
                //.AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from InvBalRealAccount");

            //if (!string.IsNullOrEmpty(BuyOrderCode))
            //{
            //    strSql.Append(" and BuyOrderCode like'%" + BuyOrderCode.Trim() + "%' ");
            //}
            //if (!string.IsNullOrEmpty(OrderState))
            //{
            //    strSql.Append(" and OrderState ='" + OrderState.Trim() + "' ");
            //}
            //if (!string.IsNullOrEmpty(UserId))
            //{
            //    strSql.Append(" and CreateUserID ='" + UserId.Trim() + "' ");
            //}

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="MaterialName"></param>
        /// <param name="MaterialClassID"></param>
        /// <param name="WareHouseID"></param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string MaterialCode, string MaterialName, string MaterialClassID, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(MaterialName))
            {
                strSql.Append(" and MaterialName like'%" + MaterialName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialCode))
            {
                strSql.Append(" and MaterialCode ='" + MaterialCode.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MaterialClassID.Trim()))
            {
                strSql.Append(" and MC.MaterialClassID ='" + MaterialClassID.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select M.*,MC.MaterialClassCode,MC.MaterialClassName,MT.MaterialTypeCode,MT.MaterialTypeName,MU.UnitCode,MU.UnitName,W.WareHouseName,Inv.CurQtyBalance,row_number() OVER (order by M.MaterialCode desc) as RowId ")
                .AppendLine(" from Materials M ")
                .AppendLine(" left join InvBalRealAccount Inv on M.MaterialID=Inv.MaterialID ")
                .AppendLine(" left join WareHouse W on W.WareHouseID = Inv.WareHouseID ")
                .AppendLine(" left join MaterialClass MC on MC.MaterialClassID=M.MaterialClassID ")
                .AppendLine(" left join MaterialType MT on MT.MaterialTypeID=M.MaterialTypeID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=M.PrimaryUnitID ")
                .AppendLine(" where not exists (select * from InvBalRealAccount where M.MaterialID=InvBalRealAccount.MaterialID)  ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="MaterialName"></param>
        /// <param name="MaterialClassID"></param>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public int GetRowCounts(string MaterialCode, string MaterialName, string MaterialClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ( ")
                .AppendLine(" select M.*,MC.MaterialClassCode,MC.MaterialClassName,MT.MaterialTypeCode,MT.MaterialTypeName,MU.UnitCode,MU.UnitName,W.WareHouseName,Inv.CurQtyBalance ")
                .AppendLine(" from Materials M ")
                .AppendLine(" left join InvBalRealAccount Inv on M.MaterialID=Inv.MaterialID ")
                .AppendLine(" left join WareHouse W on W.WareHouseID = Inv.WareHouseID ")
                .AppendLine(" left join MaterialClass MC on MC.MaterialClassID=M.MaterialClassID ")
                .AppendLine(" left join MaterialType MT on MT.MaterialTypeID=M.MaterialTypeID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=M.PrimaryUnitID ")
                .AppendLine(" where not exists (select * from InvBalRealAccount where M.MaterialID=InvBalRealAccount.MaterialID) ");

            if (!string.IsNullOrEmpty(MaterialName))
            {
                strSql.Append(" and MaterialName like'%" + MaterialName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialCode))
            {
                strSql.Append(" and MaterialCode ='" + MaterialCode.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(MaterialClassID.Trim()))
            {
                strSql.Append(" and MC.MaterialClassID ='" + MaterialClassID.Trim() + "' ");
            }
            strSql.Append(" )tmp ");

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

    }
}
