using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.Model;

/// <summary>
/// MaterialsService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class MaterialsService : System.Web.Services.WebService
{

    OA.BLL.MaterialsBLL bll = new OA.BLL.MaterialsBLL();

    public MaterialsService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void GetList()
    {
        //过滤条件
        //这里拼写过滤条件时，使用了很多sql，违背分层原则，为了省事儿先这样吧
        string whereSql = string.Empty;
        string code = Context.Request["MaterialCode"];
        if (!string.IsNullOrEmpty(code))
        {
            whereSql += string.Format(" and m.MaterialCode like '%{0}%'", code);
        }
        string name = Context.Request["MaterialName"];
        if (!string.IsNullOrEmpty(name))
        {
            whereSql += string.Format(" and m.MaterialName like '%{0}%'", name);
        }
        string materialClassID = Context.Request["MaterialClassID"];
        string materialClassName = Context.Request["MaterialClassName"];
        if (!string.IsNullOrEmpty(materialClassID))
        {
            whereSql += string.Format(" and m.MaterialClassID = '{0}'", materialClassID);
        }
        else if (!string.IsNullOrEmpty(materialClassName))
        {
            whereSql += string.Format(" and c.MaterialClassName like '%{0}%' ", materialClassName);
        }
        string materialTypeID = Context.Request["MaterialTypeID"];
        string materialTypeName = Context.Request["MaterialTypeName"];
        if (!string.IsNullOrEmpty(name))
        {
            whereSql += string.Format(" and m.MaterialTypeID = '{0}'", materialTypeID);
        }
        else if (!string.IsNullOrEmpty(materialTypeName))
        {
            whereSql += string.Format(" and t.MaterialTypeName like '%{0}%' ", materialTypeName);
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var users = bll.GetMaterialsByPage(pageEntity, whereSql, string.Empty);
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
                var materials = data.DeSerializeFromJson<List<Materials>>();
                if (materials != null && materials.Count > 0)
                {
                    //预处理
                    //保存
                    bool rst = bll.Save(materials.ToArray());
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
                var mIds = data.DeSerializeFromJson<List<string>>();
                if (mIds != null && mIds.Count > 0)
                {
                    bool rst = bll.Delete(mIds.ToArray());
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
