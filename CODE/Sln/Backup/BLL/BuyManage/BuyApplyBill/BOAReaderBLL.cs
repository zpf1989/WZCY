using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class BOAReaderBLL
    {
        private readonly OA.IDAL.IBOAReaderDAL iBOReaderDAL = DALFactory.Helper.GetIBOAReaderDAL();

        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.BOAReader model)
        {
            return iBOReaderDAL.Add(model);
        }
        /// <summary>
        /// 删除分阅人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Delete(string BuyApplyOrderID)
        {
            return iBOReaderDAL.Delete(BuyApplyOrderID);
        }
        /// <summary>
        /// 根据采购订单ID获取分阅人模型
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public List<Model.BOAReader> GetModel(string BuyApplyOrderID)
        {
            return iBOReaderDAL.GetModel(BuyApplyOrderID);
        }

    }
}
