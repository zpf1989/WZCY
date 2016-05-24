// JScript 文件
function check_all(menu_all,MENU_ID)
{
  for (i=0;i<document.all(MENU_ID).length;i++)
  {
    if(menu_all.checked)
    document.all(MENU_ID).item(i).checked=true;
    else
    document.all(MENU_ID).item(i).checked=false;
  }
//  if(i==0)
//  {
//    if(menu_all.checked)
//    document.all(MENU_ID).checked=true;
//    else
//    document.all(MENU_ID).checked=false;
//  }
}

var MENU_ID_ARRAY = new Array();

MENU_ID_ARRAY[0]="1";
MENU_ID_ARRAY[1]="2";
MENU_ID_ARRAY[2]="4";
MENU_ID_ARRAY[3]="85";
MENU_ID_ARRAY[4]="3";
//MENU_ID_ARRAY[5]="70";
//MENU_ID_ARRAY[6]="76";
//MENU_ID_ARRAY[7]="85";

function funsubmit()
{
  func_id_str="";
  
  for(j=1;j<=8;j++)
  {
    var flag = false;
    menu_id=MENU_ID_ARRAY[j-1]+'';
    if(!document.all(menu_id))
    continue;
    
    for(i=0;i<document.all(menu_id).length;i++)
    {
      el=document.all(menu_id).item(i);
      if(el.checked)
      {
        flag = true;
        val=el.value;
        func_id_str+=val + ",";
      }
    }
    if(flag)
    {
        func_id_str += menu_id + ",";
    }
    
//    if(i==0)
//    {
//      el=document.all(menu_id);
//      if(el.checked)
//      {
//        val=el.value;
//        func_id_str+=val + ",";
//      }
//    }
  }      
  document.all.FunIDlist.value=func_id_str;
}

