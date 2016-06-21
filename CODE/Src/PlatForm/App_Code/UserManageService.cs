using OA.GeneralClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OA.GeneralClass.Extensions;
using OA.Model;
using OA.GeneralClass.Logger;

/// <summary>
/// UserManageService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class UserManageService : BaseService
{
    ILogHelper<UserManageService> logger = LoggerFactory.GetLogger<UserManageService>();
    OA.BLL.UserManageBLL userManageBLL = new OA.BLL.UserManageBLL();
    gentleyh.Class1 security = new gentleyh.Class1();
    public UserManageService()
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
        string whereSql = string.Empty;
        string name = Context.Request["UserName"];
        if (!ValidateUtil.isBlank(name))
        {
            whereSql += string.Format(" and UserName like '%{0}%'", name);//待定，需加表前缀
        }
        string deptName = Context.Request["DeptName"];
        if (!ValidateUtil.isBlank(deptName))
        {
            whereSql += string.Format(" and DeptName like '%{0}%'", deptName);//待定，需加表前缀
        }
        //分页参数：easyui分页查询时，page、rows
        int pageIndex = 1;
        Int32.TryParse(Context.Request["page"], out pageIndex);
        int pageSize = 10;
        Int32.TryParse(Context.Request["rows"], out pageSize);
        PageEntity pageEntity = new PageEntity(pageIndex, pageSize);
        var users = userManageBLL.GetUsersByPage(pageEntity, whereSql, null, isForHelp);
        //easyui分页查询，要求返回json数据，并且包含total和rows
        string json = new
        {
            total = pageEntity.TotalRecords,//总记录数已被更新
            rows = users
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
                var empIds = data.DeSerializeFromJson<List<string>>();
                if (empIds != null && empIds.Count > 0)
                {
                    bool rst = userManageBLL.Delete(empIds.ToArray());
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

    [WebMethod(EnableSession = true)]
    public void Save()
    {
        //获取请求数据
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string data = reader.ReadToEnd();
            if (!ValidateUtil.isBlank(data))
            {
                var users = data.DeSerializeFromJson<List<UserInfo>>();
                if (users != null && users.Count > 0)
                {
                    var currentUser = base.GetCurrentID();
                    //预处理
                    foreach (UserInfo user in users)
                    {
                        //默认密码=用户编号
                        if (ValidateUtil.isBlank(user.UserPwd))
                        {
                            user.UserPwd = security.Encrypt(user.UserCode, security.se_yaoshi);
                        }
                        //创建人
                        if (ValidateUtil.isBlank(user.UserID))
                        {
                            user.CreateUserID = currentUser;
                        }
                    }
                    bool rst = userManageBLL.Save(users.ToArray());
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

    /// <summary>
    /// 设置操作权限
    /// </summary>
    [WebMethod(EnableSession = true)]
    public void SetOpt()
    {
        //获取请求数据
        using (var reader = new System.IO.StreamReader(Context.Request.InputStream))
        {
            string dataStr = reader.ReadToEnd();
            if (!ValidateUtil.isBlank(dataStr))
            {
                var dataList = dataStr.DeSerializeFromJson<List<string[]>>();
                if (dataList == null || dataList.Count < 2)
                {
                    Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, "参数错误", null);
                }
                string[] userIds = dataList[0];
                string[] optvalues = dataList[1];
                if (userIds != null && userIds.Length > 0)
                {
                    bool rst = userManageBLL.SetOpt(userIds, optvalues);
                    if (rst)
                    {
                        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, null, null);
                        return;
                    }
                }
                Context.Response.WriteJson(OA.GeneralClass.ResultCode.Success, null, optvalues);
            }
        }
        Context.Response.WriteJson(OA.GeneralClass.ResultCode.Failure, "保存失败", null);
    }
}
