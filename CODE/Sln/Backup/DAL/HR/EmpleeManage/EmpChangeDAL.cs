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
    public class EmpChangeDAL : IEmpChangeDAL
    {
        /// <summary>
        /// 新增员工变动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddEmpChange(EmpChange model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_EmpChange(");
            strSql.Append("EmpChangeID,EmpID,ChangeDate,ChangeType,OldPostID,NewPostID,ChangeReason,DeptManagerID,DeptView,HRManagerID,HRView,ManagerID,MView,State,CreatorID,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@EmpChangeID,@EmpID,@ChangeDate,@ChangeType,@OldPostID,@NewPostID,@ChangeReason,@DeptManagerID,@DeptView,@HRManagerID,@HRView,@ManagerID,@MView,@State,@CreatorID,@CreateTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@EmpChangeID", SqlDbType.VarChar,36),
					new SqlParameter("@EmpID", SqlDbType.VarChar,36),
					new SqlParameter("@ChangeDate", SqlDbType.Char,8),
					new SqlParameter("@ChangeType", SqlDbType.Char,1),
					new SqlParameter("@OldPostID", SqlDbType.VarChar,36),
					new SqlParameter("@NewPostID", SqlDbType.VarChar,36),
					new SqlParameter("@ChangeReason", SqlDbType.NVarChar,200),
					new SqlParameter("@DeptManagerID", SqlDbType.VarChar,36),
					new SqlParameter("@DeptView", SqlDbType.NVarChar,200),
					new SqlParameter("@HRManagerID", SqlDbType.VarChar,36),
					new SqlParameter("@HRView", SqlDbType.NVarChar,200),
					new SqlParameter("@ManagerID", SqlDbType.VarChar,36),
					new SqlParameter("@MView", SqlDbType.NVarChar,200),
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@CreatorID", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.EmpChangeID;
            parameters[1].Value = model.EmpID;
            parameters[2].Value = model.ChangeDate;
            parameters[3].Value = model.ChangeType;
            parameters[4].Value = model.OldPostID;
            parameters[5].Value = model.NewPostID;
            parameters[6].Value = model.ChangeReason;
            parameters[7].Value = model.DeptManagerID;
            parameters[8].Value = model.DeptView;
            parameters[9].Value = model.HRManagerID;
            parameters[10].Value = model.HRView;
            parameters[11].Value = model.ManagerID;
            parameters[12].Value = model.MView;
            parameters[13].Value = model.State;
            parameters[14].Value = model.CreatorID;
            parameters[15].Value = model.CreateTime;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="EmpName">姓名</param>
        /// <param name="ChangeDate">变动时间</param>
        /// <param name="UserId">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string EmpName,string ChangeDate,string UserId, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(EmpName))
            {
                strSql.Append(" and EmpName like '%" + EmpName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(ChangeDate))
            {
                strSql.Append(" and ChangeDate ='" + Convert.ToDateTime(ChangeDate.Trim()).ToString("yyyyMMdd") + "' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and CreatorID ='" + UserId.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by ChangeDate desc) as RowId from ")
                .Append(" (select EC.*,E.EmpName,P1.PostName as OldPostName,P2.PostName as NewPostName ")
                .Append(" from OA_EmpChange EC ")
                .Append(" left join OA_Emplee E on E.EmpID=EC.EmpID ")
                .Append(" left join OA_Post P1 on P1.PostID=EC.OldPostID ")
                .Append(" left join OA_Post P2 on P2.PostID=EC.NewPostID)tmp ")
                .Append(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="EmpName">姓名</param>
        /// <param name="ChangeDate">变动时间</param>
        /// <param name="UserId">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string EmpName, string ChangeDate, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ");
            strSql.AppendLine(" (select EC.*,E.EmpName,P1.PostName as OldPostName,P2.PostName as NewPostName ");
            strSql.AppendLine(" from OA_EmpChange EC ");
            strSql.AppendLine(" left join OA_Emplee E on E.EmpID=EC.EmpID ");
            strSql.AppendLine(" left join OA_Post P1 on P1.PostID=EC.OldPostID ");
            strSql.AppendLine(" left join OA_Post P2 on P2.PostID=EC.NewPostID)tmp ");
            strSql.AppendLine("  where 1=1 ");

            if (!string.IsNullOrEmpty(EmpName))
            {
                strSql.Append(" and EmpName like '%" + EmpName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(ChangeDate))
            {
                strSql.Append(" and ChangeDate ='" + Convert.ToDateTime(ChangeDate.Trim()).ToString("yyyyMMdd") + "' ");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                strSql.Append(" and CreatorID ='" + UserId.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="EmpChangeID"></param>
        /// <returns></returns>
        public EmpChange GetModel(string EmpChangeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 EmpChangeID,EmpID,ChangeDate,ChangeType,OldPostID,NewPostID,ChangeReason,DeptManagerID,DeptView,HRManagerID,HRView,ManagerID,MView,State,CreatorID,CreateTime from OA_EmpChange ");
            strSql.Append(" where EmpChangeID=@EmpChangeID ");
            SqlParameter[] parameters = {
					new SqlParameter("@EmpChangeID", SqlDbType.VarChar,36)};
            parameters[0].Value = EmpChangeID;

            EmpChange model = new EmpChange();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["EmpChangeID"] != null && ds.Tables[0].Rows[0]["EmpChangeID"].ToString() != "")
                {
                    model.EmpChangeID = ds.Tables[0].Rows[0]["EmpChangeID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpID"] != null && ds.Tables[0].Rows[0]["EmpID"].ToString() != "")
                {
                    model.EmpID = ds.Tables[0].Rows[0]["EmpID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChangeDate"] != null && ds.Tables[0].Rows[0]["ChangeDate"].ToString() != "")
                {
                    model.ChangeDate = ds.Tables[0].Rows[0]["ChangeDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChangeType"] != null && ds.Tables[0].Rows[0]["ChangeType"].ToString() != "")
                {
                    model.ChangeType = ds.Tables[0].Rows[0]["ChangeType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OldPostID"] != null && ds.Tables[0].Rows[0]["OldPostID"].ToString() != "")
                {
                    model.OldPostID = ds.Tables[0].Rows[0]["OldPostID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NewPostID"] != null && ds.Tables[0].Rows[0]["NewPostID"].ToString() != "")
                {
                    model.NewPostID = ds.Tables[0].Rows[0]["NewPostID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ChangeReason"] != null && ds.Tables[0].Rows[0]["ChangeReason"].ToString() != "")
                {
                    model.ChangeReason = ds.Tables[0].Rows[0]["ChangeReason"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeptManagerID"] != null && ds.Tables[0].Rows[0]["DeptManagerID"].ToString() != "")
                {
                    model.DeptManagerID = ds.Tables[0].Rows[0]["DeptManagerID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DeptView"] != null && ds.Tables[0].Rows[0]["DeptView"].ToString() != "")
                {
                    model.DeptView = ds.Tables[0].Rows[0]["DeptView"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HRManagerID"] != null && ds.Tables[0].Rows[0]["HRManagerID"].ToString() != "")
                {
                    model.HRManagerID = ds.Tables[0].Rows[0]["HRManagerID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HRView"] != null && ds.Tables[0].Rows[0]["HRView"].ToString() != "")
                {
                    model.HRView = ds.Tables[0].Rows[0]["HRView"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ManagerID"] != null && ds.Tables[0].Rows[0]["ManagerID"].ToString() != "")
                {
                    model.ManagerID = ds.Tables[0].Rows[0]["ManagerID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MView"] != null && ds.Tables[0].Rows[0]["MView"].ToString() != "")
                {
                    model.MView = ds.Tables[0].Rows[0]["MView"].ToString();
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreatorID"] != null && ds.Tables[0].Rows[0]["CreatorID"].ToString() != "")
                {
                    model.CreatorID = ds.Tables[0].Rows[0]["CreatorID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
