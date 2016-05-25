using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IDemoEmployeeDAL
    {
        /// <summary>
        /// 分页获取职员信息（带过滤）
        /// </summary>
        /// <param name="pageEntity"></param>
        /// <param name="whereSql">格式： and Name like '%abc%'</param>
        /// <param name="orderBySql">格式：Code asc,Name desc</param>
        /// <returns></returns>
        List<DemoEmployee> GetDemoEmployeesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null);
        /// <summary>
        /// 新增：支持单条和批量
        /// </summary>
        /// <param name="emps"></param>
        /// <returns></returns>
        bool Add(params DemoEmployee[] emps);

        bool Delete(params string[] empIds);

        bool Update(params DemoEmployee[] emps);
    }
}
