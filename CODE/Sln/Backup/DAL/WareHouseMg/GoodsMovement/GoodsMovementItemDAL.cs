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
    public class GoodsMovementItemDAL : IGoodsMovementItemDAL
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
            strSql.AppendLine(" select item.*,M.MaterialCode,M.MaterialName,M.Specs,MU1.UnitName as RecUnit,MU2.UnitName as IssUnit from GoodsMovementItem item ");
            strSql.AppendLine(" left join Materials M on M.MaterialID=item.MaterialID ");
            strSql.AppendLine(" left join MeasureUnits MU1 on MU1.UnitID=item.RecUnitID ");
            strSql.AppendLine(" left join MeasureUnits MU2 on MU2.UnitID=item.IssUnitID ");
            strSql.AppendLine(" where item.GoodsMovementID is null ");

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from GoodsMovementItem where GoodsMovementID is null ");

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
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string GoodsMovementID, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select item.*,M.MaterialCode,M.MaterialName,M.Specs,MU1.UnitName as RecUnit,MU2.UnitName as IssUnit from GoodsMovementItem item ");
            strSql.AppendLine(" left join Materials M on M.MaterialID=item.MaterialID ");
            strSql.AppendLine(" left join MeasureUnits MU1 on MU1.UnitID=item.RecUnitID ");
            strSql.AppendLine(" left join MeasureUnits MU2 on MU2.UnitID=item.IssUnitID ");
            strSql.AppendFormat(" where item.GoodsMovementID = '{0}' ", GoodsMovementID);

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <returns></returns>
        public int GetRowCounts(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(" select COUNT(*) from GoodsMovementItem where GoodsMovementID = '{0}' ", GoodsMovementID);

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
        public int InsertItem(GoodsMovementItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GoodsMovementItem(");
            strSql.Append("GoodsMovementItemID,GoodsMovementID,MaterialID,TargInpQty,ActInpQty,RecUnitID,TargOutQty,ActOutQty,IssUnitID,InpPlaPrice,InpPlaValue,InpActPrice,InpActValue,OutPlaPrice,OutPlaValue,OutActPrice,OutActValue,ReturnQuantity,Remark)");
            strSql.Append(" values (");
            strSql.Append("@GoodsMovementItemID,@GoodsMovementID,@MaterialID,@TargInpQty,@ActInpQty,@RecUnitID,@TargOutQty,@ActOutQty,@IssUnitID,@InpPlaPrice,@InpPlaValue,@InpActPrice,@InpActValue,@OutPlaPrice,@OutPlaValue,@OutActPrice,@OutActValue,@ReturnQuantity,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementItemID", SqlDbType.VarChar,36),
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@TargInpQty", SqlDbType.Decimal,13),
					new SqlParameter("@ActInpQty", SqlDbType.Decimal,13),
					new SqlParameter("@RecUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@TargOutQty", SqlDbType.Decimal,13),
					new SqlParameter("@ActOutQty", SqlDbType.Decimal,13),
					new SqlParameter("@IssUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@InpPlaPrice", SqlDbType.Decimal,13),
					new SqlParameter("@InpPlaValue", SqlDbType.Decimal,13),
					new SqlParameter("@InpActPrice", SqlDbType.Decimal,13),
					new SqlParameter("@InpActValue", SqlDbType.Decimal,13),
					new SqlParameter("@OutPlaPrice", SqlDbType.Decimal,13),
					new SqlParameter("@OutPlaValue", SqlDbType.Decimal,13),
					new SqlParameter("@OutActPrice", SqlDbType.Decimal,13),
					new SqlParameter("@OutActValue", SqlDbType.Decimal,13),
					new SqlParameter("@ReturnQuantity", SqlDbType.Decimal,13),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024)};
            parameters[0].Value = model.GoodsMovementItemID;
            parameters[1].Value = model.GoodsMovementID;
            parameters[2].Value = model.MaterialID;
            parameters[3].Value = model.TargInpQty;
            parameters[4].Value = model.ActInpQty;
            parameters[5].Value = model.RecUnitID;
            parameters[6].Value = model.TargOutQty;
            parameters[7].Value = model.ActOutQty;
            parameters[8].Value = model.IssUnitID;
            parameters[9].Value = model.InpPlaPrice;
            parameters[10].Value = model.InpPlaValue;
            parameters[11].Value = model.InpActPrice;
            parameters[12].Value = model.InpActValue;
            parameters[13].Value = model.OutPlaPrice;
            parameters[14].Value = model.OutPlaValue;
            parameters[15].Value = model.OutActPrice;
            parameters[16].Value = model.OutActValue;
            parameters[17].Value = model.ReturnQuantity;
            parameters[18].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        public int DeleteItemByGoodsMovementID(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" delete GoodsMovementItem ");
            strSql.AppendLine(" where GoodsMovementID = @GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = GoodsMovementID;

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
        /// <param name="GoodsMovementItemID">货物移动行ID</param>
        /// <returns></returns>
        public int DeleteItem(string GoodsMovementItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" delete GoodsMovementItem ");
            strSql.AppendLine(" where GoodsMovementItemID=@GoodsMovementItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementItemID", SqlDbType.VarChar,36)
            };
            parameters[0].Value = GoodsMovementItemID;

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
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <returns></returns>
        public bool Delete(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GoodsMovementItem ");
            strSql.Append(" where GoodsMovementID=@GoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = GoodsMovementID;

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
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <returns></returns>
        public bool Save(string NewGoodsMovementID, string OldGoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsMovementItem set ");
            strSql.Append("GoodsMovementID=@NewGoodsMovementID");
            strSql.Append(" where GoodsMovementID = @OldGoodsMovementID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewGoodsMovementID", SqlDbType.VarChar,36),
                    new SqlParameter("@OldGoodsMovementID", SqlDbType.VarChar,36)};
            parameters[0].Value = NewGoodsMovementID;
            parameters[1].Value = OldGoodsMovementID;

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
        /// 
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public DataSet GetModel(string GoodsMovementID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select * from GoodsMovementItem ");
            strSql.AppendFormat(" where GoodsMovementID = '{0}' ", GoodsMovementID);

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
    }
}
