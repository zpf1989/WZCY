using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;

/// <summary>
/// UserManageService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class UserManageService : System.Web.Services.WebService
{
    OA.BLL.UserManageBLL userManageBLL = new OA.BLL.UserManageBLL();

    public UserManageService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void GetList()
    {
        //过滤条件
        string whereSql = string.Empty;
        string name = Context.Request["UserName"];
        if (!string.IsNullOrEmpty(name))
        {
            whereSql += string.Format(" and UserName like '%{0}%'", name);//待定，需加表前缀
        }
        string deptName = Context.Request["DeptName"];
        if (!string.IsNullOrEmpty(deptName))
        {
            whereSql += string.Format(" and DeptName like '%{0}%'", deptName);//待定，需加表前缀
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var users = userManageBLL.GetUsersByPage(pageEntity, whereSql, null);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = users
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

    [WebMethod]
    public void Delete()
    {
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(data))
            {
                var empIds = data.DeSerializeFromJson<List<string>>();
                if (empIds != null && empIds.Count > 0)
                {
                    bool rst = userManageBLL.Delete(empIds.ToArray());
                    if (rst)
                    {
                        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, null, null);
                        return;
                    }
                }
            }
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "删除失败", null);
    }
}
