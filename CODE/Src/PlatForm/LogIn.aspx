<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>登录界面</title>
<link rel="stylesheet" type="text/css" href="CSS/wz.css" />
<style>
/*body{ margin-top:20; padding:0; font-size:12px;}*/

</style>
</head>

<body scroll="auto" style="background-color: #0e69bb">
<form id="login"  runat="server">
<table width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td><img src="images/login-image/dl_04.jpg" width="1000" height="250"></td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="570" height="147" background="images/login-image/dl_04-03.jpg"><table width="570" height="147" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td width="291">&nbsp;</td>
            <td width="279"><table width="91%" height="95" border="0" align="left" cellpadding="0" cellspacing="0">
              <tr>
                <td width="26%" align="center" class="t14Bwhite">用户名：</td>
                <td width="74%"><input name="txtUserName" type="text" id="txtID" runat="server" size="23"/></td>
              </tr>
              <tr>
                <td align="center" class="t14Bwhite">密&nbsp;&nbsp;&nbsp;&nbsp;码：</td>
                <td><input name="txtPwd" type="password" id="txtPwd" runat="server" size="23"/></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
        <td><img src="images/login-image/dl_05.jpg" width="12" height="147"></td>
        <td width="418" height="147" background="images/login-image/dl_06.jpg"><table width="408" height="128" border="0" align="right" cellpadding="0" cellspacing="0">
          <tr>
            <td width="143" height="30">&nbsp;</td>
            <td width="265" rowspan="4">&nbsp;</td>
          </tr>
          <tr>
            <td height="49">
                <asp:ImageButton ID="btnOK" runat="server" ImageUrl="images/login-image/dl_1034.jpg" width="119" height="37" onclick="btnOK_Click"/>
                                    </td>
          </tr>
          <tr>
            <td height="22"><img src="images/login-image/dl_14df.jpg" width="16" height="15"> <span class="t12white">忘记密码？</span></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><img src="images/login-image/dl_07.jpg" width="1000" height="240"></td>
  </tr>
</table>
</form>

</body>
</html>
