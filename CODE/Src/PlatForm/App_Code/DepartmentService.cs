using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.Model;

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
        //过滤条件
        string whereSql = string.Empty;
        string code = Context.Request["DeptCode"];
        if (!ValidateUtil.isBlank(code))
        {
            whereSql += string.Format(" and DeptCode like '%{0}%'", code);
        }
        string name = Context.Request["DeptName"];
        if (!ValidateUtil.isBlank(name))
        {
            whereSql += string.Format(" and DeptName like '%{0}%'", name);
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        List<DepartmentInfo> depts = departmentBLL.GetEntitiesByPageForHelp(pageEntity, whereSql, string.Empty);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = depts
        }.SerializeToJson();
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
