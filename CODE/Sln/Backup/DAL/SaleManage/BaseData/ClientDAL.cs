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
    public class ClientDAL : IClientDAL
    {
        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddClient(Client model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Client(");
            strSql.Append("ClientID,ClientCode,ClientName,ClientTel,ClientAddress,Contactor,Remark)");
            strSql.Append(" values (");
            strSql.Append("@ClientID,@ClientCode,@ClientName,@ClientTel,@ClientAddress,@Contactor,@Remark)");
            SqlParameter[] parameters = {
					new SqlParameter("@ClientID", SqlDbType.VarChar,36),
					new SqlParameter("@ClientCode", SqlDbType.VarChar,60),
					new SqlParameter("@ClientName", SqlDbType.VarChar,255),
					new SqlParameter("@ClientTel", SqlDbType.VarChar,30),
					new SqlParameter("@ClientAddress", SqlDbType.VarChar,255),
					new SqlParameter("@Contactor", SqlDbType.VarChar,30),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024)};
            parameters[0].Value = model.ClientID;
            parameters[1].Value = model.ClientCode;
            parameters[2].Value = model.ClientName;
            parameters[3].Value = model.ClientTel;
            parameters[4].Value = model.ClientAddress;
            parameters[5].Value = model.Contactor;
            parameters[6].Value = model.Remark;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateClient(Client model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Client set ");
            strSql.Append("ClientCode=@ClientCode,");
            strSql.Append("ClientName=@ClientName,");
            strSql.Append("ClientTel=@ClientTel,");
            strSql.Append("ClientAddress=@ClientAddress,");
            strSql.Append("Contactor=@Contactor,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ClientID=@ClientID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClientCode", SqlDbType.VarChar,60),
					new SqlParameter("@ClientName", SqlDbType.VarChar,255),
					new SqlParameter("@ClientTel", SqlDbType.VarChar,30),
					new SqlParameter("@ClientAddress", SqlDbType.VarChar,255),
					new SqlParameter("@Contactor", SqlDbType.VarChar,30),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@ClientID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.ClientCode;
            parameters[1].Value = model.ClientName;
            parameters[2].Value = model.ClientTel;
            parameters[3].Value = model.ClientAddress;
            parameters[4].Value = model.Contactor;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.ClientID;

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
        /// 删除客户
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        public bool DelClient(string ClientID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Client ");
            strSql.Append(" where ClientID=@ClientID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClientID", SqlDbType.VarChar,36)};
            parameters[0].Value = ClientID;

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
        /// 根据客户编号和名称判断是否已经存在
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        public bool Exists(string ClientCode, string ClientName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Client");
            strSql.Append(" where ClientCode=@ClientCode or ClientName=@ClientName");
            SqlParameter[] parameters = {
					new SqlParameter("@ClientCode", SqlDbType.VarChar,60),
                    new SqlParameter("@ClientName", SqlDbType.VarChar,255)};
            parameters[0].Value = ClientCode;
            parameters[1].Value = ClientName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据客户名称判断是否已经存在
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        public bool Exists4Update(string ClientCode, string ClientName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Client");
            strSql.Append(" where ClientCode=@ClientCode and ClientName != @ClientName");
            SqlParameter[] parameters = {
                    new SqlParameter("@ClientCode", SqlDbType.VarChar,60),
                    new SqlParameter("@ClientName", SqlDbType.VarChar,255)};
            parameters[0].Value = ClientCode;
            parameters[1].Value = ClientName;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string ClientCode, string ClientName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(ClientCode))
            {
                strSql.Append(" and ClientCode like'%" + ClientCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(ClientName))
            {
                strSql.Append(" and ClientName like'%" + ClientName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by ClientCode asc) as RowId from Client where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        public int GetRowCounts(string ClientCode, string ClientName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from Client where 1=1 ");

            if (!string.IsNullOrEmpty(ClientCode))
            {
                strSql.Append(" and ClientCode like'%" + ClientCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(ClientName))
            {
                strSql.Append(" and ClientName like'%" + ClientName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取客户模型
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        public Client GetModel(string ClientID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ClientID,ClientCode,ClientName,ClientTel,ClientAddress,Contactor,Remark from Client ");
            strSql.Append(" where ClientID=@ClientID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ClientID", SqlDbType.VarChar,36)};
            parameters[0].Value = ClientID;

            Client model = new Client();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ClientID"] != null && ds.Tables[0].Rows[0]["ClientID"].ToString() != "")
                {
                    model.ClientID = ds.Tables[0].Rows[0]["ClientID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClientCode"] != null && ds.Tables[0].Rows[0]["ClientCode"].ToString() != "")
                {
                    model.ClientCode = ds.Tables[0].Rows[0]["ClientCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClientName"] != null && ds.Tables[0].Rows[0]["ClientName"].ToString() != "")
                {
                    model.ClientName = ds.Tables[0].Rows[0]["ClientName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClientTel"] != null && ds.Tables[0].Rows[0]["ClientTel"].ToString() != "")
                {
                    model.ClientTel = ds.Tables[0].Rows[0]["ClientTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ClientAddress"] != null && ds.Tables[0].Rows[0]["ClientAddress"].ToString() != "")
                {
                    model.ClientAddress = ds.Tables[0].Rows[0]["ClientAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Contactor"] != null && ds.Tables[0].Rows[0]["Contactor"].ToString() != "")
                {
                    model.Contactor = ds.Tables[0].Rows[0]["Contactor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取客户List
        /// </summary>
        /// <returns></returns>
        public IList<Client> GetClientList()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select * from Client ");
                IList<Client> list = new List<Client>();

                DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Client client = new Client();
                        if (dr["ClientID"] != null && dr["ClientID"].ToString() != "")
                        {
                            client.ClientID = dr["ClientID"].ToString();
                        }
                        if (dr["ClientCode"] != null && dr["ClientCode"].ToString() != "")
                        {
                            client.ClientCode = dr["ClientCode"].ToString();
                        }
                        if (dr["ClientName"] != null && dr["ClientName"].ToString() != "")
                        {
                            client.ClientName = dr["ClientName"].ToString();
                        }
                        if (dr["ClientTel"] != null && dr["ClientTel"].ToString() != "")
                        {
                            client.ClientTel = dr["ClientTel"].ToString();
                        }
                        if (dr["ClientAddress"] != null && dr["ClientAddress"].ToString() != "")
                        {
                            client.ClientAddress = dr["ClientAddress"].ToString();
                        }
                        if (dr["Contactor"] != null && dr["Contactor"].ToString() != "")
                        {
                            client.Contactor = dr["Contactor"].ToString();
                        }
                        if (dr["Remark"] != null && dr["Remark"].ToString() != "")
                        {
                            client.Remark = dr["Remark"].ToString();
                        }
                        list.Add(client);
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
