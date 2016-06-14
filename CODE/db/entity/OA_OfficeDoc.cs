using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_OfficeDoc
    {
        public AnsiString OfficeDocID{ get;set; }
        public String Title{ get;set; }
        public AnsiString Contents{ get;set; }
        public AnsiString WriterID{ get;set; }
        public DateTime WriteTime{ get;set; }
    }
}