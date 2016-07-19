using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IGoodsMovementItemDAL : IBaseDAL<GoodsMovementItem>
    {
        /// <summary>
        /// 获取指定货物移动单据的行
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="gmId"></param>
        /// <returns></returns>
        IList<GoodsMovementItem> GetGMItems(PageEntity pageEntity, string gmId);

        /// <summary>
        /// 根据删除指定货物移动单据下的所有行
        /// </summary>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool DeleteByGMIds(params string[] gmIds);
    }
}
