using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OA.Model;
using OA.BLL;
using System.IO;
using System.Web.UI.HtmlControls;

/*****************************************
 *
 * 作    者：王思源
 * 创建时间：2012年03月04日
 *           
 * 描    述：附件上、修改、下载(主要用于下载页面)
 * 调用说明：
 * 页面的form需增加 enctype="multipart/form-data"
 * FunCode是添加附件的模块编码字符串
 * infoid是添加附件的信息流水号
 * readonly用于附件下载，设置为true即是下载功能，默认为false
 * 把控件拖动到页面的指定位置，不要更改其属性，尤其是id
 * 附件增加时在新增按钮事件中增加：this.Upload1.FunCode = typeof(使用类名).ToString();
                                   this.Upload1.InfoID = int.Parse(信息id);
                                   this.Upload1.Save();
 * 附件修改时在页面加载时添加：this.Upload1.FunCode = typeof(使用类名).ToString();
                               this.Upload1.InfoID = int.Parse(信息id);
                               this.Upload1.load();
 *             保存按钮时间中：this.Upload1.FunCode = typeof(使用类名).ToString();
                               this.Upload1.InfoID = int.Parse(信息id);
                               this.Upload1.save();
 * 附件下载时在页面加载时添加：this.Upload1.FunCode = typeof(使用类名).ToString();
                               this.Upload1.InfoID = int.Parse(信息id);
                               this.Upload1.ReadOnly = true;
                               this.Upload1.load();
*****************************************/
public partial class OA_UserControl_Upload2 : System.Web.UI.UserControl
{
    private gentleyh.Class1 security = new gentleyh.Class1();
    private string _funCode;
    /// <summary>
    /// 模块（功能编码）
    /// </summary>
    public string FunCode
    {
        get
        {
            return _funCode;
        }
        set
        {
            _funCode = value;
        }
    }
    /// <summary>
    /// 信息id
    /// </summary>
    private string _infoID;
    public string InfoID
    {
        get
        {
            return _infoID;
        }
        set
        {
            _infoID = value;
        }
    }
    /// <summary>
    /// 控件状态，true为下载状态，false为增加状态
    /// </summary>
    private bool _readOnly = false;
    public bool ReadOnly
    {
        get
        {
            return _readOnly;
        }
        set
        {
            _readOnly = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Script.Text = "";
        //this.Load();
    }
    private void RenderPage()
    {

    }
    /// <summary>
    /// 附件加载
    /// </summary>
    public void Load()
    {
        if (this.FunCode != "" && this.InfoID != "")
        {
            if (this.ReadOnly)
            {
                string appName = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"];
                fileTable.Rows.Clear();
                appName = appName.TrimEnd('/') + "/OA/UserControl/Down.aspx";
                //this.tableA.Style["display"] = "none";
                //绑定数据并提供下载
                HtmlTableRow row = null;
                HtmlTableCell cell = null;
                AccessoriesBLL accessory = new AccessoriesBLL();
                Accessories[] accessories = accessory.GetAccessories(this.FunCode, this.InfoID);
                int count = accessories.Length;

                for (int i = 0; i < count; i++)
                {
                    string path = AccessoriesBLL.accessoryPath.ToString() + "\\" + accessories[i].NewName;
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell("td");
                    //cell.InnerHtml = "<a href=\"#\"  onclick=\"down(" + accessories[i].ID + ")\" >" + accessories[i].OldFullName + "</a>&nbsp;&nbsp;</td>";
                    cell.InnerHtml = "<a  href=\"" + appName + "?name=" + security.Encrypt(accessories[i].OldFullName, security.se_yaoshi) + "&path=" + security.Encrypt(path, security.se_yaoshi) + "#\"  onclick=\"down(" + accessories[i].Id + ")\" >" + accessories[i].OldFullName + "</a>";
                    string[] args = accessories[i].NewFullName.Split('.');
                    if (args[1].ToString().ToLower() == "pdf")
                    {
                        string tpath = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + "/Source/modules/workflow/PdfFolder/" + accessories[i].NewFullName;

                        cell.InnerHtml += "<a href='" + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + "/Source/Modules/FileExchange/File/PDFShow.aspx?tpath=" + security.Encrypt(tpath, security.se_yaoshi) + "' target='_blank' >&nbsp;&nbsp;预览</a>";
                    }
                    //  cell.InnerHtml += "</td>";
                    row.Cells.Add(cell);
                    fileTable.Rows.Add(row);

                }
            }
            else
            {
                fileTable.Rows.Clear();
                string appName = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"];
                string phyPath = appName.TrimEnd('/') + "/OA/UserControl/Down.aspx";
                //加载已经上传的附件
                HtmlTableRow row = null;
                HtmlTableCell cell = null;
                AccessoriesBLL accessory = new AccessoriesBLL();
                Accessories[] accessories = accessory.GetAccessories(this.FunCode, this.InfoID);
                int count = accessories.Length;
                for (int i = 0; i < count; i++)
                {
                    string path = AccessoriesBLL.accessoryPath.ToString() + "\\" + accessories[i].NewName;
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell("td");
                    cell.Align = "left";
                    //cell.InnerHtml = "<a href=\"#\"  onclick=\"down(" + accessories[i].ID + ")\" >" + accessories[i].OldFullName + "</a>&nbsp;&nbsp;<a href='javascript:delTableRow(\"" + (fileTable.Rows.Count) + "\",\"" + accessories[i].ID + "\")'><IMG class=\'uploaddeleteicon\' border='0' /></a></td>";
                    cell.InnerHtml = "<a  href=\"" + phyPath + "?name=" + security.Encrypt(accessories[i].OldFullName, security.se_yaoshi) + "&path=" + security.Encrypt(path, security.se_yaoshi) + "#\"  onclick=\"down(" + accessories[i].Id + ")\" >" + accessories[i].OldFullName + "</a>&nbsp;&nbsp;<a href='javascript:delTableRow(\"" + (fileTable.Rows.Count) + "\",\"" + accessories[i].Id + "\")'></a>";

                    string[] args = accessories[i].NewFullName.Split('.');
                    if (args[1].ToString().ToLower() == "pdf")
                    {
                        string tpath = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + "/Source/modules/workflow/PdfFolder/" + accessories[i].NewFullName;
                        cell.InnerHtml += "<a href='" + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"] + "/Source/Modules/FileExchange/File/PDFShow.aspx?tpath=" + security.Encrypt(tpath, security.se_yaoshi) + "\\" + "' target='_blank' >&nbsp;&nbsp;预览</a>";
                    }
                    // cell.InnerHtml += "</td>";

                    row.Cells.Add(cell);
                    fileTable.Rows.Add(row);
                }
            }
        }
    }
}
