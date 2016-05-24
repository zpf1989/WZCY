<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleCard.aspx.cs" Inherits="OA_SysManage_RoleCard"  Debug="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户组设置</title>
    <link rel="stylesheet" type="text/css" href="../../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../../css/cskt.css" />
    <script type="text/javascript">
    function CheckForm()
    {
        if (document.getElementById("RoleCode").value == "") {
            alert("组编号不能为空!");
            return false;
        }
        if (document.getElementById("RoleName").value == "")
        {
            alert("组名称不能为空!");
            return false;
        }
        return true;
    }
    function CancelForm() {
        document.getElementById("RoleCode").value = "";
        document.getElementById("RoleName").value = "";
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                        <td align="center" background="../../images/style-image/lb_05w.jpg" style="font-size:14px; font-weight:bold; color:#FFFFFF;" class="t14Bwhite">用户组设置</td>
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
              <td width="99%" background="../../images/style-image/lb_03dff.jpg" style="font-size:14px; font-weight:bold; color:#FFFFFF;" class="t14Bwhite">用户组</td>
            </tr>
          <tr>
            <td height="222" colspan="2" valign="top">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="table3">
              <tr>
                <td width="10%" align="right" bgcolor="#DCEDFF">组编号：</td>
                  <td width="21%" bgcolor="#DCEDFF"><asp:TextBox ID="RoleCode" runat="server"></asp:TextBox><span class="red">*</span></td>
                </tr>
              <tr>
                <td align="right" bgcolor="#DCEDFF">组名称：</td>
                  <td bgcolor="#DCEDFF"><asp:TextBox ID="RoleName" runat="server"></asp:TextBox><span class="red">*</span></td>
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
