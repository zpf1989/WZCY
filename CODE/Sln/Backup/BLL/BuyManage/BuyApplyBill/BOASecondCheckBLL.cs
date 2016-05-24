using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class BOASecondCheckBLL
    {
        private readonly OA.IDAL.IBOASecondCheckDAL iBOSecondCheckDAL = DALFactory.Helper.GetIBOASecondCheckDAL();

        /// <summary>
        /// 添加复审人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(BOASecondCheck model)
        {
            return iBOSecondCheckDAL.Add(model);
        }
        /// <summary>
        /// 删除复审人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Delete(string BuyApplyOrderID)
        {
            return iBOSecondCheckDAL.Delete(BuyApplyOrderID);
        }
        /// <summary>
        /// 根据采购订单ID获取复审人
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public List<BOASecondCheck> GetModel(string BuyApplyOrderID)
        {
            return iBOSecondCheckDAL.GetModel(BuyApplyOrderID);
        }
        /// <summary>
        /// 批准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public bool Check(BOSecondCheck model)
        //{
        //    return iBOSecondCheckDAL.
        //}
        /// <summary>
        /// 根据主键获取DataSet
        /// </summary>
        /// <param name="BOSecondCheckID"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string BOASecondCheckID)
        {
            return iBOSecondCheckDAL.GetDataSet(BOASecondCheckID); ;
        }
        /// <summary>
        /// 判断是否批准完成
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        //public bool IsCheck(string BuyOrderID)
        //{
        //    return iBOSecondCheckDAL.
        //}
    }
}
