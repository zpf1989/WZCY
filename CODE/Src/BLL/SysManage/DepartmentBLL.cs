using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class DepartmentBLL
    {
        private readonly OA.IDAL.IDepartmentDAL iDepartmentDAL = DALFactory.Helper.GetIDepartmentDAL();

        /// <summary>
        /// 为部门列表帮助返回所有数据（精简数据：ID、Code、Name）
        /// </summary>
        /// <returns></returns>
        public List<OA.Model.DepartmentInfo> GetAllDepartmentsForGridHelp()
        {
            return iDepartmentDAL.GetAllDepartmentsForGridHelp();
        }
    }
}
