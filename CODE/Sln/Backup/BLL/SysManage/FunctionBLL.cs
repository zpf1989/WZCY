using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.BLL
{
    public class FunctionBLL
    {
        private readonly OA.IDAL.IFunctionDAL iFunctionDAL = DALFactory.Helper.GetIFunctionDAL();

        /// <summary>
        /// 获取功能的DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSetOfFun()
        {
            return iFunctionDAL.GetDataSetOfFun();
        }
        
    }
}
