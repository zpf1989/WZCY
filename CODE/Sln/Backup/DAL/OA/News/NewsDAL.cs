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
    public class NewsDAL : INewsDAL
    {
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNews(NewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_News(");
            strSql.Append("NewID,Title,SubTitle,Contents,CreatorID,Writer,PublishTime)");
            strSql.Append(" values (");
            strSql.Append("@NewID,@Title,@SubTitle,@Contents,@CreatorID,@Writer,@PublishTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@NewID", SqlDbType.VarChar,36),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,100),
					new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,36),
					new SqlParameter("@Writer", SqlDbType.NVarChar,50),
					new SqlParameter("@PublishTime", SqlDbType.DateTime)};
            parameters[0].Value = model.NewID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.SubTitle;
            parameters[3].Value = model.Contents;
            parameters[4].Value = model.CreatorID;
            parameters[5].Value = model.Writer;
            parameters[6].Value = model.PublishTime;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateNews(NewsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_News set ");
            strSql.Append("Title=@Title,");
            strSql.Append("SubTitle=@SubTitle,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("Writer=@Writer,");
            strSql.Append("PublishTime=@PublishTime");
            strSql.Append(" where NewID=@NewID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@SubTitle", SqlDbType.NVarChar,100),
					new SqlParameter("@Contents", SqlDbType.Text),
					new SqlParameter("@Writer", SqlDbType.NVarChar,50),
					new SqlParameter("@PublishTime", SqlDbType.DateTime),
					new SqlParameter("@NewID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.SubTitle;
            parameters[2].Value = model.Contents;
            parameters[3].Value = model.Writer;
            parameters[4].Value = model.PublishTime;
            parameters[5].Value = model.NewID;

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
        /// 删除新闻
        /// </summary>
        /// <param name="NewID"></param>
        /// <returns></returns>
        public bool DeleteNews(string NewID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_News ");
            strSql.Append(" where NewID=@NewID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewID", SqlDbType.VarChar,36)};
            parameters[0].Value = NewID;

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
        /// <param name="CreatorID">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string Title, string StartTime, string EndTime, string CreatorID, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(Title))
            {
                strSql.Append(" and Title like '%" + Title.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                strSql.Append(" and PublishTime >='" + StartTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                strSql.Append(" and PublishTime <='" + EndTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(CreatorID))
            {
                strSql.Append(" and CreatorID ='" + CreatorID.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by PublishTime desc) as RowId from OA_News ")
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
        /// <param name="CreatorID">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string Title, string StartTime, string EndTime, string CreatorID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from OA_News where 1=1 ");

            if (!string.IsNullOrEmpty(Title))
            {
                strSql.Append(" and Title like '%" + Title.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                strSql.Append(" and PublishTime >='" + StartTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                strSql.Append(" and PublishTime <='" + EndTime.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(CreatorID))
            {
                strSql.Append(" and CreatorID ='" + CreatorID.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取新闻模型
        /// </summary>
        /// <param name="NewID"></param>
        /// <returns></returns>
        public NewsInfo GetModel(string NewID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 NewID,Title,SubTitle,Contents,CreatorID,Writer,PublishTime from OA_News ");
            strSql.Append(" where NewID=@NewID ");
            SqlParameter[] parameters = {
					new SqlParameter("@NewID", SqlDbType.VarChar,36)};
            parameters[0].Value = NewID;

            NewsInfo model = new NewsInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["NewID"] != null && ds.Tables[0].Rows[0]["NewID"].ToString() != "")
                {
                    model.NewID = ds.Tables[0].Rows[0]["NewID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Title"] != null && ds.Tables[0].Rows[0]["Title"].ToString() != "")
                {
                    model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SubTitle"] != null && ds.Tables[0].Rows[0]["SubTitle"].ToString() != "")
                {
                    model.SubTitle = ds.Tables[0].Rows[0]["SubTitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Contents"] != null && ds.Tables[0].Rows[0]["Contents"].ToString() != "")
                {
                    model.Contents = ds.Tables[0].Rows[0]["Contents"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatorID"] != null && ds.Tables[0].Rows[0]["CreatorID"].ToString() != "")
                {
                    model.CreatorID = ds.Tables[0].Rows[0]["CreatorID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Writer"] != null && ds.Tables[0].Rows[0]["Writer"].ToString() != "")
                {
                    model.Writer = ds.Tables[0].Rows[0]["Writer"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PublishTime"] != null && ds.Tables[0].Rows[0]["PublishTime"].ToString() != "")
                {
                    model.PublishTime = DateTime.Parse(ds.Tables[0].Rows[0]["PublishTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from OA_News order by PublishTime desc");
            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }
        

    }
}
