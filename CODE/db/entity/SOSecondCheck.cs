using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class SOSecondCheck
    {
        public AnsiString SOSecondCheckID{ get;set; }
        public AnsiString SaleOrderID{ get;set; }
        public AnsiString SecondChecker{ get;set; }
        public DateTime SecondCheckTime{ get;set; }
        public AnsiString SecondCheckView{ get;set; }
        public AnsiStringFixedLength CheckFlag{ get;set; }
    }
}