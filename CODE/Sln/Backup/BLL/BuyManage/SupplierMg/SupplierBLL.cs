using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.BLL
{
    public class SupplierBLL
    {
        private readonly OA.IDAL.ISupplierDAL iSupplierDAL = DALFactory.Helper.GetISupplierDAL();

        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddSupplier(SupplierInfo model)
        {
            return iSupplierDAL.AddSupplier(model);
        }
        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSupplier(SupplierInfo model)
        {
            return iSupplierDAL.UpdateSupplier(model);
        }
        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public bool DelSupplier(string SupplierID)
        {
            return iSupplierDAL.DelSupplier(SupplierID);
        }
        /// <summary>
        /// 根据供应商编号和名称判断是否已经存在
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <returns></returns>
        public bool Exists(string SupplierCode, string SupplierName)
        {
            return iSupplierDAL.Exists(SupplierCode, SupplierName);
        }
        /// <summary>
        /// 根据供应商名称判断是否已经存在
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <returns></returns>
        public bool Exists4Update(string SupplierCode, string SupplierName)
        {
            return iSupplierDAL.Exists4Update(SupplierCode, SupplierName);
        }
        /// <summary>
        /// 获取供应商模型
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public SupplierInfo GetModel(string SupplierID)
        {
            return iSupplierDAL.GetModel(SupplierID);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UserCode">供应商编号</param>
        /// <param name="UserName">供应商名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SupplierCode, string SupplierName, int pageSize, int startRowIndex)
        {
            return iSupplierDAL.GetPageList(SupplierCode, SupplierName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UserCode">供应商编号</param>
        /// <param name="UserName">供应商名称</param>
        /// <returns></returns>
        public int GetRowCounts(string SupplierCode, string SupplierName)
        {
            return iSupplierDAL.GetRowCounts(SupplierCode, SupplierName);
        }
        /// <summary>
        /// 获取供应商List
        /// </summary>
        /// <returns></returns>
        public IList<SupplierInfo> GetSupplierList()
        {
            return iSupplierDAL.GetSupplierList();
        }

    }
}
