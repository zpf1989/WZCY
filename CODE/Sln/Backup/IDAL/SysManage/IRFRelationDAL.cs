using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IRFRelationDAL
    {
        /// <summary>
        /// 根据用户ID获取功能List
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        List<FunctionInfo> GetFunList(string userid);
        /// <summary>
        /// 根据角色获取该角色的权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        DataSet GetDataSetOfRFRelationByRoleID(string roleID);
        /// <summary>
        /// 根据角色ID删除相应的功能权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        int DelByRoleID(string roleID);
        /// <summary>
        /// 给角色添加权限
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int AddRole(OA.Model.RFRelation info);
    }
}
