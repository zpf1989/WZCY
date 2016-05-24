using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IGMSecondCheckDAL
    {
        /// <summary>
        /// 根据货物移动ID获取复审人
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        List<GMSecondCheck> GetModel(string GoodsMovementID);
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="GMSecondCheckID"></param>
        /// <returns></returns>
        DataSet GetDataSet(string GMSecondCheckID);

    }
}
