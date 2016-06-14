using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class GoodsMovementItem
    {
        public AnsiString GoodsMovementItemID{ get;set; }
        public AnsiString GoodsMovementID{ get;set; }
        public AnsiString MaterialID{ get;set; }
        public Decimal TargInpQty{ get;set; }
        public Decimal ActInpQty{ get;set; }
        public AnsiString RecUnitID{ get;set; }
        public Decimal TargOutQty{ get;set; }
        public Decimal ActOutQty{ get;set; }
        public AnsiString IssUnitID{ get;set; }
        public Decimal InpPlaPrice{ get;set; }
        public Decimal InpPlaValue{ get;set; }
        public Decimal InpActPrice{ get;set; }
        public Decimal InpActValue{ get;set; }
        public Decimal OutPlaPrice{ get;set; }
        public Decimal OutPlaValue{ get;set; }
        public Decimal OutActPrice{ get;set; }
        public Decimal OutActValue{ get;set; }
        public Decimal ReturnQuantity{ get;set; }
        public AnsiString Remark{ get;set; }
    }
}