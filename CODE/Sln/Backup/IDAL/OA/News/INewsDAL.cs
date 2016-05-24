using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface INewsDAL
    {
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddNews(NewsInfo model);
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
        DataSet GetPageList(string Title, string StartTime, string EndTime, string CreatorID, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="CreatorID">创建人ID</param>
        /// <returns></returns>
        int GetRowCounts(string Title, string StartTime, string EndTime, string CreatorID);
        /// <summary>
        /// 获取新闻模型
        /// </summary>
        /// <param name="NewID"></param>
        /// <returns></returns>
        NewsInfo GetModel(string NewID);
        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateNews(NewsInfo model);
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="NewID"></param>
        /// <returns></returns>
        bool DeleteNews(string NewID);
        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        DataSet GetList();
    }
}
