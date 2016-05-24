// JavaScript Document
function change1(a,b)
{
	if(eval(a).style.display=='')
	{
		eval(a).style.display='none';
		eval(b).className='menu3';
	}
	else
	{
		eval(a).style.display='';
		eval(b).className='menu4';
	}
}
function change2(a,b)
{
	if(eval(a).style.display=='')
	{
		eval(a).style.display='none';
		eval(b).className='menu1';
	}
	else
	{
		eval(a).style.display='';
		eval(b).className='menu2';
	}
}
function changeleft1(a,b)
{
	if(eval(a).style.display=='')
	{
		eval(a).style.display='none';
		eval(b).className='menuleft1';
	}
	else
	{
		eval(a).style.display='';
		eval(b).className='menuleft2';
	}
}
