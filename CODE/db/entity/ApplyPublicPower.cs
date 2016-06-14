using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class ApplyPublicPower
    {
        public AnsiString AppID{ get;set; }
        public AnsiString DeptID{ get;set; }
        public AnsiStringFixedLength ApplyDate{ get;set; }
        public AnsiString Creator{ get;set; }
        public DateTime CreateTime{ get;set; }
        public AnsiString FirstChecker{ get;set; }
        public DateTime FirstOperateTime{ get;set; }
        public AnsiString SecondChecker{ get;set; }
        public DateTime SecondOperateTime{ get;set; }
        public AnsiString Opinion{ get;set; }
    }
}