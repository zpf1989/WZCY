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
    public class PostDAL : IPostDAL
    {
        /// <summary>
        /// 新增岗位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddPost(PostInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_Post(");
            strSql.Append("PostID,PostCode,PostName,ParentPostID,DeptID)");
            strSql.Append(" values (");
            strSql.Append("@PostID,@PostCode,@PostName,@ParentPostID,@DeptID)");
            SqlParameter[] parameters = {
					new SqlParameter("@PostID", SqlDbType.VarChar,36),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PostName", SqlDbType.NVarChar,60),
					new SqlParameter("@ParentPostID", SqlDbType.VarChar,36),
					new SqlParameter("@DeptID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.PostID;
            parameters[1].Value = model.PostCode;
            parameters[2].Value = model.PostName;
            parameters[3].Value = model.ParentPostID;
            parameters[4].Value = model.DeptID;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="PostCode">岗位编号</param>
        /// <param name="PostName">岗位名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string PostCode, string PostName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(PostCode))
            {
                strSql.Append(" and PostCode like'%" + PostCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(PostName))
            {
                strSql.Append(" and PostName like'%" + PostName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by PostCode desc) as RowId from ")
                .Append(" (select P.*,D.DeptName from OA_Post P ")
                .Append(" left join OA_Dept D on D.DeptID=P.DeptID)tmp ")
                .Append(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="PostCode">岗位编号</param>
        /// <param name="PostName">岗位名称</param>
        /// <returns></returns>
        public int GetRowCounts(string PostCode, string PostName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from OA_Post where 1=1 ");

            if (!string.IsNullOrEmpty(PostCode))
            {
                strSql.Append(" and PostCode like'%" + PostCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(PostName))
            {
                strSql.Append(" and PostName like'%" + PostName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取岗位模型
        /// </summary>
        /// <param name="PostID">岗位ID</param>
        /// <returns></returns>
        public PostInfo GetModel(string PostID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PostID,PostCode,PostName,ParentPostID,DeptID from OA_Post ");
            strSql.Append(" where PostID=@PostID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PostID", SqlDbType.VarChar,36)};
            parameters[0].Value = PostID;

            PostInfo model = new PostInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PostID"] != null && ds.Tables[0].Rows[0]["PostID"].ToString() != "")
                {
                    model.PostID = ds.Tables[0].Rows[0]["PostID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PostCode"] != null && ds.Tables[0].Rows[0]["PostCode"].ToString() != "")
                {
                    model.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PostName"] != null && ds.Tables[0].Rows[0]["PostName"].ToString() != "")
                {
                    model.PostName = ds.Tables[0].Rows[0]["PostName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentPostID"] != null && ds.Tables[0].Rows[0]["ParentPostID"].ToString() != "")
                {
                    model.ParentPostID = ds.Tables[0].Rows[0]["ParentPostID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeptID"] != null && ds.Tables[0].Rows[0]["DeptID"].ToString() != "")
                {
                    model.DeptID = ds.Tables[0].Rows[0]["DeptID"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePost(PostInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_Post set ");
            strSql.Append("PostCode=@PostCode,");
            strSql.Append("PostName=@PostName,");
            strSql.Append("ParentPostID=@ParentPostID,");
            strSql.Append("DeptID=@DeptID");
            strSql.Append(" where PostID=@PostID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PostCode", SqlDbType.NVarChar,50),
					new SqlParameter("@PostName", SqlDbType.NVarChar,60),
					new SqlParameter("@ParentPostID", SqlDbType.VarChar,36),
					new SqlParameter("@DeptID", SqlDbType.VarChar,36),
					new SqlParameter("@PostID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.PostCode;
            parameters[1].Value = model.PostName;
            parameters[2].Value = model.ParentPostID;
            parameters[3].Value = model.DeptID;
            parameters[4].Value = model.PostID;

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
        /// 删除岗位
        /// </summary>
        /// <param name="PostID"></param>
        /// <returns></returns>
        public bool DeletePost(string PostID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_Post ");
            strSql.Append(" where PostID=@PostID ");
            SqlParameter[] parameters = {
					new SqlParameter("@PostID", SqlDbType.VarChar,36)};
            parameters[0].Value = PostID;

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
        /// 获取岗位DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from OA_Post ");

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
        }

    }
}
