using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class AskPrice
    {
        public AnsiString APID{ get;set; }
        public AnsiString APCode{ get;set; }
        public AnsiStringFixedLength AskDate{ get;set; }
        public AnsiString ClientName{ get;set; }
        public AnsiString ClientContact{ get;set; }
        public AnsiString ClientTel{ get;set; }
        public AnsiString ClientAddress{ get;set; }
        public AnsiString MaterialCode{ get;set; }
        public AnsiString MaterialName{ get;set; }
        public AnsiString Specs{ get;set; }
        public AnsiString Routing{ get;set; }
        public Decimal PlanPrice{ get;set; }
        public AnsiStringFixedLength Issued{ get;set; }
        public Int32 IssuedCount{ get;set; }
        public Decimal ActualPrice{ get;set; }
        public AnsiString PayTypeID{ get;set; }
        public AnsiString TrackDescription{ get;set; }
        public AnsiString ClientSurvey{ get;set; }
        public AnsiString APRemark{ get;set; }
        public AnsiString Creator{ get;set; }
        public DateTime CreateTime{ get;set; }
        public AnsiString Editor{ get;set; }
        public DateTime EditTime{ get;set; }
        public AnsiString FirstChecker{ get;set; }
        public DateTime FirstCheckTime{ get;set; }
        public AnsiString FirstCheckView{ get;set; }
        public AnsiStringFixedLength State{ get;set; }
        public AnsiString SecondCheckerName{ get;set; }
        public AnsiString ReaderName{ get;set; }
        public Decimal Qty{ get;set; }
        public AnsiString UnitID{ get;set; }
        public AnsiStringFixedLength IsTax{ get;set; }
        public AnsiStringFixedLength IsShipping{ get;set; }
    }
}