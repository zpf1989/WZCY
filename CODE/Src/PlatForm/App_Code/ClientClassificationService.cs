using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.Model;

/// <summary>
/// ClientClassificationService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class ClientClassificationService : BaseService
{
    OA.BLL.ClientClassificationBLL bll = new OA.BLL.ClientClassificationBLL();

    public ClientClassificationService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public void GetList()
    {
        //过滤条件
        //这里拼写过滤条件时，使用了很多sql，违背分层原则，为了省事儿先这样吧
        string whereSql = string.Empty;
        string levelName = Context.Request["LevelName"];
        if (!ValidateUtil.isBlank(levelName))
        {
            whereSql += string.Format(" and LevelName like '%{0}%'", levelName);
        }
        string clientName = Context.Request["ClientName"];
        if (!ValidateUtil.isBlank(clientName))
        {
            whereSql += string.Format(" and ClientName like '%{0}%'", clientName);
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var users = bll.GetClientClassificationByPage(pageEntity, whereSql, string.Empty, false);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = users
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

    [WebMethod(EnableSession = true)]
    public void Save()
    {
        //获取请求数据
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!ValidateUtil.isBlank(data))
            {
                var entities = data.DeSerializeFromJson<List<ClientClassification>>();
                if (entities != null && entities.Count > 0)
                {
                    //预处理
                    //保存
                    bool rst = bll.Save(entities.ToArray());
                    if (rst)
                    {
                        Context.Response.WriteJson(ResultCode.Success, null, null);
                        return;
                    }
                }
            }
        }
        Context.Response.WriteJson(ResultCode.Failure, "保存失败", null);
    }

    [WebMethod(EnableSession = true)]
    public void Delete()
    {
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!ValidateUtil.isBlank(data))
            {
                var ids = data.DeSerializeFromJson<List<string>>();
                if (ids != null && ids.Count > 0)
                {
                    bool rst = bll.Delete(ids.ToArray());
                    if (rst)
                    {
                        Context.Response.WriteJson(ResultCode.Success, null, null);
                        return;
                    }
                }
            }
        }
        Context.Response.WriteJson(ResultCode.Failure, "删除失败", null);
    }

    [WebMethod(EnableSession = true)]
    public void Classify()
    {
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            bool rst = bll.Classify();
            if (rst)
            {
                Context.Response.WriteJson(ResultCode.Success, null, null);
                return;
            }
        }
        Context.Response.WriteJson(ResultCode.Failure, "客户分级失败", null);
    }

}
