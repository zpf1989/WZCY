using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OA.IDAL
{
    public interface IBillTypeDAL
    {
         /// <summary>
        /// 获取单据类型
        /// </summary>
        /// <param name="BillType"></param>
        /// <returns></returns>
        DataSet GetList(string BillType);
    }
}
