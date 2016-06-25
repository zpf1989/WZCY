using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface ISOSecondCheckDAL : IBaseDAL<SOSecondCheck>
    {
        /// <summary>
        /// 根据删除指定订单下的所有复审记录
        /// </summary>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool DeleteBySOIds(params string[] soIds);
    }
}
