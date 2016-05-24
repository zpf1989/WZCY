using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.GeneralClass;
using System.Threading;
using System.Web.SessionState;
using System.Web;

namespace OA.BLL
{
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            //if (ValidateUtil.isBlank(this.GetParaValue("UserID")) && ValidateUtil.isBlank(this.GetParaValue("UserName")) && ValidateUtil.isBlank(this.GetParaValue("Operate")) && ValidateUtil.isBlank(this.GetParaValue("RoleID")))
            //{
            //    StringBuilder JScript = new StringBuilder();
            //    JScript.Append("<script type='text/javascript'>window.parent.location.replace('LogOut.aspx');</script>");
            //    ClientScript.RegisterStartupScript(this.GetType(), "key0", JScript.ToString());
            //}
            //else
            //{
            //    //StringBuilder JScript = new StringBuilder();
            //    //JScript.Append("<script type='text/javascript'>function click(){if (event.button==2){alert('拒绝查看代码，谢谢合作！');}}document.onmousedown=click;</script>");
            //    //ClientScript.RegisterStartupScript(this.GetType(), "key0", JScript.ToString());
            //}
            /*暂时没有考虑操作权限*/
            if (ValidateUtil.isBlank(this.GetParaValue("UserID")) && ValidateUtil.isBlank(this.GetParaValue("UserName")) && ValidateUtil.isBlank(this.GetParaValue("RoleID")))
            {
                StringBuilder JScript = new StringBuilder();
                //JScript.Append("<script type='text/javascript'>window.parent.location.replace('LogOut.aspx');</script>");
                //Response.Write("<script>alert('您的登录时间超时了，请重新登录！')</script>");
                //Response.Write("<script>top.location='http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ApplicationPath + "/zzb/index.aspx';</script>");
                JScript.Append("<script type='text/javascript'>alert('您的登录时间超时了，请重新登录！');top.location='http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ApplicationPath + "/LogOut.aspx';</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "key0", JScript.ToString());
            }
            else
            {
                //StringBuilder JScript = new StringBuilder();
                //JScript.Append("<script type='text/javascript'>function click(){if (event.button==2){alert('拒绝查看代码，谢谢合作！');}}document.onmousedown=click;</script>");
                //ClientScript.RegisterStartupScript(this.GetType(), "key0", JScript.ToString());
            }
        }

        /// <summary>
        /// 开始进度条
        /// </summary>
        /// <returns></returns>
        public void StartLoading()
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<div id='loaddiv' style='font-size:9pt; text-align:center; vertical-align:middle'>");
            JScript.Append("_");
            JScript.Append("</div>");
            JScript.Append("<script type='text/javascript'>loaddiv.innerText = ''; </script>");
            JScript.Append("<script type='text/javascript'>;");
            JScript.Append("var dots = 0;var dotmax = 10;function ShowWait()");
            JScript.Append("{var output; output = '数据加载中，请稍候';dots++;if(dots>=dotmax)dots=1;");
            JScript.Append("for(var x = 0;x < dots;x++){output += '·';}loaddiv.innerText =  output;}");
            JScript.Append("function StartShowWait(){loaddiv.style.visibility = 'visible'; ");
            JScript.Append("window.setInterval('ShowWait()',1000);}");
            JScript.Append("function HideWait(){loaddiv.style.display = 'none';");
            JScript.Append("window.clearInterval();}");
            JScript.Append("StartShowWait(); </script>");

            Response.Write(JScript.ToString());
            Response.Flush();
            Thread.Sleep(8000);
        }

        /// <summary>
        /// 结束进度条
        /// </summary>
        /// <returns></returns>
        public void EndLoading()
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>HideWait();</script>");
            Response.Write(JScript.ToString());
        }

        /// <summary>
        /// 获取页面参数的值
        /// </summary>
        /// <param name="oSession"></param>
        /// <param name="oRequest"></param>
        /// <param name="paraName"></param>
        /// <returns></returns>
        public string GetParaValue(HttpSessionState oSession, HttpRequest oRequest, string paraName)
        {
            string strValue = "";

            //如果Session中能够找到相应的参数值，则返回参数值
            try
            {
                strValue = oSession[paraName].ToString();
            }
            catch (NullReferenceException)
            {
                strValue = "";
            }

            //如果Session中不存在，从Request中寻找。
            if (ValidateUtil.isBlank(strValue))
            {
                try
                {
                    strValue = oRequest[paraName].ToString();
                }
                catch (NullReferenceException)
                {
                    strValue = "";
                }
                //均为空时，返回空字符串。
                if (ValidateUtil.isBlank(strValue))
                    strValue = "";
            }

            return strValue;
        }

        /// <summary>
        /// 获取页面参数的值
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <returns>参数值，字符串形式</returns>
        public string GetParaValue(string paraName)
        {
            return GetParaValue(Session, Request, paraName);
        }
    }
}
