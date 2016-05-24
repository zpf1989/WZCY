using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IMeasureUnitsDAL
    {
        /// <summary>
        /// 添加计量单位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddUnits(MeasureUnits model);
        /// <summary>
        /// 删除计量单位
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        bool DelUnits(string UnitID);
        /// <summary>
        /// 根据计量单位编号和名称判断是否已经存在
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <returns></returns>
        bool Exists(string UnitCode, string UnitName);
        /// <summary>
        /// 根据计量单位名称判断是否已经存在
        /// </summary>
        /// <param name="UnitName">计量单位名称</param>
        /// <param name="UnitCode">计量单位编号</param>
        /// <returns></returns>
        bool Exists4Update(string UnitCode, string UnitName);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string UnitCode, string UnitName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <returns></returns>
        int GetRowCounts(string UnitCode, string UnitName);
        /// <summary>
        /// 获取计量单位模型
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        MeasureUnits GetModel(string UnitID);
        /// <summary>
        /// 修改计量单位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateUnits(MeasureUnits model);
        /// <summary>
        /// 获取计量单位DataSet
        /// </summary>
        /// <returns></returns>
        DataSet GetList();
        /// <summary>
        /// 获取计量单位List
        /// </summary>
        /// <returns></returns>
        IList<MeasureUnits> GetMeasureUnitsList();
    }
}
