using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Materials
    {
        public AnsiString MaterialID{ get;set; }
        public AnsiString MaterialCode{ get;set; }
        public AnsiString MaterialName{ get;set; }
        public AnsiString Specs{ get;set; }
        public AnsiString MaterialClassID{ get;set; }
        public AnsiString MaterialTypeID{ get;set; }
        public AnsiString PrimaryUnitID{ get;set; }
        public Decimal Price{ get;set; }
        public AnsiString Remark{ get;set; }
        public AnsiString Creator{ get;set; }
        public DateTime CreateTime{ get;set; }
        public Decimal WasterRate{ get;set; }
    }
}