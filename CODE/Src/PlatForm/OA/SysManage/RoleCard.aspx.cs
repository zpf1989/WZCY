using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.BLL;
using System.Text;
using OA.Model;

public partial class OA_SysManage_RoleCard : PageBase
{
    RoleManageBLL roleBll = new RoleManageBLL();

    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
        }
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
        AddRole(roleCode, roleName);
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    private void AddRole(string RoleCode,string RoleName)
    {
        RoleInfo info = new RoleInfo();
        info.RoleID = System.Guid.NewGuid().ToString();
        info.RoleCode = RoleCode;
        info.RoleName = RoleName;

        int affect = roleBll.AddRole(info);
        if (affect > 0)
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('添加成功！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
        }
        else
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('添加失败！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
        }
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
}
