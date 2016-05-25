using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    /// <summary>
    /// 部门数据仓库接口
    /// </summary>
    public interface IDemoDepartmentDAL
    {
        /// <summary>
        /// 针对列表帮助，获取所有部门信息（精简信息）
        /// </summary>
        /// <returns></returns>
        List<DemoDepartment> GetAllDepartmentsForGridHelp();
    }
}
