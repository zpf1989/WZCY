using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.IDAL
{
    public interface IApplyPublicPowerDAL
    {
        /// <summary>
        /// 新增公章申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(OA.Model.ApplyPublicPower model, IList<ApplyPublicPowerItem> list);
    }
}
