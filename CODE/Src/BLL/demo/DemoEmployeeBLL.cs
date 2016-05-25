using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class DemoEmployeeBLL
    {
        private readonly OA.IDAL.IDemoEmployeeDAL iDemoEmployeeDAL = DALFactory.Helper.GetIDemoEmployeeDAL();

        public List<DemoEmployee> GetDemoEmployeesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return iDemoEmployeeDAL.GetDemoEmployeesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Add(params DemoEmployee[] emps)
        {
            return iDemoEmployeeDAL.Add(emps);
        }

        public bool Delete(params string[] empIds)
        {
            return iDemoEmployeeDAL.Delete(empIds);
        }

        public bool Update(params DemoEmployee[] emps)
        {
            return iDemoEmployeeDAL.Update(emps);
        }
    }
}
