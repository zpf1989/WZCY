<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AskPriceAdd.aspx.cs" Inherits="OA_SalesManage_AskPrice_AskPriceAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>询价单编制</title>
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
        <div region="north" title="询价单基本信息" style="height: 300px;">
            <form id="editForm" style="padding: 0px;">
                <input id="txtCardAPID" name="APID" hidden="hidden" />
                <table class="card-centent-table" cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <td class="card-table-label">询价单编号</td>
                        <td class="card-table-centent">
                            <input type="text" name="APCode" id="txtCardAPCode" class="easyui-textbox" data-options="required:true,validType:'maxLength[30]'" /></td>
                        <td class="card-table-label">询价单类型</td>
                        <td class="card-table-centent">
                            <select id="cbCardAPType" class="easyui-combobox" name="APType" style="width: 120px;"></select>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">询价日期</td>
                        <td class="card-table-centent">
                            <input type="text" name="AskDate" id="txtCardAskDate" class="easyui-datebox" data-options="required:true" />
                        </td>
                        <td class="card-table-label">询价单状态</td>
                        <td class="card-table-centent">
                            <label id="lblCardState"></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">客户名称</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardClientName" class="easyui-textbox help-text" readonly="readonly" data-options="required:true" />
                                <input type="button" value="..." id="btnCardHelpClient" class="btn-help" />
                                <input id="txtCardClientID" name="ClientID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">客户联系人</td>
                        <td class="card-table-centent">
                            <label id="lblClientContact"></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">客户电话</td>
                        <td class="card-table-centent">
                            <label id="lblClientTel"></label>
                        </td>
                        <td class="card-table-label">客户地址</td>
                        <td class="card-table-centent">
                            <label id="lblClientAddress"></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">付款方式</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardPayTypeName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpPayType" class="btn-help" />
                                <input id="txtCardPayTypeID" name="PayTypeID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">备注</td>
                        <td class="card-table-centent">
                            <input type="text" name="APRemark" id="txtCardAPRemark" class="easyui-textbox big-text" data-options="validType:'maxLength[1024]'" />
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">跟踪情况</td>
                        <td class="card-table-centent" colspan="3">
                            <input type="text" name="TrackDescription" id="txtCardTrackDescription" class="easyui-textbox big-text" data-options="validType:'maxLength[1024]'" />
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">客户调查</td>
                        <td class="card-table-centent" colspan="3">
                            <input type="text" name="ClientSurvey" id="txtCardClientSurvey" class="easyui-textbox big-text" data-options="validType:'maxLength[1024]'" />
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">创建人</td>
                        <td class="card-table-centent">
                            <label id="lblCardCreatorName"></label>
                        </td>
                        <td class="card-table-label">创建时间</td>
                        <td class="card-table-centent">
                            <label id="lblCardCreateTime"></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">修改人</td>
                        <td class="card-table-centent">
                            <label id="lblCardEditorName"></label>
                        </td>
                        <td class="card-table-label">修改时间</td>
                        <td class="card-table-centent">
                            <label id="lblCardEditTime"></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">初审人</td>
                        <td class="card-table-centent">
                            <label id="lblCardFirstCheckerName"></label>
                        </td>
                        <td class="card-table-label">初审时间</td>
                        <td class="card-table-centent">
                            <label id="lblCardFirstCheckTime"></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">初审意见</td>
                        <td colspan="3" class="card-table-centent">
                            <label id="lblCardFirstCheckView"></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">复核人</td>
                        <td class="card-table-centent">
                            <label id="lblCardSecondCheckerName"></label>
                        </td>
                        <td class="card-table-label">分阅人</td>
                        <td class="card-table-centent">
                            <label id="lblCardReaderName"></label>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div region="center" title="询价单行信息">
            <table id="gridAPItem" style="width: 100%; height: 100%;"></table>
        </div>
    </div>
    <script src="AskPriceAdd.js"></script>
    <script>
        $(function () {
            $('.help-text').css({ 'width': '146px' });
            var apId = gFunc.getUrlParam('apId');
            //apCard.log(apId);
            var state = gFunc.getUrlParam('state');
            //apCard.log(state);
            apCard.initCardForm(apId, state);
        });
    </script>
</body>
</html>
