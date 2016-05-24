using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.BLL
{
    public class BuyApplyBillItemBLL
    {
        private readonly OA.IDAL.IBuyApplyBillItemDAL iBuyBillItemDAL = DALFactory.Helper.GetIBuyApplyBillItemDAL();

        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(int pageSize, int startRowIndex)
        {
            return iBuyBillItemDAL.GetPageList(pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            return iBuyBillItemDAL.GetRowCounts();
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string BuyApplyOrderID, int pageSize, int startRowIndex)
        {
            return iBuyBillItemDAL.GetPageList(BuyApplyOrderID, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        public int GetRowCounts(string BuyApplyOrderID)
        {
            return iBuyBillItemDAL.GetRowCounts(BuyApplyOrderID);
        }
        /// <summary>
        /// 新增采购单行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Add(BuyApplyBillItem item)
        {
            return iBuyBillItemDAL.Add(item);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="ItemID">采购订单行</param>
        /// <returns></returns>
        public int DeleteItem(string ItemID)
        {
            return iBuyBillItemDAL.DeleteItem(ItemID);
        }
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        public int DeleteItemByBuyOrderID(string BuyApplyOrderID)
        {
            return iBuyBillItemDAL.DeleteItemByBuyApplyOrderID(BuyApplyOrderID);
        }
    }
}
