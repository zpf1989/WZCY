using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BOAReader
    {
        public AnsiString BOAReadID{ get;set; }
        public AnsiString BuyApplyOrderID{ get;set; }
        public AnsiString ReaderID{ get;set; }
        public DateTime ReadTime{ get;set; }
        public AnsiStringFixedLength ReadFlag{ get;set; }
    }
}