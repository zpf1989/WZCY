using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class SaleOrderBLL
    {
        private readonly OA.IDAL.ISaleOrderDAL iSaleOrderDAL = DALFactory.Helper.GetISaleOrderDAL();

        /// <summary>
        /// 新增销售订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddSaleOrder(SaleOrder model)
        {
            return iSaleOrderDAL.AddSaleOrder(model);
        }
        /// <summary>
        /// 修改销售订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSaleOrder(SaleOrder model)
        {
            return iSaleOrderDAL.UpdateSaleOrder(model);
        }
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool FirstCheck(SaleOrder model)
        {
            return iSaleOrderDAL.FirstCheck(model);
        }
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SecondCheck(SaleOrder model, SOSecondCheck soc)
        {
            return iSaleOrderDAL.SecondCheck(model, soc);
        }
        /// <summary>
        /// 删除销售订单
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public bool Delete(string SaleOrderID)
        {
            return iSaleOrderDAL.Delete(SaleOrderID);
        }
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="SaleOrderID">单据ID</param>
        /// <returns></returns>
        public bool UnSubmit(string SaleOrderID)
        {
            return iSaleOrderDAL.UnSubmit(SaleOrderID);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SaleOrderCode, string SaleState, int pageSize, int startRowIndex)
        {
            return iSaleOrderDAL.GetPageList(SaleOrderCode, SaleState, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string SaleOrderCode, string SaleState)
        {
            return iSaleOrderDAL.GetRowCounts(SaleOrderCode, SaleState);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string SaleOrderCode, string SaleState, string Creator, int pageSize, int startRowIndex)
        {
            return iSaleOrderDAL.GetPageList(SaleOrderCode, SaleState, Creator, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string SaleOrderCode, string SaleState, string Creator)
        {
            return iSaleOrderDAL.GetRowCounts(SaleOrderCode, SaleState, Creator);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string SaleOrderCode, string CreateUser, string FirstChecker, int pageSize, int startRowIndex)
        {
            return iSaleOrderDAL.GetPageList4FirstCheck(SaleOrderCode, CreateUser, FirstChecker, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string SaleOrderCode, string CreateUser, string FirstChecker)
        {
            return iSaleOrderDAL.GetRowCounts4FirstCheck(SaleOrderCode, CreateUser, FirstChecker);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string SaleOrderCode, string CreateUser, string SecondChecker, int pageSize, int startRowIndex)
        {
            return iSaleOrderDAL.GetPageList4SecondCheck(SaleOrderCode, CreateUser, SecondChecker, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string SaleOrderCode, string CreateUser, string SecondChecker)
        {
            return iSaleOrderDAL.GetRowCounts4SecondCheck(SaleOrderCode, CreateUser, SecondChecker);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4Read(string SaleOrderCode, string CreateUser, string Reader, int pageSize, int startRowIndex)
        {
            return iSaleOrderDAL.GetPageList4Read(SaleOrderCode, CreateUser, Reader, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        public int GetRowCounts4Read(string SaleOrderCode, string CreateUser, string Reader)
        {
            return iSaleOrderDAL.GetRowCounts4Read(SaleOrderCode, CreateUser, Reader);
        }
        /// <summary>
        /// 获取销售订单模型
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public DataSet GetModel(string SaleOrderID)
        {
            return iSaleOrderDAL.GetModel(SaleOrderID);
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
        public int Submit(string SaleState, string SaleOrderID)
        {
            return iSaleOrderDAL.Submit(SaleState, SaleOrderID);
        }
        /// <summary>
        /// 下达采购订单
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        public int DoSend(string SaleOrderID, string UserID)
        {
            return iSaleOrderDAL.DoSend(SaleOrderID, UserID);
        }

    }
}
