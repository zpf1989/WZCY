using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.IDAL
{
    public interface IBOSecondCheckDAL : IBaseDAL<BOSecondCheck>
    {
        /// <summary>
        /// 根据删除指定订单下的所有复审记录
        /// </summary>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool DeleteByBOIds(params string[] boIds);
    }
}
