using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IBOAReaderDAL
    {
        /// <summary>
        /// 添加分阅人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Model.BOAReader model);
        /// <summary>
        /// 删除分阅人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        bool Delete(string BuyApplyOrderID);
        /// <summary>
        /// 根据销售订单ID获取分阅人模型
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        List<Model.BOAReader> GetModel(string BuyApplyOrderID);
    }
}
