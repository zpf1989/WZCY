using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class OfficeDocBLL
    {
        private readonly OA.IDAL.IOfficeDocDAL iOfficeDocDAL = DALFactory.Helper.GetIOfficeDocDAL();

        /// <summary>
        /// 新增公文
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddOfficeDoc(OfficeDoc model, IList<OfficeDocItem> list)
        {
            return iOfficeDocDAL.AddOfficeDoc(model, list);
        }
        /// <summary>
        /// 修改公文
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateOfficeDoc(OfficeDoc model)
        {
            return iOfficeDocDAL.UpdateOfficeDoc(model);
        }
        /// <summary>
        /// 删除公文
        /// </summary>
        /// <param name="OfficeDocID">公文ID</param>
        /// <returns></returns>
        public int DelOfficeDoc(string OfficeDocID)
        {
            return iOfficeDocDAL.DelOfficeDoc(OfficeDocID);
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
            return iOfficeDocDAL.GetPageList(Title, StartTime, EndTime, WriterID, pageSize, startRowIndex);
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
            return iOfficeDocDAL.GetRowCounts(Title, StartTime, EndTime, WriterID);
        }
        /// <summary>
        /// 获取我的公文
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        public OfficeDoc GetModel(string OfficeDocID)
        {
            return iOfficeDocDAL.GetModel(OfficeDocID);
        }
        /// <summary>
        /// 获取我的公文集合
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        public DataSet GetList(string OfficeDocID)
        {
            return iOfficeDocDAL.GetList(OfficeDocID);
        }
    }
}
