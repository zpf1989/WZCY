﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OA.Model;

namespace OA.IDAL
{
    public interface IReachGoodsBillDAL
    {
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="ReachGoodsBillCode">单据编号</param>
        /// <param name="CreateUserID">创建单据UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string ReachGoodsBillCode, string CreateUserID, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="ReachGoodsBillCode">单据编号</param>
        /// <param name="CreateUserID">创建单据UserID</param>
        /// <returns></returns>
        int GetRowCounts(string ReachGoodsBillCode, string CreateUserID);
        /// <summary>
        /// 新增到货单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddReachGoodsBill(ReachGoodsBill model);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="BuyOrderCode">采购单据编号</param>
        /// <param name="InforPersonId">通知人UserID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4InfoPerson(string BuyOrderCode, string InforPersonId, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="BuyOrderCode">采购单据编号</param>
        /// <param name="InforPersonId">通知人UserID</param>
        /// <returns></returns>
        int GetRowCounts4InfoPerson(string BuyOrderCode, string InforPersonId);
    }
}
