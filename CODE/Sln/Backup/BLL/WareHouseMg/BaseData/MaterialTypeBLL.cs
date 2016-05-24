using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class MaterialTypeBLL
    {
        private readonly OA.IDAL.IMaterialTypeDAL iMaterialTypeDAL = DALFactory.Helper.GetIMaterialTypeDAL();

        /// <summary>
        /// 添加物料类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMaterialType(MaterialType model)
        {
            return iMaterialTypeDAL.AddMaterialType(model);
        }
        /// <summary>
        /// 修改物料类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMaterialType(MaterialType model)
        {
            return iMaterialTypeDAL.UpdateMaterialType(model);
        }
        /// <summary>
        /// 删除物料类型
        /// </summary>
        /// <param name="MaterialTypeID"></param>
        /// <returns></returns>
        public bool DelMaterialType(string MaterialTypeID)
        {
            return iMaterialTypeDAL.DelMaterialType(MaterialTypeID);
        }
        /// <summary>
        /// 根据物料类型编号和名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        public bool Exists(string MaterialTypeCode, string MaterialTypeName)
        {
            return iMaterialTypeDAL.Exists(MaterialTypeCode, MaterialTypeName);
        }
        /// <summary>
        /// 根据计量单位名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        public bool Exists4Update(string MaterialTypeCode, string MaterialTypeName)
        {
            return iMaterialTypeDAL.Exists4Update(MaterialTypeCode, MaterialTypeName);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string MaterialTypeCode, string MaterialTypeName, int pageSize, int startRowIndex)
        {
            return iMaterialTypeDAL.GetPageList(MaterialTypeCode, MaterialTypeName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialTypeCode">物料类型编号</param>
        /// <param name="MaterialTypeName">物料类型名称</param>
        /// <returns></returns>
        public int GetRowCounts(string MaterialTypeCode, string MaterialTypeName)
        {
            return iMaterialTypeDAL.GetRowCounts(MaterialTypeCode, MaterialTypeName);
        }
        /// <summary>
        /// 获取物料类型模型
        /// </summary>
        /// <param name="MaterialTypeID"></param>
        /// <returns></returns>
        public MaterialType GetModel(string MaterialTypeID)
        {
            return iMaterialTypeDAL.GetModel(MaterialTypeID);
        }
        /// <summary>
        /// 获取物料类型DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetList()
        {
            return iMaterialTypeDAL.GetList();
        }

    }
}
