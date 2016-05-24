<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Upload2.ascx.cs" Inherits="OA_UserControl_Upload2" %>
<table id='tableA' border="0" cellpadding="0" runat="server">
    <tr>
        <td>
            <asp:Label ID="lbl_Script" runat="server" ></asp:Label>
        </td>
    </tr>
</table>
<table border="0"  runat="server" id="fileTable">
</table>
<script language="vbscript" type="text/vbscript">
sub down(sel)
   
    document.all("<% =this.ID.ToString() %>_selected").value = sel
    document.all("<% =this.ID.ToString() %>_down").Click
end sub
</script>
<script language="javascript" type="text/javascript">
    var n=0;                 //初始化数组为0，之后随着化来变化
    var fileCount=1;         //总共输入了多少个有值的控件 File ,初始化为1  
    var tempRow=0;           //动态表格的临时行
    var maxRows=0;           //动态表格的临时列
    var num = 1;             //file 控件数组下标,从1开始,默认显示一个所以那个是 0
    var fileCount=1;         //整个操作中,总共用了多少个 File 控件
    var fileTable = document.getElementById("<% =this.ID.ToString() %>_fileTable");
    function addFile()
    {  
    try
    {
        //fso = new ActiveXObject("Scripting.FileSystemObject");
        var str = '<a href=#?  class="addfile" id="a' + num +'"><input type="file"  class="addfile" size="20" onchange="addFile()" name="uploadFile[' + num + '].file" ' + '/>'; //待插入的文件控件
        var fileText;     //得到文件控件的值        var ary;      //分割文件,以'\'号        var fileTextValue;    //取出最后的文件名 
        fileText = document.all("uploadFile[" + n + "].file").value;            
        var maxSize =10;
        try
        {
            maxSize =parseInt(document.all("<% =this.ID.ToString() %>_hdn_MaxSize").value);
        }
        catch(e)
        {
            maxSize= 10;
        }
        
        ary = fileText.split("\\");                       
        fileTextValue = ary[ary.length-1];
        document.all("a" + n).style.display = "none";  //将前一个 P 的子元素设为不可见         
        //在前面一个 File 控件隐藏后,接着再在原来的位置上插入一个
        document.getElementById('MyFile').insertAdjacentHTML("beforeEnd",str);

        //这里可以灵活处理  
        tempRow=fileTable.rows.length-1;    //fileTable   就是那个动态的   table 的 ID 了
        maxRows=tempRow; 
        tempRow=tempRow+1; 
        var Rows=fileTable.rows;            //Rows 数组 
        var newRow=fileTable.insertRow(fileTable.rows.length);    //插入新的一行 
        var Cells=newRow.cells;                                   //Cells 数组 
        for (i=0;i<3;i++)                                         //每行的2列数据,一列用来显示文件名,一列显示"删除"操作 
        { 
            var newCell=Rows(newRow.rowIndex).insertCell(Cells.length); 
            newCell.align="left"; 
            switch (i)
            { 
                //附件增加后可以直接在本地查看
                case 0 : newCell.innerHTML="<td align='left'><a href='file:///"+fileText+"' target='_blank'><span id='"+n+"' style='font-size:9pt'></span></a>&nbsp;&nbsp;<a href='javascript:delTableFileRow(\"" +tempRow+ "\",\"" + n + "\")'><IMG src=<%=System.Configuration.ConfigurationManager.AppSettings["ApplicationName"].ToString() %>/images/upload-image/icon_del.gif border='0' /></a></td>";break; 
                case 1 : newCell.innerHTML="<td style='width:5%' align='left'></td>"; break; 
                case 2 : newCell.innerHTML="<td style='width:80%' align='left'></td>"; break; 
            } 
        }
        maxRows+=1;
        document.getElementById(n).insertAdjacentText("beforeBegin",fileTextValue);
         
        //if(!rtCheckSize)//检查大小并删除大于10M的文件
        //{
            //delTableFileRow(tempRow,n);
            //alert('你上传的附件超过了'+maxSize+'M大小的限制！');
        //}
        n++;
        num++;
        fileCount++;
        }
       catch(e)
       {
            var vsAlert = "您的IE设置中\n";
            vsAlert += "\"对没有标记为安全的ActiveX控件进行初始化和脚本运行\"\n";
            vsAlert += "项为禁用状态\n"
            vsAlert += "请将该项设置为启用，以便正常上传附件！";
            vsAlert = e.message;
            alert(vsAlert);
       }
    }

  function delTableFileRow(rowNum,fileCount){    
    if (fileTable.rows.length >rowNum){ 
       fileTable.deleteRow(rowNum);    //删除当关行
    }else
     fileTable.deleteRow(fileTable.rows.length-1);
    document.all("MyFile").removeChild(document.all("a" + fileCount)); //从元素P上删除子结点 a 。（跟删除表格行同步）
    fileCount--;     //总数 -1
  } 
  
  //修改附件时删除原有的
  //参数a暂时没有用
  function delTableRow(rowNum,id)
  {
        if (fileTable.rows.length >rowNum)
        { 
            fileTable.deleteRow(rowNum);    //删除当关行
        }
        else
        {
            fileTable.deleteRow(fileTable.rows.length-1);
        }
        //写入删除列表
        document.getElementById("<% =this.ID.ToString() %>_deleted").value += id+",";
   }
  
  function ChangeTableFileRow(rowNum){
  
     list.rows[rowNum].style.background="#ff0000";
  }  
  
  function changeSelect(chkids)
  {
      var objs = document.getElementsByName("input");
      var objC = document.getElementById(chkids).checked;
      document.getElementById(chkids) = !objC;
  }
</script>
<style type="text/css">
 a.addfile {
  background-image:url(<%=System.Configuration.ConfigurationManager.AppSettings["ApplicationName"].ToString() %>/images/upload-image/icon_163.gif);
  background-repeat:no-repeat;  
  display:block;
  float:left;
  height:20px;
  margin-top:-1px;
  position:relative;
  text-decoration:none;
  top:0pt;
  width:80px;
 } 
 input.addfile {
  /*left:-18px;*/
 } 
 input.addfile {
  cursor:pointer !important;
  height:22px;
  left:-10px;
  filter:alpha(opacity=0); 
  position:absolute;
  top:-1px;
  width:90px;
  z-index: -1;
 }
</style>