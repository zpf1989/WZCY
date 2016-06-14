using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Client
    {
        public AnsiString ClientID{ get;set; }
        public AnsiString ClientCode{ get;set; }
        public AnsiString ClientName{ get;set; }
        public AnsiString ClientTel{ get;set; }
        public AnsiString ClientAddress{ get;set; }
        public AnsiString Contactor{ get;set; }
        public AnsiString Remark{ get;set; }
    }
}