using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BuyApplyOrderItem
    {
        public AnsiString BuyApplyOrderItemID{ get;set; }
        public AnsiString BuyApplyOrderID{ get;set; }
        public AnsiString MaterialID{ get;set; }
        public Decimal BuyQty{ get;set; }
        public Decimal BuyCost{ get;set; }
        public AnsiString BuyUnitID{ get;set; }
        public AnsiString Remark{ get;set; }
    }
}