using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class MaterialClassBLL
    {
        private readonly OA.IDAL.IMaterialClassDAL idal = DALFactory.Helper.GetIMaterialClassDAL();

        public List<MaterialClass> GetMaterialClassesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params MaterialClass[] classes)
        {
            return idal.Save(classes);
        }

        public bool Delete(params string[] classIds)
        {
            return idal.Delete(classIds);
        }
    }
}
