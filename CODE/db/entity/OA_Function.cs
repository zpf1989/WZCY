using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_Function
    {
        public Int32 FunID{ get;set; }
        public AnsiString FunName{ get;set; }
        public Int32 ParentFunID{ get;set; }
        public AnsiString FunURL{ get;set; }
    }
}