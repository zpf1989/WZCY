using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.Model;

/// <summary>
/// BuyOrderService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class BuyOrderService : BaseService
{

    OA.BLL.BuyOrderBLL bll = new OA.BLL.BuyOrderBLL();
    public BuyOrderService()
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
    public void GetBuyOrderById()
    {
        string orderId = Context.Request["BOID"];
        string json = string.Empty;
        if (ValidateUtil.isBlank(orderId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "采购订单id不能为空", new { });
            return;
        }
        var bo = bll.GetBuyOrder(orderId);
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "采购订单id不能为空", bo);
    }

    public void GetListInner(bool isForHelp)
    {
        //过滤条件
        //这里拼写过滤条件时，使用了很多sql，违背分层原则，为了省事儿先这样吧
        string whereSql = string.Empty;
        string code = Context.Request["BOCode"];
        if (!ValidateUtil.isBlank(code))
        {
            whereSql += string.Format(" and b.BuyOrderCode like '%{0}%'", code);
        }
        string supplierID = Context.Request["SupplierID"];
        if (!ValidateUtil.isBlank(supplierID))
        {
            whereSql += string.Format(" and s.SupplierID = '{0}'", supplierID);
        }
        DateTime dtTemp;
        string buyOrderDateBegin = Context.Request["BuyOrderDateBegin"];
        if (!ValidateUtil.isBlank(buyOrderDateBegin) && DateTime.TryParse(buyOrderDateBegin, out dtTemp))
        {
            whereSql += string.Format(" and CONVERT(DATE,b.BuyOrderDate,112) >='{0}'", buyOrderDateBegin);
        }
        string buyOrderDateEnd = Context.Request["BuyOrderDateEnd"];
        if (!ValidateUtil.isBlank(buyOrderDateEnd) && DateTime.TryParse(buyOrderDateEnd, out dtTemp))
        {
            whereSql += string.Format(" and CONVERT(DATE,b.BuyOrderDate,112) <='{0}'", buyOrderDateEnd);
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var soList = bll.GetSaleOrdersByPage(pageEntity, whereSql, string.Empty, isForHelp);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = soList
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
                var buyorder = data.DeSerializeFromJson<BuyOrder>();
                if (buyorder != null)
                {
                    //预处理
                    if (ValidateUtil.isBlank(buyorder.BuyOrderID))
                    {
                        buyorder.Creator = base.GetCurrentID();
                        buyorder.CreateTime = DateTime.Now;
                        buyorder.OrderState = "1";//编制
                    }
                    else
                    {
                        buyorder.Editor = base.GetCurrentID();
                        buyorder.EditTime = DateTime.Now;
                    }
                    //保存
                    bool rst = bll.Save(buyorder);
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
    [WebMethod(EnableSession = true)]
    public void Delete()
    {
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!ValidateUtil.isBlank(data))
            {
                var soIds = data.DeSerializeFromJson<List<string>>();
                if (soIds != null && soIds.Count > 0)
                {
                    bool rst = bll.Delete(soIds.ToArray());
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

    #region 流程
    [WebMethod(EnableSession = true)]
    public void SubmitToFirstChecker(string userId, string soIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择初审人", null);
            return;
        }
        if (ValidateUtil.isBlank(soIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择销售订单", null);
            return;
        }
        string[] soIdArr = soIds.DeSerializeFromJson<string[]>();
        if (soIdArr == null || soIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，销售订单数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToFirstChecker(userId, soIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }

    [WebMethod(EnableSession = true)]
    public void SubmitToSecondChecker(string userId, string soIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择复审人", null);
            return;
        }
        if (ValidateUtil.isBlank(soIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择销售订单", null);
            return;
        }
        string[] soIdArr = soIds.DeSerializeFromJson<string[]>();
        if (soIdArr == null || soIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，销售订单数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToSecondChcker(userId, soIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void SubmitToReader(string userId, string soIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择分阅人", null);
            return;
        }
        if (ValidateUtil.isBlank(soIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择销售订单", null);
            return;
        }
        string[] soIdArr = soIds.DeSerializeFromJson<string[]>();
        if (soIdArr == null || soIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，销售订单数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToReader(userId, soIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void FirstCheck(bool result, string checkView, string soIds)
    {
        if (ValidateUtil.isBlank(soIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，请选择销售订单", null);
            return;
        }
        string[] soIdArr = soIds.DeSerializeFromJson<string[]>();
        if (soIdArr == null || soIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，销售订单数据格式不正确", null);
            return;
        }
        bool rst = bll.FirstCheck(result, checkView, soIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "初审成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void SecondCheck(bool result, string checkView, string soIds)
    {
        if (ValidateUtil.isBlank(soIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，请选择销售订单", null);
            return;
        }
        string[] soIdArr = soIds.DeSerializeFromJson<string[]>();
        if (soIdArr == null || soIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，销售订单数据格式不正确", null);
            return;
        }
        bool rst = bll.SecondCheck(result, base.GetCurrentID(), checkView, soIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "复审成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void Read(string soIds)
    {
        if (ValidateUtil.isBlank(soIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，请选择销售订单", null);
            return;
        }
        string[] soIdArr = soIds.DeSerializeFromJson<string[]>();
        if (soIdArr == null || soIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，销售订单数据格式不正确", null);
            return;
        }
        bool rst = bll.Read(base.GetCurrentID(), soIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "分阅成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void Close(string soIds)
    {
        if (ValidateUtil.isBlank(soIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，请选择销售订单", null);
            return;
        }
        string[] soIdArr = soIds.DeSerializeFromJson<string[]>();
        if (soIdArr == null || soIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，销售订单数据格式不正确", null);
            return;
        }
        bool rst = bll.Close(soIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "关闭成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，未知原因", null);
    }
    #endregion
    
}
