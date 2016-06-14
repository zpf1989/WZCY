using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class SOReader
    {
        public AnsiString SOReadID{ get;set; }
        public AnsiString SaleOrderID{ get;set; }
        public AnsiString ReaderID{ get;set; }
        public DateTime ReadTime{ get;set; }
        public AnsiStringFixedLength ReadFlag{ get;set; }
    }
}