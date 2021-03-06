﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Materials.aspx.cs" Inherits="OA_InventoryManage_Materials_Materials" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../../css/easyui/themes/bootstrap/easyui.css" rel="stylesheet" />
    <link href="../../../css/easyui/themes/icon.css" rel="stylesheet" />
    <link href="../../../css/base.css" rel="stylesheet" />
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
                <div>
                    <label>物料编号：</label><input name="MaterialCode" id="MaterialCode" class="easyui-textbox" />
                </div>
                <div>
                    <label>物料名称：</label><input name="MaterialName" id="MaterialName" class="easyui-textbox" />
                </div>
                <div>
                    <label>物料分类：</label>
                    <input name="MaterialClassID" id="txtSearchClassID" hidden="hidden" />
                    <input name="MaterialClassName" id="txtSearchClassName"/>
                    <input type="button" value="..." id="btnSearchHelpMClass" class="btn-help" />
                </div>
                <div>
                    <label>物料类型：</label>
                    <input name="MaterialTypeID" id="txtSearchTypeID" hidden="hidden" />
                    <input name="MaterialTypeName" id="txtSearchTypeName" />
                    <input type="button" value="..." id="btnSearchHelpMType" class="btn-help" />
                </div>
                <div>
                    <a href="javascript:void(0);" id="btnSearch" class="easyui-linkbutton" iconcls="icon-search"
                        style="width: 75px;">查询</a>
                </div>
            </form>
        </div>
        <div region="center">
            <table id="grid" style="width: 100%; height: 100%;"></table>
        </div>
    </div>
    <script src="Materials.js"></script>
    <script>
        $(materials.init);
    </script>
</body>
</html>
