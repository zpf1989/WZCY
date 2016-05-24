using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.IDAL
{
    public interface IGMReaderDAL
    {
        /// <summary>
        /// 根据货物移动ID获取分阅人模型
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        List<GMReader> GetModel(string GoodsMovementID);
    }
}
