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

namespace OA.DAL
{
    public class MaterialClassDAL : IMaterialClassDAL
    {
        public const string TableName = "MaterialClass";
        public List<MaterialClass> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            List<MaterialClass> classes = new List<MaterialClass>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = TableName,
                PK = "MaterialClassID",
                Fields = "MaterialClassID,MaterialClassCode,MaterialClassName,Remark",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    classes.Add(new MaterialClass
                    {
                        MaterialClassID = row["MaterialClassID"].ToString(),
                        MaterialClassCode = row["MaterialClassCode"].ToString(),
                        MaterialClassName = row["MaterialClassName"].ToString(),
                        Remark = row["Remark"].ToString()
                    });
                }
            }

            return classes;
        }

        public bool Save(params MaterialClass[] classes)
        {
            if (classes == null || classes.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            MaterialClass cls = null;
            for (int i = 0; i < classes.Length; i++)
            {
                cls = classes[i];
                //1、组织sql
                if (string.IsNullOrEmpty(cls.MaterialClassID))
                {
                    //新增
                    cls.MaterialClassID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(MaterialClassID,MaterialClassCode,MaterialClassName,Remark)", TableName);
                    sbSql.AppendFormat(" values (@MaterialClassID,@MaterialClassCode,@MaterialClassName,@Remark);", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set MaterialClassCode=@MaterialClassCode{1},MaterialClassName=@MaterialClassName{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where MaterialClassID=@MaterialClassID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter("@MaterialClassID"+i, SqlDbType.VarChar,36){Value=cls.MaterialClassID},
                            new SqlParameter("@MaterialClassCode"+i, SqlDbType.VarChar,20){Value=cls.MaterialClassCode},
                            new SqlParameter("@MaterialClassName"+i, SqlDbType.VarChar,20){Value=cls.MaterialClassName},
                            new SqlParameter("@Remark"+i, SqlDbType.VarChar,36){Value=cls.Remark},
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
                throw ex;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }

        public bool Delete(params string[] classIds)
        {
            if (classIds == null || classIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where MaterialClassID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < classIds.Length; i++)
            {
                sbSql.AppendFormat("@MaterialClassID{0}", i);
                sqlParams.Add(new SqlParameter("@MaterialClassID" + i, SqlDbType.VarChar, 36) { Value = classIds[i] });
                if (i < classIds.Length - 1)
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
                throw ex;
            }
            //3、返回成功或失败的标志
            return rst > 0;
        }
    }
}
