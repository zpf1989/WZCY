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
    public class OfficeDocItemDAL : IOfficeDocItemDAL
    {
        /// <summary>
        /// 新增公文行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOfficeDocItem(OfficeDocItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_OfficeDocItem(");
            strSql.Append("OfficeDocItemID,OfficeDocID,ReceiverID,OperateType,state)");
            strSql.Append(" values (");
            strSql.Append("@OfficeDocItemID,@OfficeDocID,@ReceiverID,@OperateType,@state)");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocItemID", SqlDbType.VarChar,36),
					new SqlParameter("@OfficeDocID", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiverID", SqlDbType.VarChar,36),
					new SqlParameter("@OperateType", SqlDbType.Char,1),
					new SqlParameter("@state", SqlDbType.Char,1)};
            parameters[0].Value = model.OfficeDocItemID;
            parameters[1].Value = model.OfficeDocID;
            parameters[2].Value = model.ReceiverID;
            parameters[3].Value = model.OperateType;
            parameters[4].Value = model.state;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 删除公文行
        /// </summary>
        /// <param name="OfficeDocID">公文ID</param>
        /// <returns></returns>
        public bool DelOfficeDocItem(string OfficeDocID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_OfficeDocItem ");
            strSql.Append(" where OfficeDocID=@OfficeDocID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocID", SqlDbType.VarChar,36)};
            parameters[0].Value = OfficeDocID;

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
        /// 删除公文行
        /// </summary>
        /// <param name="OfficeDocItemID">公文行ID</param>
        /// <returns></returns>
        public bool DelItem(string OfficeDocItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_OfficeDocItem ");
            strSql.Append(" where OfficeDocItemID=@OfficeDocItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocItemID", SqlDbType.VarChar,36)};
            parameters[0].Value = OfficeDocItemID;

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
        /// 改变操作状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ChangeState(OfficeDocItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_OfficeDocItem set ");
            strSql.Append("Opinion=@Opinion,");
            strSql.Append("state=@state,");
            strSql.Append("OperateTime=@OperateTime");
            strSql.Append(" where OfficeDocItemID=@OfficeDocItemID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Opinion", SqlDbType.NVarChar,500),
					new SqlParameter("@state", SqlDbType.Char,1),
					new SqlParameter("@OperateTime", SqlDbType.DateTime),
					new SqlParameter("@OfficeDocItemID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.Opinion;
            parameters[1].Value = model.state;
            parameters[2].Value = model.OperateTime;
            parameters[3].Value = model.OfficeDocItemID;

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
        /// 返回分页列表集合
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="ReceiverID">办理人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string Title, string StartTime, string EndTime, string ReceiverID, string State, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(Title))
            {
                strSql.Append(" and Title like '%" + Title.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                strSql.Append(" and WriteTime >='" + StartTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                strSql.Append(" and WriteTime <='" + EndTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(ReceiverID))
            {
                strSql.Append(" and ReceiverID ='" + ReceiverID.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(State))
            {
                strSql.Append(" and state in (" + State + ") ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select A.*,B.OfficeDocItemID,B.state,U.UserName,row_number() OVER (order by WriteTime desc) as RowId from OA_OfficeDoc A ")
                .Append(" inner join OA_OfficeDocItem B on A.OfficeDocID=B.OfficeDocID ")
                .Append(" left join OA_User U on U.UserID=A.WriterID ")
                .Append(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="ReceiverID">办理人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string Title, string StartTime, string EndTime, string ReceiverID, string State)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from OA_OfficeDoc A ");
            strSql.AppendLine(" inner join OA_OfficeDocItem B on A.OfficeDocID=B.OfficeDocID ");
            strSql.AppendLine(" left join OA_User U on U.UserID=A.WriterID ");
            strSql.AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(Title))
            {
                strSql.Append(" and Title like '%" + Title.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                strSql.Append(" and WriteTime >='" + StartTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                strSql.Append(" and WriteTime <='" + EndTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(ReceiverID))
            {
                strSql.Append(" and ReceiverID ='" + ReceiverID.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(State))
            {
                strSql.Append(" and state in (" + State + ") ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="OfficeDocID">公文ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string OfficeDocID, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(OfficeDocID))
            {
                strSql.Append(" and A.OfficeDocID = '" + OfficeDocID.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select B.*,A.Title,A.Contents,U.UserCode,U.UserName as Receiver,row_number() OVER (order by WriteTime desc) as RowId ")
                .Append(" from OA_OfficeDocItem B ")
                .Append(" left join OA_OfficeDoc A on A.OfficeDocID=B.OfficeDocID ")
                .Append(" left join OA_User U on U.UserID=B.ReceiverID ")
                .Append(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="OfficeDocID">公文ID</param>
        /// <returns></returns>
        public int GetRowCounts(string OfficeDocID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from OA_OfficeDocItem ");
            strSql.AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(OfficeDocID))
            {
                strSql.Append(" and OfficeDocID = '" + OfficeDocID.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取办理公文模型
        /// </summary>
        /// <param name="OfficeDocItemID"></param>
        /// <returns></returns>
        public OfficeDocItem GetModel(string OfficeDocItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top 1 B.*,A.Title,A.Contents,A.WriterID,A.WriteTime,U.UserName from OA_OfficeDoc A ");
            strSql.AppendFormat(" inner join OA_OfficeDocItem B on A.OfficeDocID=B.OfficeDocID ");
            strSql.AppendFormat(" left join OA_User U on U.UserID=A.WriterID where OfficeDocItemID = '{0}' ", OfficeDocItemID);
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocItemID", SqlDbType.VarChar,36)};
            parameters[0].Value = OfficeDocItemID;

            OfficeDocItem model = new OfficeDocItem();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OfficeDocItemID"] != null && ds.Tables[0].Rows[0]["OfficeDocItemID"].ToString() != "")
                {
                    model.OfficeDocItemID = ds.Tables[0].Rows[0]["OfficeDocItemID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OfficeDocID"] != null && ds.Tables[0].Rows[0]["OfficeDocID"].ToString() != "")
                {
                    model.OfficeDocID = ds.Tables[0].Rows[0]["OfficeDocID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ReceiverID"] != null && ds.Tables[0].Rows[0]["ReceiverID"].ToString() != "")
                {
                    model.ReceiverID = ds.Tables[0].Rows[0]["ReceiverID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Opinion"] != null && ds.Tables[0].Rows[0]["Opinion"].ToString() != "")
                {
                    model.Opinion = ds.Tables[0].Rows[0]["Opinion"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OperateType"] != null && ds.Tables[0].Rows[0]["OperateType"].ToString() != "")
                {
                    model.OperateType = ds.Tables[0].Rows[0]["OperateType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["state"] != null && ds.Tables[0].Rows[0]["state"].ToString() != "")
                {
                    model.state = ds.Tables[0].Rows[0]["state"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OperateTime"] != null && ds.Tables[0].Rows[0]["OperateTime"].ToString() != "")
                {
                    model.OperateTime = DateTime.Parse(ds.Tables[0].Rows[0]["OperateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Contents"] != null && ds.Tables[0].Rows[0]["Contents"].ToString() != "")
                {
                    model.Contents = ds.Tables[0].Rows[0]["Contents"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WriterID"] != null && ds.Tables[0].Rows[0]["WriterID"].ToString() != "")
                {
                    model.WriterID = ds.Tables[0].Rows[0]["WriterID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["WriteTime"] != null && ds.Tables[0].Rows[0]["WriteTime"].ToString() != "")
                {
                    model.WriteTime = DateTime.Parse(ds.Tables[0].Rows[0]["WriteTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
                {
                    model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取公文
        /// </summary>
        /// <param name="ReceiverID"></param>
        /// <param name="State"></param>
        /// <param name="Flag">1：待办公文，2：已办公文</param>
        /// <returns></returns>
        public DataSet GetList(string ReceiverID, int Flag)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(ReceiverID))
            {
                strSql.Append(" and ReceiverID ='" + ReceiverID.Trim() + "' ");
            }
            if (Flag == 1)
                strSql.Append(" and state in ('1','4') ");
            else
                strSql.Append(" and state in ('2','3','5') ");

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" select A.*,B.OfficeDocItemID,B.state,B.OperateTime,U.UserName from OA_OfficeDoc A ")
                .Append(" inner join OA_OfficeDocItem B on A.OfficeDocID=B.OfficeDocID ")
                .Append(" left join OA_User U on U.UserID=A.WriterID ")
                .Append(" where 1=1 ")
                .AppendLine(strSql.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }

    }
}
