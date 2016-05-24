using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class GoodsMovementBLL
    {
        private readonly OA.IDAL.IGoodsMovementDAL iGoodsMovementDAL = DALFactory.Helper.GetIGoodsMovementDAL();

        /// <summary>
        /// 新增入库单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddGoodsMovement(GoodsMovement model)
        {
            return iGoodsMovementDAL.AddGoodsMovement(model);
        }
        /// <summary>
        /// 修改入库单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGoodsMovement(GoodsMovement model)
        {
            return iGoodsMovementDAL.UpdateGoodsMovement(model);
        }
        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool FirstCheck(GoodsMovement model)
        {
            return iGoodsMovementDAL.FirstCheck(model);
        }
        /// <summary>
        /// 复审
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SecondCheck(GoodsMovement model, GMSecondCheck gmsc)
        {
            return iGoodsMovementDAL.SecondCheck(model, gmsc);
        }
        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public bool Delete(string GoodsMovementID)
        {
            return iGoodsMovementDAL.Delete(GoodsMovementID);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string GoodsMovementCode, string BillState, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            return iGoodsMovementDAL.GetPageList(GoodsMovementCode, BillState, MoveTypeCode, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <returns></returns>
        public int GetRowCounts(string GoodsMovementCode, string BillState, string MoveTypeCode)
        {
            return iGoodsMovementDAL.GetRowCounts(GoodsMovementCode, BillState, MoveTypeCode);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string GoodsMovementCode, string BillState, string Creator, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            return iGoodsMovementDAL.GetPageList(GoodsMovementCode, BillState, Creator, MoveTypeCode, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="BillState">单据状态</param>
        /// <param name="Creator">创建人ID</param>
        /// <returns></returns>
        public int GetRowCounts(string GoodsMovementCode, string BillState, string Creator, string MoveTypeCode)
        {
            return iGoodsMovementDAL.GetRowCounts(GoodsMovementCode, BillState, Creator, MoveTypeCode);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4FirstCheck(string GoodsMovementCode, string CreateUser, string FirstChecker, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            return iGoodsMovementDAL.GetPageList4FirstCheck(GoodsMovementCode, CreateUser, FirstChecker, MoveTypeCode, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="FirstChecker">初审人ID</param>
        /// <returns></returns>
        public int GetRowCounts4FirstCheck(string GoodsMovementCode, string CreateUser, string FirstChecker, string MoveTypeCode)
        {
            return iGoodsMovementDAL.GetRowCounts4FirstCheck(GoodsMovementCode, CreateUser, FirstChecker, MoveTypeCode);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4SecondCheck(string GoodsMovementCode, string CreateUser, string SecondChecker, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            return iGoodsMovementDAL.GetPageList4SecondCheck(GoodsMovementCode, CreateUser, SecondChecker, MoveTypeCode, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="SecondChecker">复核人ID</param>
        /// <returns></returns>
        public int GetRowCounts4SecondCheck(string GoodsMovementCode, string CreateUser, string SecondChecker, string MoveTypeCode)
        {
            return iGoodsMovementDAL.GetRowCounts4SecondCheck(GoodsMovementCode, CreateUser, SecondChecker, MoveTypeCode);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList4Read(string GoodsMovementCode, string CreateUser, string Reader, string MoveTypeCode, int pageSize, int startRowIndex)
        {
            return iGoodsMovementDAL.GetPageList4Read(GoodsMovementCode, CreateUser, Reader, MoveTypeCode, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="GoodsMovementCode">单据编号</param>
        /// <param name="CreateUser">提交人姓名</param>
        /// <param name="Reader">分阅人ID</param>
        /// <returns></returns>
        public int GetRowCounts4Read(string GoodsMovementCode, string CreateUser, string Reader, string MoveTypeCode)
        {
            return iGoodsMovementDAL.GetRowCounts4Read(GoodsMovementCode, CreateUser, Reader, MoveTypeCode);
        }
        /// <summary>
        /// 获取入库单模型
        /// </summary>
        /// <param name="GoodsMovementID"></param>
        /// <returns></returns>
        public DataSet GetModel(string GoodsMovementID)
        {
            return iGoodsMovementDAL.GetModel(GoodsMovementID);
        }
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
        public int Submit(string BillState, string GoodsMovementID)
        {
            return iGoodsMovementDAL.Submit(BillState, GoodsMovementID);
        }
        /// <summary>
        /// 取消提交
        /// </summary>
        /// <param name="GoodsMovementID">单据ID</param>
        /// <returns></returns>
        public bool UnSubmit(string GoodsMovementID)
        {
            return iGoodsMovementDAL.UnSubmit(GoodsMovementID);
        }
    }
}
