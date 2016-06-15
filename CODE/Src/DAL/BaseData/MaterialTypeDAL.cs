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
    public class MaterialTypeDAL : BaseDAL<MaterialType>, IMaterialTypeDAL
    {
        public const string TableName = "MaterialType";
        ILogHelper<MaterialTypeDAL> logger = LoggerFactory.GetLogger<MaterialTypeDAL>();
        public override List<MaterialType> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (string.IsNullOrEmpty(orderBySql))
            {
                orderBySql = "MaterialTypeCode";
            }
            List<MaterialType> types = new List<MaterialType>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "MaterialTypeID",
                Fields = "MaterialTypeID,MaterialTypeCode,MaterialTypeName,Remark",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    types.Add(new MaterialType
                    {
                        MaterialTypeID = row["MaterialTypeID"].ToString(),
                        MaterialTypeCode = row["MaterialTypeCode"].ToString(),
                        MaterialTypeName = row["MaterialTypeName"].ToString(),
                        Remark = row["Remark"].ToString()
                    });
                }
            }

            return types;
        }

        public override bool Save(params MaterialType[] types)
        {
            if (types == null || types.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            MaterialType typ = null;
            for (int i = 0; i < types.Length; i++)
            {
                typ = types[i];
                //1、组织sql
                if (string.IsNullOrEmpty(typ.MaterialTypeID))
                {
                    //新增
                    typ.MaterialTypeID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(MaterialTypeID,MaterialTypeCode,MaterialTypeName,Remark)", TableName);
                    sbSql.AppendFormat(" values (@MaterialTypeID{0},@MaterialTypeCode{0},@MaterialTypeName{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set MaterialTypeCode=@MaterialTypeCode{1},MaterialTypeName=@MaterialTypeName{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where MaterialTypeID=@MaterialTypeID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@MaterialTypeID"+i, Value=typ.MaterialTypeID},
                            new SqlParameter{ ParameterName="@MaterialTypeCode"+i,Value=typ.MaterialTypeCode},
                            new SqlParameter{ ParameterName="@MaterialTypeName"+i,Value=typ.MaterialTypeName},
                            new SqlParameter{ ParameterName="@Remark"+i,Value=typ.Remark},
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

        public override bool Delete(params string[] typeIds)
        {
            if (typeIds == null || typeIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where MaterialTypeID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < typeIds.Length; i++)
            {
                sbSql.AppendFormat("@MaterialTypeID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@MaterialTypeID" + i, Value = typeIds[i] });
                if (i < typeIds.Length - 1)
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

        protected override string GetTableName()
        {
            return TableName;
        }

        public bool Exists(params string[] codes)
        {
            if (codes == null || codes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and MaterialTypeCode in ('{0}')", string.Join("','", codes)));
        }
    }
}
