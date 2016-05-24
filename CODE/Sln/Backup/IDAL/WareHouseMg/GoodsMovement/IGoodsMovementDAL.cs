using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IGoodsMovementDAL
    {
        /// <summary>
        /// 新增入库单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddGoodsMovement(GoodsMovement model);
        /// <summary>
        /// 修改入库单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateGoodsMovement(GoodsMovement model);
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool FirstCheck(GoodsMovement model);
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SecondCheck(GoodsMovement model, GMSecondCheck gmsc);
        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        bool Delete(string GoodsMovementID);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string GoodsMovementCode, string BillState, string MoveTypeCode, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <returns></returns>
        int GetRowCounts(string GoodsMovementCode, string BillState, string MoveTypeCode);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string GoodsMovementCode, string BillState, string Creator, string MoveTypeCode, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <returns></returns>
        int GetRowCounts(string GoodsMovementCode, string BillState, string Creator, string MoveTypeCode);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4FirstCheck(string GoodsMovementCode, string CreateUser, string FirstChecker, string MoveTypeCode, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <returns></returns>
        int GetRowCounts4FirstCheck(string GoodsMovementCode, string CreateUser, string FirstChecker, string MoveTypeCode);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4SecondCheck(string GoodsMovementCode, string CreateUser, string SecondChecker, string MoveTypeCode, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <returns></returns>
        int GetRowCounts4SecondCheck(string GoodsMovementCode, string CreateUser, string SecondChecker, string MoveTypeCode);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList4Read(string GoodsMovementCode, string CreateUser, string Reader, string MoveTypeCode, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        int GetRowCounts4Read(string GoodsMovementCode, string CreateUser, string Reader, string MoveTypeCode);
        /// <summary>
        /// 获取入库单模型
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        DataSet GetModel(string GoodsMovementID);
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="GoodsMovementID">单据ID</param>
        /// <returns></returns>
        bool UnSubmit(string GoodsMovementID);
        /// <summary>
        /// 修改单据状态
        /// </summary>
        /// <param name="BillState">单据状态
        /// 1:编制
        /// 2:提交
        /// 3:初审通过
        /// 4:初审不通过
        /// 5:复审通过
        /// 6:复审不通过
        /// 7:关闭</param>
        /// <param name="GoodsMovementID">单据ID</param>
        /// <returns></returns>
        int Submit(string BillState, string GoodsMovementID);
    }
}
