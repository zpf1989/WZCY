using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class APSecondCheck
    {
        public AnsiString APSecondCheckID{ get;set; }
        public AnsiString APID{ get;set; }
        public AnsiString SecondChecker{ get;set; }
        public AnsiString SecondCheckView{ get;set; }
        public DateTime SecondCheckTime{ get;set; }
        public AnsiStringFixedLength CheckFlag{ get;set; }
    }
}