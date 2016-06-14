using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BuyOrder
    {
        public AnsiString BuyOrderID{ get;set; }
        public AnsiString BuyOrderCode{ get;set; }
        public AnsiStringFixedLength BuyOrderDate{ get;set; }
        public AnsiString SupplierID{ get;set; }
        public AnsiStringFixedLength DeliveryDate{ get;set; }
        public AnsiString Creator{ get;set; }
        public DateTime CreateTime{ get;set; }
        public AnsiString Editor{ get;set; }
        public DateTime EditTime{ get;set; }
        public AnsiString FirstChecker{ get;set; }
        public DateTime FirstCheckTime{ get;set; }
        public AnsiString FirstCheckView{ get;set; }
        public AnsiString RecCompany{ get;set; }
        public AnsiString RecTel{ get;set; }
        public AnsiString RecFax{ get;set; }
        public AnsiStringFixedLength OrderState{ get;set; }
        public AnsiString Remark{ get;set; }
        public AnsiString SecondCheckerName{ get;set; }
        public AnsiString ReaderName{ get;set; }
    }
}