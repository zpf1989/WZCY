using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;

/// <summary>
/// GoodsMovementService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class GoodsMovementService : BaseService
{
    OA.BLL.GoodsMovementBLL bll = new OA.BLL.GoodsMovementBLL();
    public GoodsMovementService()
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
    public void GetGoodsMovementById()
    {
        string gmId = Context.Request["gmId"];
        string json = string.Empty;
        if (ValidateUtil.isBlank(gmId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "货物移动id不能为空", new { });
            return;
        }
        var so = bll.GetGoodsMovement(gmId);
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "货物移动id不能为空", so);
    }

    public void GetListInner(bool isForHelp)
    {
        //过滤条件
        //这里拼写过滤条件时，使用了很多sql，违背分层原则，为了省事儿先这样吧
        string whereSql = string.Empty;
        //string code = Context.Request["SOCode"];
        //if (!ValidateUtil.isBlank(code))
        //{
        //    whereSql += string.Format(" and s.SaleOrderCode like '%{0}%'", code);
        //}
        //string billType = Context.Request["BillTypeID"];
        //if (!ValidateUtil.isBlank(billType))
        //{
        //    whereSql += string.Format(" and s.BillTypeID = '{0}'", billType);
        //}
        //DateTime dtTemp;
        //string saleDateBegin = Context.Request["SaleDateBegin"];
        //if (!ValidateUtil.isBlank(saleDateBegin) && DateTime.TryParse(saleDateBegin, out dtTemp))
        //{
        //    whereSql += string.Format(" and CONVERT(DATE,s.SaleDate,112) >='{0}'", saleDateBegin);
        //}
        //string saleDateEnd = Context.Request["SaleDateEnd"];
        //if (!ValidateUtil.isBlank(saleDateEnd) && DateTime.TryParse(saleDateEnd, out dtTemp))
        //{
        //    whereSql += string.Format(" and CONVERT(DATE,s.SaleDate,112) <='{0}'", saleDateEnd);
        //}
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var list = bll.GetGoodsMovementsByPage(pageEntity, whereSql, string.Empty, isForHelp);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = list
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }
}
