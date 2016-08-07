using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.IDAL
{
    public interface IBuyOrderDAL : IBaseDAL<BuyOrder>
    {
        /// <summary>
        /// 编号是否已存在
        /// </summary>
        /// <param name="codes"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(params string[] codes);

        /// <summary>
        /// 获取采购订单，包含采购订单行
        /// </summary>
        /// <param name="orderId">采购订单id</param>
        /// <returns></returns>
        BuyOrder GetBuyOrderWithItems(string orderId);
        /// <summary>
        /// 获取采购订单，不含子项
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        BuyOrder GetBuyOrder(string orderId);

        /// <summary>
        /// 提交到初审人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SubmitToFirstChecker(string userId, params string[] boIds);
        /// <summary>
        /// 提交到复审人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SubmitToSecondChecker(string userId, params string[] boIds);
        /// <summary>
        /// 设置分阅人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SubmitToReader(string userId, params string[] boIds);
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="result">初审结果</param>
        /// <param name="checkView">初审意见</param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool FirstCheck(bool result, string checkView, params string[] boIds);

        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="checkResult"></param>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool SecondCheck(bool checkResult, params string[] boIds);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="soIds"></param>
        /// <returns></returns>
        bool Close(string[] boIds);
    }
}
