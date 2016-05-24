<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="OA_SysManage_ChangePwd" Debug="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改密码</title>
    <link rel="stylesheet" type="text/css" href="../../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../../css/cskt.css" />
    <script type="text/javascript">  
    function CheckForm()
    {
      if(document.form1.OldPwd.value=="")
      {
        alert("旧密码不能为空！");
        return false;
      }
      if(document.form1.NewPwd.value=="")
      {
        alert("新密码不能为空！");
        return false;
      }
      if(document.form1.ConfirmPwd.value=="")
      {
        alert("确认密码不能为空！");
        return false;
      }
      else
      {
        if(document.form1.NewPwd.value!=document.form1.ConfirmPwd.value)
        {
          alert("密码不一致！");
          return false;
        }
      }
      return true;
    }
    function CancelForm() {
        document.getElementById("OldPwd").value = "";
        document.getElementById("NewPwd").value = "";
        document.getElementById("ConfirmPwd").value = "";
    }
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return CheckForm();">
    <table width="100%" height="490" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td colspan="2" valign="top" bgcolor="#e6f2fd"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="15" height="33" background="../../images/style-image/lb_03545.jpg"><img src="../../images/style-image/lb_03545.jpg" width="15" height="33" /></td>
              <td align="left" valign="bottom" background="../../images/style-image/lb_03545.jpg"><table width="44%" border="0" align="left" cellpadding="0" cellspacing="0">
                  <tr>
                    <td width="137">
                    <table width="132" height="26" border="0" align="left" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="center" background="../../images/style-image/lb_05w.jpg" style="font-size:14px; font-weight:bold; color:#FFFFFF;" class="t14Bwhite">个人设置</td>
                      </tr>
                    </table>
                    </td>
                  </tr>
                            </table></td>
              </tr>
          </table>  
          <table width="100%" height="25" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td background="../../images/toolbar/csd.jpg" bgcolor="#D7EBF9">
              <table border="0" align="left" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                      <asp:ImageButton ID="imgBtnSave" runat="server" 
                          ImageUrl="../../images/toolbar/bc.jpg" OnClientClick="return CheckForm();" onclick="imgBtnSave_Click"
                          /></td>
                  <td><asp:ImageButton ID="imgBtnReset" runat="server" 
                          ImageUrl="../../images/toolbar/qx.jpg" OnClientClick="CancelForm();"/></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <br />
          <table width="99%" height="250" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td width="1%" height="28" background="../../images/style-image/lb_03dff.jpg">&nbsp;</td>
              <td width="99%" background="../../images/style-image/lb_03dff.jpg" style="font-size:14px; font-weight:bold; color:#FFFFFF;" class="t14Bwhite">密码修改</td>
            </tr>
          <tr>
            <td height="222" colspan="2" valign="top">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="table3">
              <tr>
                <td width="10%" align="right" bgcolor="#DCEDFF">旧密码：</td>
                  <td width="21%" bgcolor="#DCEDFF"><asp:TextBox ID="OldPwd" runat="server" TextMode="Password"></asp:TextBox><span class="red">*</span></td>
                </tr>
              <tr>
                <td align="right" bgcolor="#DCEDFF">新密码：</td>
                  <td bgcolor="#DCEDFF"><asp:TextBox ID="NewPwd" runat="server" TextMode="Password"></asp:TextBox><span class="red">*</span></td>
                </tr>
              <tr>
                <td align="right" bgcolor="#DCEDFF">确认密码：</td>
                  <td bgcolor="#DCEDFF"><asp:TextBox ID="ConfirmPwd" runat="server" TextMode="Password"></asp:TextBox><span class="red">*</span></td>
                </tr>
              </table>
              </td>
            </tr>
        </table>
        </td>
        </tr>
    </table>
</form>
</body>
</html>
