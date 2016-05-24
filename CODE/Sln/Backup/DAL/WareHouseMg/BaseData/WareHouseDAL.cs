using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;
using GentleUtil.DB;
using OA.IDAL;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class WareHouseDAL : IWareHouseDAL
    {
        /// <summary>
        /// 新增仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddWareHouse(WareHouse model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into WareHouse(");
            strSql.Append("WareHouseID,WareHouseCode,WareHouseName,WareHouseMan,Address,Tel,Remark)");
            strSql.Append(" values (");
            strSql.Append("@WareHouseID,@WareHouseCode,@WareHouseName,@WareHouseMan,@Address,@Tel,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36),
					new SqlParameter("@WareHouseCode", SqlDbType.VarChar,60),
					new SqlParameter("@WareHouseName", SqlDbType.VarChar,255),
					new SqlParameter("@WareHouseMan", SqlDbType.VarChar,36),
					new SqlParameter("@Address", SqlDbType.VarChar,255),
					new SqlParameter("@Tel", SqlDbType.VarChar,30),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024)};
            parameters[0].Value = model.WareHouseID;
            parameters[1].Value = model.WareHouseCode;
            parameters[2].Value = model.WareHouseName;
            parameters[3].Value = model.WareHouseMan;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.Tel;
            parameters[6].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateWareHouse(WareHouse model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update WareHouse set ");
            strSql.Append("WareHouseCode=@WareHouseCode,");
            strSql.Append("WareHouseName=@WareHouseName,");
            strSql.Append("WareHouseMan=@WareHouseMan,");
            strSql.Append("Address=@Address,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where WareHouseID=@WareHouseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseCode", SqlDbType.VarChar,60),
					new SqlParameter("@WareHouseName", SqlDbType.VarChar,255),
					new SqlParameter("@WareHouseMan", SqlDbType.VarChar,36),
					new SqlParameter("@Address", SqlDbType.VarChar,255),
					new SqlParameter("@Tel", SqlDbType.VarChar,30),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.WareHouseCode;
            parameters[1].Value = model.WareHouseName;
            parameters[2].Value = model.WareHouseMan;
            parameters[3].Value = model.Address;
            parameters[4].Value = model.Tel;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.WareHouseID;

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
        /// 删除仓库
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public bool DelWareHouse(string WareHouseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from WareHouse ");
            strSql.Append(" where WareHouseID=@WareHouseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36)};
            parameters[0].Value = WareHouseID;

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
        /// 获取模型
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public WareHouse GetModel(string WareHouseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 WareHouseID,WareHouseCode,WareHouseName,WareHouseMan,Address,Tel,Remark from WareHouse ");
            strSql.Append(" where WareHouseID=@WareHouseID ");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseID", SqlDbType.VarChar,36)};
            parameters[0].Value = WareHouseID;

            WareHouse model = new WareHouse();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["WareHouseID"] != null && ds.Tables[0].Rows[0]["WareHouseID"].ToString() != "")
                {
                    model.WareHouseID = ds.Tables[0].Rows[0]["WareHouseID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WareHouseCode"] != null && ds.Tables[0].Rows[0]["WareHouseCode"].ToString() != "")
                {
                    model.WareHouseCode = ds.Tables[0].Rows[0]["WareHouseCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WareHouseName"] != null && ds.Tables[0].Rows[0]["WareHouseName"].ToString() != "")
                {
                    model.WareHouseName = ds.Tables[0].Rows[0]["WareHouseName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WareHouseMan"] != null && ds.Tables[0].Rows[0]["WareHouseMan"].ToString() != "")
                {
                    model.WareHouseMan = ds.Tables[0].Rows[0]["WareHouseMan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null && ds.Tables[0].Rows[0]["Address"].ToString() != "")
                {
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Tel"] != null && ds.Tables[0].Rows[0]["Tel"].ToString() != "")
                {
                    model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据仓库编号和名称判断是否已经存在
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        public bool Exists(string WareHouseCode, string WareHouseName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WareHouse");
            strSql.Append(" where WareHouseCode=@WareHouseCode or WareHouseName=@WareHouseName");
            SqlParameter[] parameters = {
					new SqlParameter("@WareHouseCode", SqlDbType.VarChar,60),
                    new SqlParameter("@WareHouseName", SqlDbType.VarChar,255)};
            parameters[0].Value = WareHouseCode;
            parameters[1].Value = WareHouseName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据仓库名称判断是否已经存在
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        public bool Exists4Update(string WareHouseCode, string WareHouseName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from WareHouse");
            strSql.Append(" where WareHouseName=@WareHouseName and WareHouseCode != @WareHouseCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@WareHouseName", SqlDbType.VarChar,255),
                    new SqlParameter("@WareHouseCode", SqlDbType.VarChar,60)};
            parameters[0].Value = WareHouseName;
            parameters[1].Value = WareHouseCode;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string WareHouseCode, string WareHouseName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(WareHouseCode))
            {
                strSql.Append(" and WareHouseCode like'%" + WareHouseCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(WareHouseName))
            {
                strSql.Append(" and WareHouseName like'%" + WareHouseName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by WareHouseCode asc) as RowId from WareHouse where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        public int GetRowCounts(string WareHouseCode, string WareHouseName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from WareHouse where 1=1 ");

            if (!string.IsNullOrEmpty(WareHouseCode))
            {
                strSql.Append(" and WareHouseCode like'%" + WareHouseCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(WareHouseName))
            {
                strSql.Append(" and WareHouseName like'%" + WareHouseName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取物料分类List
        /// </summary>
        /// <returns></returns>
        public IList<WareHouse> GetWareHouseList()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select * from WareHouse ");
                IList<WareHouse> list = new List<WareHouse>();

                DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        WareHouse model = new WareHouse();
                        if (dr["WareHouseID"] != null && dr["WareHouseID"].ToString() != "")
                        {
                            model.WareHouseID = dr["WareHouseID"].ToString();
                        }
                        if (dr["WareHouseCode"] != null && dr["WareHouseCode"].ToString() != "")
                        {
                            model.WareHouseCode = dr["WareHouseCode"].ToString();
                        }
                        if (dr["WareHouseName"] != null && dr["WareHouseName"].ToString() != "")
                        {
                            model.WareHouseName = dr["WareHouseName"].ToString();
                        }
                        if (dr["WareHouseMan"] != null && dr["WareHouseMan"].ToString() != "")
                        {
                            model.WareHouseMan = dr["WareHouseMan"].ToString();
                        }
                        if (dr["Address"] != null && dr["Address"].ToString() != "")
                        {
                            model.Address = dr["Address"].ToString();
                        }
                        if (dr["Tel"] != null && dr["Tel"].ToString() != "")
                        {
                            model.Tel = dr["Tel"].ToString();
                        }
                        if (dr["Remark"] != null && dr["Remark"].ToString() != "")
                        {
                            model.Remark = dr["Remark"].ToString();
                        }
                        list.Add(model);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //
            }
        }
    }
}
