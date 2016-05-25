<%@ WebHandler Language="C#" Class="GetList" %>

using System;
using System.Web;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
 
public class GetList : IHttpHandler
{
    OA.BLL.DemoEmployeeBLL demoEmployeeBLL = new OA.BLL.DemoEmployeeBLL();

    public void ProcessRequest(HttpContext context)
    {
        //过滤条件
        string whereSql = string.Empty;
        string name = context.Request["Name"];
        if (!string.IsNullOrEmpty(name))
        {
            whereSql += string.Format(" and Name like '%{0}%'", name);
        }
        string deptName = context.Request["DeptName"];
        if (!string.IsNullOrEmpty(deptName))
        {
            whereSql += string.Format(" and DeptName like '%{0}%'", deptName);
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var demoEmployees = demoEmployeeBLL.GetDemoEmployeesByPage(pageEntity, whereSql, null);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = demoEmployees
        }.SerializeToJson();
        context.Response.WriteJson(json);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}