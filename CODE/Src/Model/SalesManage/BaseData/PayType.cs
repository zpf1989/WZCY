using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 付款方式
    /// </summary>
    public class PayType
    {
        public String PayTypeID{ get;set; }
        public String PayTypeCode{ get;set; }
        public String PayTypeName{ get;set; }
        public String PayTypeRemark{ get;set; }
    }
}