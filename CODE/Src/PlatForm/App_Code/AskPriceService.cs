using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.GeneralClass;
using OA.Model;

/// <summary>
/// AskPriceService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class AskPriceService : BaseService
{
    OA.BLL.AskPriceBLL bll = new OA.BLL.AskPriceBLL();
    public AskPriceService()
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

    public void GetListInner(bool isForHelp)
    {
        //过滤条件
        //这里拼写过滤条件时，使用了很多sql，违背分层原则，为了省事儿先这样吧
        string whereSql = string.Empty;
        string code = Context.Request["APCode"];
        if (!ValidateUtil.isBlank(code))
        {
            whereSql += string.Format(" and ap.APCode like '%{0}%'", code);
        }
        string apType = Context.Request["APType"];
        if (!ValidateUtil.isBlank(apType))
        {
            whereSql += string.Format(" and ap.APType = '{0}'", apType);
        }
        DateTime dtTemp;
        string askDateBegin = Context.Request["AskDateBegin"];
        if (!ValidateUtil.isBlank(askDateBegin) && DateTime.TryParse(askDateBegin, out dtTemp))
        {
            whereSql += string.Format(" and ap.AskDate >='{0}'", askDateBegin);
        }
        string askDateEnd = Context.Request["AskDateEnd"];
        if (!ValidateUtil.isBlank(askDateEnd) && DateTime.TryParse(askDateEnd, out dtTemp))
        {
            whereSql += string.Format(" and ap.AskDate <='{0}'", askDateEnd);
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var apList = bll.GetAskPricesByPage(pageEntity, whereSql, string.Empty, isForHelp);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = apList
        }.SerializeToJson();
        Context.Response.WriteJson(json);
    }

    [WebMethod(EnableSession = true)]
    public void GetAskPriceById()
    {
        string apId = Context.Request["apId"];
        string json = string.Empty;
        if (ValidateUtil.isBlank(apId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "询价单id不能为空", new { });
            return;
        }
        var ap = bll.GetAskPrice(apId);
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "获取成功", ap);
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
                var askprice = data.DeSerializeFromJson<AskPrice>();
                if (askprice != null)
                {
                    //预处理
                    if (ValidateUtil.isBlank(askprice.APID))
                    {
                        askprice.Creator = base.GetCurrentID();
                        askprice.CreateTime = DateTime.Now;
                        askprice.State = "1";//编制
                    }
                    else
                    {
                        askprice.Editor = base.GetCurrentID();
                        askprice.EditTime = DateTime.Now;
                    }
                    //保存
                    bool rst = bll.Save(askprice);
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
                var apIds = data.DeSerializeFromJson<List<string>>();
                if (apIds != null && apIds.Count > 0)
                {
                    bool rst = bll.Delete(apIds.ToArray());
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
    public void SubmitToFirstChecker(string userId, string apIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择初审人", null);
            return;
        }
        if (ValidateUtil.isBlank(apIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择询价单", null);
            return;
        }
        string[] apIdArr = apIds.DeSerializeFromJson<string[]>();
        if (apIdArr == null || apIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，询价单数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToFirstChecker(userId, apIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }

    [WebMethod(EnableSession = true)]
    public void SubmitToSecondChecker(string userId, string apIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择复审人", null);
            return;
        }
        if (ValidateUtil.isBlank(apIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择询价单", null);
            return;
        }
        string[] apIdArr = apIds.DeSerializeFromJson<string[]>();
        if (apIdArr == null || apIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，询价单数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToSecondChcker(userId, apIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void SubmitToReader(string userId, string apIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择分阅人", null);
            return;
        }
        if (ValidateUtil.isBlank(apIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择询价单", null);
            return;
        }
        string[] apIdArr = apIds.DeSerializeFromJson<string[]>();
        if (apIdArr == null || apIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，询价单数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToReader(userId, apIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void FirstCheck(bool result, string checkView, string apIds)
    {
        if (ValidateUtil.isBlank(apIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，请选择询价单", null);
            return;
        }
        string[] apIdArr = apIds.DeSerializeFromJson<string[]>();
        if (apIdArr == null || apIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，询价单数据格式不正确", null);
            return;
        }
        bool rst = bll.FirstCheck(result, checkView, apIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "初审成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void SecondCheck(bool result, string checkView, string apIds)
    {
        if (ValidateUtil.isBlank(apIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，请选择询价单", null);
            return;
        }
        string[] apIdArr = apIds.DeSerializeFromJson<string[]>();
        if (apIdArr == null || apIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，询价单数据格式不正确", null);
            return;
        }
        bool rst = bll.SecondCheck(result, base.GetCurrentID(), checkView, apIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "复审成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void Read(string apIds)
    {
        if (ValidateUtil.isBlank(apIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，请选择询价单", null);
            return;
        }
        string[] apIdArr = apIds.DeSerializeFromJson<string[]>();
        if (apIdArr == null || apIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，询价单数据格式不正确", null);
            return;
        }
        bool rst = bll.Read(base.GetCurrentID(), apIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "分阅成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void Close(string apIds)
    {
        if (ValidateUtil.isBlank(apIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，请选择询价单", null);
            return;
        }
        string[] apIdArr = apIds.DeSerializeFromJson<string[]>();
        if (apIdArr == null || apIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，询价单数据格式不正确", null);
            return;
        }
        bool rst = bll.Close(apIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "关闭成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，未知原因", null);
    }
    #endregion

}
