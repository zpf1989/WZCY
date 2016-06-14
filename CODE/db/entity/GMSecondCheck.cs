using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class GMSecondCheck
    {
        public AnsiString GMSecondCheckID{ get;set; }
        public AnsiString GoodsMovementID{ get;set; }
        public AnsiString SecondChecker{ get;set; }
        public AnsiString SecondCheckView{ get;set; }
        public DateTime SecondCheckTime{ get;set; }
        public AnsiStringFixedLength CheckFlag{ get;set; }
    }
}