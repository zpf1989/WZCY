using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class MeasureUnitsBLL
    {
        private readonly OA.IDAL.IMeasureUnitsDAL iMeasureUnitsDAL = DALFactory.Helper.GetIMeasureUnitsDAL();

        /// <summary>
        /// 添加计量单位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddUnits(MeasureUnits model)
        {
            return iMeasureUnitsDAL.AddUnits(model);
        }
        /// <summary>
        /// 修改计量单位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUnits(MeasureUnits model)
        {
            return iMeasureUnitsDAL.UpdateUnits(model);
        }
        /// <summary>
        /// 删除计量单位
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public bool DelUnits(string UnitID)
        {
            return iMeasureUnitsDAL.DelUnits(UnitID);
        }
        /// <summary>
        /// 根据计量单位编号和名称判断是否已经存在
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <returns></returns>
        public bool Exists(string UnitCode, string UnitName)
        {
            return iMeasureUnitsDAL.Exists(UnitCode, UnitName);
        }
        /// <summary>
        /// 根据计量单位名称判断是否已经存在
        /// </summary>
        /// <param name="UnitName">计量单位名称</param>
        /// <param name="UnitCode">计量单位编号</param>
        /// <returns></returns>
        public bool Exists4Update(string UnitCode, string UnitName)
        {
            return iMeasureUnitsDAL.Exists4Update(UnitCode, UnitName);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string UnitCode, string UnitName, int pageSize, int startRowIndex)
        {
            return iMeasureUnitsDAL.GetPageList(UnitCode, UnitName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="UnitCode">计量单位编号</param>
        /// <param name="UnitName">计量单位名称</param>
        /// <returns></returns>
        public int GetRowCounts(string UnitCode, string UnitName)
        {
            return iMeasureUnitsDAL.GetRowCounts(UnitCode, UnitName);
        }
        /// <summary>
        /// 获取计量单位模型
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public MeasureUnits GetModel(string UnitID)
        {
            return iMeasureUnitsDAL.GetModel(UnitID);
        }
        /// <summary>
        /// 获取计量单位DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetList()
        {
            return iMeasureUnitsDAL.GetList();
        }
        /// <summary>
        /// 获取计量单位List
        /// </summary>
        /// <returns></returns>
        public IList<MeasureUnits> GetMeasureUnitsList()
        {
            return iMeasureUnitsDAL.GetMeasureUnitsList();
        }

    }
}
