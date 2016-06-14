using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class MaterialsBLL
    {
        private readonly OA.IDAL.IMaterialsDAL idal = DALFactory.Helper.GetIMaterialsDAL();

        public List<Materials> GetMaterialsByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params Materials[] materials)
        {
            return idal.Save(materials);
        }

        public bool Delete(params string[] mIds)
        {
            return idal.Delete(mIds);
        }
    }
}
