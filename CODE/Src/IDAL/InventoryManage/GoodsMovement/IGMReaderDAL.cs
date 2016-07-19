using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IGMReaderDAL : IBaseDAL<GMReader>
    {
        /// <summary>
        /// 删除指定单据下的所有分阅记录
        /// </summary>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool DeleteByGMIds(params string[] gmIds);
    }
}
