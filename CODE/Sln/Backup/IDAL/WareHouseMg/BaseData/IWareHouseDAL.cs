using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IWareHouseDAL
    {
        /// <summary>
        /// 新增仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddWareHouse(WareHouse model);
        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateWareHouse(WareHouse model);
        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        bool DelWareHouse(string WareHouseID);
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        WareHouse GetModel(string WareHouseID);
        /// <summary>
        /// 根据仓库编号和名称判断是否已经存在
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        bool Exists(string WareHouseCode, string WareHouseName);
        /// <summary>
        /// 根据仓库名称判断是否已经存在
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        bool Exists4Update(string WareHouseCode, string WareHouseName);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string WareHouseCode, string WareHouseName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="WareHouseCode">仓库编号</param>
        /// <param name="WareHouseName">仓库名称</param>
        /// <returns></returns>
        int GetRowCounts(string WareHouseCode, string WareHouseName);
        /// <summary>
        /// 获取物料分类List
        /// </summary>
        /// <returns></returns>
        IList<WareHouse> GetWareHouseList();
    }
}
