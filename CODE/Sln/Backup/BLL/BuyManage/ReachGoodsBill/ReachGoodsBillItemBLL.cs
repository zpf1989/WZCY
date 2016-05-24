using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class ReachGoodsBillItemBLL
    {
        private readonly OA.IDAL.IReachGoodsBillItemDAL iReachGoodsBillItemDAL = DALFactory.Helper.GetIReachGoodsBillItemDAL();

        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertItem(ReachGoodsBillItem model)
        {
            return iReachGoodsBillItemDAL.InsertItem(model);
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="ReachGoodsBillItemID"></param>
        /// <returns></returns>
        public bool DelItem(string ReachGoodsBillItemID)
        {
            return iReachGoodsBillItemDAL.DelItem(ReachGoodsBillItemID);
        }
        /// <summary>
        /// 删除所有空白行
        /// </summary>
        /// <returns></returns>
        public bool DelItem()
        {
            return iReachGoodsBillItemDAL.DelItem();
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(int pageSize, int startRowIndex)
        {
            return iReachGoodsBillItemDAL.GetPageList(pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            return iReachGoodsBillItemDAL.GetRowCounts();
        }
    }
}
