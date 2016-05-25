using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA.GeneralClass.Extensions
{
    /// <summary>
    /// Http响应扩展
    /// </summary>
    public static class HttpResponseExtension
    {
        /// <summary>
        /// 写入json响应
        /// </summary>
        /// <param name="response"></param>
        /// <param name="rst">结果代码</param>
        /// <param name="msg">提示信息</param>
        /// <param name="data">响应数据</param>
        public static void WriteJson(this HttpResponse response,ResultCode rst, string msg = "", object data = null)
        {
            string jsonString = JsonConvert.SerializeObject(new { code = (int)rst, msg = msg, data = data });
            response.WriteJson(jsonString);
        }
        /// <summary>
        /// 写入json响应
        /// </summary>
        /// <param name="response"></param>
        /// <param name="jsonString">json字符串</param>
        public static void WriteJson(this HttpResponse response,string jsonString)
        {
            response.ContentType = "application/json";
            response.Write(jsonString);
            response.End();
        }
    }
}