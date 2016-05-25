<%@ WebHandler Language="C#" Class="Save" %>

using System;
using System.Web;
using OA.GeneralClass.Extensions;

public class Save : IHttpHandler
{
    OA.BLL.DemoEmployeeBLL demoEmployeeBLL = new OA.BLL.DemoEmployeeBLL();

    public void ProcessRequest(HttpContext context)
    {
        //获取请求数据
        using (var reader = new System.IO.StreamReader(context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(data))
            {
                var emp = data.DeSerializeFromJson<OA.Model.DemoEmployee>();
                if (emp != null)
                {
                    bool rst = string.IsNullOrEmpty(emp.Id) ? demoEmployeeBLL.Add(emp) : demoEmployeeBLL.Update(emp);
                    if (rst)
                    {
                        context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, null, null);
                        return;
                    }
                }
            }
        }
        context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "保存失败", null);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}