using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.Model;

/// <summary>
/// SaleOrderService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class SaleOrderService : BaseService
{
    OA.BLL.SaleOrderBLL soBLL = new OA.BLL.SaleOrderBLL();
    public SaleOrderService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public void GetList()
    {
        GetListInner(false);
    }
    [WebMethod(EnableSession = true)]
    public void GetListForHelp()
    {
        GetListInner(true);
    }

    [WebMethod(EnableSession = true)]
    public void GetSaleOrderWithItems()
    {
        string orderId = Context.Request["SaleOrderID"];
        string json = string.Empty;
        if (ValidateUtil.isBlank(orderId))
        {
            json = new
            {
                saleorder = new { }
            }.SerializeToJson();
            Context.Response.WriteJson(json);
            return;
        }
        var so = soBLL.GetSaleOrderWithItems(orderId);
        json = new
        {
            saleorder = so
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

    public void GetListInner(bool isForHelp)
    {
        //过滤条件
        //这里拼写过滤条件时，使用了很多sql，违背分层原则，为了省事儿先这样吧
        string whereSql = string.Empty;
        string code = Context.Request["SaleOrderCode"];
        if (!ValidateUtil.isBlank(code))
        {
            whereSql += string.Format(" and si.SaleOrderCode like '%{0}%'", code);
        }

        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var soList = soBLL.GetSaleOrdersByPage(pageEntity, whereSql, string.Empty, isForHelp);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = soList
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

}
