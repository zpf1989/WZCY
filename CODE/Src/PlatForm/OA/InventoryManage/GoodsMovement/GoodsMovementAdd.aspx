<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsMovementAdd.aspx.cs" Inherits="OA_InventoryManage_GoodsMovement_GoodsMovementAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>货物移动编制</title>
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
        <div region="north" title="基本信息" style="height: 300px;">
            <form id="editForm" style="padding: 0px;">
                <input id="txtGMID" name="GoodsMovementID" hidden="hidden" />
                <table id="tbBasicInfo" class="card-centent-table" cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <td class="card-table-label">单据编号</td>
                        <td class="card-table-centent">
                            <input type="text" name="GoodsMovementCode" id="txtCardGMCode" class="easyui-textbox" data-options="required:true,validType:'maxLength[60]'" />
                        </td>
                        <td class="card-table-label">业务类型</td>
                        <td class="card-table-centent">
                            <select id="cbCardBusinessType" class="easyui-combobox" name="BusinessType" style="width: 150px;">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">移动类型</td>
                        <td class="card-table-centent">
                            <select id="cbCardMoveType" class="easyui-combobox" name="MoveTypeCode" style="width: 150px;">
                            </select>
                        </td>
                        <td class="card-table-label">单据日期</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardCreateDate" name="CreateDate" class="easyui-datebox" data-options="required:true" editable="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">单据状态</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardBillState" name="BillState" class="easyui-textbox" readonly="readonly" />
                        </td>
                        <td class="card-table-label">是否红单</td>
                        <td class="card-table-centent">
                            <input type="checkbox" id="checkCardIsRed" name="IsRed" class="easyui-checkbox" />
                        </td>
                    </tr>
                    <!--
                        这里放置动态内容，在第3行后面；$("#tab tr:eq(2)").after(trHTML);
                        -->
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
                            <input type="text" id="txtCardFirstCheckView" class="easyui-textbox" style="width: 480px;" readonly="readonly" />
                        </td>
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
                            <input type="text" id="txtCardRemark" name="Remark" class="easyui-textbox" style="width: 480px;" data-options="validType:'maxLength[1024]'" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div region="center" title="行信息">
            <table id="gridGMItem" style="width: 100%; height: 100%;"></table>
        </div>
    </div>
    <script src="GoodsMovementAdd.js"></script>
    <script>
        $(function () {
            $('.help-text').css({ 'width': '146px' });
            var gmId = gFunc.getUrlParam('gmId');
            //gmCard.log(gmId);
            var state = gFunc.getUrlParam('state');
            //gmCard.log(state);
            var busType = gFunc.getUrlParam('busType');
            var moveType = gFunc.getUrlParam('moveType');
            gmCard.initCardForm({
                gmId: gmId,
                state: state,
                busType: busType,
                moveType: moveType
            });
        });
    </script>
</body>
</html>
