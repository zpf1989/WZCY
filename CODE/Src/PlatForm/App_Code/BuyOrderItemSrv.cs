using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;

/// <summary>
/// BuyOrderItemSrv 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class BuyOrderItemSrv : BaseService {

    OA.BLL.BuyOrderItemBLL bll = new OA.BLL.BuyOrderItemBLL();
    public BuyOrderItemSrv()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public void GetOrderItems()
    {
        string buyOrderId = Context.Request["BOID"];
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        string json = string.Empty;
        if (ValidateUtil.isBlank(buyOrderId))
        {
            json = new
            {
                total = 0,
                rows = new { }
            }.SerializeToJson();
            Context.Response.WriteJson(json);
            return;
        }
        var boItems = bll.GetOrderItems(pageEntity, buyOrderId);
        json = new
        {
            total = pageEntity.TotalRecords,
            rows = boItems
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

    [WebMethod(EnableSession = true)]
    public void Delete()
    {
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!ValidateUtil.isBlank(data))
            {
                var boIds = data.DeSerializeFromJson<List<string>>();
                if (boIds != null && boIds.Count > 0)
                {
                    bool rst = bll.Delete(boIds.ToArray());
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
