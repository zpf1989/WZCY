using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.BLL;
using System.Text;

public partial class OA_SysManage_ChangePwd : PageBase
{
    UserInfoBLL userBll = new UserInfoBLL();
    public gentleyh.Class1 security = new gentleyh.Class1();

    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
        }
    }
    /// <summary>
    /// 提交按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        string userCode = Convert.ToString(Session["UserCode"]);
        string userOldPwd = security.Encrypt(this.OldPwd.Text.Trim(), security.se_yaoshi);
        string userNewPwd = security.Encrypt(this.NewPwd.Text.Trim(), security.se_yaoshi);
        int affect = ChangePwd(userCode, userOldPwd, userNewPwd);
        if (affect > 0)
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('修改成功！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key1", JScript.ToString());
        }
        else
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('你输入的密码不正确！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key1", JScript.ToString());
        }
    }
    /// <summary>
    /// 检验输入的老密码是否正确
    /// </summary>
    /// <returns></returns>
    private bool CheckOldPwd(string userCode, string userPwd)
    {
        int count = userBll.CheckUserCodeAndPwd(userCode, userPwd);
        if (count > 0)
            return true;
        return false;
    }
    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userOldPwd"></param>
    /// <param name="userNewPwd"></param>
    /// <returns></returns>
    private int ChangePwd(string userCode, string userOldPwd, string userNewPwd)
    {
        if (this.CheckOldPwd(userCode, userOldPwd))
            return userBll.ChangePwd(userCode, userNewPwd);
        return 0;
    }
}
