using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_OfficeDocItem
    {
        public AnsiString OfficeDocItemID{ get;set; }
        public AnsiString OfficeDocID{ get;set; }
        public AnsiString ReceiverID{ get;set; }
        public String Opinion{ get;set; }
        public AnsiStringFixedLength OperateType{ get;set; }
        public AnsiStringFixedLength state{ get;set; }
        public DateTime OperateTime{ get;set; }
    }
}