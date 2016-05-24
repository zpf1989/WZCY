using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface ISaleOrderDAL
    {
        /// <summary>
        /// 新增销售订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddSaleOrder(SaleOrder model);
        /// <summary>
        /// 修改销售订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateSaleOrder(SaleOrder model);
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool FirstCheck(SaleOrder model);
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SecondCheck(SaleOrder model, SOSecondCheck soc);
        /// <summary>
        /// 删除销售订单
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        bool Delete(string SaleOrderID);
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="SaleOrderID">单据ID</param>
        /// <returns></returns>
        bool UnSubmit(string SaleOrderID);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string SaleOrderCode, string SaleState, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <returns></returns>
        int GetRowCounts(string SaleOrderCode, string SaleState);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string SaleOrderCode, string SaleState, string Creator, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="SaleState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <returns></returns>
        int GetRowCounts(string SaleOrderCode, string SaleState, string Creator);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4FirstCheck(string SaleOrderCode, string CreateUser, string FirstChecker, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <returns></returns>
        int GetRowCounts4FirstCheck(string SaleOrderCode, string CreateUser, string FirstChecker);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4SecondCheck(string SaleOrderCode, string CreateUser, string SecondChecker, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <returns></returns>
        int GetRowCounts4SecondCheck(string SaleOrderCode, string CreateUser, string SecondChecker);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4Read(string SaleOrderCode, string CreateUser, string Reader, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="SaleOrderCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        int GetRowCounts4Read(string SaleOrderCode, string CreateUser, string Reader);
        /// <summary>
        /// 获取销售订单模型
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        DataSet GetModel(string SaleOrderID);
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
        int Submit(string SaleState, string SaleOrderID);
        /// <summary>
        /// 下达采购订单
        /// </summary>
        /// <param name="SaleOrderID"></param>
        /// <returns></returns>
        int DoSend(string SaleOrderID, string UserID);

    }
}
