using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.IDAL
{
    public interface IRoleManageDAL
    {
        /// <summary>
        /// 检验添加的角色是否存在
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="RoleName">角色名称</param>
        /// <returns></returns>
        int CheckRole(string RoleCode, string RoleName);
        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="roleInfo">角色实例</param>
        /// <returns></returns>
        int AddRole(OA.Model.RoleInfo roleInfo);
        /// <summary>
        /// 修改用户组
        /// </summary>
        /// <param name="roleInfo">角色实例</param>
        /// <returns></returns>
        int UpdateRole(OA.Model.RoleInfo roleInfo);
        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="roleInfo">角色ID</param>
        /// <returns></returns>
        int DeleteRole(string RoleID);
        /// <summary>
        /// 根据RoleID获取模型
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
        RoleInfo GetModelByRoleID(string RoleID);
        /// <summary>
        /// 获取角色的DataSet
        /// </summary>
        /// <returns></returns>
        DataSet GetDataSetOfRole();
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="RoleName">角色名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string RoleCode, string RoleName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="RoleName">角色名称</param>
        /// <returns></returns>
        int GetRowCounts(string RoleCode, string RoleName);
    }
}
