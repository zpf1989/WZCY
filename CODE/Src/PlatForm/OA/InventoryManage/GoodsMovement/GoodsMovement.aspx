<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsMovement.aspx.cs" Inherits="OA_InventoryManage_GoodsMovement_GoodsMovement" %>

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
                    <label>单据编号：</label><input name="GMCode" id="txtSearchGMCode" class="easyui-textbox" />
                </div>
                <div>
                    <label>业务类型：</label>
                    <select id="cbBusinessType" class="easyui-combobox" name="BusinessType" style="width: 120px;">
                    </select>
                </div>
                <div>
                    <label>移动类型：</label>
                    <select id="cbMoveTypeCode" class="easyui-combobox" name="MoveTypeCode" style="width: 120px;">
                    </select>
                </div>
                <div>
                    <label>单据日期：</label>
                    <input name="CreateDateBegin" id="dateSearchCreateDateBegin" class="easyui-datebox" />&nbsp;~&nbsp;<input name="CreateDateEnd" id="dateSearchCreateDateEnd" class="easyui-datebox" />
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
    <script src="GoodsMovement.js"></script>
    <script>
        $(gm.init());
    </script>
</body>
</html>
