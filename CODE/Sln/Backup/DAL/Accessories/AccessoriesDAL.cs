using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data.SqlClient;
using System.Data;
using GentleUtil.DB;
using System.Collections;
using OA.IDAL;

namespace OA.DAL
{
    public class AccessoriesDAL : IAccessoriesDAL
    {
        /// <summary>
        /// 获取一个附件对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Accessories Read(string id)
        {
            Accessories accessory = new Accessories();
            string strSql = "select id,funcode,infoid,oldname,oldfullname,newname,newfullname,filetype,filelength,addtime,edittime from OA_Accessories where id = @id";
            //设置参数
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.NVarChar,36);
            parameters[0].Value = id;
            //执行SQL
            using (SqlDataReader reader = (SqlDataReader)DBAccess.ExecuteReader(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                try
                {
                    while (reader.Read())
                    {
                        accessory = new Accessories();
                        accessory.Id = Convert.ToString(reader["id"]);
                        accessory.FunCode = Convert.ToString(reader["funcode"]);
                        accessory.InfoId = Convert.ToString(reader["infoid"]);
                        accessory.OldName = Convert.ToString(reader["oldname"]);
                        accessory.OldFullName = Convert.ToString(reader["oldfullname"]);
                        accessory.NewName = Convert.ToString(reader["newname"]);
                        accessory.NewFullName = Convert.ToString(reader["newfullname"]);
                        accessory.FileType = Convert.ToString(reader["filetype"]);
                        accessory.FileLength = Convert.ToInt32(reader["filelength"]);
                        accessory.AddTime = Convert.ToDateTime(reader["addtime"]);
                        accessory.EditTime = Convert.ToDateTime(reader["edittime"]);
                    }
                }
                catch (Exception ex)
                {
                    accessory = null;
                }
            }
            return accessory;
        }
        /// <summary>
        /// 获取一条信息对应的所有附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <returns></returns>
        public Accessories[] Read(string funCode, string infoID)
        {
            ArrayList accessoyarray = new ArrayList();
            string strSql = "select id,funcode,infoid,oldname,oldfullname,newname,newfullname,filetype,filelength,addtime,edittime from OA_Accessories where funcode = @funCode and infoid = @infoid";
            //设置参数
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@funCode", SqlDbType.NVarChar);
            parameters[0].Value = funCode;
            parameters[1] = new SqlParameter("@infoid", SqlDbType.NVarChar);
            parameters[1].Value = infoID;
            using (SqlDataReader reader = (SqlDataReader)DBAccess.ExecuteReader(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters))
            {
                try
                {
                    while (reader.Read())
                    {
                        Accessories accessory = new Accessories();
                        accessory.Id = Convert.ToString(reader["id"]);
                        accessory.FunCode = Convert.ToString(reader["funcode"]);
                        accessory.InfoId = Convert.ToString(reader["infoid"]);
                        accessory.OldName = Convert.ToString(reader["oldname"]);
                        accessory.OldFullName = Convert.ToString(reader["oldfullname"]);
                        accessory.NewName = Convert.ToString(reader["newname"]);
                        accessory.NewFullName = Convert.ToString(reader["newfullname"]);
                        accessory.FileType = Convert.ToString(reader["filetype"]);
                        accessory.FileLength = Convert.ToInt32(reader["filelength"]);
                        accessory.AddTime = Convert.ToDateTime(reader["addtime"]);
                        accessory.EditTime = Convert.ToDateTime(reader["edittime"]);
                        accessoyarray.Add(accessory);
                    }
                }
                catch (Exception ex)
                {
                    // accessories = null;
                }
            }
            Accessories[] accessories = new Accessories[accessoyarray.Count];
            for (int i = 0; i < accessoyarray.Count; i++)
            {
                accessories[i] = (Accessories)accessoyarray[i];
            }
            return accessories;
        }
        /// <summary>
        /// 增加一条附件信息
        /// </summary>
        /// <param name="accessory"></param>
        /// <returns></returns>
        public int Add(Accessories accessory)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OA_Accessories (")
                .Append("funcode,infoid,oldname,oldfullname,newname,newfullname,filetype,filelength,addtime,edittime,Id")
                .Append(") values (")
                .Append("@FunCode,@InfoID,@OldName,@OldFullName,@NewName,@NewFullName,@FileType,@FileLength,@AddTime,@EditTime,@Id)");
            SqlParameter[] parameters = {
				    new SqlParameter("@FunCode", SqlDbType.NVarChar,50),
				    new SqlParameter("@InfoID", SqlDbType.NVarChar,36),
				    new SqlParameter("@OldName", SqlDbType.NVarChar,100),
				    new SqlParameter("@OldFullName", SqlDbType.NVarChar,100),
				    new SqlParameter("@NewName", SqlDbType.NVarChar,50),
				    new SqlParameter("@NewFullName", SqlDbType.NVarChar,100),
				    new SqlParameter("@FileType", SqlDbType.NVarChar,50),
				    new SqlParameter("@FileLength", SqlDbType.Int,4),
                    new SqlParameter("@AddTime",SqlDbType.DateTime,50),
                    new SqlParameter("@EditTime",SqlDbType.DateTime,50),
				    new SqlParameter("@Id", SqlDbType.NVarChar,36)};
            parameters[0].Value = accessory.FunCode;
            parameters[1].Value = accessory.InfoId;
            parameters[2].Value = accessory.OldName;
            parameters[3].Value = accessory.OldFullName;
            parameters[4].Value = accessory.NewName;
            parameters[5].Value = accessory.NewFullName;
            parameters[6].Value = accessory.FileType;
            parameters[7].Value = accessory.FileLength;
            parameters[8].Value = accessory.AddTime;
            parameters[9].Value = accessory.EditTime;
            parameters[10].Value = System.Guid.NewGuid().ToString();
            int obj = GentleUtil.DB.DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj <= 0)
            {
                throw (new Exception("未能将数据写入附件表中"));
            }
            else
            {
                //每次只能有一条记录弹出窗口
                return obj;
            }
        }
        /// <summary>
        /// 批量增加附件信息
        /// </summary>
        /// <param name="accessories"></param>
        /// <returns></returns>
        public int Add(Accessories[] accessories)
        {
            int count = 0;
            foreach (Accessories accessory in accessories)
            {
                if (accessory != null)
                {
                    count += Add(accessory);
                }
            }
            return count;
        }
        /// <summary>
        /// 修改附件信息
        /// </summary>
        /// <param name="accessories"></param>
        /// <returns></returns>
        public int Update(string funCode, string infoID, Accessories[] accessories)
        {
            Del(funCode, infoID);
            return Add(accessories);
        }
        /// <summary>
        /// 删除一条附件信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Del(string id)
        {
            string strSql = "delete from OA_Accessories where id = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.NVarChar, 36);
            parameters[0].Value = id;
            object obj = GentleUtil.DB.DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                throw (new Exception("未能将数据写入附件表中"));
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 删除一条信息对应的所有附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <returns></returns>
        public int Del(string funCode, string infoID)
        {
            string strSql = "delete from OA_Accessories where funcode = @FunCode and infoid = @InfoID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@FunCode", SqlDbType.NVarChar, 50);
            parameters[0].Value = funCode;
            parameters[1] = new SqlParameter("@InfoID", SqlDbType.NVarChar, 36);
            parameters[1].Value = infoID;
            object obj = GentleUtil.DB.DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                throw (new Exception("未能删除附件信息"));
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 删除一条信息对应的所有附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <returns></returns>
        public int Del(string funCode, string infoID, List<string> accessID)
        {
            string strSql = "delete from OA_Accessories where funcode = @FunCode and infoid = @InfoID";
            int count = accessID.Count;
            SqlParameter[] parameters = new SqlParameter[count + 2];
            parameters[0] = new SqlParameter("@FunCode", SqlDbType.NVarChar, 50);
            parameters[0].Value = funCode;
            parameters[1] = new SqlParameter("@InfoID", SqlDbType.NVarChar, 36);
            parameters[1].Value = infoID;
            for (int i = 0; i < count; i++)
            {
                strSql += " and id <>@ID" + i.ToString();
                parameters[i + 1] = new SqlParameter("@ID" + i.ToString(), SqlDbType.NVarChar, 36);
                parameters[i + 1].Value = accessID[i];
            }
            object obj = GentleUtil.DB.DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                //throw (new Exception("未能删除附件信息"));
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 将相同栏目一条信息的附件拷贝给令一条记录，用于邮件转发
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="oldInfoID"></param>
        /// <param name="newInfoID"></param>
        /// <returns></returns>
        public bool Copy(string funCode, string oldInfoID, string newInfoID)
        {
            bool returnValue = true;
            //首先读取原来信息的附件信息
            Accessories[] accessories = Read(funCode, oldInfoID);
            foreach (Accessories accessory in accessories)
            {
                if (accessory != null)
                {
                    accessory.InfoId = newInfoID;
                    accessory.AddTime = System.DateTime.Now;
                    accessory.EditTime = accessory.AddTime;
                    if (Add(accessory) <= 0)
                    {
                        returnValue = false;
                    }
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 附件转子流程
        /// </summary>
        /// <param name="parentInstance">父流程ID</param>
        /// <param name="instanceId">子流程Id</param>
        /// <param name="funCode"></param>
        /// <returns></returns>
        public bool GoToChild(string parentInstance, string instanceId, string funCode)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append("insert into OA_Accessories(funcode,infoid,oldname,oldfullname,newname,newfullname,fileType,filelength,addTime,editTime)")
            .Append("select funcode,@ChildInfoId,oldname,oldfullname,newname,newfullname,fileType,filelength,addTime,editTime from OA_Accessories ")
            .Append(" where funcode=@FunCode and infoid=@InfoId");

            SqlParameter[] parameters = {
				    new SqlParameter("@FunCode", SqlDbType.NVarChar,50),
				    new SqlParameter("@InfoId", SqlDbType.NVarChar,36),
                    new SqlParameter("@ChildInfoId", SqlDbType.NVarChar,36)
				};
            parameters[0].Value = funCode;
            parameters[1].Value = parentInstance;
            parameters[2].Value = instanceId;

            try
            {
                GentleUtil.DB.DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, temp.ToString(), parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 附件转父流程
        /// </summary>
        /// <param name="parentInstance">父流程ID</param>
        /// <param name="instanceId">子流程Id</param>
        /// <param name="funCode"></param>
        /// <returns></returns>
        public bool GoToParent(string oldInstance, string newInstance, string oldCode, string newFunCode)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append("delete from OA_Accessories where funcode=@FunCode and infoid=@InfoId ;")
            .Append("insert into OA_Accessories(funcode,infoid,oldname,oldfullname,newname,newfullname,fileType,filelength,addTime,editTime)")
            .Append("select @NewCode,@NewInstance,oldname,oldfullname,newname,newfullname,fileType,filelength,addTime,editTime from OA_Accessories ")
            .Append(" where funcode=@OldCode and infoid=@OldInstance");

            SqlParameter[] parameters = {
				    new SqlParameter("@OldCode", SqlDbType.NVarChar,50),
				    new SqlParameter("@OldInstance", SqlDbType.NVarChar,36),
                    new SqlParameter("@NewInstance", SqlDbType.NVarChar,36),
                    new SqlParameter("@NewCode", SqlDbType.NVarChar,50)
				};
            parameters[0].Value = oldCode;
            parameters[1].Value = oldInstance;
            parameters[2].Value = newInstance;
            parameters[3].Value = newFunCode;

            try
            {
                GentleUtil.DB.DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, temp.ToString(), parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
