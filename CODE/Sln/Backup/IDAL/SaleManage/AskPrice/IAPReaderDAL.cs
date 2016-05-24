using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IAPReaderDAL
    {
        /// <summary>
        /// 根据询价单ID获取分阅人模型
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        List<Model.APReader> GetModel(string apID);
    }
}
