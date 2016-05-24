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
    public class EmpleeDAL : IEmpleeDAL
    {
        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddEmp(EmpleeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_Emplee(");
            strSql.Append("EmpID,EmpCode,EmpName,EmpSex,EmpAge,EmpBrithday,EmpNative,EmpNation,EmpPolitics,EmpPosition,EmpEducation,EmpSpecialty,EmpSchool,EmpState,EmpTel,EmpMobile,EmpEmail,Remart,CreateTime,CreateUserID,EmpCard,EmpAddress,EmpType,EmpDate,EmpContractStart,EmpContractEnd)");
            strSql.Append(" values (");
            strSql.Append("@EmpID,@EmpCode,@EmpName,@EmpSex,@EmpAge,@EmpBrithday,@EmpNative,@EmpNation,@EmpPolitics,@EmpPosition,@EmpEducation,@EmpSpecialty,@EmpSchool,@EmpState,@EmpTel,@EmpMobile,@EmpEmail,@Remart,@CreateTime,@CreateUserID,@EmpCard,@EmpAddress,@EmpType,@EmpDate,@EmpContractStart,@EmpContractEnd)");
            SqlParameter[] parameters = {
					new SqlParameter("@EmpID", SqlDbType.VarChar,36),
					new SqlParameter("@EmpCode", SqlDbType.VarChar,36),
					new SqlParameter("@EmpName", SqlDbType.VarChar,36),
					new SqlParameter("@EmpSex", SqlDbType.Char,1),
					new SqlParameter("@EmpAge", SqlDbType.Int,4),
					new SqlParameter("@EmpBrithday", SqlDbType.VarChar,10),
					new SqlParameter("@EmpNative", SqlDbType.VarChar,36),
					new SqlParameter("@EmpNation", SqlDbType.VarChar,20),
					new SqlParameter("@EmpPolitics", SqlDbType.VarChar,10),
					new SqlParameter("@EmpPosition", SqlDbType.VarChar,36),
					new SqlParameter("@EmpEducation", SqlDbType.VarChar,10),
					new SqlParameter("@EmpSpecialty", SqlDbType.VarChar,36),
					new SqlParameter("@EmpSchool", SqlDbType.VarChar,36),
					new SqlParameter("@EmpState", SqlDbType.Char,1),
					new SqlParameter("@EmpTel", SqlDbType.VarChar,20),
					new SqlParameter("@EmpMobile", SqlDbType.VarChar,20),
					new SqlParameter("@EmpEmail", SqlDbType.VarChar,36),
					new SqlParameter("@Remart", SqlDbType.VarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.VarChar,36),
					new SqlParameter("@EmpCard", SqlDbType.VarChar,18),
					new SqlParameter("@EmpAddress", SqlDbType.NVarChar,150),
					new SqlParameter("@EmpType", SqlDbType.Char,1),
					new SqlParameter("@EmpDate", SqlDbType.Char,8),
					new SqlParameter("@EmpContractStart", SqlDbType.Char,8),
					new SqlParameter("@EmpContractEnd", SqlDbType.Char,8)};
            parameters[0].Value = model.EmpID;
            parameters[1].Value = model.EmpCode;
            parameters[2].Value = model.EmpName;
            parameters[3].Value = model.EmpSex;
            parameters[4].Value = model.EmpAge;
            parameters[5].Value = model.EmpBrithday;
            parameters[6].Value = model.EmpNative;
            parameters[7].Value = model.EmpNation;
            parameters[8].Value = model.EmpPolitics;
            parameters[9].Value = model.EmpPosition;
            parameters[10].Value = model.EmpEducation;
            parameters[11].Value = model.EmpSpecialty;
            parameters[12].Value = model.EmpSchool;
            parameters[13].Value = model.EmpState;
            parameters[14].Value = model.EmpTel;
            parameters[15].Value = model.EmpMobile;
            parameters[16].Value = model.EmpEmail;
            parameters[17].Value = model.Remart;
            parameters[18].Value = model.CreateTime;
            parameters[19].Value = model.CreateUserID;
            parameters[20].Value = model.EmpCard;
            parameters[21].Value = model.EmpAddress;
            parameters[22].Value = model.EmpType;
            parameters[23].Value = model.EmpDate;
            parameters[24].Value = model.EmpContractStart;
            parameters[25].Value = model.EmpContractEnd;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEmp(EmpleeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OA_Emplee set ");
            strSql.Append("EmpCode=@EmpCode,");
            strSql.Append("EmpName=@EmpName,");
            strSql.Append("EmpSex=@EmpSex,");
            strSql.Append("EmpAge=@EmpAge,");
            strSql.Append("EmpBrithday=@EmpBrithday,");
            strSql.Append("EmpNative=@EmpNative,");
            strSql.Append("EmpNation=@EmpNation,");
            strSql.Append("EmpPolitics=@EmpPolitics,");
            strSql.Append("EmpPosition=@EmpPosition,");
            strSql.Append("EmpEducation=@EmpEducation,");
            strSql.Append("EmpSpecialty=@EmpSpecialty,");
            strSql.Append("EmpSchool=@EmpSchool,");
            strSql.Append("EmpState=@EmpState,");
            strSql.Append("EmpTel=@EmpTel,");
            strSql.Append("EmpMobile=@EmpMobile,");
            strSql.Append("EmpEmail=@EmpEmail,");
            strSql.Append("Remart=@Remart,");
            strSql.Append("EmpCard=@EmpCard,");
            strSql.Append("EmpAddress=@EmpAddress,");
            strSql.Append("EmpType=@EmpType,");
            strSql.Append("EmpDate=@EmpDate,");
            strSql.Append("EmpContractStart=@EmpContractStart,");
            strSql.Append("EmpContractEnd=@EmpContractEnd");
            strSql.Append(" where EmpID=@EmpID ");
            SqlParameter[] parameters = {
					new SqlParameter("@EmpCode", SqlDbType.VarChar,36),
					new SqlParameter("@EmpName", SqlDbType.VarChar,36),
					new SqlParameter("@EmpSex", SqlDbType.Char,1),
					new SqlParameter("@EmpAge", SqlDbType.Int,4),
					new SqlParameter("@EmpBrithday", SqlDbType.VarChar,10),
					new SqlParameter("@EmpNative", SqlDbType.VarChar,36),
					new SqlParameter("@EmpNation", SqlDbType.VarChar,20),
					new SqlParameter("@EmpPolitics", SqlDbType.VarChar,10),
					new SqlParameter("@EmpPosition", SqlDbType.VarChar,36),
					new SqlParameter("@EmpEducation", SqlDbType.VarChar,10),
					new SqlParameter("@EmpSpecialty", SqlDbType.VarChar,36),
					new SqlParameter("@EmpSchool", SqlDbType.VarChar,36),
					new SqlParameter("@EmpState", SqlDbType.Char,1),
					new SqlParameter("@EmpTel", SqlDbType.VarChar,20),
					new SqlParameter("@EmpMobile", SqlDbType.VarChar,20),
					new SqlParameter("@EmpEmail", SqlDbType.VarChar,36),
					new SqlParameter("@Remart", SqlDbType.VarChar,200),
					new SqlParameter("@EmpCard", SqlDbType.VarChar,18),
					new SqlParameter("@EmpAddress", SqlDbType.NVarChar,150),
					new SqlParameter("@EmpType", SqlDbType.Char,1),
					new SqlParameter("@EmpDate", SqlDbType.Char,8),
					new SqlParameter("@EmpContractStart", SqlDbType.Char,8),
					new SqlParameter("@EmpContractEnd", SqlDbType.Char,8),
					new SqlParameter("@EmpID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.EmpCode;
            parameters[1].Value = model.EmpName;
            parameters[2].Value = model.EmpSex;
            parameters[3].Value = model.EmpAge;
            parameters[4].Value = model.EmpBrithday;
            parameters[5].Value = model.EmpNative;
            parameters[6].Value = model.EmpNation;
            parameters[7].Value = model.EmpPolitics;
            parameters[8].Value = model.EmpPosition;
            parameters[9].Value = model.EmpEducation;
            parameters[10].Value = model.EmpSpecialty;
            parameters[11].Value = model.EmpSchool;
            parameters[12].Value = model.EmpState;
            parameters[13].Value = model.EmpTel;
            parameters[14].Value = model.EmpMobile;
            parameters[15].Value = model.EmpEmail;
            parameters[16].Value = model.Remart;
            parameters[17].Value = model.EmpCard;
            parameters[18].Value = model.EmpAddress;
            parameters[19].Value = model.EmpType;
            parameters[20].Value = model.EmpDate;
            parameters[21].Value = model.EmpContractStart;
            parameters[22].Value = model.EmpContractEnd;
            parameters[23].Value = model.EmpID;

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
        /// 删除员工
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public bool DeleteEmp(string EmpID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OA_Emplee ");
            strSql.Append(" where EmpID=@EmpID ");
            SqlParameter[] parameters = {
					new SqlParameter("@EmpID", SqlDbType.VarChar,36)};
            parameters[0].Value = EmpID;

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
        /// <param name="EmpCode">员工编号</param>
        /// <param name="EmpName">员工名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string EmpCode, string EmpName, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(EmpCode))
            {
                strSql.Append(" and EmpCode like'%" + EmpCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(EmpName))
            {
                strSql.Append(" and EmpName like'%" + EmpName.Trim() + "%' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select *,row_number() OVER (order by EmpCode desc) as RowId from ")
                .Append(" (select E.*,P.PostCode,P.PostName,D.DeptCode,D.DeptName from OA_Emplee E ")
                .Append(" left join OA_Post P on P.PostID=E.EmpPosition ")
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
        /// <param name="EmpCode">员工编号</param>
        /// <param name="EmpName">员工名称</param>
        /// <returns></returns>
        public int GetRowCounts(string EmpCode, string EmpName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(*) from OA_Emplee where 1=1 ");

            if (!string.IsNullOrEmpty(EmpCode))
            {
                strSql.Append(" and EmpCode like'%" + EmpCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(EmpName))
            {
                strSql.Append(" and EmpName like'%" + EmpName.Trim() + "%' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        /// <summary>
        /// 获取员工模型
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public EmpleeInfo GetModel(string EmpID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 EmpID,EmpCode,EmpName,EmpSex,EmpAge,EmpBrithday,EmpNative,EmpNation,EmpPolitics,EmpPosition,EmpEducation,EmpSpecialty,EmpSchool,EmpState,EmpTel,EmpMobile,EmpEmail,Remart,CreateTime,CreateUserID,EmpCard,EmpAddress,EmpType,EmpDate,EmpContractStart,EmpContractEnd from OA_Emplee ");
            strSql.Append(" where EmpID=@EmpID ");
            SqlParameter[] parameters = {
					new SqlParameter("@EmpID", SqlDbType.VarChar,36)};
            parameters[0].Value = EmpID;

            EmpleeInfo model = new EmpleeInfo();
            DataSet ds = DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["EmpID"] != null && ds.Tables[0].Rows[0]["EmpID"].ToString() != "")
                {
                    model.EmpID = ds.Tables[0].Rows[0]["EmpID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpCode"] != null && ds.Tables[0].Rows[0]["EmpCode"].ToString() != "")
                {
                    model.EmpCode = ds.Tables[0].Rows[0]["EmpCode"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpName"] != null && ds.Tables[0].Rows[0]["EmpName"].ToString() != "")
                {
                    model.EmpName = ds.Tables[0].Rows[0]["EmpName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpSex"] != null && ds.Tables[0].Rows[0]["EmpSex"].ToString() != "")
                {
                    model.EmpSex = ds.Tables[0].Rows[0]["EmpSex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpAge"] != null && ds.Tables[0].Rows[0]["EmpAge"].ToString() != "")
                {
                    model.EmpAge = int.Parse(ds.Tables[0].Rows[0]["EmpAge"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EmpBrithday"] != null && ds.Tables[0].Rows[0]["EmpBrithday"].ToString() != "")
                {
                    model.EmpBrithday = ds.Tables[0].Rows[0]["EmpBrithday"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpNative"] != null && ds.Tables[0].Rows[0]["EmpNative"].ToString() != "")
                {
                    model.EmpNative = ds.Tables[0].Rows[0]["EmpNative"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpNation"] != null && ds.Tables[0].Rows[0]["EmpNation"].ToString() != "")
                {
                    model.EmpNation = ds.Tables[0].Rows[0]["EmpNation"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpPolitics"] != null && ds.Tables[0].Rows[0]["EmpPolitics"].ToString() != "")
                {
                    model.EmpPolitics = ds.Tables[0].Rows[0]["EmpPolitics"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpPosition"] != null && ds.Tables[0].Rows[0]["EmpPosition"].ToString() != "")
                {
                    model.EmpPosition = ds.Tables[0].Rows[0]["EmpPosition"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpEducation"] != null && ds.Tables[0].Rows[0]["EmpEducation"].ToString() != "")
                {
                    model.EmpEducation = ds.Tables[0].Rows[0]["EmpEducation"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpSpecialty"] != null && ds.Tables[0].Rows[0]["EmpSpecialty"].ToString() != "")
                {
                    model.EmpSpecialty = ds.Tables[0].Rows[0]["EmpSpecialty"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpSchool"] != null && ds.Tables[0].Rows[0]["EmpSchool"].ToString() != "")
                {
                    model.EmpSchool = ds.Tables[0].Rows[0]["EmpSchool"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpState"] != null && ds.Tables[0].Rows[0]["EmpState"].ToString() != "")
                {
                    model.EmpState = ds.Tables[0].Rows[0]["EmpState"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpTel"] != null && ds.Tables[0].Rows[0]["EmpTel"].ToString() != "")
                {
                    model.EmpTel = ds.Tables[0].Rows[0]["EmpTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpMobile"] != null && ds.Tables[0].Rows[0]["EmpMobile"].ToString() != "")
                {
                    model.EmpMobile = ds.Tables[0].Rows[0]["EmpMobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpEmail"] != null && ds.Tables[0].Rows[0]["EmpEmail"].ToString() != "")
                {
                    model.EmpEmail = ds.Tables[0].Rows[0]["EmpEmail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remart"] != null && ds.Tables[0].Rows[0]["Remart"].ToString() != "")
                {
                    model.Remart = ds.Tables[0].Rows[0]["Remart"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateUserID"] != null && ds.Tables[0].Rows[0]["CreateUserID"].ToString() != "")
                {
                    model.CreateUserID = ds.Tables[0].Rows[0]["CreateUserID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpCard"] != null && ds.Tables[0].Rows[0]["EmpCard"].ToString() != "")
                {
                    model.EmpCard = ds.Tables[0].Rows[0]["EmpCard"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpAddress"] != null && ds.Tables[0].Rows[0]["EmpAddress"].ToString() != "")
                {
                    model.EmpAddress = ds.Tables[0].Rows[0]["EmpAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpType"] != null && ds.Tables[0].Rows[0]["EmpType"].ToString() != "")
                {
                    model.EmpType = ds.Tables[0].Rows[0]["EmpType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpDate"] != null && ds.Tables[0].Rows[0]["EmpDate"].ToString() != "")
                {
                    model.EmpDate = ds.Tables[0].Rows[0]["EmpDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpContractStart"] != null && ds.Tables[0].Rows[0]["EmpContractStart"].ToString() != "")
                {
                    model.EmpContractStart = ds.Tables[0].Rows[0]["EmpContractStart"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EmpContractEnd"] != null && ds.Tables[0].Rows[0]["EmpContractEnd"].ToString() != "")
                {
                    model.EmpContractEnd = ds.Tables[0].Rows[0]["EmpContractEnd"].ToString();
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
