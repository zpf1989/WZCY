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
    public class ReachGoodsBillItemDAL : IReachGoodsBillItemDAL
    {
        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertItem(ReachGoodsBillItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_ReachGoodsBillItem(");
            strSql.Append("ReachGoodsBillItemID,ReachGoodsBillID,MaterialsID,ReachQty,BuyQty,Remark,ReachCost)");
            strSql.Append(" values (");
            strSql.Append("@ReachGoodsBillItemID,@ReachGoodsBillID,@MaterialsID,@ReachQty,@BuyQty,@Remark,@ReachCost)");
            SqlParameter[] parameters = {
					new SqlParameter("@ReachGoodsBillItemID", SqlDbType.VarChar,36),
					new SqlParameter("@ReachGoodsBillID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialsID", SqlDbType.VarChar,36),
					new SqlParameter("@ReachQty", SqlDbType.Int,4),
					new SqlParameter("@BuyQty", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,300),
					new SqlParameter("@ReachCost", SqlDbType.Decimal,9)};
            parameters[0].Value = model.ReachGoodsBillItemID;
            parameters[1].Value = model.ReachGoodsBillID;
            parameters[2].Value = model.MaterialsID;
            parameters[3].Value = model.ReachQty;
            parameters[4].Value = model.BuyQty;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.ReachCost;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="ReachGoodsBillItemID"></param>
        /// <returns></returns>
        public bool DelItem(string ReachGoodsBillItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_ReachGoodsBillItem ");
            strSql.Append(" where ReachGoodsBillItemID=@ReachGoodsBillItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReachGoodsBillItemID", SqlDbType.VarChar,36)};
            parameters[0].Value = ReachGoodsBillItemID;

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
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        public bool DelItem()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_ReachGoodsBillItem ");
            strSql.Append(" where ReachGoodsBillID is null ");

            int rows = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
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
            strSql.AppendLine(" select B.*,M.MaterialsCode,M.MaterialsName,M.MaterialsTypeID,M.Price,M.Space,M.Unit from OA_ReachGoodsBillItem B ");
            strSql.AppendLine(" inner join OA_Materials M on M.MaterialsID=B.MaterialsID ");
            strSql.AppendLine(" where ReachGoodsBillID is null ");

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ");
            strSql.AppendLine(" (select B.*,M.MaterialsCode,M.MaterialsName,M.MaterialsTypeID,M.Price,M.Space,M.Unit from OA_ReachGoodsBillItem B ");
            strSql.AppendLine(" inner join OA_Materials M on M.MaterialsID=B.MaterialsID where ReachGoodsBillID is null)tmp ");

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 保存行
        /// </summary>
        /// <param name="ReachGoodsBillID"></param>
        /// <returns></returns>
        public bool Save(string ReachGoodsBillID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_ReachGoodsBillItem set ");
            strSql.Append("ReachGoodsBillID=@ReachGoodsBillID");
            strSql.Append(" where ReachGoodsBillID is null ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReachGoodsBillID", SqlDbType.VarChar,36)};
            parameters[0].Value = ReachGoodsBillID;

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
