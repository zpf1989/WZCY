using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OA.BLL
{
    public class SOSecondCheckBLL
    {
        private readonly OA.IDAL.ISOSecondCheckDAL iSOSecondCheckDAL = DALFactory.Helper.GetISOSecondCheckDAL();

        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.SOSecondCheck model)
        {
            return iSOSecondCheckDAL.Add(model);
        }
        /// <summary>
        /// 删除复审人
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool Delete(string SaleOrderID)
        {
            return iSOSecondCheckDAL.Delete(SaleOrderID);
        }
        /// <summary>
        /// 根据销售订单ID获取复审人
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public List<Model.SOSecondCheck> GetModel(string SaleOrderID)
        {
            return iSOSecondCheckDAL.GetModel(SaleOrderID);
        }
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="SOSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string SOSecondCheckID)
        {
            return iSOSecondCheckDAL.GetDataSet(SOSecondCheckID);
        }

    }
}
