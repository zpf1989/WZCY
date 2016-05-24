using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OA.IDAL
{
    public interface ISOSecondCheckDAL
    {
        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Model.SOSecondCheck model);
        /// <summary>
        /// 删除复审人
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        bool Delete(string SaleOrderID);
        /// <summary>
        /// 根据销售订单ID获取复审人
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        List<Model.SOSecondCheck> GetModel(string SaleOrderID);
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="SOSecondCheckID"></param>
        /// <returns></returns>
        DataSet GetDataSet(string SOSecondCheckID);
    }
}
