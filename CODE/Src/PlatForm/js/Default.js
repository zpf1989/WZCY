//打开一个窗口
function showDialog(url,height,width)
{
    var l = (screen.width - width) /2;
    var t = (screen.height - height) /2;
    var newWindow = window.open(url,'newWin','modal=no,width='+width+',height='+height+',top='+t+',left='+l+',resizable=no,scrollbars=yes'); 
    newWindow.focus();
} 

    //全选
      function selectAll()
    {
        var check = document.getElementById("checkAll");
        var checks = document.getElementsByName("check");
        var state = check.checked;
        var length = checks.length;
        for(var i = 0 ; i < length ; i ++)
        {
            if(checks[i].disabled==false)
            {
                checks[i].checked = state;
            }
        }
    }
    
       //删除
     function DeleteInfo()
    {
        var id = 0;
        var checks = document.getElementsByName("check");
        var length = checks.length;
        for(var i = 0 ; i < length ; i ++)
        {
            if(checks[i].checked == true)
            {
                id = checks[i].value;
                break;
            }
        }
        if(id != 0)
        {
            return confirm('将从系统中彻底删除,且删除后不可恢复,确定删除吗?');
        }
        else
        {
            alert("没有选择信息");
            return false;
        }
    } 
    function DeleteInfo2() {
        var id = 0;
        var checks = document.getElementsByName("check2");
        var length = checks.length;
        for (var i = 0; i < length; i++) {
            if (checks[i].checked == true) {
                id = checks[i].value;
                break;
            }
        }
        if (id != 0) {
            return confirm('将从系统中彻底删除,且删除后不可恢复,确定删除吗?');
        }
        else {
            alert("没有选择信息");
            return false;
        }
    }
    function DeleteInfo4Iframe() {
        var id = "";
        var hf = document.getElementById("hfItemId");
        var obj = document.getElementById("myFrame").contentWindow;
        var checks = obj.document.getElementsByName("check");
        var length = checks.length;
        for (var i = 0; i < length; i++) {
            if (checks[i].checked == true) {
                id += "," + checks[i].value;
            }
        }
        if (id != 0) {
            hf.value = id;
            return confirm('确定删除行吗?');
        }
        else {
            alert("没有选择信息");
            return false;
        }
    }  
     //点击修改按钮
     function EditInfo()
    {
        var id = 0;
        var checks = document.getElementsByName("check");
        var length = checks.length;
        var num=0;
        for(var i = 0 ; i < length ; i ++)
        {
            if(checks[i].checked == true)
            {
                id = checks[i].value;
                num++;
            }
        }
        if(num>1)
        {
          alert('只能选择一条信息'); 
          return false; 
        }
        else if(num==0)
        {
            alert("没有选择信息");
             return false; 
        }
        return true;
    } 
    
    //获得内嵌iframe 的连接地址  （.htm文件）
    function ChangeSrc()
    {
      var url=document.getElementById("hid_Src").value;
      document.getElementById("myFrame").src =url; 
      
    }
        //选择所有
    function Check()
    {
  
        var checks = document.getElementsByName("check");
 
        var length = checks.length;
        var n=0;
        
        for(var i=0;i<length;i++)
        {
            if(checks[i].checked)
            {
                n++;
            }
    
        }
          if(n==0)
        {
             alert("请选择一条要修改的信息");
            return false;
        }
        
        if(n>1)
        {
            alert("每次只能修改一条信息");
            return false;
        }
     
        return true;
    }

  
  
