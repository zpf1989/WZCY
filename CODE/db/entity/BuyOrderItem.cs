using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BuyOrderItem
    {
        public AnsiString BuyOrderItemID{ get;set; }
        public AnsiString BuyOrderID{ get;set; }
        public AnsiString MaterialID{ get;set; }
        public Decimal BuyQty{ get;set; }
        public Decimal BuyCost{ get;set; }
        public AnsiString BuyUnitID{ get;set; }
        public AnsiString Remark{ get;set; }
    }
}