using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IBOReaderDAL
    {
        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Model.BOReader model);
        /// <summary>
        /// 删除分阅人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        bool Delete(string BuyOrderID);
        /// <summary>
        /// 根据销售订单ID获取分阅人模型
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        List<Model.BOReader> GetModel(string BuyOrderID);
    }
}
