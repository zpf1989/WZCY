using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class OfficeDocItemBLL
    {
        private readonly OA.IDAL.IOfficeDocItemDAL iOfficeDocItemDAL = DALFactory.Helper.GetIOfficeDocItemDAL();

        /// <summary>
        /// 删除公文行
        /// </summary>
        /// <param name="OfficeDocItemID">公文行ID</param>
        /// <returns></returns>
        public bool DelItem(string OfficeDocItemID)
        {
            return iOfficeDocItemDAL.DelItem(OfficeDocItemID);
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
            return iOfficeDocItemDAL.GetPageList(Title, StartTime, EndTime, ReceiverID, State, pageSize, startRowIndex);
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
            return iOfficeDocItemDAL.GetRowCounts(Title, StartTime, EndTime, ReceiverID, State);
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
            return iOfficeDocItemDAL.GetPageList(OfficeDocID, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="OfficeDocID">公文ID</param>
        /// <returns></returns>
        public int GetRowCounts(string OfficeDocID)
        {
            return iOfficeDocItemDAL.GetRowCounts(OfficeDocID);
        }
        /// <summary>
        /// 获取办理公文模型
        /// </summary>
        /// <param name="OfficeDocItemID"></param>
        /// <returns></returns>
        public OfficeDocItem GetModel(string OfficeDocItemID)
        {
            return iOfficeDocItemDAL.GetModel(OfficeDocItemID);
        }
        /// <summary>
        /// 改变操作状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ChangeState(OfficeDocItem model)
        {
            return iOfficeDocItemDAL.ChangeState(model);
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
            return iOfficeDocItemDAL.GetList(ReceiverID, Flag);
        }
    }
}
