<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" Debug="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD>
        <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
        <link href="css/wz.css" rel="stylesheet" type="text/css">
		<title>首页</title>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="5">
		<form id="frmDesktop" method="post" runat="server">
			<table width="100%" height="490" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="50%" height="245" bgcolor="#e6f2fd"><table width="90%" height="200" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="36" background="images/style-image/oa_43err.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="48%"><table width="160" border="0" align="left" cellpadding="0" cellspacing="0">
                  <tr>
                    <td width="8"><img src="images/style-image/oa_40.jpg" width="8" height="36" /></td>
                    <td width="52"><img src="images/style-image/oa_41.jpg" width="52" height="36" /></td>
                    <td width="100" align="center" valign="bottom" class="t14Bblue"><table width="85%" height="30" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td class="t14Bblue">新闻摘要</td>
                      </tr>
                    </table></td>
                  </tr>
                </table></td>
                <td width="52%" align="right"><table width="66" border="0" align="right" cellpadding="0" cellspacing="0">
                  <tr>
                    <td width="58" class="t12black">丨&gt; 更多</td>
                    <td width="8"><img src="images/style-image/oa_43.jpg" width="8" height="36" /></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td height="164"><table width="100%" height="180" border="0" cellpadding="0" cellspacing="0" class="table3">
              <tr>
                <td valign="top"><table width="96%" height="170" border="0" align="center" cellpadding="0" cellspacing="0" class="table4">
                  <tr>
                    <td valign="top">
                        <asp:Label ID="lblNews" runat="server"></asp:Label>
                    </td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
        <td width="50%" bgcolor="#e6f2fd"><table width="90%" height="200" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="36" background="images/style-image/oa_43err.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="48%"><table width="160" border="0" align="left" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="8"><img src="images/style-image/oa_40.jpg" width="8" height="36" /></td>
                        <td width="52"><img src="images/style-image/oa_41rt.jpg" width="52" height="36" /></td>
                        <td width="100" align="center" valign="bottom" class="t14Bblue"><table width="85%" height="30" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="t14Bblue">已办公文</td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                  <td width="52%" align="right"><table width="66" border="0" align="right" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="58" class="t12black">丨&gt; 更多</td>
                        <td width="8"><img src="images/style-image/oa_43.jpg" width="8" height="36" /></td>
                      </tr>
                  </table></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td height="164"><table width="100%" height="180" border="0" cellpadding="0" cellspacing="0" class="table3">
                <tr>
                  <td valign="top"><table width="96%" height="170" border="0" align="center" cellpadding="0" cellspacing="0" class="table4">
                      <tr>
                        <td valign="top">
                            <asp:Label ID="lblOfficeDocYB" runat="server"></asp:Label>
                        </td>
                      </tr>
                  </table></td>
                </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="240" bgcolor="#e6f2fd"><table width="90%" height="200" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="36" background="images/style-image/oa_43err.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="48%"><table width="160" border="0" align="left" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="8"><img src="images/style-image/oa_40.jpg" width="8" height="36" /></td>
                        <td width="52"><img src="images/style-image/oa_41hg.jpg" width="52" height="36" /></td>
                        <td width="100" align="center" valign="bottom" class="t14Bblue"><table width="85%" height="30" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="t14Bblue">待办公文</td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                  <td width="52%" align="right"><table width="66" border="0" align="right" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="58" class="t12black">丨&gt; 更多</td>
                        <td width="8"><img src="images/style-image/oa_43.jpg" width="8" height="36" /></td>
                      </tr>
                  </table></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td height="164"><table width="100%" height="180" border="0" cellpadding="0" cellspacing="0" class="table3">
                <tr>
                  <td valign="top"><table width="96%" height="170" border="0" align="center" cellpadding="0" cellspacing="0" class="table4">
                      <tr>
                        <td valign="top">
                            <asp:Label ID="lblOfficeDocDB" runat="server"></asp:Label>
                        </td>
                      </tr>
                  </table></td>
                </tr>
            </table></td>
          </tr>
        </table></td>
        <td height="240" bgcolor="#e6f2fd"><table width="90%" height="200" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="36" background="images/style-image/oa_43err.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="48%"><table width="160" border="0" align="left" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="8"><img src="images/style-image/oa_40.jpg" width="8" height="36" /></td>
                        <td width="52"><img src="images/style-image/oa_41io.jpg" width="52" height="36" /></td>
                        <td width="100" align="center" valign="bottom" class="t14Bblue"><table width="85%" height="30" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="t14Bblue">公司论坛</td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                  <td width="52%" align="right"><table width="66" border="0" align="right" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="58" class="t12black">丨&gt; 更多</td>
                        <td width="8"><img src="images/style-image/oa_43.jpg" width="8" height="36" /></td>
                      </tr>
                  </table></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td height="164"><table width="100%" height="180" border="0" cellpadding="0" cellspacing="0" class="table3">
                <tr>
                  <td valign="top"><table width="96%" height="170" border="0" align="center" cellpadding="0" cellspacing="0" class="table4">
                      <tr>
                        <td valign="top"><table width="95%" height="27" border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td width="3%"><img src="images/style-image/oa_58.jpg" width="3" height="7" /></td>
                            <td width="80%" class="t12black">全省政府采购工作座谈会圆满结束</td>
                            <td width="17%" class="t12black">2013-07-12 </td>
                          </tr>
                        </table></td>
                      </tr>
                  </table></td>
                </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
    </table>
		</form>
	</body>
</HTML>
