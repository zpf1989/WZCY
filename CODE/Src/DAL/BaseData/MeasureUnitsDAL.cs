using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.IDAL;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using OA.Model;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.GeneralClass.Logger;

namespace OA.DAL
{
    public class MeasureUnitsDAL : IMeasureUnitsDAL
    {
        public const string TableName = "MeasureUnits";

        ILogHelper<MeasureUnitsDAL> logger = LoggerFactory.GetLogger<MeasureUnitsDAL>();

        public List<MeasureUnits> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (string.IsNullOrEmpty(orderBySql))
            {
                orderBySql = "UnitCode";
            }

            List<MeasureUnits> units = new List<MeasureUnits>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "UnitID",
                Fields = "UnitID,UnitCode,UnitName",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    units.Add(new MeasureUnits
                    {
                        UnitID = row["UnitID"].ToString(),
                        UnitCode = row["UnitCode"].ToString(),
                        UnitName = row["UnitName"].ToString()
                    });
                }
            }

            return units;
        }

        public bool Save(params MeasureUnits[] units)
        {
            if (units == null || units.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            MeasureUnits cls = null;
            for (int i = 0; i < units.Length; i++)
            {
                cls = units[i];
                //1、组织sql
                if (string.IsNullOrEmpty(cls.UnitID))
                {
                    //新增
                    cls.UnitID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(UnitID,UnitCode,UnitName)", TableName);
                    sbSql.AppendFormat(" values (@UnitID{0},@UnitCode{0},@UnitName{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set UnitCode=@UnitCode{1},UnitName=@UnitName{1}", TableName, i);
                    sbSql.AppendFormat(" where UnitID=@UnitID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter("@UnitID"+i, SqlDbType.VarChar,36){Value=cls.UnitID},
                            new SqlParameter("@UnitCode"+i, SqlDbType.VarChar,20){Value=cls.UnitCode},
                            new SqlParameter("@UnitName"+i, SqlDbType.VarChar,20){Value=cls.UnitName},
                                            });
            }
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public bool Delete(params string[] unitIds)
        {
            if (unitIds == null || unitIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where UnitID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < unitIds.Length; i++)
            {
                sbSql.AppendFormat("@UnitID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@UnitID" + i, Value = unitIds[i] });
                if (i < unitIds.Length - 1)
                {
                    sbSql.Append(",");
                }
            }
            sbSql.Append(");");
            //2、执行sql
            int rst = 0;
            try
            {
                rst = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, sbSql.ToString(), sqlParams.ToArray());
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }
    }
}
