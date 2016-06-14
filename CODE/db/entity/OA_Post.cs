using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OA_Post
    {
        public AnsiString PostID{ get;set; }
        public String PostCode{ get;set; }
        public String PostName{ get;set; }
        public AnsiString ParentPostID{ get;set; }
        public AnsiString DeptID{ get;set; }
    }
}