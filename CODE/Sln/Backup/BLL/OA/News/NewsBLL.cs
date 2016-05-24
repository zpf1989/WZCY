using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class NewsBLL
    {
        private readonly OA.IDAL.INewsDAL iNewsDAL = DALFactory.Helper.GetINewsDAL();

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddNews(NewsInfo model)
        {
            return iNewsDAL.AddNews(model);
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
            return iNewsDAL.GetPageList(Title, StartTime, EndTime, CreatorID, pageSize, startRowIndex);
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
            return iNewsDAL.GetRowCounts(Title, StartTime, EndTime, CreatorID);
        }
        /// <summary>
        /// 获取新闻模型
        /// </summary>
        /// <param name="NewID"></param>
        /// <returns></returns>
        public NewsInfo GetModel(string NewID)
        {
            return iNewsDAL.GetModel(NewID);
        }
        /// <summary>
        /// 修改新闻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateNews(NewsInfo model)
        {
            return iNewsDAL.UpdateNews(model);
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="NewID"></param>
        /// <returns></returns>
        public bool DeleteNews(string NewID)
        {
            return iNewsDAL.DeleteNews(NewID);
        }
        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        public DataSet GetList()
        {
            return iNewsDAL.GetList();
        }
    }
}
