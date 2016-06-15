using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public abstract class BaseBLL<T> where T : class
    {
        /// <summary>
        /// 重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public abstract bool RepeatCheck(T[] entities);
    }
}
