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
    public interface IDepartmentDAL : IBaseDAL<DepartmentInfo>
    {
        /// <summary>
        /// 部门编号是否已存在
        /// </summary>
        /// <param name="deptCodes"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(params string[] deptCodes);
    }
}
