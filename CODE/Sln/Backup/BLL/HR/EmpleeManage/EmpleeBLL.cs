using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class EmpleeBLL
    {
        private readonly OA.IDAL.IEmpleeDAL iEmpleeDAL = DALFactory.Helper.GetIEmpleeDAL();

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddEmp(EmpleeInfo model)
        {
            return iEmpleeDAL.AddEmp(model);
        }
        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEmp(EmpleeInfo model)
        {
            return iEmpleeDAL.UpdateEmp(model);
        }
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public bool DeleteEmp(string EmpID)
        {
            return iEmpleeDAL.DeleteEmp(EmpID);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="EmpCode">员工编号</param>
        /// <param name="EmpName">员工名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string EmpCode, string EmpName, int pageSize, int startRowIndex)
        {
            return iEmpleeDAL.GetPageList(EmpCode, EmpName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="EmpCode">员工编号</param>
        /// <param name="EmpName">员工名称</param>
        /// <returns></returns>
        public int GetRowCounts(string EmpCode, string EmpName)
        {
            return iEmpleeDAL.GetRowCounts(EmpCode, EmpName);
        }
        /// <summary>
        /// 获取员工模型
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public EmpleeInfo GetModel(string EmpID)
        {
            return iEmpleeDAL.GetModel(EmpID);
        }

    }
}
