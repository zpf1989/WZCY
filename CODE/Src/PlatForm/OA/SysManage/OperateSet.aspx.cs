using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.BLL;
using System.Data;
using System.Text;
using OA.Model;

public partial class OA_SysManage_OperateSet : PageBase
{
    RoleManageBLL roleBll = new RoleManageBLL();
    FunctionBLL funBll = new FunctionBLL();
    RFRelationBLL relationBll = new RFRelationBLL();
    DataSet ds4Repeater = new DataSet();
    DataSet ds4RPRelation = new DataSet();

    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            InitPage();
        }
    }
    private void InitPage()
    {
        ViewState["Flag"] = false;
        InitRoleList();
        InitRepeater();
        this.btnSave.Attributes.Add("onclick", "funsubmit()");
    }
    /// <summary>
    /// 初始化角色控件
    /// </summary>
    private void InitRoleList()
    {
        ddlRole.Items.Clear();
        ddlRole.Items.Add(new ListItem("--请选择--", "0"));
        DataSet ds = roleBll.GetDataSetOfRole();
        ddlRole.DataSource = ds;
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
    }
    /// <summary>
    /// 初始化Repeater控件
    /// </summary>
    private void InitRepeater()
    {
        ds4Repeater = funBll.GetDataSetOfFun();
        ds4Repeater.Tables[0].DefaultView.RowFilter = "ParentFunID=0";
        Repeater1.DataSource = ds4Repeater.Tables[0].DefaultView;
        Repeater1.DataBind();
    }
    /// <summary>
    /// 角色选择控件时间
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Flag"] = true;
        ds4RPRelation = relationBll.GetDataSetOfRFRelationByRoleID(ddlRole.SelectedValue.Trim());
        InitRepeater();
    }
    /// <summary>
    /// Repeater1数据绑定时对应的事件处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hf = (HiddenField)e.Item.FindControl("HiddenField1");
            string funid = hf.Value;
            string js = GetTreeList(funid);
            Label label = (Label)e.Item.FindControl("Label1");
            label.Text = js;
        }
    }
    /// <summary>
    /// 获得功能树数据
    /// </summary>
    /// <param name="funid"></param>
    /// <returns></returns>
    private string GetTreeList(string funid)
    {
        string treeName = "d" + funid;
        StringBuilder builder = new StringBuilder();
        builder.Append("\r\n<script type='text/javascript'>\r\n");
        builder.Append(treeName + " = new dTree('" + treeName + "');\r\n");
        DataRow[] root = ds4Repeater.Tables[0].Select("FunID=" + funid);
        string okid = string.Empty;
        if ((bool)ViewState["Flag"])
        {
            okid = GetFromMapping(funid);
        }
        builder.Append(treeName + ".add(" + funid + ",-1,'" + root[0]["FunName"].ToString().Trim() + "','','','frmMainBody','" + okid + "');\r\n");
        string format = treeName + ".add({0},{1},'{2}','','','frmMainBody','{3}');\r\n";
        DataRow[] rows = ds4Repeater.Tables[0].Select("ParentFunID=" + funid);
        foreach (DataRow dr in rows)
        {
            if ((bool)ViewState["Flag"])
            {
                okid = GetFromMapping(dr["FunID"].ToString());
            }
            string js = string.Format(format,
                dr["FunID"].ToString(),
                dr["ParentFunID"].ToString(),
                dr["FunName"].ToString().Trim(),
                okid
            );
            builder.Append(js);
            TurnBack(treeName, builder, dr["FunID"].ToString());
        }
        builder.Append("document.write(" + treeName + ");\r\n");
        builder.Append("</script>");
        return builder.ToString();
    }
    private string GetFromMapping(string funid)
    {
        if (ds4RPRelation.Tables.Count == 0)
            return "";
        DataRow[] row = ds4RPRelation.Tables[0].Select("FunID=" + funid);
        if (row.Length > 0)
        {
            return funid;
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// 递归获得功能树数据
    /// </summary>
    /// <param name="treeName"></param>
    /// <param name="builder"></param>
    /// <param name="parentID"></param>
    private void TurnBack(string treeName, StringBuilder builder, string parentID)
    {
        string okid = string.Empty;
        string format = treeName + ".add({0},{1},'{2}','','','frmMainBody','{3}');\r\n";
        DataRow[] rows = ds4Repeater.Tables[0].Select("ParentFunID=" + parentID);
        foreach (DataRow dr in rows)
        {
            if ((bool)ViewState["Flag"])
            {
                okid = GetFromMapping(dr["FunID"].ToString());
            }
            string js = string.Format(format,
                dr["FunID"].ToString(),
                dr["ParentFunID"].ToString(),
                dr["FunName"].ToString().Trim(),
                okid
            );
            builder.Append(js);
            TurnBack(treeName, builder, dr["FunID"].ToString());
        }
    }
    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(FunIDlist.Value))
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('您没选择角色权限！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
            return;
        }
        if (ddlRole.SelectedValue.Trim().Equals("0"))
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('您没选择角色！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
            return;
        }
        string roleID = ddlRole.SelectedValue.Trim();
        string funIDs = FunIDlist.Value.Trim();
        funIDs = funIDs.Remove(funIDs.Length - 1);
        int affect = relationBll.DelByRoleID(roleID);
        string[] arryFunIDs = funIDs.Split(',');
        int affect1 = 0;
        int affectCount = 0;
        foreach (string str in arryFunIDs)
        {
            RFRelation info = new RFRelation();
            info.ID = System.Guid.NewGuid().ToString();
            info.RoleID = roleID;
            info.FunID = Convert.ToInt32(str.Trim());
            affect1 = relationBll.AddRole(info);
            affectCount += affect1 / affect1;
        }
        if (affectCount == arryFunIDs.Length)
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('权限添加成功！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
        }
        else
        {
            StringBuilder JScript = new StringBuilder();
            JScript.Append("<script type='text/javascript'>alert('权限添加失败！');;</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "key", JScript.ToString());
        }
    }
}
