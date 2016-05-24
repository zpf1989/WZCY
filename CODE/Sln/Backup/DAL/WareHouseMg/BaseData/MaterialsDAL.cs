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
    public class MaterialsDAL : IMaterialsDAL
    {
        /// <summary>
        /// 新增物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMaterials(Materials model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Materials(");
            strSql.Append("MaterialID,MaterialCode,MaterialName,Specs,MaterialClassID,MaterialTypeID,PrimaryUnitID,Price,Remark,Creator,CreateTime,WasterRate)");
            strSql.Append(" values (");
            strSql.Append("@MaterialID,@MaterialCode,@MaterialName,@Specs,@MaterialClassID,@MaterialTypeID,@PrimaryUnitID,@Price,@Remark,@Creator,@CreateTime,@WasterRate)");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialCode", SqlDbType.VarChar,30),
					new SqlParameter("@MaterialName", SqlDbType.VarChar,1024),
					new SqlParameter("@Specs", SqlDbType.VarChar,1024),
					new SqlParameter("@MaterialClassID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@PrimaryUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@Price", SqlDbType.Decimal,13),
					new SqlParameter("@Remark", SqlDbType.VarChar,256),
					new SqlParameter("@Creator", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@WasterRate", SqlDbType.Decimal,13)};
            parameters[0].Value = model.MaterialID;
            parameters[1].Value = model.MaterialCode;
            parameters[2].Value = model.MaterialName;
            parameters[3].Value = model.Specs;
            parameters[4].Value = model.MaterialClassID;
            parameters[5].Value = model.MaterialTypeID;
            parameters[6].Value = model.PrimaryUnitID;
            parameters[7].Value = model.Price;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.Creator;
            parameters[10].Value = model.CreateTime;
            parameters[11].Value = model.WasterRate;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改物料
        /// </summary>
        public bool UpdateMaterials(Materials model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Materials set ");
            strSql.Append("MaterialName=@MaterialName,");
            strSql.Append("Specs=@Specs,");
            strSql.Append("MaterialClassID=@MaterialClassID,");
            strSql.Append("MaterialTypeID=@MaterialTypeID,");
            strSql.Append("PrimaryUnitID=@PrimaryUnitID,");
            strSql.Append("Price=@Price,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("WasterRate=@WasterRate");
            strSql.Append(" where MaterialID=@MaterialID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialName", SqlDbType.VarChar,1024),
					new SqlParameter("@Specs", SqlDbType.VarChar,1024),
					new SqlParameter("@MaterialClassID", SqlDbType.VarChar,36),
					new SqlParameter("@MaterialTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@PrimaryUnitID", SqlDbType.VarChar,36),
					new SqlParameter("@Price", SqlDbType.Decimal,13),
					new SqlParameter("@Remark", SqlDbType.VarChar,256),
					new SqlParameter("@WasterRate", SqlDbType.Decimal,13),
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.MaterialName;
            parameters[1].Value = model.Specs;
            parameters[2].Value = model.MaterialClassID;
            parameters[3].Value = model.MaterialTypeID;
            parameters[4].Value = model.PrimaryUnitID;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.WasterRate;
            parameters[8].Value = model.MaterialID;

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
        /// 删除物料
        /// </summary>
        /// <param name="MaterialID"></param>
        /// <returns></returns>
        public bool DelMaterials(string MaterialID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Materials ");
            strSql.Append(" where MaterialID=@MaterialID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36)};
            parameters[0].Value = MaterialID;

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
        /// 根据物料编号、名称、规格判断是否已经存在
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <param name="Specs">规格型号</param>
        /// <returns></returns>
        public bool Exists(string MaterialCode, string MaterialName, string Specs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Materials");
            strSql.Append(" where MaterialCode=@MaterialCode or MaterialName=@MaterialName and Specs=@Specs");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialCode", SqlDbType.VarChar,30),
                    new SqlParameter("@MaterialName", SqlDbType.VarChar,1024),
                    new SqlParameter("@Specs", SqlDbType.VarChar,1024)};
            parameters[0].Value = MaterialCode;
            parameters[1].Value = MaterialName;
            parameters[2].Value = Specs;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据物料名称、规格判断是否已经存在
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <param name="Specs">规格型号</param>
        /// <returns></returns>
        public bool Exists4Update(string MaterialCode, string MaterialName, string Specs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Materials");
            strSql.Append(" where MaterialName=@MaterialName and Specs=@Specs and MaterialCode != @MaterialCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@MaterialCode", SqlDbType.VarChar,30),
                    new SqlParameter("@MaterialName", SqlDbType.VarChar,1024),
                    new SqlParameter("@Specs", SqlDbType.VarChar,1024)};
            parameters[0].Value = MaterialCode;
            parameters[1].Value = MaterialName;
            parameters[2].Value = Specs;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 看物流是否存在，如存在返回MaterialID
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="MaterialName"></param>
        /// <param name="Specs"></param>
        /// <returns></returns>
        public string Exists4ReturnMaterialID(string MaterialCode, string MaterialName, string Specs)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Materials");
            strSql.Append(" where MaterialCode=@MaterialCode or MaterialName=@MaterialName and Specs=@Specs");
            SqlParameter[] parameters = {
                    new SqlParameter("@MaterialCode", SqlDbType.VarChar,30),
                    new SqlParameter("@MaterialName", SqlDbType.VarChar,1024),
                    new SqlParameter("@Specs", SqlDbType.VarChar,1024)};
            parameters[0].Value = MaterialCode.Trim();
            parameters[1].Value = MaterialName.Trim();
            parameters[2].Value = Specs.Trim();

            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["MaterialID"].ToString();
            else
                return string.Empty;
        }
        /// <summary>
        /// 获取物料模型
        /// </summary>
        /// <param name="MaterialID"></param>
        /// <returns></returns>
        public Materials GetModel(string MaterialID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 MaterialID,MaterialCode,MaterialName,Specs,MaterialClassID,MaterialTypeID,PrimaryUnitID,Price,Remark,Creator,CreateTime,WasterRate from Materials ");
            strSql.Append(" where MaterialID=@MaterialID ");
            SqlParameter[] parameters = {
					new SqlParameter("@MaterialID", SqlDbType.VarChar,36)};
            parameters[0].Value = MaterialID;

            Materials model = new Materials();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MaterialID"] != null && ds.Tables[0].Rows[0]["MaterialID"].ToString() != "")
                {
                    model.MaterialID = ds.Tables[0].Rows[0]["MaterialID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialCode"] != null && ds.Tables[0].Rows[0]["MaterialCode"].ToString() != "")
                {
                    model.MaterialCode = ds.Tables[0].Rows[0]["MaterialCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialName"] != null && ds.Tables[0].Rows[0]["MaterialName"].ToString() != "")
                {
                    model.MaterialName = ds.Tables[0].Rows[0]["MaterialName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Specs"] != null && ds.Tables[0].Rows[0]["Specs"].ToString() != "")
                {
                    model.Specs = ds.Tables[0].Rows[0]["Specs"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialClassID"] != null && ds.Tables[0].Rows[0]["MaterialClassID"].ToString() != "")
                {
                    model.MaterialClassID = ds.Tables[0].Rows[0]["MaterialClassID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MaterialTypeID"] != null && ds.Tables[0].Rows[0]["MaterialTypeID"].ToString() != "")
                {
                    model.MaterialTypeID = ds.Tables[0].Rows[0]["MaterialTypeID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PrimaryUnitID"] != null && ds.Tables[0].Rows[0]["PrimaryUnitID"].ToString() != "")
                {
                    model.PrimaryUnitID = ds.Tables[0].Rows[0]["PrimaryUnitID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Creator"] != null && ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = ds.Tables[0].Rows[0]["Creator"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WasterRate"] != null && ds.Tables[0].Rows[0]["WasterRate"].ToString() != "")
                {
                    model.WasterRate = decimal.Parse(ds.Tables[0].Rows[0]["WasterRate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string MaterialCode, string MaterialName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(MaterialCode))
            {
                strSql.Append(" and MaterialCode like'%" + MaterialCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialName))
            {
                strSql.Append(" and MaterialName like'%" + MaterialName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select M.*,MU.UnitCode,MU.UnitName,MT.MaterialTypeCode,MT.MaterialTypeName,MC.MaterialClassCode,MC.MaterialClassName ")
                .Append(" ,row_number() OVER (order by MaterialCode asc) as RowId from Materials M ")
                .Append(" left join MeasureUnits MU on MU.UnitID=M.PrimaryUnitID ")
                .Append(" left join MaterialType MT on MT.MaterialTypeID=M.MaterialTypeID ")
                .Append(" left join MaterialClass MC on MC.MaterialClassID=M.MaterialClassID ")
                .Append(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <returns></returns>
        public int GetRowCounts(string MaterialCode, string MaterialName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from Materials where 1=1 ");

            if (!string.IsNullOrEmpty(MaterialCode))
            {
                strSql.Append(" and MaterialCode like'%" + MaterialCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(MaterialName))
            {
                strSql.Append(" and MaterialName like'%" + MaterialName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 根据物料分类ID获取物料
        /// </summary>
        /// <param name="MaterialClassID">物料分类ID</param>
        /// <returns></returns>
        public IList<Materials> GetMaterialsList(string MaterialClassID)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" select * from Materials where MaterialClassID = '{0}' ", MaterialClassID);
                IList<Materials> list = new List<Materials>();

                DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Materials materialsInfo = new Materials();
                        if (dr["MaterialID"] != null && dr["MaterialID"].ToString() != "")
                        {
                            materialsInfo.MaterialID = dr["MaterialID"].ToString();
                        }
                        if (dr["MaterialCode"] != null && dr["MaterialCode"].ToString() != "")
                        {
                            materialsInfo.MaterialCode = dr["MaterialCode"].ToString();
                        }
                        if (dr["MaterialName"] != null && dr["MaterialName"].ToString() != "")
                        {
                            materialsInfo.MaterialName = dr["MaterialName"].ToString();
                        }
                        if (dr["Specs"] != null && dr["Specs"].ToString() != "")
                        {
                            materialsInfo.Specs = dr["Specs"].ToString();
                        }
                        else
                            materialsInfo.Specs = "";
                        if (dr["MaterialClassID"] != null && dr["MaterialClassID"].ToString() != "")
                        {
                            materialsInfo.MaterialClassID = dr["MaterialClassID"].ToString();
                        }
                        if (dr["MaterialTypeID"] != null && dr["MaterialTypeID"].ToString() != "")
                        {
                            materialsInfo.MaterialTypeID = dr["MaterialTypeID"].ToString();
                        }
                        if (dr["PrimaryUnitID"] != null && dr["PrimaryUnitID"].ToString() != "")
                        {
                            materialsInfo.PrimaryUnitID = dr["PrimaryUnitID"].ToString();
                        }
                        if (dr["Price"] != null && dr["Price"].ToString() != "")
                        {
                            materialsInfo.Price = decimal.Parse(dr["Price"].ToString());
                        }
                        if (dr["Remark"] != null && dr["Remark"].ToString() != "")
                        {
                            materialsInfo.Remark = dr["Remark"].ToString();
                        }
                        if (dr["Creator"] != null && dr["Creator"].ToString() != "")
                        {
                            materialsInfo.Creator = dr["Creator"].ToString();
                        }
                        if (dr["CreateTime"] != null && dr["CreateTime"].ToString() != "")
                        {
                            materialsInfo.CreateTime = DateTime.Parse(dr["CreateTime"].ToString());
                        }
                        if (dr["WasterRate"] != null && dr["WasterRate"].ToString() != "")
                        {
                            materialsInfo.WasterRate = decimal.Parse(dr["WasterRate"].ToString());
                        }
                        list.Add(materialsInfo);
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
