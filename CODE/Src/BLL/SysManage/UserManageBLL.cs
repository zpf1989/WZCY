using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class UserManageBLL : BaseBLL<UserInfo>
    {
        private readonly OA.IDAL.IUserManageDAL iUserManageDAL = DALFactory.Helper.GetIUserManageDAL();

        public List<UserInfo> GetUsersByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {
            return isForHelp ? iUserManageDAL.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : iUserManageDAL.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params UserInfo[] users)
        {
            if (!RepeatCheck(users))
            {
                return false;
            }
            return iUserManageDAL.Save(users);
        }

        public bool Delete(params string[] userIds)
        {
            return iUserManageDAL.Delete(userIds);
        }

        public bool SetOpt(string[] userIds, string[] optValues)
        {
            return iUserManageDAL.SetOpt(userIds, optValues);
        }

        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(UserInfo[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.UserCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from m in entities
                            where ValidateUtil.isBlank(m.UserID)
                            select m.UserCode).ToArray();
            if (newCodes == null || newCodes.Length < 1)
            {
                return true;
            }
            return !iUserManageDAL.Exists(newCodes);
        }
    }
}
