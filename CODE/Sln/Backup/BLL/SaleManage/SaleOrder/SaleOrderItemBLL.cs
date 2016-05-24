using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class SaleOrderItemBLL
    {
        private readonly OA.IDAL.ISaleOrderItemDAL iSaleOrderItemDAL = DALFactory.Helper.GetISaleOrderItemDAL();

        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(int pageSize, int startRowIndex)
        {
            return iSaleOrderItemDAL.GetPageList(pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            return iSaleOrderItemDAL.GetRowCounts();
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderID">销售订单ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SaleOrderID, int pageSize, int startRowIndex)
        {
            return iSaleOrderItemDAL.GetPageList(SaleOrderID, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderID">销售订单ID</param>
        /// <returns></returns>
        public int GetRowCounts(string SaleOrderID)
        {
            return iSaleOrderItemDAL.GetRowCounts(SaleOrderID);
        }
        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertItem(SaleOrderItem model)
        {
            return iSaleOrderItemDAL.InsertItem(model);
        }
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <param name="SaleOrderID">销售订单ID</param>
        /// <returns></returns>
        public int DeleteItemBySaleOrderID(string SaleOrderID)
        {
            return iSaleOrderItemDAL.DeleteItemBySaleOrderID(SaleOrderID);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="SaleOrderItemID">销售订单行ID</param>
        /// <returns></returns>
        public int DeleteItem(string SaleOrderItemID)
        {
            return iSaleOrderItemDAL.DeleteItem(SaleOrderItemID);
        }
    }
}
