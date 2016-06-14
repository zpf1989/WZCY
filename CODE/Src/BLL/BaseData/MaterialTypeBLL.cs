using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class MaterialTypeBLL
    {
        private readonly OA.IDAL.IMaterialTypeDAL idal = DALFactory.Helper.GetIMaterialTypeDAL();

        public List<MaterialType> GetTypesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params MaterialType[] types)
        {
            return idal.Save(types);
        }

        public bool Delete(params string[] typeIds)
        {
            return idal.Delete(typeIds);
        }
    }
}
