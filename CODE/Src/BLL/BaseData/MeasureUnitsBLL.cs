using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class MeasureUnitsBLL
    {
        private readonly OA.IDAL.IMeasureUnitsDAL idal = DALFactory.Helper.GetIMeasureUnitsDAL();

        public List<MeasureUnits> GetUnitsByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params MeasureUnits[] units)
        {
            return idal.Save(units);
        }

        public bool Delete(params string[] unitIds)
        {
            return idal.Delete(unitIds);
        }
    }
}
