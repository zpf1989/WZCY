using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.BLL;
using OA.Model;

public partial class LogIn : System.Web.UI.Page
{
    UserInfoBLL userBll = new UserInfoBLL();
    URrelationBLL urRelationBll = new URrelationBLL();
    public gentleyh.Class1 security = new gentleyh.Class1();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        //string currentTime = DateTime.Now.ToShortDateString();
        //int currentYear = DateTime.Now.Year;
        //int currentMonth = DateTime.Now.Month;
        //int currentDay = DateTime.Now.Day;
        //if (currentYear == 2011 && currentMonth == 4 && currentDay > 1)
        //{
        //    Response.Write("<script>alert('软件的使用期已到，如有问题请联系系统管理员！');window.top.close();</script>");
        //    return;
        //}
        GetModelByCode();
    }

    /// <summary>
    /// 检查用户是否存在
    /// </summary>
    private void GetModelByCode()
    {
        if (string.IsNullOrEmpty(txtID.Value.Trim()))
        {
            Response.Write("<script>alert('请输入用户名！')</script>");
            return;
        }
        if (string.IsNullOrEmpty(txtPwd.Value.Trim()))
        {
            Response.Write("<script>alert('请输入密码！')</script>");
            return;
        }
        if (userBll.CheckUserCode(txtID.Value.Trim()) > 0)
        {
            //PreCheckLock();
            this.GetModelByCodeAndPwd();
        }
        else
        {
            Response.Write("<script>alert('账号错误！')</script>");
        }
    }
    /// <summary>
    /// 检查密码是否正确
    /// </summary>
    private void GetModelByCodeAndPwd()
    {
        string userCode = txtID.Value.Trim();
        string userPwd = security.Encrypt(txtPwd.Value.Trim(), security.se_yaoshi);

        if (userBll.CheckUserCodeAndPwd(userCode, userPwd) > 0)
        {
            UserInfo info = userBll.GetModelOfUserByUserCodeAndPwd(userCode, userPwd);
            Session["UserID"] = info.UserID;
            Session["UserCode"] = info.UserCode;
            Session["UserName"] = info.UserName;
            Session["UserPwd"] = info.UserPwd;
            URrelation relation = urRelationBll.GetModelOfURrelationByUserID(info.UserID.Trim());
            if (relation != null)
            {
                Session["RoleID"] = relation.RoleID;
            }
            //if (info1.RoleCode == 4 || info1.RoleCode == 6)
            //{
            //    Session["SaleHallCode"] = Convert.ToString(info1.SaleHallCode);
            //}
            //else
            //{
            //    Session["SaleHallCode"] = 0;
            //}
            Response.Redirect("frmMain.htm");
        }
        else
        {
            //连续输入错误密码时
            //if (Session["User"] == null)
            //{
            //    Session["User"] = txtUserName.Text;
            //}
            //else
            //{
            //    if (Session["User"].ToString() == txtUserName.Text)
            //    {
            //        int Lock = Convert.ToInt32(Session["LoginCount"].ToString());
            //        Lock += 1;
            //        if (Lock == 3)
            //        {
            //            int affect = lockBLL.ModifyLockCodeByUserName(txtUserName.Text);
            //            Response.Write(CommonClass.MessageBox("你已经连续输入三次错误密码!"));
            //            Session["LoginCount"] = "1";
            //            return;
            //        }

            //        Session["LoginCount"] = Convert.ToString(Lock);
            //    }
            //    else
            //    {
            //        Session["User"] = txtUserName.Text;
            //        Session["LoginCount"] = "1";
            //    }
            //}
            Response.Write("<script>alert('你输入的秘密错误！')</script>");
        }
    }
}
