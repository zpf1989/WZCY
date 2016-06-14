using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_Dept
    {
        public AnsiString DeptID{ get;set; }
        public String DeptCode{ get;set; }
        public String DeptName{ get;set; }
        public AnsiString ParentDeptID{ get;set; }
        public String Remark{ get;set; }
    }
}