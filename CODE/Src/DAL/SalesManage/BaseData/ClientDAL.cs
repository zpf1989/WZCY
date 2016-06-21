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
    public class ClientDAL : BaseDAL<Client>, IClientDAL
    {
        public const string TableName = "Client";
        public override List<Client> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            if (ValidateUtil.isBlank(orderBySql))
            {
                orderBySql = "c.ClientCode";
            }
            List<Client> classes = new List<Client>();
            DataSet ds = DB.GetDataByPage(new PageQueryEntity
            {
                PageEntity = pageEntity,
                TableName = string.Format(" {0} c left join {1} p on c.PayTypeID=p.PayTypeID ", TableName, PayTypeDAL.TableName),
                PK = "c.ClientID",
                Fields = "c.ClientID,c.ClientCode,c.ClientName,c.ClientTel,c.ClientAddress,c.Contactor,c.Remark,c.Receiver,c.ReceiverTel,c.BillingInfo,c.PayTypeID,p.PayTypeName PayType_Name",
                OrderBySql = orderBySql,
                WhereSql = whereSql
            });
            if (ds.HasRow())
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    classes.Add(new Client
                    {
                        ClientID = row["ClientID"].ToString(),
                        ClientCode = row["ClientCode"].ToString(),
                        ClientName = row["ClientName"].ToString(),
                        ClientTel = row["ClientTel"].ToString(),
                        ClientAddress = row["ClientAddress"].ToString(),
                        Contactor = row["Contactor"].ToString(),
                        Receiver = row["Receiver"].ToString(),
                        ReceiverTel = row["ReceiverTel"].ToString(),
                        BillingInfo = row["BillingInfo"].ToString(),
                        PayTypeID = row["PayTypeID"].ToString(),
                        PayType_Name = row["PayType_Name"].ToString(),
                        Remark = row["Remark"].ToString()
                    });
                }
            }

            return classes;
        }

        public override bool Save(params Client[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return false;
            }
            StringBuilder sbSql = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            Client entity = null;
            for (int i = 0; i < entities.Length; i++)
            {
                entity = entities[i];
                //1、组织sql
                if (ValidateUtil.isBlank(entity.ClientID))
                {
                    //新增
                    entity.ClientID = Guid.NewGuid().ToString();
                    sbSql.AppendFormat("insert into {0}(ClientID,ClientCode,ClientName,ClientTel,ClientAddress,Contactor,Receiver,ReceiverTel,BillingInfo,PayTypeID,Remark)", TableName);
                    sbSql.AppendFormat(" values (@ClientID{0},@ClientCode{0},@ClientName{0},@ClientTel{0},@ClientAddress{0},@Contactor{0},@Receiver{0},@ReceiverTel{0},@BillingInfo{0},@PayTypeID{0},@Remark{0});", i);
                }
                else
                {
                    //修改
                    sbSql.AppendFormat("update {0} set ClientCode=@ClientCode{1},ClientName=@ClientName{1},ClientTel=@ClientTel{1},ClientAddress=@ClientAddress{1},Contactor=@Contactor{1},Receiver=@Receiver{1},ReceiverTel=@ReceiverTel{1},BillingInfo=@BillingInfo{1},PayTypeID=@PayTypeID{1},Remark=@Remark{1}", TableName, i);
                    sbSql.AppendFormat(" where ClientID=@ClientID{0};", i);
                }
                //不管新增或修改， 参数都一样
                sqlParams.AddRange(new SqlParameter[]{ 
                            new SqlParameter{ParameterName="@ClientID"+i, Value=entity.ClientID},
                            new SqlParameter{ParameterName="@ClientCode"+i, Value=entity.ClientCode},
                            new SqlParameter{ParameterName="@ClientName"+i, Value=entity.ClientName},
                            new SqlParameter{ParameterName="@ClientTel"+i, Value=entity.ClientTel},
                            new SqlParameter{ParameterName="@ClientAddress"+i, Value=entity.ClientAddress},
                            new SqlParameter{ParameterName="@Contactor"+i, Value=entity.Contactor},
                             new SqlParameter{ParameterName="@Receiver"+i, Value=entity.Receiver},
                            new SqlParameter{ParameterName="@ReceiverTel"+i, Value=entity.ReceiverTel},
                            new SqlParameter{ParameterName="@BillingInfo"+i, Value=entity.BillingInfo},
                            new SqlParameter{ParameterName="@PayTypeID"+i, Value=entity.PayTypeID},
                            new SqlParameter{ParameterName="@Remark"+i, Value=entity.Remark},
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

        public override bool Delete(params string[] classIds)
        {
            if (classIds == null || classIds.Length < 1)
            {
                return false;
            }
            //1、组织sql
            StringBuilder sbSql = new StringBuilder();//删除用户的sql
            sbSql.AppendFormat("delete from {0} where ClientID in (", TableName);
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            for (int i = 0; i < classIds.Length; i++)
            {
                sbSql.AppendFormat("@ClientID{0}", i);
                sqlParams.Add(new SqlParameter { ParameterName = "@ClientID" + i, Value = classIds[i] });
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
            return base.Exists(string.Format(" and ClientCode in ('{0}')", string.Join("','", codes)));
        }

        protected override string GetTableName()
        {
            return TableName;
        }

        public override List<Client> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }
    }
}
