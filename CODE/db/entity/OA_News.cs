using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_News
    {
        public AnsiString NewID{ get;set; }
        public String Title{ get;set; }
        public String SubTitle{ get;set; }
        public AnsiString Contents{ get;set; }
        public AnsiString CreatorID{ get;set; }
        public String Writer{ get;set; }
        public DateTime PublishTime{ get;set; }
    }
}