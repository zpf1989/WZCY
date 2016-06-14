using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BOSecondCheck
    {
        public AnsiString BOSecondCheckID{ get;set; }
        public AnsiString BuyOrderID{ get;set; }
        public AnsiString SecondChecker{ get;set; }
        public AnsiString SecondCheckView{ get;set; }
        public DateTime SecondCheckTime{ get;set; }
        public AnsiStringFixedLength CheckFlag{ get;set; }
    }
}