using OA.GeneralClass;
using OA.GeneralClass.Logger;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OA.GeneralClass.Extensions;
using System.Data.SqlClient;
using GentleUtil.DB;

namespace OA.DAL
{
    public class WareHouseDAL : BaseDAL<WareHouse>, IWareHouseDAL
    {
        public const string TableName = "WareHouse";

        public override List<WareHouse> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "w.WareHouseCode";
            }
            List<WareHouse> entities = new List<WareHouse>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(" {0} w left join {1} u on w.WareHouseMan=u.UserID ", TableName, UserManageDAL.TableName),
                PK = "w.WareHouseID",
                Fields = "w.WareHouseID,w.WareHouseCode,w.WareHouseName,w.WareHouseMan,w.Tel,w.Address,w.Remark,u.UserName WareHouseMan_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    entities.Add(new WareHouse
                    {
                        WareHouseID = row["WareHouseID"].ToString(),
                        WareHouseCode = row["WareHouseCode"].ToString(),
                        WareHouseName = row["WareHouseName"].ToString(),
                        WareHouseMan = row["WareHouseMan"].ToString(),
                        Tel = row["Tel"].ToString(),
                        Address = row["Address"].ToString(),
                        WareHouseMan_Name = row["WareHouseMan_Name"].ToString(),
                        Remark = row["Remark"].ToString()
                    });
                }
            }

            return entities;
        }
        public override List<WareHouse> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }
        public override bool Save(params WareHouse[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            WareHouse entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.WareHouseID))
                {
                    //新增
                    entity.WareHouseID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(WareHouseID,WareHouseCode,WareHouseName,WareHouseMan,Address,Tel,Remark)", TableName);
                    sbSql.AppendFormat(" values (@WareHouseID{0},@WareHouseCode{0},@WareHouseName{0},@WareHouseMan{0},@Address{0},@Tel{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set WareHouseCode=@WareHouseCode{1},WareHouseName=@WareHouseName{1},WareHouseMan=@WareHouseMan{1},Address=@Address{1},Tel=@Tel{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where WareHouseID=@WareHouseID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@WareHouseID"+i,Value=entity.WareHouseID},
                            new SqlParameter{ParameterName="@WareHouseCode"+i,Value=entity.WareHouseCode},
                            new SqlParameter{ParameterName="@WareHouseName"+i,Value=entity.WareHouseName},
                            new SqlParameter{ParameterName="@WareHouseMan"+i,Value=entity.WareHouseMan},
                            new SqlParameter{ParameterName="@Address"+i,Value=entity.Address},
                            new SqlParameter{ParameterName="@Tel"+i,Value=entity.Tel},
                            new SqlParameter{ParameterName="@Remark"+i,Value=entity.Remark},
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

        public override bool Delete(params string[] ids)
        {
            if (ids == null || ids.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where WareHouseID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < ids.Length; i++)
            {
                sbSql.AppendFormat("@WareHouseID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@WareHouseID" + i, Value = ids[i] });
                if (i < ids.Length - 1)
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
            return base.Exists(string.Format(" and WareHouseCode in ('{0}')", string.Join("','", codes)));
        }

    }
}
