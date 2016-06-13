using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.BLL;
using System.Data;

public partial class _Default : PageBase
{
    //NewsBLL newsBll = new NewsBLL();
    //OfficeDocItemBLL odBll = new OfficeDocItemBLL();

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
    private void InitPage()
    {
        InitNews();
        InitOfficeDocDB();
        InitOfficeDocYB();
    }
    /// <summary>
    /// 初始化新闻
    /// </summary>
    private void InitNews()
    {
        //string strNews = string.Empty;

        //DataSet ds = newsBll.GetList();
        //if (ds == null || ds.Tables[0].Rows.Count <= 0)
        //{ 
        //    strNews += "<table> width=\"95%\" height=\"27\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"                                    ";
        //    strNews += "	<tr>                                   ";
        //    strNews += "		<td>                               ";
        //    strNews += "			                               ";
        //    strNews += "		</td>                              ";
        //    strNews += "	</tr>                                  ";
        //    strNews += "</table>                                   ";
        //    //lblNews.Text = strNews;
        //    return;
        //}
        //strNews += "<table width=\"95%\" height=\"27\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" >                                   ";
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    strNews += "	<tr>                                   ";
        //    strNews += "		<td width=\"3%\"><img src=\"images/style-image/oa_58.jpg\" width=\"3\" height=\"7\" /></td>                               ";
        //    if (int.Parse(Convert.ToDateTime(dr["PublishTime"]).ToString("yyyyMMdd")) < int.Parse(System.DateTime.Now.ToString("yyyyMMdd")))
        //        strNews += "	<td width=\"80%\" class=\"t12black\"><a href='OA/News/ShowNews.aspx?newsID=" + Convert.ToString(dr["NewID"]) + "' target='_blank'>" + Convert.ToString(dr["Title"]) + "</a></td>";
        //    else
        //        strNews += "	<td width=\"80%\" class=\"t12black\"><a href='OA/News/ShowNews.aspx?newsID=" + Convert.ToString(dr["NewID"]) + "' target='_blank'>" + Convert.ToString(dr["Title"]) + "<IMG alt='' src='images/style-image/new.gif' border='0'></td>";
        //    strNews += "		<td width=\"17%\" class=\"t12black\">" + Convert.ToDateTime(dr["PublishTime"]).ToString("yyyy-MM-dd") + " </td>                              ";
        //    strNews += "	</tr>                                  ";
        //}
        //strNews += "</table>                                   ";
        //lblNews.Text = strNews;
    }
    /// <summary>
    /// 初始化待办公文
    /// </summary>
    private void InitOfficeDocDB()
    {
        //string strNews = string.Empty;
        //string userId = Convert.ToString(Session["UserID"]);

        //DataSet ds = odBll.GetList(userId,1);
        //if (ds == null || ds.Tables[0].Rows.Count <= 0)
        //{
        //    strNews += "<table width=\"95%\" height=\"27\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">                                   ";
        //    strNews += "	<tr>                                   ";
        //    strNews += "		<td>                               ";
        //    strNews += "			                               ";
        //    strNews += "		</td>                              ";
        //    strNews += "	</tr>                                  ";
        //    strNews += "</table>                                   ";
        //    //lblOfficeDocDB.Text = strNews;
        //    return;
        //}
        //strNews += "<table width=\"95%\" height=\"27\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">                                  ";
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    strNews += "	<tr>                                   ";
        //    strNews += "		<td width=\"3%\"><img src=\"images/oa_58.jpg\" width=\"3\" height=\"7\" /></td>                             ";
        //    strNews += "		<td width=\"80%\" class=\"t12black\"><a href='OA/WorkFlow/ShowOfficeDoc.aspx?OfficeDocItemID=" + Convert.ToString(dr["OfficeDocItemID"]) + "' target='_blank'>" + Convert.ToString(dr["Title"]) + "</a></td>";
        //    if (string.IsNullOrEmpty(dr["OperateTime"].ToString()))
        //        strNews += "		<td width=\"17%\" class=\"t12black\"> </td>                         ";
        //    else
        //        strNews += "		<td width=\"17%\" class=\"t12black\">" + Convert.ToDateTime(dr["OperateTime"]).ToString("yyyy-MM-dd") + " </td>                         ";
        //    strNews += "	</tr>                                  ";
        //}
        //strNews += "</table>                                   ";
        //lblOfficeDocDB.Text = strNews;
    }
    /// <summary>
    /// 初始化已办公文
    /// </summary>
    private void InitOfficeDocYB()
    {
        //string strNews = string.Empty;
        //string userId = Convert.ToString(Session["UserID"]);

        //DataSet ds = odBll.GetList(userId, 2);
        //if (ds == null || ds.Tables[0].Rows.Count <= 0)
        //{
        //    strNews += "<table width=\"95%\" height=\"27\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">                                       ";
        //    strNews += "	<tr>                                   ";
        //    strNews += "		<td>                               ";
        //    strNews += "			                               ";
        //    strNews += "		</td>                              ";
        //    strNews += "	</tr>                                  ";
        //    strNews += "</table>                                   ";
        //    //lblOfficeDocYB.Text = strNews;
        //    return;
        //}
        //strNews += "<table width=\"95%\" height=\"27\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">                        ";
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    strNews += "	<tr>                                   ";
        //    strNews += "		<td width=\"3%\"><img src=\"images/oa_58.jpg\" width=\"3\" height=\"7\" /></td>                                  ";
        //    strNews += "		<td width=\"80%\" class=\"t12black\"><a href='OA/WorkFlow/ShowOfficeDocYB.aspx?OfficeDocItemID=" + Convert.ToString(dr["OfficeDocItemID"]) + "' target='_blank'>" + Convert.ToString(dr["Title"]) + "</a></td>";
        //    if (string.IsNullOrEmpty(dr["OperateTime"].ToString()))
        //        strNews += "		<td width=\"17%\" class=\"t12black\"> </td>                ";
        //    else
        //        strNews += "		<td width=\"17%\" class=\"t12black\">" + Convert.ToDateTime(dr["OperateTime"]).ToString("yyyy-MM-dd") + " </td>                ";
        //    strNews += "	</tr>                                  ";
        //}
        //strNews += "</table>                                   ";
        //lblOfficeDocYB.Text = strNews;
    }
}
