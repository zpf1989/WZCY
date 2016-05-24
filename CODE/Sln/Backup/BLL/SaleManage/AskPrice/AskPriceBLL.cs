using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class AskPriceBLL
    {
        private readonly OA.IDAL.IAskPriceDAL iAskPriceDAL = DALFactory.Helper.GetIAskPriceDAL();

        /// <summary>
        /// 判断是否有相同的编号
        /// </summary>
        /// <param name="APCode"></param>
        /// <returns></returns>
        public bool Exists(string APCode)
        {
            return iAskPriceDAL.Exists(APCode);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddAskPrice(AskPrice model)
        {
            return iAskPriceDAL.AddAskPrice(model);
        }
        /// <summary>
        /// 删除询价单
        /// </summary>
        /// <param name="APID"></param>
        /// <returns></returns>
        public bool Delete(string APID)
        {
            return iAskPriceDAL.Delete(APID);
        }
        /// <summary>
        /// 修改询价单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAskPrice(AskPrice model)
        {
            return iAskPriceDAL.UpdateAskPrice(model);
        }
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool FirstCheck(AskPrice model)
        {
            return iAskPriceDAL.FirstCheck(model);
        }
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SecondCheck(AskPrice model, APSecondCheck apsc)
        {
            return iAskPriceDAL.SecondCheck(model, apsc);
        }
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
        public int Submit(string state, string apID)
        {
            return iAskPriceDAL.Submit(state, apID);
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="apID"></param>
        /// <returns></returns>
        public DataSet GetModel(string apID)
        {
            return iAskPriceDAL.GetModel(apID);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string apCode, string clientName, string state, int pageSize, int startRowIndex)
        {
            return iAskPriceDAL.GetPageList(apCode, clientName, state, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string apCode, string clientName, string state)
        {
            return iAskPriceDAL.GetRowCounts(apCode, clientName, state);
        }
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
        public DataSet GetPageList(string apCode, string clientName, string state, string creator, int pageSize, int startRowIndex)
        {
            return iAskPriceDAL.GetPageList(apCode, clientName, state, creator, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="state">单据状态</param>
        /// <param name="creator">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string apCode, string clientName, string state, string creator)
        {
            return iAskPriceDAL.GetRowCounts(apCode, clientName, state, creator);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="firstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string apCode, string clientName, string firstChecker, int pageSize, int startRowIndex)
        {
            return iAskPriceDAL.GetPageList4FirstCheck(apCode, clientName, firstChecker, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="firstChecker">初审人ID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string apCode, string clientName, string firstChecker)
        {
            return iAskPriceDAL.GetRowCounts4FirstCheck(apCode, clientName, firstChecker);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="secondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string apCode, string clientName, string secondChecker, int pageSize, int startRowIndex)
        {
            return iAskPriceDAL.GetPageList4SecondCheck(apCode, clientName, secondChecker, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="apCode">单据编号</param>
        /// <param name="clientName">客户名称</param>
        /// <param name="secondChecker">复核人ID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string apCode, string clientName, string secondChecker)
        {
            return iAskPriceDAL.GetRowCounts4SecondCheck(apCode, clientName, secondChecker);
        }
    }
}
