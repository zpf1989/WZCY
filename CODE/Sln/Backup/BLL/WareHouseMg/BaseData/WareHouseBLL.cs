using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class WareHouseBLL
    {
        private readonly OA.IDAL.IWareHouseDAL iWareHouseDAL = DALFactory.Helper.GetIWareHouseDAL();

        /// <summary>
        /// 新增仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddWareHouse(WareHouse model)
        {
            return iWareHouseDAL.AddWareHouse(model);
        }
        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateWareHouse(WareHouse model)
        {
            return iWareHouseDAL.UpdateWareHouse(model);
        }
        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public bool DelWareHouse(string WareHouseID)
        {
            return iWareHouseDAL.DelWareHouse(WareHouseID);
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public WareHouse GetModel(string WareHouseID)
        {
            return iWareHouseDAL.GetModel(WareHouseID);
        }
        /// <summary>
        /// 根据仓库编号和名称判断是否已经存在
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        public bool Exists(string WareHouseCode, string WareHouseName)
        {
            return iWareHouseDAL.Exists(WareHouseCode, WareHouseName);
        }
        /// <summary>
        /// 根据仓库名称判断是否已经存在
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        public bool Exists4Update(string WareHouseCode, string WareHouseName)
        {
            return iWareHouseDAL.Exists4Update(WareHouseCode, WareHouseName);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string WareHouseCode, string WareHouseName, int pageSize, int startRowIndex)
        {
            return iWareHouseDAL.GetPageList(WareHouseCode, WareHouseName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        public int GetRowCounts(string WareHouseCode, string WareHouseName)
        {
            return iWareHouseDAL.GetRowCounts(WareHouseCode, WareHouseName);
        }
        /// <summary>
        /// 获取物料分类List
        /// </summary>
        /// <returns></returns>
        public IList<WareHouse> GetWareHouseList()
        {
            return iWareHouseDAL.GetWareHouseList();
        }

    }
}
