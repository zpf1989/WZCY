using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class SaleOrderItem
    {
        public AnsiString SaleOrderItemID{ get;set; }
        public AnsiString SaleOrderID{ get;set; }
        public AnsiString MaterialID{ get;set; }
        public Decimal PlanQty{ get;set; }
        public Decimal ActualQty{ get;set; }
        public Decimal PlanCost{ get;set; }
        public AnsiString PrimaryUnitID{ get;set; }
        public AnsiString Remark{ get;set; }
    }
}