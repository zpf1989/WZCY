using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IPayTypeDAL
    {
        /// <summary>
        /// 新增付款方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(PayType model);
        /// <summary>
        /// 修改付款方式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Update(PayType model);
        /// <summary>
        /// 保存时校验是否有重复数据
        /// </summary>
        /// <param name="payTypeCode">付款方式编号</param>
        /// <param name="payTypeName">付款方式名称</param>
        /// <returns></returns>
        bool Exists(string payTypeCode, string payTypeName);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="payTypeCode">付款方式编号</param>
        /// <param name="payTypeName">付款方式名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string payTypeCode, string payTypeName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="payTypeCode">付款方式编号</param>
        /// <param name="payTypeName">付款方式名称</param>
        /// <returns></returns>
        int GetRowCounts(string payTypeCode, string payTypeName);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="payTypeID"></param>
        /// <returns></returns>
        bool DelPayType(string payTypeID);
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="payTypeID"></param>
        /// <returns></returns>
        PayType GetModel(string payTypeID);
        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <returns></returns>
        DataSet GetDataSet();

    }
}
