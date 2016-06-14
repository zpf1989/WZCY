using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class DemoEmployee
    {
        public AnsiString EmpId{ get;set; }
        public AnsiString EmpCode{ get;set; }
        public AnsiString EmpName{ get;set; }
        public AnsiStringFixedLength EmpGender{ get;set; }
        public Int32 EmpAge{ get;set; }
        public Date EmpBirthDay{ get;set; }
        public Decimal EmpSalary{ get;set; }
        public AnsiString DeptId{ get;set; }
        public AnsiString DeptName{ get;set; }
    }
}