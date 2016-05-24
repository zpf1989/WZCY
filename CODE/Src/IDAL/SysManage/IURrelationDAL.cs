using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.IDAL
{
    public interface IURrelationDAL
    {
        /// <summary>
        /// 根据用户ID获取角色ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        URrelation GetModelOfURrelationByUserID(string UserID);
    }
}
