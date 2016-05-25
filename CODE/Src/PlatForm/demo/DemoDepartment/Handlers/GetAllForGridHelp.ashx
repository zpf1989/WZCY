<%@ WebHandler Language="C#" Class="GetAllForGridHelp" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using OA.GeneralClass.Extensions;

public class GetAllForGridHelp : IHttpHandler
{
    OA.BLL.DemoDepartmentBLL demoDepartmentBLL = new OA.BLL.DemoDepartmentBLL();
    
    public void ProcessRequest(HttpContext context)
    {
        List<OA.Model.DemoDepartment> demoDepts = demoDepartmentBLL.GetAllDepartmentsForGridHelp();
        string json = demoDepts.SerializeToJson();
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