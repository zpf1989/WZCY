using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.Model;

/// <summary>
/// GoodsMovementService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Servicegm.ScriptService]
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
        string code = Context.Request["GMCode"];
        if (!ValidateUtil.isBlank(code))
        {
            whereSql += string.Format(" and gm.GoodsMovementCode like '%{0}%'", code);
        }
        string busType = Context.Request["BusinessType"];
        if (!ValidateUtil.isBlank(busType))
        {
            whereSql += string.Format(" and gm.BusinessType = '{0}'", busType);
        }
        string moveType = Context.Request["MoveTypeCode"];
        if (!ValidateUtil.isBlank(moveType))
        {
            whereSql += string.Format(" and gm.MoveTypeCode = '{0}'", moveType);
        }
        DateTime dtTemp;
        string createDateBegin = Context.Request["CreateDateBegin"];
        if (!ValidateUtil.isBlank(createDateBegin) && DateTime.TryParse(createDateBegin, out dtTemp))
        {
            whereSql += string.Format(" and CONVERT(DATE,gm.CreateDate,112) >='{0}'", createDateBegin);
        }
        string createDateEnd = Context.Request["CreateDateEnd"];
        if (!ValidateUtil.isBlank(createDateEnd) && DateTime.TryParse(createDateEnd, out dtTemp))
        {
            whereSql += string.Format(" and CONVERT(DATE,gm.CreateDate,112) <='{0}'", createDateEnd);
        }
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

    [WebMethod(EnableSession = true)]
    public void Save()
    {
        //获取请求数据
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!ValidateUtil.isBlank(data))
            {
                var gm = data.DeSerializeFromJson<GoodsMovement>();
                if (gm != null)
                {
                    //预处理
                    if (ValidateUtil.isBlank(gm.GoodsMovementID))
                    {
                        gm.Creator = base.GetCurrentID();
                        gm.CreateTime = DateTime.Now;
                        gm.BillState = "1";//编制
                    }
                    else
                    {
                        gm.Editor = base.GetCurrentID();
                        gm.EditTime = DateTime.Now;
                    }
                    //保存
                    bool rst = bll.Save(gm);
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
                var gmIds = data.DeSerializeFromJson<List<string>>();
                if (gmIds != null && gmIds.Count > 0)
                {
                    bool rst = bll.Delete(gmIds.ToArray());
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
    public void SubmitToFirstChecker(string userId, string gmIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择初审人", null);
            return;
        }
        if (ValidateUtil.isBlank(gmIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择单据", null);
            return;
        }
        string[] gmIdArr = gmIds.DeSerializeFromJson<string[]>();
        if (gmIdArr == null || gmIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，单据数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToFirstChecker(userId, gmIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }

    [WebMethod(EnableSession = true)]
    public void SubmitToSecondChecker(string userId, string gmIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择复审人", null);
            return;
        }
        if (ValidateUtil.isBlank(gmIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择单据", null);
            return;
        }
        string[] gmIdArr = gmIds.DeSerializeFromJson<string[]>();
        if (gmIdArr == null || gmIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，单据数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToSecondChcker(userId, gmIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void SubmitToReader(string userId, string gmIds)
    {
        if (ValidateUtil.isBlank(userId))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择分阅人", null);
            return;
        }
        if (ValidateUtil.isBlank(gmIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，请选择单据", null);
            return;
        }
        string[] gmIdArr = gmIds.DeSerializeFromJson<string[]>();
        if (gmIdArr == null || gmIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，单据数据格式不正确", null);
            return;
        }

        bool rst = bll.SubmitToReader(userId, gmIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "提交成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "提交失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void FirstCheck(bool result, string checkView, string gmIds)
    {
        if (ValidateUtil.isBlank(gmIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，请选择单据", null);
            return;
        }
        string[] gmIdArr = gmIds.DeSerializeFromJson<string[]>();
        if (gmIdArr == null || gmIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，单据数据格式不正确", null);
            return;
        }
        bool rst = bll.FirstCheck(result, checkView, gmIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "初审成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "初审失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void SecondCheck(bool result, string checkView, string gmIds)
    {
        if (ValidateUtil.isBlank(gmIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，请选择单据", null);
            return;
        }
        string[] gmIdArr = gmIds.DeSerializeFromJson<string[]>();
        if (gmIdArr == null || gmIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，单据数据格式不正确", null);
            return;
        }
        bool rst = bll.SecondCheck(result, base.GetCurrentID(), checkView, gmIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "复审成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "复审失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void Read(string gmIds)
    {
        if (ValidateUtil.isBlank(gmIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，请选择单据", null);
            return;
        }
        string[] gmIdArr = gmIds.DeSerializeFromJson<string[]>();
        if (gmIdArr == null || gmIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，单据数据格式不正确", null);
            return;
        }
        bool rst = bll.Read(base.GetCurrentID(), gmIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "分阅成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "分阅失败，未知原因", null);
    }
    [WebMethod(EnableSession = true)]
    public void Close(string gmIds)
    {
        if (ValidateUtil.isBlank(gmIds))
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，请选择单据", null);
            return;
        }
        string[] gmIdArr = gmIds.DeSerializeFromJson<string[]>();
        if (gmIdArr == null || gmIdArr.Length < 1)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，单据数据格式不正确", null);
            return;
        }
        bool rst = bll.Close(gmIdArr);
        if (rst)
        {
            Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "关闭成功", null);
            return;
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "关闭失败，未知原因", null);
    }
    #endregion
}
