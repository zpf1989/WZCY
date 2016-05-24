using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class GMSecondCheckBLL
    {
        private readonly OA.IDAL.IGMSecondCheckDAL iGMSecondCheckDAL = DALFactory.Helper.GetIGMSecondCheckDAL();

        /// <summary>
        /// 根据货物移动ID获取复审人
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public List<GMSecondCheck> GetModel(string GoodsMovementID)
        {
            return iGMSecondCheckDAL.GetModel(GoodsMovementID);
        }
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="GMSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string GMSecondCheckID)
        {
            return iGMSecondCheckDAL.GetDataSet(GMSecondCheckID);
        }
    }
}
