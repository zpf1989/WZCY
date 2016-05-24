using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IMaterialClassDAL
    {
        /// <summary>
        /// 添加物料分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddMaterialClass(MaterialClass model);
        /// <summary>
        /// 修改物料分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMaterialClass(MaterialClass model);
        /// <summary>
        /// 删除物料分类
        /// </summary>
        /// <param name="MaterialClassID"></param>
        /// <returns></returns>
        bool DelMaterialClass(string MaterialClassID);
        /// <summary>
        /// 根据物料分类编号和名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        bool Exists(string MaterialClassCode, string MaterialClassName);
        /// <summary>
        /// 根据物料分类名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        bool Exists4Update(string MaterialClassCode, string MaterialClassName);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string MaterialClassCode, string MaterialClassName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        int GetRowCounts(string MaterialClassCode, string MaterialClassName);
        /// <summary>
        /// 获取物料分类模型
        /// </summary>
        /// <param name="MaterialClassID"></param>
        /// <returns></returns>
        MaterialClass GetModel(string MaterialClassID);
        /// <summary>
        /// 获取物料分类DataSet
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetList();
        /// <summary>
        /// 获取物料分类List
        /// </summary>
        /// <returns></returns>
        IList<MaterialClass> GetMaterialClass();
    }
}
