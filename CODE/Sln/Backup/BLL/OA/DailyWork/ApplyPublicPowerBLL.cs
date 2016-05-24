using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.BLL
{
    public class ApplyPublicPowerBLL
    {
        private readonly OA.IDAL.IApplyPublicPowerDAL iApplyPublicPowerDAL = DALFactory.Helper.GetIApplyPublicPowerDAL();

        /// <summary>
        /// 新增公章申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(OA.Model.ApplyPublicPower model, IList<ApplyPublicPowerItem> list)
        {
            return iApplyPublicPowerDAL.Add(model, list);
        }
    }
}
