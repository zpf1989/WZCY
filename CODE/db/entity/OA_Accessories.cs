using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_Accessories
    {
        public AnsiString Id{ get;set; }
        public String FunCode{ get;set; }
        public AnsiString InfoId{ get;set; }
        public String OldName{ get;set; }
        public String OldFullName{ get;set; }
        public String NewName{ get;set; }
        public String NewFullName{ get;set; }
        public String FileType{ get;set; }
        public Int32 FileLength{ get;set; }
        public DateTime AddTime{ get;set; }
        public DateTime EditTime{ get;set; }
    }
}