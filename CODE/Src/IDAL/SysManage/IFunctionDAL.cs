using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.IDAL
{
    public interface IFunctionDAL
    {
        /// <summary>
        /// 获取功能的DataSet
        /// </summary>
        /// <returns></returns>
        DataSet GetDataSetOfFun();
        
    }
}
