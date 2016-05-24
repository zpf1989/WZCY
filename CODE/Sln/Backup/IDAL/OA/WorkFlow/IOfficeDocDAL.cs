using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IOfficeDocDAL
    {
        /// <summary>
        /// 新增公文
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddOfficeDoc(OfficeDoc model, IList<OfficeDocItem> list);
        /// <summary>
        /// 修改公文
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateOfficeDoc(OfficeDoc model);
        /// <summary>
        /// 删除公文
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        int DelOfficeDoc(string OfficeDocID);
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
        DataSet GetPageList(string Title, string StartTime, string EndTime, string WriterID, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="WriterID">拟稿人ID</param>
        /// <returns></returns>
        int GetRowCounts(string Title, string StartTime, string EndTime, string WriterID);
        /// <summary>
        /// 获取我的公文
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        OfficeDoc GetModel(string OfficeDocID);
        /// <summary>
        /// 获取我的公文集合
        /// </summary>
        /// <param name="OfficeDocID"></param>
        /// <returns></returns>
        DataSet GetList(string OfficeDocID);
    }
}
