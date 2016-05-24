using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class OA_UserControl_Down : System.Web.UI.Page
{
    public gentleyh.Class1 security = new gentleyh.Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request["name"] != null && Request["path"] != null)
            {
                //显示名称
                string name = security.Decrypt(Request["name"].ToString(), security.se_yaoshi);
                //文件地址
                string path = security.Decrypt(Request["path"].ToString(), security.se_yaoshi);
                FileInfo file = new FileInfo(path);
                if (System.IO.File.Exists(path))
                {
                    long length = file.Length;
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;  filename=" + System.Web.HttpUtility.UrlEncode(name));
                    Response.AddHeader("Content-Length", length.ToString());
                    byte[] tmpbyte = new byte[1024 * 1000];
                    FileStream fs = file.OpenRead();
                    int count;
                    while ((count = fs.Read(tmpbyte, 0, tmpbyte.Length)) > 0)
                    {
                        Response.OutputStream.Write(tmpbyte, 0, count);
                        Response.Flush();
                    }
                    fs.Close();
                    Response.End();
                }
                else
                {
                    Response.Write("<script>alert('附件文件不存在,或已从服务器上删除！！');</script>");
                    Response.Write("<script>window.history.go(-1);</script>");
                }
            }
        }
        catch (Exception ex)
        {
            string s = ex.Message;
        }
    }
}
