using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class EmpChangeBLL
    {
        private readonly OA.IDAL.IEmpChangeDAL iEmpChangeDAL = DALFactory.Helper.GetIEmpChangeDAL();

        /// <summary>
        /// 新增员工变动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddEmpChange(EmpChange model)
        {
            return iEmpChangeDAL.AddEmpChange(model);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="EmpName">姓名</param>
        /// <param name="ChangeDate">变动时间</param>
        /// <param name="UserId">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string EmpName, string ChangeDate, string UserId, int pageSize, int startRowIndex)
        {
            return iEmpChangeDAL.GetPageList(EmpName, ChangeDate, UserId, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="EmpName">姓名</param>
        /// <param name="ChangeDate">变动时间</param>
        /// <param name="UserId">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string EmpName, string ChangeDate, string UserId)
        {
            return iEmpChangeDAL.GetRowCounts(EmpName, ChangeDate, UserId);
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="EmpChangeID"></param>
        /// <returns></returns>
        public EmpChange GetModel(string EmpChangeID)
        {
            return iEmpChangeDAL.GetModel(EmpChangeID);
        }
    }
}
