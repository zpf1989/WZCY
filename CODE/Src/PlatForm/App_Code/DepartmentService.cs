using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;

/// <summary>
/// DepartmentService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class DepartmentService : System.Web.Services.WebService
{
     OA.BLL.DepartmentBLL departmentBLL = new OA.BLL.DepartmentBLL();
     public DepartmentService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public void GetForGridHelp()
    {
        List<OA.Model.DepartmentInfo> depts = departmentBLL.GetAllDepartmentsForGridHelp();
        string json = depts.SerializeToJson();
        Context.Response.WriteJson(json);
    }

    public gentleyh.Class1 security = new gentleyh.Class1();
    [WebMethod(EnableSession = true)]
    public void Decrypt(string str)
    {
        string _str = security.Decrypt(str, security.se_yaoshi);
        Context.Response.WriteJson(_str);
    }
}
