using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class InvBalRealAccount
    {
        public AnsiString ID{ get;set; }
        public AnsiString WareHouseID{ get;set; }
        public AnsiString MaterialID{ get;set; }
        public Decimal CurQtyBalance{ get;set; }
    }
}