using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class APSecondCheckBLL
    {
        private readonly OA.IDAL.IAPSecondCheckDAL iAPSecondCheckDAL = DALFactory.Helper.GetIAPSecondCheckDAL();

        /// <summary>
        /// 根据询价单ID获取复审人
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        public List<APSecondCheck> GetModel(string apID)
        {
            return iAPSecondCheckDAL.GetModel(apID);
        }
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="APSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string APSecondCheckID)
        {
            return iAPSecondCheckDAL.GetDataSet(APSecondCheckID);
        }
    }
}
