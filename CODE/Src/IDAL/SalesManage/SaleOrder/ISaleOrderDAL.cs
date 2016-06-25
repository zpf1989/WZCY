using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface ISaleOrderDAL : IBaseDAL<SaleOrder>
    {
        /// <summary>
        /// 编号是否已存在
        /// </summary>
        /// <param name="codes"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(params string[] codes);

        /// <summary>
        /// 获取销售订单，包含销售订单行
        /// </summary>
        /// <param name="orderId">销售订单id</param>
        /// <returns></returns>
        SaleOrder GetSaleOrderWithItems(string orderId);
        /// <summary>
        /// 提交到初审人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SubmitToFirstChecker(string userId, params string[] soIds);
        /// <summary>
        /// 提交到复审人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SubmitToSecondChecker(string userId, params string[] soIds);
        /// <summary>
        /// 设置分阅人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SubmitToReader(string userId, params string[] soIds);
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="result">初审结果</param>
        /// <param name="checkView">初审意见</param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool FirstCheck(bool result, string checkView, params string[] soIds);

        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="checkResult"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SecondCheck( bool checkResult, params string[] soIds);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool Close(string[] soIds);
    }
}
