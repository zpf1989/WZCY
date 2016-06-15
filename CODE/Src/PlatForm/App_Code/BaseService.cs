using OA.GeneralClass;
using OA.GeneralClass.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;

/// <summary>
/// BaseService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class BaseService : System.Web.Services.WebService
{
    public BaseService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    protected string GetCurrentID()
    {
        return GetParaValue("UserID");
    }

    /// <summary>
    /// 获取参数值，来源：session、request
    /// </summary>
    /// <param name="oSession"></param>
    /// <param name="oRequest"></param>
    /// <param name="paraName"></param>
    /// <returns></returns>
    protected string GetParaValue(HttpSessionState oSession, HttpRequest oRequest, string paraName)
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
    protected string GetParaValue(string paraName)
    {
        return GetParaValue(Session, Context.Request, paraName);
    }
}
