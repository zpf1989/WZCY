using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IMaterialsDAL
    {
        /// <summary>
        /// 新增物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddMaterials(Materials model);
        /// <summary>
        /// 修改物料
        /// </summary>
        bool UpdateMaterials(Materials model);
        /// <summary>
        /// 删除物料
        /// </summary>
        /// <param name="MaterialID"></param>
        /// <returns></returns>
        bool DelMaterials(string MaterialID);
        /// <summary>
        /// 根据物料编号、名称、规格判断是否已经存在
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <param name="Specs">规格型号</param>
        /// <returns></returns>
        bool Exists(string MaterialCode, string MaterialName, string Specs);
        /// <summary>
        /// 根据物料名称、规格判断是否已经存在
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <param name="Specs">规格型号</param>
        /// <returns></returns>
        bool Exists4Update(string MaterialCode, string MaterialName, string Specs);
        /// <summary>
        /// 获取物料模型
        /// </summary>
        /// <param name="MaterialID"></param>
        /// <returns></returns>
        Materials GetModel(string MaterialID);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string MaterialCode, string MaterialName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialCode">物料编号</param>
        /// <param name="MaterialName">物料名称</param>
        /// <returns></returns>
        int GetRowCounts(string MaterialCode, string MaterialName);
        /// <summary>
        /// 根据物料分类ID获取物料
        /// </summary>
        /// <param name="MaterialClassID">物料分类ID</param>
        /// <returns></returns>
        IList<Materials> GetMaterialsList(string MaterialClassID);
        /// <summary>
        /// 看物流是否存在，如存在返回MaterialID
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="MaterialName"></param>
        /// <param name="Specs"></param>
        /// <returns></returns>
        string Exists4ReturnMaterialID(string MaterialCode, string MaterialName, string Specs);
    }
}
