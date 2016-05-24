using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class APReaderBLL
    {
        private readonly OA.IDAL.IAPReaderDAL iAPReaderDAL = DALFactory.Helper.GetIAPReaderDAL();

        /// <summary>
        /// 根据询价单ID获取分阅人模型
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        public List<Model.APReader> GetModel(string apID)
        {
            return iAPReaderDAL.GetModel(apID);
        }
    }
}
