using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class GMReader
    {
        public AnsiString GMReadID{ get;set; }
        public AnsiString GoodsMovementID{ get;set; }
        public AnsiString ReaderID{ get;set; }
        public DateTime ReadTime{ get;set; }
        public AnsiStringFixedLength ReadFlag{ get;set; }
    }
}