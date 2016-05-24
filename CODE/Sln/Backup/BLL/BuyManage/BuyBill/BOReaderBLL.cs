using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class BOReaderBLL
    {
        private readonly OA.IDAL.IBOReaderDAL iBOReaderDAL = DALFactory.Helper.GetIBOReaderDAL();

        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Model.BOReader model)
        {
            return iBOReaderDAL.Add(model);
        }
        /// <summary>
        /// 删除分阅人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Delete(string BuyOrderID)
        {
            return iBOReaderDAL.Delete(BuyOrderID);
        }
        /// <summary>
        /// 根据采购订单ID获取分阅人模型
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public List<Model.BOReader> GetModel(string BuyOrderID)
        {
            return iBOReaderDAL.GetModel(BuyOrderID);
        }

    }
}
