<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperateSet.aspx.cs" Inherits="OA_SysManage_OperateSet"  Debug="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>权限管理</title>
    <link rel="stylesheet" type="text/css" href="../../CSS/style.css" />
    <link rel="stylesheet" type="text/css" href="../../CSS/cskt.css" />
    <link href="../../CSS/funtree.css" rel="Stylesheet" type="text/css" />

    <script src="../../JS/funtree.js" type="text/javascript"></script>

    <script src="../../JS/fun.js" type="text/javascript"></script>

    <script type="text/javascript">
        function check_node(root, funid) {
            var arr = document.form1.elements;
            for (var i = 0; i < arr.length; i++) {
                if (arr(i).type == "checkbox") {
                    if (arr(i).id.indexOf(funid + "_") == 0) {
                        arr(i).checked = root.checked;
                    }
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="border-color: #e2e2e1; width: 100%; text-align: left; background-color: #ffffff"
                cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td style="width: 100%; height: 81px; vertical-align: top">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="background: url(../../images/style-image/m_mpbg.gif);">
                            <tr>
                                <td class="place">
                                    操作权限设置
                                </td>
                                <td style="width: 20%">
                                </td>
                                <td align="right">
                                    <input class="anybutton" onclick="javascript:window.open('RoleManage.aspx','','fullscreen =no,scrollbars=no,toolbar=no,resizable=no,left=200,top=150')"
                                        type="button" value="设置用户组" name="Submit32" />
                                </td>
                                <td style="width: 3">
                                    <img height="32" src="../../images/style-image/m_mpr.gif" width="3" alt="" />
                                </td>
                            </tr>
                        </table>   
                            <table cellspacing="0" cellpadding="0" style="background: ../../images/style-image/m_mpbg.gif; width: 100%"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td style="height: 34px">
                                            &nbsp;&nbsp; 请选择用户组：<asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" Width="151px" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="保存" CssClass="func" OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <center>
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>                                
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <td valign="top">
                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("FunID") %>' />
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                                    </tr>
                                </table>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td class="page">
                            &nbsp;</td>
                    </tr>
                </tbody>
            </table>
            <asp:HiddenField ID="FunIDlist" runat="server" />
        </div>
    </form>
</body>
</html>
