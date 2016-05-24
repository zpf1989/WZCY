using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class RFRelationBLL
    {
        private readonly OA.IDAL.IRFRelationDAL iRFRelationDAL = DALFactory.Helper.GetIRFRelationDAL();

        /// <summary>
        /// 根据用户ID获取功能List
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<FunctionInfo> GetFunList(string userid)
        {
            return iRFRelationDAL.GetFunList(userid);
        }
        /// <summary>
        /// 根据角色获取该角色的权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public DataSet GetDataSetOfRFRelationByRoleID(string roleID)
        {
            return iRFRelationDAL.GetDataSetOfRFRelationByRoleID(roleID);
        }
        /// <summary>
        /// 根据角色ID删除相应的功能权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public int DelByRoleID(string roleID)
        {
            return iRFRelationDAL.DelByRoleID(roleID);
        }
        /// <summary>
        /// 给角色添加权限
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddRole(OA.Model.RFRelation info)
        {
            return iRFRelationDAL.AddRole(info);
        }
    }
}
