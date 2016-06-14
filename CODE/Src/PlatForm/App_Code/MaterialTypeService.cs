using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.Model;

/// <summary>
/// MaterialTypeService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class MaterialTypeService : System.Web.Services.WebService
{

    OA.BLL.MaterialTypeBLL bll = new OA.BLL.MaterialTypeBLL();

    public MaterialTypeService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void GetList()
    {
        //过滤条件
        string whereSql = string.Empty;
        string code = Context.Request["MaterialTypeCode"];
        if (!string.IsNullOrEmpty(code))
        {
            whereSql += string.Format(" and MaterialTypeCode like '%{0}%'", code);
        }
        string name = Context.Request["MaterialTypeName"];
        if (!string.IsNullOrEmpty(name))
        {
            whereSql += string.Format(" and MaterialTypeName like '%{0}%'", name);
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var users = bll.GetTypesByPage(pageEntity, whereSql, string.Empty);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = users
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

    [WebMethod]
    public void Save()
    {
        //获取请求数据
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(data))
            {
                var types = data.DeSerializeFromJson<List<MaterialType>>();
                if (types != null && types.Count > 0)
                {
                    //预处理
                    //保存
                    bool rst = bll.Save(types.ToArray());
                    if (rst)
                    {
                        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, null, null);
                        return;
                    }
                }
            }
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "保存失败", null);
    }

    [WebMethod]
    public void Delete()
    {
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(data))
            {
                var typeIds = data.DeSerializeFromJson<List<string>>();
                if (typeIds != null && typeIds.Count > 0)
                {
                    bool rst = bll.Delete(typeIds.ToArray());
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
