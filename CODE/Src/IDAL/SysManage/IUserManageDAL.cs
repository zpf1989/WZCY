using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IUserManageDAL
    {
        /// <summary>
        /// 分页获取用户信息（带过滤）
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="whereSql">格式： and Name like '%abc%'</param>
        /// <param name="orderBySql">格式：Code asc,Name desc</param>
        /// <returns></returns>
        List<UserInfo> GetUsersByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null);
        /// <summary>
        /// 新增或更新：支持单条和批量
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        bool Save(params UserInfo[] users);

        bool Delete(params string[] userIds);
    }
}
