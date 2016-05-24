using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.BLL
{
    public class GoodsMovementItemBLL
    {
        private readonly OA.IDAL.IGoodsMovementItemDAL iGoodsMovementItemDAL = DALFactory.Helper.GetIGoodsMovementItemDAL();

        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(int pageSize, int startRowIndex)
        {
            return iGoodsMovementItemDAL.GetPageList(pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            return iGoodsMovementItemDAL.GetRowCounts();
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string GoodsMovementID, int pageSize, int startRowIndex)
        {
            return iGoodsMovementItemDAL.GetPageList(GoodsMovementID, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <returns></returns>
        public int GetRowCounts(string GoodsMovementID)
        {
            return iGoodsMovementItemDAL.GetRowCounts(GoodsMovementID);
        }
        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertItem(GoodsMovementItem model)
        {
            return iGoodsMovementItemDAL.InsertItem(model);
        }
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        public int DeleteItemByGoodsMovementID(string GoodsMovementID)
        {
            return iGoodsMovementItemDAL.DeleteItemByGoodsMovementID(GoodsMovementID);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="GoodsMovementItemID">货物移动行ID</param>
        /// <returns></returns>
        public int DeleteItem(string GoodsMovementItemID)
        {
            return iGoodsMovementItemDAL.DeleteItem(GoodsMovementItemID);
        }
    }
}
