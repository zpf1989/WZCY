using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_ReachGoodsBillItem
    {
        public AnsiString ReachGoodsBillItemID{ get;set; }
        public AnsiString ReachGoodsBillID{ get;set; }
        public AnsiString MaterialsID{ get;set; }
        public Int32 ReachQty{ get;set; }
        public Int32 BuyQty{ get;set; }
        public String Remark{ get;set; }
        public Decimal ReachCost{ get;set; }
    }
}