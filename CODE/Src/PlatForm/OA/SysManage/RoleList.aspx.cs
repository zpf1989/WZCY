using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.BLL;
using OA.Model;
using System.Text;

public partial class OA_SysManage_RoleList : PageBase
{
    public gentleyh.Class1 security = new gentleyh.Class1();
    RoleManageBLL roleBll = new RoleManageBLL();

    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            InitPage();
        }
    }
    /// <summary>
    /// 初始化页面
    /// </summary>
    public void InitPage()
    {
        this.cgv_List.DataSourceID = "ods_ListDataSource";
        this.Card.Visible = false;
    }
    protected void cgv_List_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        
    }
    protected void cgv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
        
    }
    /// <summary>
    /// 修改按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.Params["check"] != null)
        {
            string roleID = security.Decrypt(Request.Params["check"].ToString(), security.se_yaoshi);
            ViewState["RoleID"] = roleID;

            this.List.Visible = false;
            this.Card.Visible = true;
            InitUpdatePage(roleID);
        }
    }
    /// <summary>
    /// 删除按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnDel_Click(object sender, ImageClickEventArgs e)
    {
        if (Request.Params["check"] != null)
        {
            string[] ids = Request.Params["check"].ToString().Split(',');
            foreach (string id in ids)
            {
                string i = security.Decrypt(id, security.se_yaoshi);
                roleBll.DeleteRole(i);
            }

            this.cgv_List.DataSourceID = "ods_ListDataSource";
        }
    }
    /// <summary>
    /// 初始化修改页面
    /// </summary>
    /// <param name="id"></param>
    public void InitUpdatePage(string roleID)
    {
        RoleInfo info = roleBll.GetModelByRoleID(roleID);
        if (info == null)
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('数据错误！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
            return;
        }
        this.RoleCode.Text = info.RoleCode;
        this.RoleName.Text = info.RoleName;
    }
    /// <summary>
    /// 检验添加项是否已存在
    /// </summary>
    private void CheckRole()
    {
        string roleCode = this.RoleCode.Text.Trim();
        string roleName = this.RoleName.Text.Trim();

        int count = roleBll.CheckRole(roleCode, roleName);
        if (count > 0)
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('填写的内容已经存在！请重新输入');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
            return;
        }
        UpdateRole(roleCode, roleName);
    }
    /// <summary>
    /// 添加角色
    /// </summary>
    private void UpdateRole(string RoleCode, string RoleName)
    {
        RoleInfo info = new RoleInfo();
        info.RoleID = Convert.ToString(ViewState["RoleID"]);
        info.RoleCode = RoleCode;
        info.RoleName = RoleName;

        int affect = roleBll.UpdateRole(info);
        if (affect > 0)
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('修改成功！');</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
        }
        else
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('修改失败！');</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
        }
        this.cgv_List.DataSourceID = "ods_ListDataSource";
        this.Card.Visible = false;
        this.List.Visible = true;
    }
    /// <summary>
    /// 保存按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        this.CheckRole();
    }
    /// <summary>
    /// 取消按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnReset_Click(object sender, ImageClickEventArgs e)
    {
        this.List.Visible = true;
        this.Card.Visible = false;
    }
}
