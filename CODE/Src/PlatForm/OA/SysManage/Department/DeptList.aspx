﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptList.aspx.cs" Inherits="OA_SysManage_Department_DeptList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../../css/easyui/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="../../../css/easyui/themes/icon.css" rel="stylesheet" />
    <script src="../../../js/jquery.min.js"></script>
    <script src="../../../js/jquery.easyui.min.js"></script>
    <script src="../../../js/base.js"></script>
    <script src="../../../js/help.js"></script>
    <script src="../../../css/easyui/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <div class="easyui-layout" fit="true">
        <div region="north" collapsible="true" title="查询条件" height="80px" style="padding: 10px;">
            <form id="searchForm" action="#" style="text-align: left">
                <label>编号：</label><input name="DeptCode" id="DeptCode" class="easyui-textbox" />
                <label>名称：</label><input name="DeptName" id="DeptName" class="easyui-textbox" />
                <a href="javascript:void(0);" id="btnSearch" class="easyui-linkbutton" iconcls="icon-search"
                    style="width: 75px;">查询</a>
            </form>
        </div>
        <div region="center">
            <table id="gridDept" style="width: 100%; height: 100%;"></table>
        </div>
    </div>
    <script src="DeptList.js"></script>
    <script>
        $(dept.init);
    </script>
</body>
</html>
