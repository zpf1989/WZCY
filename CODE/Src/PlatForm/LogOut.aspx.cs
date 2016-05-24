using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.BLL;
using System.Text;

public partial class LogOut : PageBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            Session.Clear();
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>window.close();</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
            Server.Transfer("LogIn.aspx");
        }
    }
}
