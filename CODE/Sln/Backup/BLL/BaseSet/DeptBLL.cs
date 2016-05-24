using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class DeptBLL
    {
        private readonly OA.IDAL.IDeptDAL iDeptDAL = DALFactory.Helper.GetIDeptDAL();

        /// <summary>
        /// 获取部门List
        /// </summary>
        /// <returns></returns>
        public IList<DeptInfo> GetDeptList()
        {
            return iDeptDAL.GetDeptList();
        }
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddDept(DeptInfo model)
        {
            return iDeptDAL.AddDept(model);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="DeptCode">部门编号</param>
        /// <param name="DeptName">部门名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string DeptCode, string DeptName, int pageSize, int startRowIndex)
        {
            return iDeptDAL.GetPageList(DeptCode, DeptName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="DeptCode">部门编号</param>
        /// <param name="DeptName">部门名称</param>
        /// <returns></returns>
        public int GetRowCounts(string DeptCode, string DeptName)
        {
            return iDeptDAL.GetRowCounts(DeptCode, DeptName);
        }
        /// <summary>
        /// 根据部门主键获取模型
        /// </summary>
        /// <param name="DeptID">部门ID</param>
        /// <returns></returns>
        public DeptInfo GetModel(string DeptID)
        {
            return iDeptDAL.GetModel(DeptID);
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDept(DeptInfo model)
        {
            return iDeptDAL.UpdateDept(model);
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public bool DeleteDept(string deptID)
        {
            return iDeptDAL.DeleteDept(deptID);
        }
        /// <summary>
        /// 获取部门DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDeptDataSet()
        {
            return iDeptDAL.GetDeptDataSet();
        }
    }
}
