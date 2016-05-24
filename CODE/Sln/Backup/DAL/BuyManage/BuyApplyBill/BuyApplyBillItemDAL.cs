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
    public class BuyApplyBillItemDAL : IBuyApplyBillItemDAL
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
            strSql.AppendLine(" select Item.*,M.MaterialCode,M.MaterialName,M.Specs,M.Price,MU.UnitCode,MU.UnitName from BuyApplyOrderItem Item ");
            strSql.AppendLine(" left join Materials M on M.MaterialID=Item.MaterialID ");
            strSql.AppendLine(" left join MeasureUnits MU on MU.UnitID=Item.BuyUnitID ");
            strSql.AppendLine(" where BuyApplyOrderID is null ");

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from BuyApplyOrderItem where BuyApplyOrderID is null ");

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
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string BuyApplyOrderID, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select Item.*,M.MaterialCode,M.MaterialName,M.Specs,M.Price,MU.UnitCode,MU.UnitName from BuyApplyOrderItem Item ");
            strSql.AppendFormat(" left join Materials M on M.MaterialID=Item.MaterialID ");
            strSql.AppendFormat(" left join MeasureUnits MU on MU.UnitID=Item.BuyUnitID ");
            strSql.AppendFormat(" where BuyApplyOrderID = '{0}' ", BuyApplyOrderID);

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        public int GetRowCounts(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select COUNT(*) from BuyApplyOrderItem where BuyApplyOrderID = '{0}' ", BuyApplyOrderID);
            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 新增采购单行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Add(BuyApplyBillItem item)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BuyApplyOrderItem(");
            strSql.Append("BuyApplyOrderItemID,BuyApplyOrderID,MaterialID,BuyQty,BuyCost,BuyUnitID,Remark)");
            strSql.Append(" values (");
            strSql.Append("@BuyApplyOrderItemID,@BuyApplyOrderID,@MaterialID,@BuyQty,@BuyCost,@BuyUnitID,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderItemID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@BuyQty", SqlDbType.Decimal,13),
					new SqlParameter("@BuyCost", SqlDbType.Decimal,13),
					new SqlParameter("@BuyUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024)};
            parameters[0].Value = item.BuyApplyOrderItemID;
            parameters[1].Value = item.BuyApplyOrderID;
            parameters[2].Value = item.MaterialID;
            parameters[3].Value = item.BuyQty;
            parameters[4].Value = item.BuyCost;
            parameters[5].Value = item.BuyUnitID;
            parameters[6].Value = item.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="ItemID">采购订单行</param>
        /// <returns></returns>
        public int DeleteItem(string ItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" delete BuyApplyOrderItem ");
            strSql.AppendLine(" where BuyApplyOrderItemID=@BuyApplyOrderItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderItemID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = ItemID;

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
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        public int DeleteItemByBuyApplyOrderID(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" delete BuyApplyOrderItem ");
            strSql.AppendLine(" where BuyApplyOrderID = @BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = BuyApplyOrderID;

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
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        public bool Delete(string BuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BuyApplyOrderItem ");
            strSql.Append(" where BuyApplyOrderID=@BuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = BuyApplyOrderID;

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
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Save(string NewBuyApplyOrderID, string OldBuyApplyOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BuyApplyOrderItem set ");
            strSql.Append("BuyApplyOrderID=@NewBuyApplyOrderID");
            strSql.Append(" where BuyApplyOrderID = @OldBuyApplyOrderID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewBuyApplyOrderID", SqlDbType.VarChar,36),
                    new SqlParameter("@OldBuyApplyOrderID", SqlDbType.VarChar,36)};
            parameters[0].Value = NewBuyApplyOrderID;
            parameters[1].Value = OldBuyApplyOrderID;

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
