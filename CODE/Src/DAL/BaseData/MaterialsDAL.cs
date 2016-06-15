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
    public class MaterialsDAL : BaseDAL<Materials>, IMaterialsDAL
    {
        public const string TableName = "Materials";

        ILogHelper<MaterialsDAL> logger = LoggerFactory.GetLogger<MaterialsDAL>();

        public override List<Materials> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (string.IsNullOrEmpty(orderBySql))
            {
                orderBySql = "MaterialCode";
            }
            List<Materials> classes = new List<Materials>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(" {0} m left join {1} c on m.MaterialClassID=c.MaterialClassID left join {2} t on m.MaterialTypeID=t.MaterialTypeID left join {3} un on m.PrimaryUnitID=un.UnitID left join {4} usr on m.Creator=usr.UserID ",
                TableName, MaterialClassDAL.TableName, MaterialTypeDAL.TableName, MeasureUnitsDAL.TableName, UserManageDAL.TableName),
                PK = "m.MaterialID",
                Fields = "m.MaterialID,m.MaterialCode,m.MaterialName,m.Specs,m.MaterialClassID,m.MaterialTypeID,m.PrimaryUnitID,m.Price,m.Remark,m.Creator,m.CreateTime,m.WasterRate,c.MaterialClassName MaterialClass_Name,t.MaterialTypeName MaterialType_Name,un.UnitName PrimaryUnit_Name,usr.UserName Creator_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    classes.Add(new Materials
                    {
                        MaterialID = row["MaterialID"].ToString(),
                        MaterialCode = row["MaterialCode"].ToString(),
                        MaterialName = row["MaterialName"].ToString(),
                        Specs = row["Specs"].ToString(),
                        MaterialClassID = row["MaterialClassID"].ToString(),
                        MaterialClass_Name = row["MaterialClass_Name"].ToString(),
                        MaterialTypeID = row["MaterialTypeID"].ToString(),
                        MaterialType_Name = row["MaterialType_Name"].ToString(),
                        PrimaryUnitID = row["PrimaryUnitID"].ToString(),
                        PrimaryUnit_Name = row["PrimaryUnit_Name"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        WasterRate = Convert.ToDecimal(row["WasterRate"]),
                        Remark = row["Remark"].ToString(),
                        Creator = row["Creator"].ToString(),
                        Creator_Name = row["Creator_Name"].ToString(),
                        CreateTime = Convert.ToDateTime(row["CreateTime"])
                    });
                }
            }

            return classes;
        }

        public override bool Save(params Materials[] materials)
        {
            if (materials == null || materials.Length < 1)
            {
                return false;
            }

            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Materials material = null;
            for (int i = 0; i < materials.Length; i++)
            {
                material = materials[i];
                //1、组织sql
                if (string.IsNullOrEmpty(material.MaterialID))
                {
                    //新增
                    material.MaterialID = Guid.NewGuid().ToString();
                    //material.Creator=
                    sbSql.AppendFormat("insert into {0}(MaterialID,MaterialCode,MaterialName,Specs,MaterialClassID,MaterialTypeID,PrimaryUnitID,Price,Remark,Creator,CreateTime,WasterRate)", TableName);
                    sbSql.AppendFormat(" values (@MaterialID{0},@MaterialCode{0},@MaterialName{0},@Specs{0},@MaterialClassID{0},@MaterialTypeID{0},@PrimaryUnitID{0},@Price{0},@Remark{0},@Creator{0},@CreateTime{0},@WasterRate{0});", i);
                    //新增独有的参数
                    sqlParams.AddRange(new SqlParameter[] { 
                        new SqlParameter{ParameterName="@Creator"+i,Value=material.Creator},
                            new SqlParameter{ParameterName="@CreateTime"+i,Value=material.CreateTime}
                    });
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set MaterialCode=@MaterialCode{1},MaterialName=@MaterialName{1},Specs=@Specs{1},MaterialClassID=@MaterialClassID{1},MaterialTypeID=@MaterialTypeID{1},PrimaryUnitID=@PrimaryUnitID{1},Price=@Price{1},Remark=@Remark{1},WasterRate=@WasterRate{1}", TableName, i);
                    sbSql.AppendFormat(" where MaterialID=@MaterialID{0};", i);
                }
                //
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@MaterialID"+i,Value=material.MaterialID},
                            new SqlParameter{ParameterName="@MaterialCode"+i,Value=material.MaterialCode},
                            new SqlParameter{ParameterName="@MaterialName"+i,Value=material.MaterialName},
                            new SqlParameter{ParameterName="@Specs"+i,Value=material.Specs},
                            new SqlParameter{ParameterName="@MaterialClassID"+i,Value=material.MaterialClassID},
                            new SqlParameter{ParameterName="@MaterialTypeID"+i,Value=material.MaterialTypeID},
                            new SqlParameter{ParameterName="@PrimaryUnitID"+i,Value=material.PrimaryUnitID},
                            new SqlParameter{ParameterName="@Price"+i,Value=material.Price},
                            new SqlParameter{ParameterName="@Remark"+i,Value=material.Remark},
                            new SqlParameter{ParameterName="@WasterRate"+i,Value=material.WasterRate}
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

        public override bool Delete(params string[] materialIds)
        {
            if (materialIds == null || materialIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//
            sbSql.AppendFormat("delete from {0} where MaterialID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < materialIds.Length; i++)
            {
                sbSql.AppendFormat("@MaterialID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@MaterialID" + i, Value = materialIds[i] });
                if (i < materialIds.Length - 1)
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

        public bool Exists(params string[] codes)
        {
            if (codes == null || codes.Length < 1)
            {
                return false;
            }
            return base.Exists(string.Format(" and MaterialCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }
    }
}
