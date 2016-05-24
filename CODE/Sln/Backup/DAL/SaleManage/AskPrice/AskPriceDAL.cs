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
    public class AskPriceDAL : IAskPriceDAL
    {
        /// <summary>
        /// 判断是否有相同的编号
        /// </summary>
        /// <param name="APCode"></param>
        /// <returns></returns>
        public bool Exists(string APCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AskPrice");
            strSql.Append(" where APCode=@APCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@APCode", SqlDbType.VarChar,36)			};
            parameters[0].Value = APCode;

            int count = (int)DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            if (count > 0)
                return true;
            else
                return false;
        }

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAskPrice(AskPrice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AskPrice(");
            strSql.Append("APID,APCode,AskDate,ClientName,ClientContact,ClientTel,ClientAddress,MaterialCode,MaterialName,Specs,Routing,PlanPrice,Issued,IssuedCount,ActualPrice,PayTypeID,TrackDescription,ClientSurvey,APRemark,Creator,CreateTime,Editor,EditTime,FirstChecker,State,SecondCheckerName,ReaderName,Qty,UnitID,IsTax,IsShipping)");
            strSql.Append(" values (");
            strSql.Append("@APID,@APCode,@AskDate,@ClientName,@ClientContact,@ClientTel,@ClientAddress,@MaterialCode,@MaterialName,@Specs,@Routing,@PlanPrice,@Issued,@IssuedCount,@ActualPrice,@PayTypeID,@TrackDescription,@ClientSurvey,@APRemark,@Creator,@CreateTime,@Editor,@EditTime,@FirstChecker,@State,@SecondCheckerName,@ReaderName,@Qty,@UnitID,@IsTax,@IsShipping)");
            SqlParameter[] parameters = {
					new SqlParameter("@APID", SqlDbType.VarChar,36),
					new SqlParameter("@APCode", SqlDbType.VarChar,30),
					new SqlParameter("@AskDate", SqlDbType.Char,8),
					new SqlParameter("@ClientName", SqlDbType.VarChar,60),
					new SqlParameter("@ClientContact", SqlDbType.VarChar,30),
					new SqlParameter("@ClientTel", SqlDbType.VarChar,30),
					new SqlParameter("@ClientAddress", SqlDbType.VarChar,90),
					new SqlParameter("@MaterialCode", SqlDbType.VarChar,60),
					new SqlParameter("@MaterialName", SqlDbType.VarChar,90),
					new SqlParameter("@Specs", SqlDbType.VarChar,90),
					new SqlParameter("@Routing", SqlDbType.VarChar,1024),
					new SqlParameter("@PlanPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Issued", SqlDbType.Char,1),
					new SqlParameter("@IssuedCount", SqlDbType.Int,4),
					new SqlParameter("@ActualPrice", SqlDbType.Decimal,9),
					new SqlParameter("@PayTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@TrackDescription", SqlDbType.VarChar,1024),
					new SqlParameter("@ClientSurvey", SqlDbType.VarChar,1024),
					new SqlParameter("@APRemark", SqlDbType.VarChar,1024),
					new SqlParameter("@Creator", SqlDbType.VarChar,36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
					new SqlParameter("@ReaderName", SqlDbType.VarChar,255),
					new SqlParameter("@Qty", SqlDbType.Decimal,9),
					new SqlParameter("@UnitID", SqlDbType.VarChar,36),
					new SqlParameter("@IsTax", SqlDbType.Char,1),
					new SqlParameter("@IsShipping", SqlDbType.Char,1)};
            parameters[0].Value = model.APID;
            parameters[1].Value = model.APCode;
            if (string.IsNullOrEmpty(model.APCode))
                parameters[1].Value = "XJ" + System.DateTime.Now.Year + 
                                             System.DateTime.Now.Month + 
                                             System.DateTime.Now.Day + 
                                             System.DateTime.Now.Hour + 
                                             System.DateTime.Now.Minute + 
                                             System.DateTime.Now.Second + 
                                             System.DateTime.Now.Millisecond;
            parameters[2].Value = model.AskDate;
            parameters[3].Value = model.ClientName;
            parameters[4].Value = model.ClientContact;
            parameters[5].Value = model.ClientTel;
            parameters[6].Value = model.ClientAddress;
            parameters[7].Value = model.MaterialCode;
            parameters[8].Value = model.MaterialName;
            parameters[9].Value = model.Specs;
            parameters[10].Value = model.Routing;
            parameters[11].Value = model.PlanPrice;
            parameters[12].Value = model.Issued;
            parameters[13].Value = model.IssuedCount;
            parameters[14].Value = model.ActualPrice;
            parameters[15].Value = model.PayTypeID;
            parameters[16].Value = model.TrackDescription;
            parameters[17].Value = model.ClientSurvey;
            parameters[18].Value = model.APRemark;
            parameters[19].Value = model.Creator;
            parameters[20].Value = model.CreateTime;
            parameters[21].Value = model.Editor;
            parameters[22].Value = model.EditTime;
            parameters[23].Value = model.FirstChecker;
            parameters[24].Value = model.State;
            parameters[25].Value = model.SecondCheckerName;
            parameters[26].Value = model.ReaderName;
            parameters[27].Value = model.Qty;
            parameters[28].Value = model.UnitID;
            parameters[29].Value = model.IsTax;
            parameters[30].Value = model.IsShipping;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                APSecondCheckDAL apSC = new APSecondCheckDAL();
                int socount = model.SCList.Count;
                if (socount > 0)
                {
                    for (int i = 0; i < socount; i++)
                    {
                        APSecondCheck apsc = model.SCList[i] as APSecondCheck;
                        apSC.Add(apsc);
                    }
                }

                APReaderDAL apR = new APReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    for (int i = 0; i < rcount; i++)
                    {
                        APReader sor = model.RList[i] as APReader;
                        apR.Add(sor);
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            return 1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改询价单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAskPrice(AskPrice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AskPrice set ");
            strSql.Append("APCode=@APCode,");
            strSql.Append("AskDate=@AskDate,");
            strSql.Append("ClientName=@ClientName,");
            strSql.Append("ClientContact=@ClientContact,");
            strSql.Append("ClientTel=@ClientTel,");
            strSql.Append("ClientAddress=@ClientAddress,");
            strSql.Append("MaterialCode=@MaterialCode,");
            strSql.Append("MaterialName=@MaterialName,");
            strSql.Append("Specs=@Specs,");
            strSql.Append("Routing=@Routing,");
            strSql.Append("PlanPrice=@PlanPrice,");
            strSql.Append("Issued=@Issued,");
            strSql.Append("IssuedCount=@IssuedCount,");
            strSql.Append("ActualPrice=@ActualPrice,");
            strSql.Append("PayTypeID=@PayTypeID,");
            strSql.Append("TrackDescription=@TrackDescription,");
            strSql.Append("ClientSurvey=@ClientSurvey,");
            strSql.Append("APRemark=@APRemark,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("Editor=@Editor,");
            strSql.Append("EditTime=@EditTime,");
            strSql.Append("FirstChecker=@FirstChecker,");
            strSql.Append("State=@State,");
            strSql.Append("SecondCheckerName=@SecondCheckerName,");
            strSql.Append("ReaderName=@ReaderName,");
            strSql.Append("Qty=@Qty,");
            strSql.Append("UnitID=@UnitID,");
            strSql.Append("IsTax=@IsTax,");
            strSql.Append("IsShipping=@IsShipping");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APCode", SqlDbType.VarChar,30),
					new SqlParameter("@AskDate", SqlDbType.Char,8),
					new SqlParameter("@ClientName", SqlDbType.VarChar,60),
					new SqlParameter("@ClientContact", SqlDbType.VarChar,30),
					new SqlParameter("@ClientTel", SqlDbType.VarChar,30),
					new SqlParameter("@ClientAddress", SqlDbType.VarChar,90),
					new SqlParameter("@MaterialCode", SqlDbType.VarChar,60),
					new SqlParameter("@MaterialName", SqlDbType.VarChar,90),
					new SqlParameter("@Specs", SqlDbType.VarChar,90),
					new SqlParameter("@Routing", SqlDbType.VarChar,1024),
					new SqlParameter("@PlanPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Issued", SqlDbType.Char,1),
					new SqlParameter("@IssuedCount", SqlDbType.Int,4),
					new SqlParameter("@ActualPrice", SqlDbType.Decimal,9),
					new SqlParameter("@PayTypeID", SqlDbType.VarChar,36),
					new SqlParameter("@TrackDescription", SqlDbType.VarChar,1024),
					new SqlParameter("@ClientSurvey", SqlDbType.VarChar,1024),
					new SqlParameter("@APRemark", SqlDbType.VarChar,1024),
					new SqlParameter("@Creator", SqlDbType.VarChar,36),
					new SqlParameter("@Editor", SqlDbType.VarChar,36),
					new SqlParameter("@EditTime", SqlDbType.DateTime),
					new SqlParameter("@FirstChecker", SqlDbType.VarChar,36),
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@SecondCheckerName", SqlDbType.VarChar,255),
					new SqlParameter("@ReaderName", SqlDbType.VarChar,255),
					new SqlParameter("@Qty", SqlDbType.Decimal,9),
					new SqlParameter("@UnitID", SqlDbType.VarChar,36),
					new SqlParameter("@IsTax", SqlDbType.Char,1),
					new SqlParameter("@IsShipping", SqlDbType.Char,1),
					new SqlParameter("@APID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.APCode;
            parameters[1].Value = model.AskDate;
            parameters[2].Value = model.ClientName;
            parameters[3].Value = model.ClientContact;
            parameters[4].Value = model.ClientTel;
            parameters[5].Value = model.ClientAddress;
            parameters[6].Value = model.MaterialCode;
            parameters[7].Value = model.MaterialName;
            parameters[8].Value = model.Specs;
            parameters[9].Value = model.Routing;
            parameters[10].Value = model.PlanPrice;
            parameters[11].Value = model.Issued;
            parameters[12].Value = model.IssuedCount;
            parameters[13].Value = model.ActualPrice;
            parameters[14].Value = model.PayTypeID;
            parameters[15].Value = model.TrackDescription;
            parameters[16].Value = model.ClientSurvey;
            parameters[17].Value = model.APRemark;
            parameters[18].Value = model.Creator;
            parameters[19].Value = model.Editor;
            parameters[20].Value = model.EditTime;
            parameters[21].Value = model.FirstChecker;
            parameters[22].Value = model.State;
            parameters[23].Value = model.SecondCheckerName;
            parameters[24].Value = model.ReaderName;
            parameters[25].Value = model.Qty;
            parameters[26].Value = model.UnitID;
            parameters[27].Value = model.IsTax;
            parameters[28].Value = model.IsShipping;
            parameters[29].Value = model.APID;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                APSecondCheckDAL soSC = new APSecondCheckDAL();
                int socount = model.SCList.Count;
                if (socount > 0)
                {
                    soSC.Delete(model.APID);
                    for (int i = 0; i < socount; i++)
                    {
                        APSecondCheck sosc = model.SCList[i] as APSecondCheck;
                        soSC.Add(sosc);
                    }
                }

                APReaderDAL rSC = new APReaderDAL();
                int rcount = model.RList.Count;
                if (rcount > 0)
                {
                    rSC.Delete(model.APID);
                    for (int i = 0; i < rcount; i++)
                    {
                        APReader sor = model.RList[i] as APReader;
                        rSC.Add(sor);
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            return true;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除询价单
        /// </summary>
        /// <param name="APID"></param>
        /// <returns></returns>
        public bool Delete(string APID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AskPrice ");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APID", SqlDbType.VarChar,36)};
            parameters[0].Value = APID;

            int rows = 0;

            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);
            try
            {
                //删除分录
                AskPriceItemDAL item = new AskPriceItemDAL();
                item.Delete(APID);

                //删除分阅人
                APReaderDAL sor = new APReaderDAL();
                sor.Delete(APID);

                //删除复核人
                APSecondCheckDAL sos = new APSecondCheckDAL();
                sos.Delete(APID);

                rows = DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 审核
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool FirstCheck(AskPrice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AskPrice set ");
            strSql.Append("FirstCheckTime=@FirstCheckTime,");
            strSql.Append("FirstCheckView=@FirstCheckView,");
            strSql.Append("State=@State");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@FirstCheckTime", SqlDbType.DateTime),
					new SqlParameter("@FirstCheckView", SqlDbType.VarChar,255),
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@APID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.FirstCheckTime;
            parameters[1].Value = model.FirstCheckView;
            parameters[2].Value = model.State;
            parameters[3].Value = model.APID;

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
        #endregion

        #region 批准
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SecondCheck(AskPrice model, APSecondCheck apsc)
        {
            APSecondCheckDAL apscDal = new APSecondCheckDAL();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AskPrice set ");
            strSql.Append("State=@State");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@APID", SqlDbType.VarChar,36)};
            parameters[0].Value = model.State;
            parameters[1].Value = model.APID;


            IDbTransaction transaction = DBAccess.BeginDbTransaction(DB.Type, DB.ConnectionString);

            try
            {
                apscDal.Check(apsc);
                if (apscDal.IsCheck(model.APID))
                {
                    DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            return true;
        }
        #endregion

        #region 修改单据状态
        /// <summary>
        /// 修改单据状态
        /// </summary>
        /// <param name="state">单据状态
        /// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭</param>
        /// <param name="apID">单据ID</param>
        /// <returns></returns>
        public int Submit(string state, string apID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AskPrice set ");
            strSql.Append("State=@State");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@APID", SqlDbType.VarChar,36)};
            parameters[0].Value = state;
            parameters[1].Value = apID;

            return DBAccess.ExecuteNonQuery(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region 获取模型
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        public DataSet GetModel(string apID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 APID,APCode,AskDate,ClientName,ClientContact,ClientTel,ClientAddress,MaterialCode,MaterialName,Specs,Routing,PlanPrice,Issued,IssuedCount,ActualPrice,PayTypeID,TrackDescription,ClientSurvey,APRemark,Creator,AP.CreateTime,AP.Editor,AP.EditTime,FirstChecker,FirstCheckTime,FirstCheckView,State,SecondCheckerName,ReaderName,U.UserName as CreateUser,U1.UserName as FirstCheckerName,Qty,AP.UnitID,IsTax,IsShipping,MU.UnitName,MU.UnitCode from AskPrice AP");
            strSql.Append(" left join OA_User U on U.UserID=AP.Creator ");
            strSql.Append(" left join OA_User U1 on U1.UserID=AP.FirstChecker ");
            strSql.Append(" left join MeasureUnits MU on MU.UnitID=AP.UnitID ");
            strSql.Append(" where APID=@APID ");
            SqlParameter[] parameters = {
					new SqlParameter("@APID", SqlDbType.VarChar,36)			};
            parameters[0].Value = apID;

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region 所有拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string apCode, string clientName, string state, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and AP.ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(state))
            {
                strSql.Append(" and AP.State ='" + state.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select AP.*,PT.PayTypeName,U.UserName as CreateUser,U1.UserName as FirstCheckerName,MU.UnitName,row_number() OVER (order by AP.CreateTime desc) as RowId ")
                .AppendLine(" from AskPrice AP ")
                .AppendLine(" left join OA_User U on U.UserID=AP.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=AP.FirstChecker ")
                .AppendLine(" left join PayType PT on PT.PayTypeID=AP.PayTypeID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=AP.UnitID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string apCode, string clientName, string state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from AskPrice")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(state))
            {
                strSql.Append(" and State ='" + state.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 我的询价单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string apCode, string clientName, string state, string creator, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and AP.ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(state))
            {
                strSql.Append(" and AP.State ='" + state.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(creator))
            {
                strSql.Append(" and AP.Creator ='" + creator.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select AP.*,PT.PayTypeName,U.UserName as CreateUser,U1.UserName as FirstCheckerName,MU.UnitName,row_number() OVER (order by AP.CreateTime desc) as RowId ")
                .AppendLine(" from AskPrice AP ")
                .AppendLine(" left join OA_User U on U.UserID=AP.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=AP.FirstChecker ")
                .AppendLine(" left join PayType PT on PT.PayTypeID=AP.PayTypeID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=AP.UnitID ")
                .AppendLine(" where 1=1 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="creator">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string apCode, string clientName, string state, string creator)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from AskPrice")
            .AppendLine(" where 1=1 ");

            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(state))
            {
                strSql.Append(" and State ='" + state.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(creator))
            {
                strSql.Append(" and Creator ='" + creator.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 审核拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="firstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string apCode, string clientName, string firstChecker, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(firstChecker))
            {
                strSql.Append(" and FirstChecker ='" + firstChecker.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select AP.*,PT.PayTypeName,U.UserName as CreateUser,U1.UserName as FirstCheckerName,MU.UnitName,row_number() OVER (order by AP.CreateTime desc) as RowId ")
                .AppendLine(" from AskPrice AP ")
                .AppendLine(" left join PayType PT on PT.PayTypeID=AP.PayTypeID ")
                .AppendLine(" left join OA_User U on U.UserID=AP.Creator ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=AP.UnitID ")
                .AppendLine(" left join OA_User U1 on U1.UserID=AP.FirstChecker) tmp ")
                .AppendLine(" where State=2 ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="firstChecker">初审人ID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string apCode, string clientName, string firstChecker)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) ")
            .AppendLine(" from AskPrice ")
            .AppendLine(" where State=2 ");

            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(firstChecker))
            {
                strSql.Append(" and FirstChecker ='" + firstChecker.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

        #region 批准拼料单列表
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="secondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string apCode, string clientName, string secondChecker, int pageSize, int startRowIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(secondChecker))
            {
                strSql.Append(" and SecondChecker ='" + secondChecker.Trim() + "' ");
            }
            //处理选择行数
            int beginrow = startRowIndex + 1;
            int endrow = startRowIndex + pageSize;

            StringBuilder tmpSQL = new StringBuilder();
            tmpSQL.AppendLine(" with m as(select * from ")
                .AppendLine(" (select AP.*,PT.PayTypeName,U.UserName as CreateUser,U1.UserName as FirstCheckerName,U2.UserName as SecondCheckeName,SecondChecker,APSecondCheckID,CheckFlag,MU.UnitName,row_number() OVER (order by AP.CreateTime desc) as RowId ")
                .AppendLine(" from AskPrice AP ")
                .AppendLine(" inner join APSecondCheck APSC on APSC.APID = AP.APID ")
                .AppendLine(" left join PayType PT on PT.PayTypeID=AP.PayTypeID ")
                .AppendLine(" left join MeasureUnits MU on MU.UnitID=AP.UnitID ")
                .AppendLine(" left join OA_User U on U.UserID=AP.Creator ")
                .AppendLine(" left join OA_User U1 on U1.UserID=AP.FirstChecker ")
                .AppendLine(" left join OA_User U2 on U2.UserID=APSC.SecondChecker) tmp ")
                .AppendLine(" where State=3 and CheckFlag = '0' ")
                .AppendLine(strSql.ToString())
                .Append(" ) ")
                .Append("select * from m where RowId between " + beginrow.ToString() + " and " + endrow.ToString());

            return DBAccess.ExecuteDataset(DB.Type, DB.ConnectionString, CommandType.Text, tmpSQL.ToString(), null);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="secondChecker">复核人ID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string apCode, string clientName, string secondChecker)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select COUNT(*) from ")
            .AppendLine(" AskPrice AP ")
            .AppendLine(" inner join APSecondCheck APSC on APSC.APID = AP.APID ")
            .AppendLine(" where State=3 and CheckFlag = '0' ");

            if (!string.IsNullOrEmpty(apCode))
            {
                strSql.Append(" and APCode like'%" + apCode.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                strSql.Append(" and ClientName like'%" + clientName.Trim() + "%' ");
            }
            if (!string.IsNullOrEmpty(secondChecker))
            {
                strSql.Append(" and SecondChecker ='" + secondChecker.Trim() + "' ");
            }

            object obj = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, strSql.ToString(), null);
            if (obj != null && obj.ToString() != "")
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
        #endregion

    }
}
