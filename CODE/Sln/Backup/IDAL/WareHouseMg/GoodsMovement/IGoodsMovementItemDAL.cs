using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.IDAL
{
    public interface IGoodsMovementItemDAL
    {
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        int GetRowCounts();
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string GoodsMovementID, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementID">货物移动ID</param>
        /// <returns></returns>
        int GetRowCounts(string GoodsMovementID);
        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int InsertItem(GoodsMovementItem model);
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        int DeleteItemByGoodsMovementID(string GoodsMovementID);
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="GoodsMovementItemID">货物移动行ID</param>
        /// <returns></returns>
        int DeleteItem(string GoodsMovementItemID);
    }
}
