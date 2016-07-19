using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IGoodsMovementDAL : IBaseDAL<GoodsMovement>
    {
        /// <summary>
        /// 编号是否已存在
        /// </summary>
        /// <param name="codes"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(params string[] codes);

        /// <summary>
        /// 获取货物移动单据，包含货物移动单据行
        /// </summary>
        /// <param name="gmId">货物移动单据id</param>
        /// <returns></returns>
        GoodsMovement GetGMWithItems(string gmId);
        /// <summary>
        /// 获取货物移动单据，不含子项
        /// </summary>
        /// <param name="gmId"></param>
        /// <returns></returns>
        GoodsMovement GetGoodsMovement(string gmId);

        /// <summary>
        /// 提交到初审人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool SubmitToFirstChecker(string userId, params string[] gmIds);
        /// <summary>
        /// 提交到复审人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool SubmitToSecondChecker(string userId, params string[] gmIds);
        /// <summary>
        /// 设置分阅人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool SubmitToReader(string userId, params string[] gmIds);
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="result">初审结果</param>
        /// <param name="checkView">初审意见</param>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool FirstCheck(bool result, string checkView, params string[] gmIds);

        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="checkResult"></param>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool SecondCheck(bool checkResult, params string[] gmIds);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="gmIds"></param>
        /// <returns></returns>
        bool Close(string[] gmIds);
    }
}
