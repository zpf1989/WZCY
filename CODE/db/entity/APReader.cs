using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class APReader
    {
        public AnsiString APReadID{ get;set; }
        public AnsiString APID{ get;set; }
        public AnsiString ReaderID{ get;set; }
        public DateTime ReadTime{ get;set; }
        public AnsiStringFixedLength ReadFlag{ get;set; }
    }
}