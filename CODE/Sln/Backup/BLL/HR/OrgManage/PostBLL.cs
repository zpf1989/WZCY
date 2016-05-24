using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class PostBLL
    {
        private readonly OA.IDAL.IPostDAL iPostDAL = DALFactory.Helper.GetIPostDAL();

        /// <summary>
        /// 新增岗位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddPost(PostInfo model)
        {
            return iPostDAL.AddPost(model);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="PostCode">岗位编号</param>
        /// <param name="PostName">岗位名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string PostCode, string PostName, int pageSize, int startRowIndex)
        {
            return iPostDAL.GetPageList(PostCode, PostName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="PostCode">岗位编号</param>
        /// <param name="PostName">岗位名称</param>
        /// <returns></returns>
        public int GetRowCounts(string PostCode, string PostName)
        {
            return iPostDAL.GetRowCounts(PostCode, PostName);
        }
        /// <summary>
        /// 获取岗位模型
        /// </summary>
        /// <param name="PostID">岗位ID</param>
        /// <returns></returns>
        public PostInfo GetModel(string PostID)
        {
            return iPostDAL.GetModel(PostID);
        }
        /// <summary>
        /// 修改岗位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdatePost(PostInfo model)
        {
            return iPostDAL.UpdatePost(model);
        }
        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="PostID"></param>
        /// <returns></returns>
        public bool DeletePost(string PostID)
        {
            return iPostDAL.DeletePost(PostID);
        }
        /// <summary>
        /// 获取岗位DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            return iPostDAL.GetDataSet();
        }
    }
}
