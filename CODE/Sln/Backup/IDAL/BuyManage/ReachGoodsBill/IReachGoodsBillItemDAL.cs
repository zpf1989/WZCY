using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IReachGoodsBillItemDAL
    {
        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int InsertItem(ReachGoodsBillItem model);
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="ReachGoodsBillItemID"></param>
        /// <returns></returns>
        bool DelItem(string ReachGoodsBillItemID);
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        bool DelItem();
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
    }
}
