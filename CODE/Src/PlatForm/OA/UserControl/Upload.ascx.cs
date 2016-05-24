using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using OA.Model;
using OA.BLL;
using System.IO;

/*****************************************
 *
 * 作    者：王思源
 * 创建时间：2012年03月04日
 *           
 * 描    述：附件上、修改、下载
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
public partial class OA_UserControl_Upload : System.Web.UI.UserControl
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
                this.tableA.Style["display"] = "none";
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
                    cell.InnerHtml = "<a  href=\"" + phyPath + "?name=" + security.Encrypt(accessories[i].OldFullName, security.se_yaoshi) + "&path=" + security.Encrypt(path, security.se_yaoshi) + "#\"  onclick=\"down(" + accessories[i].Id + ")\" >" + accessories[i].OldFullName + "</a>&nbsp;&nbsp;<a href='javascript:delTableRow(\"" + (fileTable.Rows.Count) + "\",\"" + accessories[i].Id + "\")'><IMG src=\'" + System.Configuration.ConfigurationManager.AppSettings["ApplicationName"].ToString() + "/images/upload-image/icon_del.gif\' border='0' /></a>";

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
    /// <summary>
    /// 附件保存,普通保存
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        if (this.FunCode != "" && this.InfoID != "")
        {
            //首先保存新附件
            int count = Request.Files.Count;
            HttpPostedFile[] files = new HttpPostedFile[count];
            for (int i = 0; i < count; i++)
            {
                //if (i != 0)
                //{
                files[i] = (HttpPostedFile)Request.Files[i];
                // }

            }
            AccessoriesBLL accessory = new AccessoriesBLL();
            accessory.AddAccessories(this.FunCode, this.InfoID, files);

            //删掉需要删除掉的
            string deletedID = this.deleted.Value.ToString();
            string[] deletedIds = deletedID.Split(',');
            foreach (string deletedId in deletedIds)
            {
                if (deletedId != "")
                {
                    accessory.Delete(deletedId);
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 附件保存，用于需要把已有信息的附件指定给当前信息的情况
    /// </summary>
    /// <param name="oldInfoID">已有信息的id</param>
    /// <returns></returns>
    public bool Save(string oldInfoID)
    {
        if (this.FunCode != "" && this.InfoID != "")
        {
            //首先保存新附件
            int count = Request.Files.Count;
            HttpPostedFile[] files = new HttpPostedFile[count];
            for (int i = 0; i < count; i++)
            {
                files[i] = (HttpPostedFile)Request.Files[i];
            }
            AccessoriesBLL accessory = new AccessoriesBLL();
            accessory.AddAccessories(this.FunCode, this.InfoID, files);
            //添加从原来信息继承过来的附件
            accessory.Copy(this.FunCode, oldInfoID, this.InfoID);
            //删掉需要删除掉的
            string deletedID = this.deleted.Value.ToString();
            string[] deletedIds = deletedID.Split(',');
            foreach (string deletedId in deletedIds)
            {
                if (deletedId != "")
                {
                    accessory.Delete(deletedId);
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void Down_OnClick(object sender, EventArgs e)
    {
        try
        {
            string selAccessory = this.selected.Value.ToString();
            AccessoriesBLL accessory = new AccessoriesBLL();
            Accessories _accessory = accessory.Read(selAccessory);
            string path = AccessoriesBLL.accessoryPath.ToString() + "\\" + _accessory.NewName;
            FileInfo file = new FileInfo(path);
            if (System.IO.File.Exists(path))
            {
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                System.Web.HttpContext.Current.Response.ContentType = _accessory.FileType;
                string stroldname = System.Web.HttpUtility.UrlEncode(_accessory.OldFullName);
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + stroldname);
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", _accessory.FileLength.ToString());
                byte[] tmpbyte = new byte[1024 * 1000];
                FileStream fs = file.OpenRead();
                int count;
                while ((count = fs.Read(tmpbyte, 0, tmpbyte.Length)) > 0)
                {
                    System.Web.HttpContext.Current.Response.OutputStream.Write(tmpbyte, 0, count);
                    System.Web.HttpContext.Current.Response.Flush();
                }
                fs.Close();
                System.Web.HttpContext.Current.Response.End();
            }
            else
            {
                //System.Web.HttpContext.Current.Response.Write("");
                lbl_Script.Text = "<script>alert('附件文件不存在，请联系管理员！！');</script>";
                return;
            }
        }
        catch
        {

        }

    }
}
