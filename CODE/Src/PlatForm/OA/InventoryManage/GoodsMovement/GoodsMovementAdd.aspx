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
                <table class="card-centent-table" cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <td class="card-table-label">单据编号</td>
                        <td class="card-table-centent">
                            <input type="text" name="GoodsMovementCode" id="txtCardGMCode" class="easyui-textbox" data-options="required:true,validType:'maxLength[60]'" /></td>
                        <td class="card-table-label">业务类型</td>
                        <td class="card-table-centent">
                            <select id="cbCardBusinessType" class="easyui-combobox" name="BusinessType" style="width: 150px;" data-options="required:true" editable="false">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">移动类型</td>
                        <td class="card-table-centent">
                            <select id="cbCardMoveType" class="easyui-combobox" name="MoveTypeCode" style="width: 150px;" data-options="required:true" editable="false">
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
                            <input type="checkbox" id="checkCardIsRed" name="IsRed" class="easyui-checkbox" value="0"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">接收日期</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardReceiptDate" name="ReceiptDate" class="easyui-datebox" data-options="required:true" editable="false" />
                        </td>
                        <td class="card-table-label">接收部门</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardRecDeptName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpRecDept" class="btn-help" />
                                <input id="txtCardRecDeptID" name="RecDeptID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">接收经办人</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardRecHandlerName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpRecHandler" class="btn-help" />
                                <input id="txtCardRecHandler" name="RecHandler" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">接收仓库</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardRecWHName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpRecWH" class="btn-help" />
                                <input id="txtCardRecWHID" name="RecWHID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">接收仓库保管员</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardRecWHEmpName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpRecWHEmp" class="btn-help" />
                                <input id="txtCardRecWHEmpID" name="RecWHEmpID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">发出日期</td>
                        <td class="card-table-centent">
                            <input type="text" id="txtCardIssDate" name="IssDate" class="easyui-datebox" editable="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">发出部门</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardIssDeptName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpIssDept" class="btn-help" />
                                <input id="txtCardIssDeptID" name="IssDeptID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">发出经办人</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardIssHandlerName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpIssHandler" class="btn-help" />
                                <input id="txtCardIssHandler" name="IssHandler" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">发出仓库</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardIssWHName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpIssWH" class="btn-help" />
                                <input id="txtCardIssWHID" name="IssWHID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">发出仓库保管员</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardIssWHEmpName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpIssWHEmp" class="btn-help" />
                                <input id="txtCardIssWHEmpID" name="IssWHEmpID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">采购部门</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardPurDeptName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpPurDept" class="btn-help" />
                                <input id="txtCardPurDeptID" name="PurDeptID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">采购人员</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardPurEmpName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpPurEmp" class="btn-help" />
                                <input id="txtCardPurEmpID" name="PurEmpID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">供应商</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardSupplierName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpSupplier" class="btn-help" />
                                <input id="txtCardSupplierID" name="SupplierID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">销售部门</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardSalesDepName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpSalesDep" class="btn-help" />
                                <input id="txtCardSalesDepID" name="SalesDepID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">销售人员</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardSalesEmpName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpSalesEmp" class="btn-help" />
                                <input id="txtCardSalesEmpID" name="SalesEmpID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">销售客户</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardCustomerName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpCustomer" class="btn-help" />
                                <input id="txtCardCustomerID" name="CustomerID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">生产部门</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardProDepName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpProDep" class="btn-help" />
                                <input id="txtCardProDepID" name="ProDepID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">生产人</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardProEmpName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpProEmp" class="btn-help" />
                                <input id="txtCardProEmpID" name="ProEmpID" hidden="hidden" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="card-table-label">领用部门</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardConDepName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpConDep" class="btn-help" />
                                <input id="txtCardConDepID" name="ConDepID" hidden="hidden" />
                            </span>
                        </td>
                        <td class="card-table-label">领用人</td>
                        <td class="card-table-centent">
                            <span>
                                <input type="text" id="txtCardConEmpName" class="easyui-textbox help-text" readonly="readonly" />
                                <input type="button" value="..." id="btnCardHelpConEmp" class="btn-help" />
                                <input id="txtCardConEmpID" name="ConEmpID" hidden="hidden" />
                            </span>
                        </td>
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
            //gmCard.log(soId);
            var state = gFunc.getUrlParam('state');
            //gmCard.log(state);
            gmCard.initCardForm(gmId, state);
        });
    </script>
</body>
</html>
