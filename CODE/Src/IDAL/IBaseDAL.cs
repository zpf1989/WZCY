using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    /// <summary>
    /// 数据层接口
    /// </summary>
    public interface IBaseDAL<T> where T : class
    {
        /// <summary>
        /// 分页获取（带过滤）
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="whereSql">格式： and Name like '%abc%'</param>
        /// <param name="orderBySql">格式：Code asc,Name desc</param>
        /// <returns></returns>
        List<T> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null);
        /// <summary>
        /// 新增或更新：支持单条和批量
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool Save(params T[] entities);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete(params string[] ids);

        /// <summary>
        /// 指定条件的数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(string where);
    }
}
