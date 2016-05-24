using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace OA.GeneralClass
{
    public class JScript
    {
        public JScript()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="message"></param>
        public static void AlertMessage(string message)
        {
            HttpContext.Current.Response.Write("<script type='text/javascript'>alert('" + message + "');</script>");
        }

        /// <summary>
        /// 提示信息并跳转页面
        /// </summary>
        /// <param name="message"></param>
        /// <param name="url"></param>
        public static void AlertMessageAndRedirect(string message, string url)
        {
            HttpContext.Current.Response.Write("<script type='text/javascript'>alert('" + message + "');location.href('" + url + "');</script>");
        }

        /// <summary>
        /// 跳转页面
        /// </summary>
        /// <param name="url"></param>
        public static void Redirect(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                HttpContext.Current.Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
            }
            else
            {
                HttpContext.Current.Response.Write("<script type='text/javascript'>window.opener.location.reload(); self.opener = null; self.close();</script>");
            }
        }

        public static void RedirectByUrl(string url)
        {
            HttpContext.Current.Response.Write("<script type='text/javascript'>window.opener.location.href='" + url + "'; self.opener = null; self.close();</script>");

        }
        public static void BackAndRedirect(string url)
        {
            HttpContext.Current.Response.Write("<script type='text/javascript'>window.opener = null; window.close();location.href('" + url + "');</script>");
        }

        public static string GetScript()
        {
            return "<script type='text/javascript'>window.opener.location.reload(); self.opener = null; self.close();</script>";
        }

        public static void OpenWindow(string url)
        {
            HttpContext.Current.Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
        }

        /// <summary>
        /// 去除数组中重复的项
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string[] GetString(string[] values)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < values.Length; i++)//遍历数组成员  
            {
                if (list.IndexOf(values[i].ToLower()) == -1)  //对每个成员做一次新数组查询如果没有相等的则加到新数组  
                    list.Add(values[i]);
            }
            return list.ToArray();
        }
    }
}
