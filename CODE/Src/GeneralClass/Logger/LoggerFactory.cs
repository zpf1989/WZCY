using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.GeneralClass.Logger
{
    public class LoggerFactory
    {
        public static ILogHelper<T> GetLogger<T>()
        {
            return new Log4NetHelper<T>();
        }
    }
}
