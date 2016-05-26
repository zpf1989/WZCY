using GentleUtil.DB;
using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OA.DAL
{
    public class DB
    {
        public static string ConnectionString
        {
            get
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"] == null)
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                }
                else
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                }
            }
        }
        public static GentleUtil.DB.DBAccess.DBType Type
        {
            get
            {
                string type = string.Empty;
                type = System.Configuration.ConfigurationManager.AppSettings["MainDBType"].ToString();
                return (GentleUtil.DB.DBAccess.DBType)System.Enum.Parse(typeof(GentleUtil.DB.DBAccess.DBType), type);
            }
        }
        /// <summary>
        /// 通用分页查询方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataSet GetDataByPage(PageQueryEntity entity)
        {
            //组织查询参数
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("tableName", SqlDbType.NVarChar, 100) { Value = entity.TableName });
            sqlParams.Add(new SqlParameter("pk", SqlDbType.NVarChar, 100) { Value = entity.PK });
            sqlParams.Add(new SqlParameter("fields", SqlDbType.NVarChar, 1000) { Value = entity.Fields });
            sqlParams.Add(new SqlParameter("orderBySql", SqlDbType.NVarChar, 1000) { Value = entity.OrderBySql });
            sqlParams.Add(new SqlParameter("whereSql", SqlDbType.NVarChar, 1000) { Value = entity.WhereSql });
            sqlParams.Add(new SqlParameter("pageSize", SqlDbType.Int) { Value = entity.PageEntity.PageSize });
            SqlParameter sqlParamPageIndex = new SqlParameter("pageIndex", SqlDbType.Int) { Value = entity.PageEntity.PageIndex, Direction = ParameterDirection.InputOutput };
            sqlParams.Add(sqlParamPageIndex);
            SqlParameter sqlParamTotalRecord = new SqlParameter("totalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
            sqlParams.Add(sqlParamTotalRecord);
            DataSet ds = DBAccess.ExecuteDataset(DB.ConnectionString, CommandType.StoredProcedure, "Proc_GetDataByPage", sqlParams.ToArray());
            entity.PageEntity.PageIndex = Convert.ToInt32(sqlParamPageIndex.Value);
            entity.PageEntity.TotalRecords = Convert.ToInt32(sqlParamTotalRecord.Value);
            return ds;
        }
    }
    /// <summary>
    /// 分页查询参数实体类
    /// </summary>
    public class PageQueryEntity
    {
        /// <summary>
        /// 分页设置
        /// </summary>
        public PageEntity PageEntity;
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName;
        /// <summary>
        /// 主键名称
        /// </summary>
        public string PK;
        /// <summary>
        /// 查询的字段
        /// </summary>
        public string Fields;
        /// <summary>
        /// 排序sql
        /// </summary>
        public string OrderBySql;
        /// <summary>
        /// 过滤条件sql
        /// </summary>
        public string WhereSql;
    }
}
