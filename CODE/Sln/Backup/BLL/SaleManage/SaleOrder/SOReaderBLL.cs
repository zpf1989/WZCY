using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class SOReaderBLL
    {
        private readonly OA.IDAL.ISOReaderDAL iSOReaderDAL = DALFactory.Helper.GetISOReaderDAL();

        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.SOReader model)
        {
            return iSOReaderDAL.Add(model);
        }
        /// <summary>
        /// 删除分阅人
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool Delete(string SaleOrderID)
        {
            return iSOReaderDAL.Delete(SaleOrderID);
        }
        /// <summary>
        /// 根据销售订单ID获取分阅人模型
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public List<Model.SOReader> GetModel(string SaleOrderID)
        {
            return iSOReaderDAL.GetModel(SaleOrderID);
        }

    }
}
