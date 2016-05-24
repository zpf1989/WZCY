using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.BLL
{
    public class URrelationBLL
    {
        private readonly OA.IDAL.IURrelationDAL iURrelationDAL = DALFactory.Helper.GetIURrelationDAL();

        /// <summary>
        /// 根据用户ID获取角色ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public URrelation GetModelOfURrelationByUserID(string UserID)
        {
            return iURrelationDAL.GetModelOfURrelationByUserID(UserID);
        }
    }
}
