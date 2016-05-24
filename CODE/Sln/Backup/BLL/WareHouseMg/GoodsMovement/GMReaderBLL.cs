using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.BLL
{
    public class GMReaderBLL
    {
        private readonly OA.IDAL.IGMReaderDAL iGMReaderDAL = DALFactory.Helper.GetIGMReaderDAL();

        /// <summary>
        /// 根据货物移动ID获取分阅人模型
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public List<GMReader> GetModel(string GoodsMovementID)
        {
            return iGMReaderDAL.GetModel(GoodsMovementID);
        }
    }
}
