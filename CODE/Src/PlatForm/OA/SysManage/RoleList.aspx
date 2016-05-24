<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleList.aspx.cs" Inherits="OA_SysManage_RoleList" Debug="true" Theme="1"%>
<%@ Register Assembly="Gentle.WebControlLibrary" Namespace="Gentle.WebControlLibrary" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户组管理</title>
    <link rel="stylesheet" type="text/css" href="../../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../../css/cskt.css" />
    <script type="text/javascript" src="../../js/Mouse.js"></script>
    <script type="text/javascript" src="../../js/Default.js"></script>
    <script type="text/javascript">
        function CheckForm() {
            if (document.getElementById("RoleCode").value == "") {
                alert("编号不能为空!");
                return false;
            }
            if (document.getElementById("RoleName").value == "") {
                alert("名称不能为空!");
                return false;
            }
            return true;
        }
    </script>
</head>
<body onunload="myclose()">
    <form id="form1" runat="server">
        <table width="100%" height="490" border="0" cellpadding="0" cellspacing="0" id="List" runat="server">
            <tr>
                <td colspan="2" valign="top" bgcolor="#e6f2fd">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="15" height="33" background="../../images/style-image/lb_03545.jpg"><img src="../../images/style-image/lb_03545.jpg" width="15" height="33" /></td>
              <td align="left" valign="bottom" background="../../images/style-image/lb_03545.jpg"><table width="44%" border="0" align="left" cellpadding="0" cellspacing="0">
                  <tr>
                    <td width="137">
                    <table width="132" height="26" border="0" align="left" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="center" background="../../images/style-image/lb_05w.jpg" style="font-size:14px; font-weight:bold; color:#FFFFFF;" class="t14Bwhite">用户组管理</td>
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
                      <asp:ImageButton ID="imgBtnRefresh" runat="server" 
                          ImageUrl="../../images/toolbar/sx.jpg"/></td>
                  <td>
                      <asp:ImageButton ID="imgBtnDel" runat="server" 
                          ImageUrl="../../images/toolbar/sc.jpg" OnClientClick="return DeleteInfo();" onclick="imgBtnDel_Click" /></td>
                  <td><asp:ImageButton ID="imgBtnEdit" runat="server"  OnClientClick="return EditInfo()"
                          ImageUrl="../../images/toolbar/xg.jpg" onclick="imgBtnEdit_Click"/></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <table width="100%" height="25" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td bgcolor="#D3EAFA"><table border="0" align="left" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="11">&nbsp;</td>
                  <td width="65" class="t12blue">用户组编号:</td>
                  <td>
                      <asp:TextBox ID="txtSearchRoleCode" runat="server"></asp:TextBox></td>
                  <td width="65" class="t12blue">用户组名称:</td>
                  <td><asp:TextBox ID="txtSearchRoleName" runat="server"></asp:TextBox></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td width="1%" height="28" background="../../images/style-image/lb_03dff.jpg">&nbsp;</td>
              <td width="99%" background="../../images/style-image/lb_03dff.jpg" style="font-size:14px; font-weight:bold; color:#FFFFFF;" class="t14Bwhite">用户信息列表</td>
            </tr>
          <tr>
            <td width="100%" colspan="2" valign="top">
                <asp:CustomGridView ID="cgv_List" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="99%"
                                        CustomPagingStyle="NumericNextPrevioudFirstLastGo" DataSourceID="ods_ListDataSource" 
                                        PagingStyle="Default" OnRowDataBound="cgv_List_RowDataBound" OnRowCommand="cgv_List_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <input id="checkAll" onclick="selectAll()" type="checkbox" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center"/>
                                                <ItemTemplate>
                                                    <input id="check" name="check" type="checkbox" value='<%# security.Encrypt(Eval("RoleID").ToString(),security.se_yaoshi)%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_RoleID" runat="server" Text='<%# Eval("RoleID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>用户组编号</HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center"/>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnRoleCode" runat="server" CommandName="View" CommandArgument='<%# Eval("RoleID") %>'><%# Eval("RoleCode")%></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>用户组名称</HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center"/>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_RoleName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"/>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
                                        <EmptyDataTemplate>
                                            暂无内容！
                                        </EmptyDataTemplate>
                                    </asp:CustomGridView>
              </td>
          </tr>
            <tr>
                <td align=center width="100%" colspan="2">
                        <asp:ObjectDataSource ID="ods_ListDataSource" runat="server" CacheDuration="0" EnablePaging="true"
                                        MaximumRowsParameterName="pageSize" SelectCountMethod="GetRowCounts" SelectMethod="GetPageList"
                                        TypeName="OA.BLL.RoleManageBLL">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtSearchRoleCode" Name="RoleCode" Type="string" />
                                            <asp:ControlParameter ControlID="txtSearchRoleName" Name="RoleName" Type="string" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table></td>
            </tr>
        </table>
        <table width="100%" height="490" border="0" cellpadding="0" cellspacing="0" id="Card" runat="server">
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
                          ImageUrl="../../images/toolbar/bc.jpg" OnClientClick="return CheckForm();"
                          onclick="imgBtnSave_Click"/></td>
                  <td><asp:ImageButton ID="imgBtnReset" runat="server" 
                          ImageUrl="../../images/toolbar/qx.jpg" onclick="imgBtnReset_Click"/></td>
                </tr>
              </table>
              </td>
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
        </table></td></tr>
        </table>
    </form>
</body>
</html>
