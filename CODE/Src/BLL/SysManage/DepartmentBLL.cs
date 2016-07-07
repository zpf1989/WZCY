using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class DepartmentBLL : BaseBLL<DepartmentInfo>
    {
        private readonly OA.IDAL.IDepartmentDAL iDepartmentDAL = DALFactory.Helper.GetIDepartmentDAL();

        /// <summary>
        /// 为部门列表帮助返回所有数据（精简数据：ID、Code、Name）
        /// </summary>
        /// <returns></returns>
        public List<DepartmentInfo> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return iDepartmentDAL.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params DepartmentInfo[] dept)
        {
            if (!RepeatCheck(dept))
            {
                return false;
            }
            return iDepartmentDAL.Save(dept);
        }
        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(DepartmentInfo[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.DeptCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from m in entities
                            where ValidateUtil.isBlank(m.DeptID)
                            select m.DeptCode).ToArray();
            if (newCodes == null || newCodes.Length < 1)
            {
                return true;
            }
            return !iDepartmentDAL.Exists(newCodes);
        }
        public bool Delete(params string[] deptIds)
        {
            return iDepartmentDAL.Delete(deptIds);
        }
    }
}
