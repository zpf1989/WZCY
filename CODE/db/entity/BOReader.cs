using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BOReader
    {
        public AnsiString BOReadID{ get;set; }
        public AnsiString BuyOrderID{ get;set; }
        public AnsiString ReaderID{ get;set; }
        public DateTime ReadTime{ get;set; }
        public AnsiStringFixedLength ReadFlag{ get;set; }
    }
}