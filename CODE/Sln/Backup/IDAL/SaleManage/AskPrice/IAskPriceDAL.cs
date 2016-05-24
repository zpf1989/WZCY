using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IAskPriceDAL
    {
        /// <summary>
        /// 判断是否有相同的编号
        /// </summary>
        /// <param name="APCode"></param>
        /// <returns></returns>
        bool Exists(string APCode);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddAskPrice(AskPrice model);
        /// <summary>
        /// 修改询价单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateAskPrice(AskPrice model);
        /// <summary>
        /// 删除询价单
        /// </summary>
        /// <param name="APID"></param>
        /// <returns></returns>
        bool Delete(string APID);
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool FirstCheck(AskPrice model);
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SecondCheck(AskPrice model, APSecondCheck apsc);
        /// <summary>
        /// 修改单据状态
        /// </summary>
        /// <param name="state">单据状态
        /// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭</param>
        /// <param name="apID">单据ID</param>
        /// <returns></returns>
        int Submit(string state, string apID);
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        DataSet GetModel(string apID);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string apCode, string clientName, string state, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <returns></returns>
        int GetRowCounts(string apCode, string clientName, string state);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string apCode, string clientName, string state, string creator, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="creator">创建人ID</param>
        /// <returns></returns>
        int GetRowCounts(string apCode, string clientName, string state, string creator);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="firstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4FirstCheck(string apCode, string clientName, string firstChecker, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="firstChecker">初审人ID</param>
        /// <returns></returns>
        int GetRowCounts4FirstCheck(string apCode, string clientName, string firstChecker);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="secondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4SecondCheck(string apCode, string clientName, string secondChecker, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="secondChecker">复核人ID</param>
        /// <returns></returns>
        int GetRowCounts4SecondCheck(string apCode, string clientName, string secondChecker);
    }
}
