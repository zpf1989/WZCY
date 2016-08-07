using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.IDAL
{
    public interface IBOReaderDAL : IBaseDAL<BOReader>
    {
        /// <summary>
        /// 根据删除指定订单下的所有分阅记录
        /// </summary>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool DeleteByBOIds(params string[] boIds);
    }
}
