using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IEmpleeDAL
    {
        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddEmp(EmpleeInfo model);
        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateEmp(EmpleeInfo model);
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        bool DeleteEmp(string EmpID);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="EmpCode">员工编号</param>
        /// <param name="EmpName">员工名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string EmpCode, string EmpName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="EmpCode">员工编号</param>
        /// <param name="EmpName">员工名称</param>
        /// <returns></returns>
        int GetRowCounts(string EmpCode, string EmpName);
        /// <summary>
        /// 获取员工模型
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        EmpleeInfo GetModel(string EmpID);
    }
}
