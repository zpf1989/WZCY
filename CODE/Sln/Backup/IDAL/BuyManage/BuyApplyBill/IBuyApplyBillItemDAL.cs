using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.IDAL
{
    public interface IBuyApplyBillItemDAL
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
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string BuyApplyOrderID, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        int GetRowCounts(string BuyApplyOrderID);
        /// <summary>
        /// 新增采购单行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Add(BuyApplyBillItem item);
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="ItemID">采购订单行</param>
        /// <returns></returns>
        int DeleteItem(string ItemID);
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        int DeleteItemByBuyApplyOrderID(string BuyApplyOrderID);
    }
}
