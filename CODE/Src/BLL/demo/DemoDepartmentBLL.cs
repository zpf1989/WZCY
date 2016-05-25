using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class DemoDepartmentBLL
    {
        private readonly OA.IDAL.IDemoDepartmentDAL iDemoDepartmentDAL = DALFactory.Helper.GetIDemoDepartmentDAL();

        /// <summary>
        /// 为部门列表帮助返回所有数据（精简数据：ID、Code、Name）
        /// </summary>
        /// <returns></returns>
        public List<OA.Model.DemoDepartment> GetAllDepartmentsForGridHelp()
        {
            return iDemoDepartmentDAL.GetAllDepartmentsForGridHelp();
        }
    }
}
