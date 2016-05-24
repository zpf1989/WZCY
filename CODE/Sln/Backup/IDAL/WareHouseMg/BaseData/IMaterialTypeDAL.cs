using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IMaterialTypeDAL
    {
        /// <summary>
        /// 添加物料类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddMaterialType(MaterialType model);
        /// <summary>
        /// 修改物料类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMaterialType(MaterialType model);
        /// <summary>
        /// 删除物料类型
        /// </summary>
        /// <param name="MaterialTypeID"></param>
        /// <returns></returns>
        bool DelMaterialType(string MaterialTypeID);
        /// <summary>
        /// 根据物料类型编号和名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        bool Exists(string MaterialTypeCode, string MaterialTypeName);
        /// <summary>
        /// 根据计量单位名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        bool Exists4Update(string MaterialTypeCode, string MaterialTypeName);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string MaterialTypeCode, string MaterialTypeName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        int GetRowCounts(string MaterialTypeCode, string MaterialTypeName);
        /// <summary>
        /// 获取物料类型模型
        /// </summary>
        /// <param name="MaterialTypeID"></param>
        /// <returns></returns>
        MaterialType GetModel(string MaterialTypeID);
        /// <summary>
        /// 获取物料类型DataSet
        /// </summary>
        /// <returns></returns>
        DataSet GetList();
    }
}
