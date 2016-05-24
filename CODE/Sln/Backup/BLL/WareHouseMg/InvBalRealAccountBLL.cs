using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class InvBalRealAccountBLL
    {
        private readonly OA.IDAL.IInvBalRealAccountDAL iInvBalRealAccountDAL = DALFactory.Helper.GetIInvBalRealAccountDAL();

        /// <summary>
        /// 新入库物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int NewIn(InvBalRealAccount model)
        {
            return iInvBalRealAccountDAL.NewIn(model);
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool OutWH(InvBalRealAccount model)
        {
            return iInvBalRealAccountDAL.OutWH(model);
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InWH(InvBalRealAccount model)
        {
            return iInvBalRealAccountDAL.InWH(model);
        }
        /// <summary>
        /// 修改入库数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCurQtyBalance(InvBalRealAccount model)
        {
            return iInvBalRealAccountDAL.UpdateCurQtyBalance(model);
        }
        /// <summary>
        /// 判断是否在库存里次物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsNewMaterials(InvBalRealAccount model)
        {
            return iInvBalRealAccountDAL.IsNewMaterials(model);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(int pageSize, int startRowIndex)
        {
            return iInvBalRealAccountDAL.GetPageList(pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <returns></returns>
        public int GetRowCounts()
        {
            return iInvBalRealAccountDAL.GetRowCounts();
        }
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
        public DataSet GetPageList(string MaterialCode, string MaterialName, string MaterialClassID, int pageSize, int startRowIndex)
        {
            return iInvBalRealAccountDAL.GetPageList(MaterialCode, MaterialName, MaterialClassID, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="MaterialName"></param>
        /// <param name="MaterialClassID"></param>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public int GetRowCounts(string MaterialCode, string MaterialName, string MaterialClassID)
        {
            return iInvBalRealAccountDAL.GetRowCounts(MaterialCode, MaterialName, MaterialClassID);
        }

    }
}
