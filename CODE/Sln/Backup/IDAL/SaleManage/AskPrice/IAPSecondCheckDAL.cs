using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IAPSecondCheckDAL
    {
        /// <summary>
        /// 根据询价单ID获取复审人
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        List<APSecondCheck> GetModel(string apID);
        
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="APSecondCheckID"></param>
        /// <returns></returns>
        DataSet GetDataSet(string APSecondCheckID);
    }
}
