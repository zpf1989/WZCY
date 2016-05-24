using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class MaterialClassBLL
    {
        private readonly OA.IDAL.IMaterialClassDAL iMaterialClassDAL = DALFactory.Helper.GetIMaterialClassDAL();

        /// <summary>
        /// 添加物料分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMaterialClass(MaterialClass model)
        {
            return iMaterialClassDAL.AddMaterialClass(model);
        }
        /// <summary>
        /// 修改物料分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMaterialClass(MaterialClass model)
        {
            return iMaterialClassDAL.UpdateMaterialClass(model);
        }
        /// <summary>
        /// 删除物料分类
        /// </summary>
        /// <param name="MaterialClassID"></param>
        /// <returns></returns>
        public bool DelMaterialClass(string MaterialClassID)
        {
            return iMaterialClassDAL.DelMaterialClass(MaterialClassID);
        }
        /// <summary>
        /// 根据物料分类编号和名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        public bool Exists(string MaterialClassCode, string MaterialClassName)
        {
            return iMaterialClassDAL.Exists(MaterialClassCode, MaterialClassName);
        }
        /// <summary>
        /// 根据物料分类名称判断是否已经存在
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        public bool Exists4Update(string MaterialClassCode, string MaterialClassName)
        {
            return iMaterialClassDAL.Exists4Update(MaterialClassCode, MaterialClassName);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string MaterialClassCode, string MaterialClassName, int pageSize, int startRowIndex)
        {
            return iMaterialClassDAL.GetPageList(MaterialClassCode, MaterialClassName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="MaterialClassCode">物料分类编号</param>
        /// <param name="MaterialClassName">物料分类名称</param>
        /// <returns></returns>
        public int GetRowCounts(string MaterialClassCode, string MaterialClassName)
        {
            return iMaterialClassDAL.GetRowCounts(MaterialClassCode, MaterialClassName);
        }
        /// <summary>
        /// 获取物料分类模型
        /// </summary>
        /// <param name="MaterialClassID"></param>
        /// <returns></returns>
        public MaterialClass GetModel(string MaterialClassID)
        {
            return iMaterialClassDAL.GetModel(MaterialClassID);
        }
        /// <summary>
        /// 获取物料分类DataSet
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList()
        {
            return iMaterialClassDAL.GetList();
        }
        /// <summary>
        /// 获取物料分类List
        /// </summary>
        /// <returns></returns>
        public IList<MaterialClass> GetMaterialClass()
        {
            return iMaterialClassDAL.GetMaterialClass();
        }

    }
}
