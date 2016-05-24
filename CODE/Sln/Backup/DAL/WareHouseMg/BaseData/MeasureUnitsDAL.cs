using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data.SqlClient;
using GentleUtil.DB;
using System.Data;
using OA.IDAL;

namespace OA.DAL
{
    public class MeasureUnitsDAL : IMeasureUnitsDAL
    {
        /// <summary>
        /// 添加计量单位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddUnits(MeasureUnits model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MeasureUnits(");
            strSql.Append("UnitID,UnitCode,UnitName)");
            strSql.Append(" values (");
            strSql.Append("@UnitID,@UnitCode,@UnitName)");
            SqlParameter[] parameters = {
					new SqlParameter("@UnitID", SqlDbType.VarChar,36),
					new SqlParameter("@UnitCode", SqlDbType.VarChar,30),
					new SqlParameter("@UnitName", SqlDbType.VarChar,1024)};
            parameters[0].Value = model.UnitID;
            parameters[1].Value = model.UnitCode;
            parameters[2].Value = model.UnitName;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改计量单位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUnits(MeasureUnits model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MeasureUnits set ");
            strSql.Append("UnitCode=@UnitCode,");
            strSql.Append("UnitName=@UnitName");
            strSql.Append(" where UnitID=@UnitID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnitCode", SqlDbType.VarChar,30),
					new SqlParameter("@UnitName", SqlDbType.VarChar,1024),
					new SqlParameter("@UnitID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.UnitCode;
            parameters[1].Value = model.UnitName;
            parameters[2].Value = model.UnitID;

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
        /// 删除计量单位
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public bool DelUnits(string UnitID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MeasureUnits ");
            strSql.Append(" where UnitID=@UnitID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnitID", SqlDbType.VarChar,36)};
            parameters[0].Value = UnitID;

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
        /// 根据计量单位编号和名称判断是否已经存在
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <returns></returns>
        public bool Exists(string UnitCode, string UnitName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MeasureUnits");
            strSql.Append(" where UnitCode=@UnitCode or UnitName=@UnitName");
            SqlParameter[] parameters = {
					new SqlParameter("@UnitCode", SqlDbType.VarChar,30),
                    new SqlParameter("@UnitName", SqlDbType.VarChar,1024)};
            parameters[0].Value = UnitCode;
            parameters[1].Value = UnitName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据计量单位名称判断是否已经存在
        /// </summary>
        /// <param name="UnitName">计量单位名称</param>
        /// <param name="UnitCode">计量单位编号</param>
        /// <returns></returns>
        public bool Exists4Update(string UnitCode, string UnitName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MeasureUnits");
            strSql.Append(" where UnitName=@UnitName and UnitCode != @UnitCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@UnitName", SqlDbType.VarChar,1024),
                    new SqlParameter("@UnitCode", SqlDbType.VarChar,30)};
            parameters[0].Value = UnitName;
            parameters[1].Value = UnitCode;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string UnitCode, string UnitName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(UnitCode))
            {
                strSql.Append(" and UnitCode like'%" + UnitCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(UnitName))
            {
                strSql.Append(" and UnitName like'%" + UnitName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by UnitCode asc) as RowId from MeasureUnits where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <returns></returns>
        public int GetRowCounts(string UnitCode, string UnitName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from MeasureUnits where 1=1 ");

            if (!string.IsNullOrEmpty(UnitCode))
            {
                strSql.Append(" and UnitCode like'%" + UnitCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(UnitName))
            {
                strSql.Append(" and UnitName like'%" + UnitName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取计量单位模型
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public MeasureUnits GetModel(string UnitID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UnitID,UnitCode,UnitName from MeasureUnits ");
            strSql.Append(" where UnitID=@UnitID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UnitID", SqlDbType.VarChar,36)};
            parameters[0].Value = UnitID;

            MeasureUnits model = new MeasureUnits();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UnitID"] != null && ds.Tables[0].Rows[0]["UnitID"].ToString() != "")
                {
                    model.UnitID = ds.Tables[0].Rows[0]["UnitID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UnitCode"] != null && ds.Tables[0].Rows[0]["UnitCode"].ToString() != "")
                {
                    model.UnitCode = ds.Tables[0].Rows[0]["UnitCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UnitName"] != null && ds.Tables[0].Rows[0]["UnitName"].ToString() != "")
                {
                    model.UnitName = ds.Tables[0].Rows[0]["UnitName"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取计量单位DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UnitID,UnitCode,UnitName ");
            strSql.Append(" FROM MeasureUnits ");
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        /// <summary>
        /// 获取计量单位List
        /// </summary>
        /// <returns></returns>
        public IList<MeasureUnits> GetMeasureUnitsList()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select * from MeasureUnits ");
                IList<MeasureUnits> list = new List<MeasureUnits>();

                DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MeasureUnits unit = new MeasureUnits();
                        if (dr["UnitID"] != null && dr["UnitID"].ToString() != "")
                        {
                            unit.UnitID = dr["UnitID"].ToString();
                        }
                        if (dr["UnitCode"] != null && dr["UnitCode"].ToString() != "")
                        {
                            unit.UnitCode = dr["UnitCode"].ToString();
                        }
                        if (dr["UnitName"] != null && dr["UnitName"].ToString() != "")
                        {
                            unit.UnitName = dr["UnitName"].ToString();
                        }
                        list.Add(unit);
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
