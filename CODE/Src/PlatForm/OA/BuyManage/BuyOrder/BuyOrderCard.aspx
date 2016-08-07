<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuyOrderCard.aspx.cs" Inherits="OA_BuyManage_BuyOrder_BuyOrderCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>采购订单编制</title>
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
    <div style="padding: 4px 8px;">
        <a href="javascript:void(0);" id="btnSave" class="easyui-linkbutton" iconcls="icon-save">保存</a>
        <a href="javascript:void(0);" id="btnBack" class="easyui-linkbutton" iconcls="icon-back">返回</a>
    </div>
    <div class="easyui-layout" fit="true">
        <div region="north" title="订单基本信息" style="height:300px">
            <form id="editForm" style="padding: 0px;">
                <input id="txtBOID" name="BuyOrderID" hidden="hidden" />
                <table class="card-centent-table" cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <td class="card-table-label">订单编号</td>
                        <td class="card-table-centent">
                            <input type="text" name="BuyOrderCode" id="txtCardBOCode" class="easyui-textbox" data-options="required:true,validType:'maxLength[60]'" /></td>
                        <td class="card-table-label">采购日期</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardBuyOrderDate" name="BuyOrderDate" class="easyui-datebox" data-options="required:true" editable="false" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">供应商</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardSupplierName" class="easyui-textbox help-text" readonly="readonly" data-options="required:true" />
                                <input type="button" value="..." id="btnCardHelpSupplier" class="btn-help" />
                                <input id="txtCardBOSupplierID" name="SupplierID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">到货日期</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardDeliveryDate" name="DeliveryDate" class="easyui-datebox" data-options="required:true" editable="false" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">订单状态</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardBOState" name="OrderState" class="easyui-textbox" readonly="readonly" /></td>
                        <td class="card-table-label"></td>
                        <td class="card-table-centent"></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">创建人</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardCreatorName" class="easyui-textbox" readonly="readonly" /></td>
                        <td class="card-table-label">创建时间</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardCreateTime" class="easyui-textbox" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">修改人</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardEditorName" class="easyui-textbox" readonly="readonly" /></td>
                        <td class="card-table-label">修改时间</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardEditTime" class="easyui-textbox" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">初审人</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardFirstCheckerName" class="easyui-textbox" readonly="readonly" /></td>
                        <td class="card-table-label">初审时间</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardFirstCheckTime" class="easyui-textbox" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">初审意见</td>
                        <td colspan="3" class="card-table-centent">
                            <input type="text" id="txtCardFirstCheckView" class="easyui-textbox" style="width: 480px;" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">复核人</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardSecondCheckerName" class="easyui-textbox" readonly="readonly" /></td>
                        <td class="card-table-label">分阅人</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardReaderName" class="easyui-textbox" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">备注</td>
                        <td class="card-table-centent" colspan="3">
                            <input type="text" id="txtCardRemark" name="Remark" class="easyui-textbox" style="width: 480px;" data-options="validType:'maxLength[1024]'" /></td>
                    </tr>
                </table>
            </form>
        </div>
        <div region="center" title="订单行信息">
            <table id="gridBOItem" style="width: 100%; height: 100%;"></table>
        </div>
    </div>
    <script src="BuyOrderCard.js"></script>
    <script>
        $(function () {
            $('.help-text').css({ 'width': '146px' });
            var boId = gFunc.getUrlParam('boId');
            //soCard.log(soId);
            var state = gFunc.getUrlParam('state');
            //soCard.log(state);
            boCard.initCardForm(boId, state);
        });
    </script>
</body>
</html>