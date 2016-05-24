<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>温州才艺印业管理系统</title>
    <link href="css/wz.css" rel="stylesheet" type="text/css">
    <script type="text/javascript">
    function initArray()
    {
        this.length=initArray.arguments.length;
        for(var i=0;i<this.length;i++)
        this[i+1]=initArray.arguments[i];
    }
    
    var d=new initArray("星期日","星期一","星期二","星期三","星期四","星期五","星期六");
    var timerID = null;
    var timerRunning = false;
    
    function stopclock ()
    {
        if(timerRunning)
        clearTimeout(timerID);
        timerRunning = false;
    }

    function startclock ()
    {
        stopclock();
        showtime();
		top.frames.frmLeft.document.location.replace("frmLeft.aspx");
	}
	
	function showtime ()
	{
        var now = new Date();
        var hours = now.getHours();
        var minutes = now.getMinutes();
        var seconds = now.getSeconds()
        var timeValue = "" + (now.getYear() +"年" + (now.getMonth() +1) +"月" + now.getDate() + "日 " + d[now.getDay()+1])
        if(hours < 6){timeValue += " 凌晨 "}
        else if (hours < 9){timeValue += " 早上 "}
        else if (hours < 12){timeValue += " 上午 "}
        else if (hours < 14){timeValue += " 中午 "}
        else if (hours < 17){timeValue += " 下午 "}
        else if (hours < 19){timeValue += " 傍晚 "}
        else if (hours < 22){timeValue += " 晚上 "}
        else {timeValue += " 深夜 "}

        timeValue += ((hours >12) ? hours -12 :hours)
        timeValue += ((minutes < 10) ? ":0" : ":") + minutes
        timeValue += ((seconds < 10) ? ":0" : ":") + seconds
        document.frmAll.thetime.value = timeValue;
        timerID = setTimeout("showtime()",1000);
        timerRunning = true;
    }
    	
	function funSetSubMenuVisible()
	{
		if (window.frmAll.txtIT.checked==true)
		{
			CloseWin=true;
		}
		else
		{
			CloseWin=false;
		}
		
		if (CloseWin==true)
			i=parseInt(parent.framesetMenuAndBody.cols.split(",")[0])-15;		
		else
			i=parseInt(parent.framesetMenuAndBody.cols.split(",")[0])+15;	
			
		parent.framesetMenuAndBody.cols=i+",*";
		
		if (parent.framesetMenuAndBody.cols.split(",")[0]<1){
			clearTimeout(timer);
			parent.framesetMenuAndBody.cols="0,*";
			return true;
		}
		if (parent.framesetMenuAndBody.cols.split(",")[0]>190){
			clearTimeout(timer);
			parent.framesetMenuAndBody.cols="190,*";
			return true;
		}								
		var timer = window.setTimeout("funSetSubMenuVisible()", 5);
		return true;	
	}
	</script>
</head>
<body onload="startclock();" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
    <form id="frmAll" runat="server">
    <table width="100%" height="96" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="36%" align="left" background="images/style-image/oa_02.jpg"><img src="images/style-image/oa_01.jpg" width="478" height="96" /></td>
    <td width="64%" background="images/style-image/oa_02.jpg"><table width="684" height="96" border="0" align="right" cellpadding="0" cellspacing="0">
      <tr>
        <td height="30" background="images/style-image/oa_05.jpg"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="5%"><img src="images/style-image/oa_04.jpg" width="31" height="30" /></td>
            <td width="653"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="4%"><input onclick="funSetSubMenuVisible()" type="checkbox" value="Show menu" name="txtIT" id="Checkbox1" style="color: black" /></td>
                <td width="9%" class="t12white">关闭菜单</td>
                <td width="27%" align="center"><span class="t12white">您好，<asp:Label ID="lblEmpName" runat="server"></asp:Label></span><span class="t12yellow">【<A href="LogOut.aspx" target="_parent">安全退出</A>】</span></td>
                <td width="35%" align="center" class="t12white"><input id="thetime" style="border:0; background-color: transparent; text-align:right;" size="34" /></td>
                <td width="8%" align="center"><A onmouseover="MM_swapImage('backtop','','images/style-image/oa_08.jpg',1)" onclick="top.frames.frmMainBody.history.back();" onmouseout=MM_swapImgRestore()><img src="images/style-image/oa_08.jpg" width="18" height="18" /></A></td>
                <td width="7%" align="center"><A onmouseover="MM_swapImage('backtop2','','images/style-image/oa_10.jpg',1)" onclick="top.frames.frmMainBody.history.forward();" onmouseout=MM_swapImgRestore()><img src="images/style-image/oa_10.jpg" width="18" height="18" /></A></td>
                <td width="10%" class="t12yellow"><A href="OA/SysManage/ChangePwd.aspx" target="frmMainBody">系统设置</A></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="66"><table width="86%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td class="t12white"><A href="Default.aspx" target="frmMainBody"><img src="images/style-image/oa_16.jpg" width="28" height="30" /></A></td>
            <td class="t12white">首 页</td>
            <td class="t12white"><img src="images/style-image/oa_21.jpg" width="34" height="28" /></td>
            <td class="t12white">公司论坛</td>
            <td class="t12white"><img src="images/style-image/oa_23.jpg" width="36" height="28" /></td>
            <td class="t12white">合同管理</td>
            <td class="t12white"><img src="images/style-image/oa_25.jpg" width="29" height="30" /></td>
            <td class="t12white">库存管理</td>
            <td class="t12white"><img src="images/style-image/oa_18.jpg" width="29" height="32" /></td>
            <td class="t12white">财务管理</td>
            <td class="t12white"><img src="images/style-image/oa_28.jpg" width="29" height="29" /></td>
            <td class="t12white">统计分析</td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
</table>
    </form>
</body>
</html>
