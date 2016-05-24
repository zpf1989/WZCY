using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;
using OA.Model;

namespace OA.DAL
{
    public class SaleOrderItemDAL : ISaleOrderItemDAL
    {
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select Item.*,M.MaterialCode,M.MaterialName,M.Specs,M.Price,MU.UnitCode,MU.UnitName from SaleOrderItem Item ");
            strSql.AppendLine(" left join Materials M on M.MaterialID=Item.MaterialID ");
            strSql.AppendLine(" left join MeasureUnits MU on MU.UnitID=Item.PrimaryUnitID ");
            strSql.AppendLine(" where SaleOrderID is null ");

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from SaleOrderItem where SaleOrderID is null ");

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
        /// <param name="SaleOrderID">销售订单ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SaleOrderID, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select Item.*,M.MaterialCode,M.MaterialName,M.Specs,M.Price,MU.UnitCode,MU.UnitName from SaleOrderItem Item ");
            strSql.AppendFormat(" left join Materials M on M.MaterialID=Item.MaterialID ");
            strSql.AppendFormat(" left join MeasureUnits MU on MU.UnitID=Item.PrimaryUnitID ");
            strSql.AppendFormat(" where SaleOrderID = '{0}' ", SaleOrderID);

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderID">销售订单ID</param>
        /// <returns></returns>
        public int GetRowCounts(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select COUNT(*) from SaleOrderItem where SaleOrderID = '{0}' ", SaleOrderID);

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertItem(SaleOrderItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SaleOrderItem(");
            strSql.Append("SaleOrderItemID,SaleOrderID,MaterialID,PlanQty,PlanCost,PrimaryUnitID,Remark,ActualQty)");
            strSql.Append(" values (");
            strSql.Append("@SaleOrderItemID,@SaleOrderID,@MaterialID,@PlanQty,@PlanCost,@PrimaryUnitID,@Remark,@ActualQty)");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderItemID", SqlDbType.VarChar,36),
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@PlanQty", SqlDbType.Decimal,13),
					new SqlParameter("@ActualQty", SqlDbType.Decimal,13),
					new SqlParameter("@PlanCost", SqlDbType.Decimal,13),
					new SqlParameter("@PrimaryUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024)};
            parameters[0].Value = model.SaleOrderItemID;
            parameters[1].Value = model.SaleOrderID;
            parameters[2].Value = model.MaterialID;
            parameters[3].Value = model.PlanQty;
            parameters[4].Value = model.ActualQty;
            parameters[5].Value = model.PlanCost;
            parameters[6].Value = model.PrimaryUnitID;
            parameters[7].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <param name="SaleOrderID">销售订单ID</param>
        /// <returns></returns>
        public int DeleteItemBySaleOrderID(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" delete SaleOrderItem ");
            strSql.AppendLine(" where SaleOrderID = @SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = SaleOrderID;

            int obj = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == 0)
            {
                return 0;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="SaleOrderItemID">销售订单行ID</param>
        /// <returns></returns>
        public int DeleteItem(string SaleOrderItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" delete SaleOrderItem ");
            strSql.AppendLine(" where SaleOrderItemID=@SaleOrderItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderItemID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = SaleOrderItemID;

            int obj = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == 0)
            {
                return 0;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// 删除单据下的所有行
        /// </summary>
        /// <param name="SaleOrderID">销售单据ID</param>
        /// <returns></returns>
        public bool Delete(string SaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SaleOrderItem ");
            strSql.Append(" where SaleOrderID=@SaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = SaleOrderID;

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
        /// 保存行
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool Save(string NewSaleOrderID, string OldSaleOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SaleOrderItem set ");
            strSql.Append("SaleOrderID=@NewSaleOrderID");
            strSql.Append(" where SaleOrderID = @OldSaleOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewSaleOrderID", SqlDbType.VarChar,36),
                    new SqlParameter("@OldSaleOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = NewSaleOrderID;
            parameters[1].Value = OldSaleOrderID;

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

    }
}
