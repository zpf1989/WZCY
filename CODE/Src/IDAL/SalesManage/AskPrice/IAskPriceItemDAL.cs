using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IAskPriceItemDAL : IBaseDAL<AskPriceItem>
    {
        /// <summary>
        /// 获取指定询价单的行
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="apId"></param>
        /// <returns></returns>
        IList<AskPriceItem> GetAPItems(PageEntity pageEntity, string apId);

        /// <summary>
        /// 根据删除指定询价单下的所有行
        /// </summary>
        /// <param name="apIds"></param>
        /// <returns></returns>
        bool DeleteByAPIds(params string[] apIds);
    }
}
