using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class BuyApplyBillBLL
    {
        private readonly OA.IDAL.IBuyApplyBillDAL iBuyBillDAL = DALFactory.Helper.GetIBuyApplyBillDAL();

        /// <summary>
        /// 新增采购单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddBuyApplyBill(BuyApplyBill model)
        {
            return iBuyBillDAL.AddBuyApplyBill(model);
        }
        /// <summary>
        /// 修改采购单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateBuyApplyBill(BuyApplyBill model)
        {
            return iBuyBillDAL.UpdateBuyApplyBill(model);
        }
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCheckF(BuyApplyBill model)
        {
            return iBuyBillDAL.UpdateCheckF(model);
        }
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCheckS(BuyApplyBill model, BOASecondCheck boc)
        {
            return iBuyBillDAL.UpdateCheckS(model, boc);
        }
        /// <summary>
        /// 删除采购订单
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public bool Delete(string BuyApplyOrderID)
        {
            return iBuyBillDAL.Delete(BuyApplyOrderID);
        }
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        public bool UnSubmit(string BuyApplyOrderID)
        {
            return iBuyBillDAL.UnSubmit(BuyApplyOrderID);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string BuyApplyOrderCode, string OrderState, int pageSize, int startRowIndex)
        {
            return iBuyBillDAL.GetPageList(BuyApplyOrderCode, OrderState, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string BuyApplyOrderCode, string OrderState)
        {
            return iBuyBillDAL.GetRowCounts(BuyApplyOrderCode, OrderState);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="UserId">创建单据UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string BuyApplyOrderCode, string OrderState, string UserId, int pageSize, int startRowIndex)
        {
            return iBuyBillDAL.GetPageList(BuyApplyOrderCode, OrderState, UserId, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="UserId">创建单据UserID</param>
        /// <returns></returns>
        public int GetRowCounts(string BuyApplyOrderCode, string OrderState, string UserId)
        {
            return iBuyBillDAL.GetRowCounts(BuyApplyOrderCode, OrderState, UserId);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">初审UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string BuyApplyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex)
        {
            return iBuyBillDAL.GetPageList4FirstCheck(BuyApplyOrderCode, CreateUser, UserId, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="OrderState">单据状态</param>
        /// <param name="UserId">初审UserID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string BuyApplyOrderCode, string CreateUser, string UserId)
        {
            return iBuyBillDAL.GetRowCounts4FirstCheck(BuyApplyOrderCode, CreateUser, UserId);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">复审UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string BuyApplyOrderCode, string CreateUser, string UserId, int pageSize, int startRowIndex)
        {
            return iBuyBillDAL.GetPageList4SecondCheck(BuyApplyOrderCode, CreateUser, UserId, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="UserId">复审UserID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string BuyApplyOrderCode, string CreateUser, string UserId)
        {
            return iBuyBillDAL.GetRowCounts4SecondCheck(BuyApplyOrderCode, CreateUser, UserId);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4Read(string BuyApplyOrderCode, string CreateUser, string Reader, int pageSize, int startRowIndex)
        {
            return iBuyBillDAL.GetPageList4Read(BuyApplyOrderCode, CreateUser, Reader, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        public int GetRowCounts4Read(string BuyApplyOrderCode, string CreateUser, string Reader)
        {
            return iBuyBillDAL.GetRowCounts4Read(BuyApplyOrderCode, CreateUser, Reader);
        }
        /// <summary>
        /// 获取采购订单实体类
        /// </summary>
        /// <param name="BuyOrderID"></param>
        /// <returns></returns>
        public DataSet GetModel(string BuyApplyOrderID)
        {
            return iBuyBillDAL.GetModel(BuyApplyOrderID);
        }
        /// <summary>
        /// 检查是否已经复审了
        /// </summary>
        /// <param name="BuyOrderID">采购订单ID</param>
        /// <returns></returns>
        public int CheckedS(string BuyApplyOrderID, out string Mes)
        {
            return iBuyBillDAL.CheckedS(BuyApplyOrderID, out Mes);
        }
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
        public int Submit(string OrderState, string BuyApplyBillID)
        {
            return iBuyBillDAL.Submit(OrderState, BuyApplyBillID);
        }
    }
}
