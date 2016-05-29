using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;

/// <summary>
/// RoleManageService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class RoleManageService : System.Web.Services.WebService
{
    OA.BLL.RoleManageBLL roleManageBLL = new OA.BLL.RoleManageBLL();
    public RoleManageService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void GetForGridHelp()
    {
        List<OA.Model.RoleInfo> depts = roleManageBLL.GetRoleList();
        string json = depts.SerializeToJson();
        Context.Response.WriteJson(json);
    }

}
