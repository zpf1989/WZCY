using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_EmpChange
    {
        public AnsiString EmpChangeID{ get;set; }
        public AnsiString EmpID{ get;set; }
        public AnsiStringFixedLength ChangeDate{ get;set; }
        public AnsiStringFixedLength ChangeType{ get;set; }
        public AnsiString OldPostID{ get;set; }
        public AnsiString NewPostID{ get;set; }
        public String ChangeReason{ get;set; }
        public AnsiString DeptManagerID{ get;set; }
        public String DeptView{ get;set; }
        public AnsiString HRManagerID{ get;set; }
        public String HRView{ get;set; }
        public AnsiString ManagerID{ get;set; }
        public String MView{ get;set; }
        public AnsiStringFixedLength State{ get;set; }
        public AnsiString CreatorID{ get;set; }
        public DateTime CreateTime{ get;set; }
    }
}