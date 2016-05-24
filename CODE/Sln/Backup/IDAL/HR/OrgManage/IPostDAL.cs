using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IPostDAL
    {
        /// <summary>
        /// 新增岗位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddPost(PostInfo model);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="PostCode">岗位编号</param>
        /// <param name="PostName">岗位名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string PostCode, string PostName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="PostCode">岗位编号</param>
        /// <param name="PostName">岗位名称</param>
        /// <returns></returns>
        int GetRowCounts(string PostCode, string PostName);
        /// <summary>
        /// 获取岗位模型
        /// </summary>
        /// <param name="PostID">岗位ID</param>
        /// <returns></returns>
        PostInfo GetModel(string PostID);
        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdatePost(PostInfo model);
        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="PostID"></param>
        /// <returns></returns>
        bool DeletePost(string PostID);
        // <summary>
        /// 获取岗位DataSet
        /// </summary>
        /// <returns></returns>
        DataSet GetDataSet();
    }
}
