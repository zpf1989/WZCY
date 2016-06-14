using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class SaleOrder
    {
        public AnsiString SaleOrderID{ get;set; }
        public AnsiString SaleOrderCode{ get;set; }
        public AnsiString BillTypeID{ get;set; }
        public AnsiString MaterialID{ get;set; }
        public AnsiString SaleUnitID{ get;set; }
        public AnsiString ClientID{ get;set; }
        public AnsiStringFixedLength SaleDate{ get;set; }
        public Decimal SaleQty{ get;set; }
        public Decimal SalePrice{ get;set; }
        public Decimal SaleCost{ get;set; }
        public AnsiStringFixedLength FinishDate{ get;set; }
        public AnsiString Creator{ get;set; }
        public DateTime CreateTime{ get;set; }
        public AnsiString Editor{ get;set; }
        public DateTime EditTime{ get;set; }
        public AnsiString FirstChecker{ get;set; }
        public DateTime FirstCheckTime{ get;set; }
        public AnsiString FirstCheckView{ get;set; }
        public AnsiString RoutingID{ get;set; }
        public AnsiStringFixedLength SaleState{ get;set; }
        public AnsiString Remark{ get;set; }
        public AnsiString SecondCheckerName{ get;set; }
        public AnsiString ReaderName{ get;set; }
        public AnsiString Routing{ get;set; }
    }
}