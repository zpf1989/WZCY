using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OA.IDAL
{
    public interface IBOSecondCheckDAL
    {
        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Model.BOSecondCheck model);
        /// <summary>
        /// 删除复审人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        bool Delete(string BuyOrderID);
        /// <summary>
        /// 根据销售订单ID获取复审人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        List<Model.BOSecondCheck> GetModel(string BuyOrderID);
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="BOSecondCheckID"></param>
        /// <returns></returns>
        DataSet GetDataSet(string BOSecondCheckID);
    }
}
