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
    public class OfficeDocDAL : IOfficeDocDAL
    {
        /// <summary>
        /// 新增公文
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOfficeDoc(OfficeDoc model,IList<OfficeDocItem> list)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_OfficeDoc(");
            strSql.Append("OfficeDocID,Title,Contents,WriterID,WriteTime)");
            strSql.Append(" values (");
            strSql.Append("@OfficeDocID,@Title,@Contents,@WriterID,@WriteTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocID", SqlDbType.VarChar,36),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@WriterID", SqlDbType.VarChar,36),
					new SqlParameter("@WriteTime", SqlDbType.DateTime)};
            parameters[0].Value = model.OfficeDocID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Contents;
            parameters[3].Value = model.WriterID;
            parameters[4].Value = model.WriteTime;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                OfficeDocItemDAL itemBll = new OfficeDocItemDAL();

                for (int i = 0; i < list.Count; i++)
                {
                    OfficeDocItem item = list[i] as OfficeDocItem;
                    itemBll.AddOfficeDocItem(item);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return 1;
        }
        /// <summary>
        /// 修改公文
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateOfficeDoc(OfficeDoc model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_OfficeDoc set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Contents=@Contents");
            strSql.Append(" where OfficeDocID=@OfficeDocID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@OfficeDocID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Contents;
            parameters[2].Value = model.OfficeDocID;

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
        /// 删除公文
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        public int DelOfficeDoc(string OfficeDocID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_OfficeDoc ");
            strSql.Append(" where OfficeDocID=@OfficeDocID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocID", SqlDbType.VarChar,36)};
            parameters[0].Value = OfficeDocID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                OfficeDocItemDAL itemBll = new OfficeDocItemDAL();
                itemBll.DelOfficeDocItem(OfficeDocID);

                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return 1;
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="WriterID">拟稿人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string Title, string StartTime, string EndTime, string WriterID, int pageSize, int startRowIndex)
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
            if (!string.IsNullOrEmpty(WriterID))
            {
                strSql.Append(" and WriterID ='" + WriterID.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by WriteTime desc) as RowId from OA_OfficeDoc ")
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
        /// <param name="WriterID">拟稿人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string Title, string StartTime, string EndTime, string WriterID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from OA_OfficeDoc where 1=1 ");

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
            if (!string.IsNullOrEmpty(WriterID))
            {
                strSql.Append(" and WriterID ='" + WriterID.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取我的公文
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        public OfficeDoc GetModel(string OfficeDocID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OfficeDocID,Title,Contents,WriterID,WriteTime from OA_OfficeDoc ");
            strSql.Append(" where OfficeDocID=@OfficeDocID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocID", SqlDbType.VarChar,36)};
            parameters[0].Value = OfficeDocID;

            OfficeDoc model = new OfficeDoc();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OfficeDocID"] != null && ds.Tables[0].Rows[0]["OfficeDocID"].ToString() != "")
                {
                    model.OfficeDocID = ds.Tables[0].Rows[0]["OfficeDocID"].ToString();
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
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取我的公文集合
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        public DataSet GetList(string OfficeDocID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OfficeDocID,Title,Contents,WriterID,WriteTime,U.UserCode,U.UserName from OA_OfficeDoc Doc ");
            strSql.Append(" left join OA_User U on U.UserID=Doc.WriterID ");
            strSql.Append(" where OfficeDocID=@OfficeDocID ");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeDocID", SqlDbType.VarChar,36)};
            parameters[0].Value = OfficeDocID;

            OfficeDoc model = new OfficeDoc();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
    }
}
