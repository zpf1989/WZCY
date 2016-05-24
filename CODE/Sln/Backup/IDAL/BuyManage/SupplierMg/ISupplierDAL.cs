using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.IDAL
{
    public interface ISupplierDAL
    {
        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddSupplier(SupplierInfo model);
        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateSupplier(SupplierInfo model);
        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        bool DelSupplier(string SupplierID);
        /// <summary>
        /// 根据供应商编号和名称判断是否已经存在
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <returns></returns>
        bool Exists(string SupplierCode, string SupplierName);
        /// <summary>
        /// 根据供应商名称判断是否已经存在
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <returns></returns>
        bool Exists4Update(string SupplierCode, string SupplierName);
        /// <summary>
        /// 获取供应商模型
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        SupplierInfo GetModel(string SupplierID);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UserCode">供应商编号</param>
        /// <param name="UserName">供应商名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string SupplierCode, string SupplierName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UserCode">供应商编号</param>
        /// <param name="UserName">供应商名称</param>
        /// <returns></returns>
        int GetRowCounts(string SupplierCode, string SupplierName);
        /// <summary>
        /// 获取供应商List
        /// </summary>
        /// <returns></returns>
        IList<SupplierInfo> GetSupplierList();
    }
}
