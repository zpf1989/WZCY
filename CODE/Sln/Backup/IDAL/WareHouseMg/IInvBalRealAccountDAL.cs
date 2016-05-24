using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IInvBalRealAccountDAL
    {
        /// <summary>
        /// 新入库物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int NewIn(InvBalRealAccount model);
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool OutWH(InvBalRealAccount model);
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool InWH(InvBalRealAccount model);
        /// <summary>
        /// 修改入库数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCurQtyBalance(InvBalRealAccount model);
        /// <summary>
        /// 判断是否在库存里次物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool IsNewMaterials(InvBalRealAccount model);
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
        /// <param name="MaterialCode"></param>
        /// <param name="MaterialName"></param>
        /// <param name="MaterialClassID"></param>
        /// <param name="WareHouseID"></param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string MaterialCode, string MaterialName, string MaterialClassID, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="MaterialName"></param>
        /// <param name="MaterialClassID"></param>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        int GetRowCounts(string MaterialCode, string MaterialName, string MaterialClassID);
    }
}
