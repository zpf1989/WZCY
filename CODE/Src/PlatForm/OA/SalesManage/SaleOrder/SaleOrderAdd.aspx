<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleOrderAdd.aspx.cs" Inherits="OA_SalesManage_SaleOrder_SaleOrderAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>销售订单编制</title>
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
        <div region="north" title="订单基本信息" height="300px;">
            <form id="editForm" style="padding: 0px;">
                <input id="txtSOID" name="SaleOrderID" hidden="hidden" />
                <table class="card-centent-table" cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <td class="card-table-label">订单编号</td>
                        <td class="card-table-centent">
                            <input type="text" name="SaleOrderCode" id="txtCardSOCode" class="easyui-textbox" data-options="required:true,validType:'maxLength[60]'" /></td>
                        <td class="card-table-label">订单类型</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardBillTypeName" class="easyui-textbox help-text" readonly="readonly" data-options="required:true" />
                                <input type="button" value="..." id="btnCardHelpBillType" class="btn-help" />
                                <input id="txtCardSOTypeID" name="BillTypeID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">订单物料</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardMName" class="easyui-textbox help-text" readonly="readonly" data-options="required:true" />
                                <input type="button" value="..." id="btnCardHelpMaterial" class="btn-help" />
                                <input id="txtCardMID" name="MaterialID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">计量单位</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardUName" class="easyui-textbox help-text" readonly="readonly" data-options="required:true" />
                                <input type="button" value="..." id="btnCardHelpUnit" class="btn-help" />
                                <input id="txtCardUID" name="SaleUnitID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">客户</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardClientName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpClient" class="btn-help" />
                                <input id="txtCardClientID" name="ClientID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">销售日期</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardSaleDate" name="SaleDate" class="easyui-datebox" data-options="required:true" editable="false" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">订单状态</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardSOState" name="SaleState" class="easyui-textbox" readonly="readonly" /></td>
                        <td class="card-table-label">生产工艺</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardRouting" name="Routing" class="easyui-textbox" data-options="validType:'maxLength[1024]'" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">销售数量</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardSaleQty" name="SaleQty" class="easyui-numberbox" data-options="validType:'maxLength[20]'" precision="2" /></td>
                        <td class="card-table-label">销售单价</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardPrice" name="SalePrice" class="easyui-numberbox" data-options="validType:'maxLength[20]'" precision="2" /></td>
                    </tr>
                    <tr>
                        <td class="card-table-label">销售金额</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardSaleCost" name="SaleCost" class="easyui-numberbox" data-options="validType:'maxLength[20]'" precision="2" /></td>
                        <td class="card-table-label">交货日期</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardFinishDate" name="FinishDate" class="easyui-datebox" data-options="required:true" editable="false" /></td>
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
            <table id="gridSOItem" style="width: 100%; height: 100%;"></table>
        </div>
    </div>
    <script src="SaleOrderAdd.js"></script>
    <script>
        $(function () {
            $('.help-text').css({ 'width': '146px' });
            var soData = JSON.parse(decodeURI(gFunc.getUrlParam('sodata')));
            //soCard.log(soData);
            var state = gFunc.getUrlParam('state');
            //soCard.log(state);
            soCard.initCardForm(soData, state);
        });
    </script>
</body>
</html>
