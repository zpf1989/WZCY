using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IBuyApplyBillDAL
    {
        /// <summary>
        /// 新增采购单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddBuyApplyBill(BuyApplyBill model);
        /// <summary>
        /// 修改采购单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateBuyApplyBill(BuyApplyBill model);
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCheckF(BuyApplyBill model);
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateCheckS(BuyApplyBill model, BOASecondCheck boc);
        /// <summary>
        /// 删除采购订单
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        bool Delete(string BuyApplyOrderID);
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        bool UnSubmit(string BuyApplyOrderID);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string BuyApplyOrderCode, string OrderState, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <returns></returns>
        int GetRowCounts(string BuyApplyOrderCode, string OrderState);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="UserId">创建单据UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string BuyApplyOrderCode, string OrderState, string UserId, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="UserId">创建单据UserID</param>
        /// <returns></returns>
        int GetRowCounts(string BuyApplyOrderCode, string OrderState, string UserId);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">初审UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4FirstCheck(string BuyApplyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        // <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">初审UserID</param>
        /// <returns></returns>
        int GetRowCounts4FirstCheck(string BuyApplyOrderCode, string CreateUser, string UserId);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">复审UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4SecondCheck(string BuyApplyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">复审UserID</param>
        /// <returns></returns>
        int GetRowCounts4SecondCheck(string BuyApplyOrderCode, string CreateUser, string UserId);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4Read(string BuyApplyOrderCode, string CreateUser, string Reader, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        int GetRowCounts4Read(string BuyApplyOrderCode, string CreateUser, string Reader);
        /// <summary>
        /// 获取采购订单实体类
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        DataSet GetModel(string BuyApplyOrderID);
        /// <summary>
        /// 检查是否已经复审了
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        int CheckedS(string BuyApplyOrderID, out string Mes);
        /// <summary>
        /// 修改单据状态
        /// </summary>
        /// <param name="SaleState">单据状态
        /// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭</param>
        /// <param name="SaleOrderID">单据ID</param>
        /// <returns></returns>
        int Submit(string OrderState, string BuyApplyBillID);

    }
}
