using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_ReachGoodsBill
    {
        public AnsiString ReachGoodsBillID{ get;set; }
        public String ReachGoodsBillCode{ get;set; }
        public String BuyOrderCode{ get;set; }
        public AnsiStringFixedLength ReachGoodsDate{ get;set; }
        public DateTime CreateBillTime{ get;set; }
        public AnsiString CreateUserID{ get;set; }
        public AnsiString InforPerson{ get;set; }
        public String Remark{ get;set; }
        public AnsiString BuyOrderID{ get;set; }
    }
}