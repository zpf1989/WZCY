using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class UserManageBLL
    {
        private readonly OA.IDAL.IUserManageDAL iUserManageDAL = DALFactory.Helper.GetIUserManageDAL();

        public List<UserInfo> GetUsersByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return iUserManageDAL.GetUsersByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params UserInfo[] users)
        {
            return iUserManageDAL.Save(users);
        }

        public bool Delete(params string[] userIds)
        {
            return iUserManageDAL.Delete(userIds);
        }
    }
}
