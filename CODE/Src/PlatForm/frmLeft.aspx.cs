using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using OA.BLL;
using OA.Model;

public partial class frmLeft : PageBase
{
    FunctionBLL funBLL = new FunctionBLL();
    RFRelationBLL rfRelationBll = new RFRelationBLL();

    /// <summary>
    /// 实例化StringBuilder
    /// </summary>
    private StringBuilder builder = new StringBuilder();

    /// <summary>
    /// 实例化DataSet
    /// </summary>
    private DataSet ds = new DataSet();

    /// <summary>
    /// 实例化DataSet
    /// </summary>
    private DataSet ds1 = new DataSet();

    /// <summary>
    /// 实例化List<FunctionInfo>
    /// </summary>
    private List<FunctionInfo> list = new List<FunctionInfo>();

    protected override void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            LoadData();
        }
    }

    /// <summary>
    /// 加载功能树
    /// </summary>
    public void LoadData()
    {
        ds = funBLL.GetDataSetOfFun();
        list = rfRelationBll.GetFunList(Session["RoleID"].ToString());
        DataRow[] rows = ds.Tables[0].Select("ParentFunID=0");

        builder.Append("<div class='dtree'>\r\n");
        builder.Append("<a href='javascript: d.openAll();'><span>展开</span></a><span style='font-size: 10pt; font-family: 宋体'> | </span><a href='javascript: d.closeAll();'>关闭</a>\r\n");
        builder.Append("<script type='text/javascript'>\r\n");
        builder.Append("d = new dTree('d');\r\n");
        builder.Append("d.add(0,-1,'OA企业自动化办公平台');\r\n");
        string format = "d.add({0},{1},'{2}','{3}','','frmMainBody');\r\n";
        foreach (DataRow dr in rows)
        {
            string js = string.Format(format, dr["FunID"].ToString(), dr["ParentFunID"].ToString(),
                dr["FunName"].ToString(), dr["FunURL"].ToString());
            builder.Append(js);
            TurnBack(dr["FunID"].ToString());
        }
        builder.Append("document.write(d);\r\n");
        builder.Append("d.closeAll();\r\n");
        builder.Append("</script>\r\n");
        builder.Append("</div>\r\n");
        ClientScript.RegisterStartupScript(this.GetType(), "KEY", builder.ToString());
    }

    /// <summary>
    /// 递归加载功能数据
    /// </summary>
    /// <param name="parentid"></param>
    /// <param name="builder"></param>
    private void TurnBack(string parentid)
    {
        //switch (parentid)
        //{
        //    case "7":
        //        InitArea(parentid);
        //        break;
        //    case "8":
        //        InitWay(parentid);
        //        break;
        //    default:
        //        break;
        //}
        DataRow[] rows = ds.Tables[0].Select("ParentFunID=" + parentid);
        string format = "d.add({0},{1},'{2}','{3}','','frmMainBody');\r\n";
        foreach (DataRow dr in rows)
        {
            foreach (FunctionInfo info in list)
            {
                if (info.FunID.ToString().Equals(dr["FunID"].ToString()))
                {
                    string js = string.Format(format, info.FunID.ToString(), info.ParentFunID.ToString(),
                        info.FunName.Trim(), info.FunURL);
                    builder.Append(js);
                    TurnBack(info.FunID.ToString());
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 加载区域数据
    /// </summary>
    /// <param name="parentid"></param>
    //private void InitArea(string parentid)
    //{
    //    ds1 = area.GetAreaDataSet();
    //    DataRow[] rows = ds1.Tables[0].Select("ParentID=0");
    //    string format = "d.add({0},{1},'{2}','','','frmMainBody');\r\n";
    //    foreach (DataRow dr in rows)
    //    {
    //        string js = string.Format(format, dr["AreaID"].ToString(), parentid, dr["AreaName"].ToString());
    //        builder.Append(js);
    //        TurnBackArea(dr["AreaID"].ToString());
    //    }
    //}

    /// <summary>
    /// 递归加载区域数据
    /// </summary>
    /// <param name="parentid"></param>
    //private void TurnBackArea(string parentid)
    //{
    //    DataRow[] rows = ds1.Tables[0].Select("ParentID=" + parentid);
    //    string format = "d.add({0},{1},'{2}','Customer/CustomerListByTerm.aspx?Key=区域&AreaID={3}&AreaName={4}','','frmMainBody');\r\n";
    //    foreach (DataRow dr in rows)
    //    {
    //        string js = string.Format(format, dr["AreaID"].ToString(), parentid, dr["AreaName"].ToString(), dr["AreaID"].ToString(), dr["AreaName"].ToString());
    //        builder.Append(js);
    //        TurnBackArea(dr["AreaID"].ToString());
    //    }
    //}

    /// <summary>
    /// 加载行业数据
    /// </summary>
    /// <param name="parentid"></param>
    //private void InitWay(string parentid)
    //{
    //    DataSet ds = way.GetWayDataSet();
    //    string format = "d.add({0},{1},'{2}','Customer/CustomerListByTerm.aspx?Key=行业&WayID={3}&WayName={4}','','frmMainBody');\r\n";
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        string js = string.Format(format, dr["WayID"].ToString(),
    //                parentid, dr["WayName"].ToString(), dr["WayID"].ToString(), dr["WayName"].ToString());
    //        builder.Append(js);
    //    }
    //}
}
