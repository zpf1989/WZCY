using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IDeptDAL
    {
        /// <summary>
        /// 获取部门List
        /// </summary>
        /// <returns></returns>
        IList<DeptInfo> GetDeptList();
        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddDept(DeptInfo model);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="DeptCode">部门编号</param>
        /// <param name="DeptName">部门名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string DeptCode, string DeptName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="DeptCode">部门编号</param>
        /// <param name="DeptName">部门名称</param>
        /// <returns></returns>
        int GetRowCounts(string DeptCode, string DeptName);
        /// <summary>
        /// 根据部门主键获取模型
        /// </summary>
        /// <param name="DeptID">部门ID</param>
        /// <returns></returns>
        DeptInfo GetModel(string DeptID);
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateDept(DeptInfo model);
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        bool DeleteDept(string deptID);
        /// <summary>
        /// 获取部门DataSet
        /// </summary>
        /// <returns></returns>
        DataSet GetDeptDataSet();
    }
}
