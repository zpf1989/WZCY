using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_User
    {
        public AnsiString UserID{ get;set; }
        public String UserCode{ get;set; }
        public AnsiString UserName{ get;set; }
        public AnsiString UserPwd{ get;set; }
        public AnsiStringFixedLength UserState{ get;set; }
        public DateTime CreateTime{ get;set; }
        public AnsiString CreateUserID{ get;set; }
        public AnsiString DeptID{ get;set; }
        public String Operator{ get;set; }
    }
}