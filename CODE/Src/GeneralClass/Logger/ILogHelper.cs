using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.GeneralClass.Logger
{
    public interface ILogHelper<T>
    {
        void LogError(Exception ex);
        void LogError(string msg);
        void LogInfo(Exception ex);
        void LogInfo(string msg);
        void LogWarning(Exception ex);
        void LogWarning(string msg);

        /// <summary>
        /// 是否记录日志
        /// </summary>
        bool Log { get; }
    }
}
