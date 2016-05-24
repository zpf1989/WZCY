using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OA.BLL
{
    public class BillTypeBLL
    {
        private readonly OA.IDAL.IBillTypeDAL iBillTypeDAL = DALFactory.Helper.GetIBillTypeDAL();

        /// <summary>
        /// 获取单据类型
        /// </summary>
        /// <param name="BillType"></param>
        /// <returns></returns>
        public DataSet GetList(string BillType)
        {
            return iBillTypeDAL.GetList(BillType);
        }

    }
}
