using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace OA.GeneralClass
{
    public class AppSettingHelper
    {
        /// <summary>
        /// 获取AppSettings设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            string value = ConfigurationManager.AppSettings[key].ToString();
            return value;
        }
    }
}
