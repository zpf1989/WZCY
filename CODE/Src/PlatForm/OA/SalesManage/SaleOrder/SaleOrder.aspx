<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleOrder.aspx.cs" Inherits="OA_SalesManage_SaleOrder_SaleOrder" %>

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
                    <label>订单编号：</label><input name="SOCode" id="txtSearchSOCode" class="easyui-textbox" />
                </div>
                <div>
                    <label>单据类型：</label>
                    <input name="BillTypeID" id="txtSearchBillTypeID" hidden="hidden" />
                    <input name="BillTypeName" id="txtSearchBillTypeName"  class="easyui-textbox"/>
                    <input type="button" value="..." id="btnSearchBillType" class="btn-help" />
                </div>
                <div>
                    <label>交货日期：</label>
                    <input name="SaleDateBegin" id="dateSearchSaleDateBegin" class="easyui-datebox" />
                    <input name="SaleDateEnd" id="dateSearchSaleDateEnd" class="easyui-datebox" />
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
    <script src="SaleOrder.js"></script>
    <script>
        $(saleorder.init);
    </script>
</body>
</html>
