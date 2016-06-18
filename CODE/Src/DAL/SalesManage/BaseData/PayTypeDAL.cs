using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.IDAL;
using System.Data;
using GentleUtil.DB;
using OA.Model;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.GeneralClass.Logger;
using System.Data.SqlClient;

namespace OA.DAL
{
    public class PayTypeDAL : BaseDAL<PayType>, IPayTypeDAL
    {
        public const string TableName = "PayType";
        public override List<PayType> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "PayTypeCode";
            }
            List<PayType> classes = new List<PayType>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "PayTypeID",
                Fields = "PayTypeID,PayTypeCode,PayTypeName,PayTypeRemark",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    classes.Add(new PayType
                    {
                        PayTypeID = row["PayTypeID"].ToString(),
                        PayTypeCode = row["PayTypeCode"].ToString(),
                        PayTypeName = row["PayTypeName"].ToString(),
                        PayTypeRemark = row["PayTypeRemark"].ToString()
                    });
                }
            }

            return classes;
        }

        public override bool Save(params PayType[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            PayType entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.PayTypeID))
                {
                    //新增
                    entity.PayTypeID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(PayTypeID,PayTypeCode,PayTypeName,PayTypeRemark)", TableName);
                    sbSql.AppendFormat(" values (@PayTypeID{0},@PayTypeCode{0},@PayTypeName{0},@PayTypeRemark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set PayTypeCode=@PayTypeCode{1},PayTypeName=@PayTypeName{1},PayTypeRemark=@PayTypeRemark{1}", TableName, i);
                    sbSql.AppendFormat(" where PayTypeID=@PayTypeID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@PayTypeID"+i, Value=entity.PayTypeID},
                            new SqlParameter{ParameterName="@PayTypeCode"+i, Value=entity.PayTypeCode},
                            new SqlParameter{ParameterName="@PayTypeName"+i, Value=entity.PayTypeName},
                            new SqlParameter{ParameterName="@PayTypeRemark"+i, Value=entity.PayTypeRemark},
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
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public override bool Delete(params string[] entityIds)
        {
            if (entityIds == null || entityIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where PayTypeID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < entityIds.Length; i++)
            {
                sbSql.AppendFormat("@PayTypeID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@PayTypeID" + i, Value = entityIds[i] });
                if (i < entityIds.Length - 1)
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
                base.Logger.LogError(ex);
                return false;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public bool Exists(params string[] codes)
        {
            if (codes == null || codes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and PayTypeCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<PayType> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }
    }
}
