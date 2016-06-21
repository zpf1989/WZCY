using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;

/// <summary>
/// SaleOrderItemService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class SaleOrderItemService : BaseService
{
    OA.BLL.SaleOrderItemBLL soItemBll = new OA.BLL.SaleOrderItemBLL();
    public SaleOrderItemService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public void GetOrderItems()
    {
        string saleOrderId = Context.Request["SaleOrderID"]; ;
        string json = string.Empty;
        if (ValidateUtil.isBlank(saleOrderId))
        {
            json = new
            {
                items = new { }
            }.SerializeToJson();
            Context.Response.WriteJson(json);
            return;
        }
        var soItems = soItemBll.GetOrderItems(saleOrderId);
        json = new { items = soItems }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

}
