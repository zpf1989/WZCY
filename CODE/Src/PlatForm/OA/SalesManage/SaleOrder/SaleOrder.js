//订单信息格式化对象
var soFormatter = {
    //订单状态
    soState: {
        src: [{ value: '1', text: '编制' }, { value: '2', text: '提交' }, { value: '3', text: '初审通过' }, { value: '4', text: '初审不通过' }, { value: '5', text: '复审通过' }, { value: '6', text: '复审不通过' }, { value: '7', text: '关闭' }],
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            var rst = "";
            for (var idx = 0; idx < this.src.length; idx++) {
                if (value == this.src[idx].value) {
                    rst = this.src[idx].text;
                    break;
                }
            }
            return rst;
        }
    },
};

//订单列表对象
var saleorder = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtSearchBillTypeName: $('#txtSearchBillTypeName'),
    txtSearchBillTypeID: $('#txtSearchBillTypeID'),
    btnSearchBillType: $('#btnSearchBillType'),
    btnSearchBillType: $('#btnSearchBillType'),
    dateSearchSaleDateBegin: $('#dateSearchSaleDateBegin'),
    dateSearchSaleDateEnd: $('#dateSearchSaleDateEnd'),
    btnCardBillType: $('#btnCardHelpBillType'),
    txtCardBillTypeName: $('#txtCardBillTypeName'),
    txtCardBillTypeID: $('#txtCardSOTypeID'),
    btnCardMaterial: $('#btnCardHelpMaterial'),
    txtCardMName: $('#txtCardMName'),
    txtCardMID: $('#txtCardMID'),
    btnCardUnit: $('#btnCardHelpUnit'),
    txtCardUName: $('#txtCardUName'),
    txtCardUID: $('#txtCardUID'),
    btnCardClient: $('#btnCardHelpClient'),
    txtCardClientName: $('#txtCardClientName'),
    txtCardClientID: $('#txtCardClientID'),
    cardFormWidth: 700,
    cardFormHeight: 640,
    cardFormUrl: 'SaleOrderAdd.html',
    saveUrl: 'SaleOrderService.asmx/Save',
    searchUrl: 'SaleOrderService.asmx/GetList',
    init: function () {
        saleorder.initgrid();
        saleorder.bindingEvents();
        saleorder.formSearch.children('div').css({ 'float': 'left', 'padding-left': '8px' });
    },
    initCardControls: function () {
        //注册卡片按钮事件
        saleorder.btnCardBillType.click(saleorder.cardHandler.onClickHelpBillType);
        saleorder.btnCardMaterial.click(saleorder.cardHandler.onClickHelpMaterial);
        saleorder.btnCardUnit.click(saleorder.cardHandler.onClickHelpUnit);
        saleorder.btnCardClient.click(saleorder.cardHandler.onClickHelpClient);
    },
    //绑定（注册）事件
    bindingEvents: function () {
        saleorder.btnSearch.click(saleorder.doSearch);
        saleorder.btnSearchBillType.click(saleorder.onClickSearchBillType);
        saleorder.txtSearchBillTypeName.textbox({
            onChange: function (newValue, oldValue) {
                if (oldValue != newValue) {
                    //手动输入时，id设置空
                    saleorder.txtSearchBillTypeID.val('');
                }
            }
        });
    },
    initgrid: function () {
        gFunc.initGridPublic(saleorder.grid, {
            title: '订单列表',
            icon: 'icon-edit',
            key: 'SaleOrderID',
            url: saleorder.searchUrl,
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: saleorder.addSaleOrder
            }, "-", {
                id: 'btnView',
                text: '查看',
                iconCls: 'icon-search',
                handler: saleorder.viewSaleOrder
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: saleorder.editSaleOrder
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: saleorder.deleteRowBatch
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'SaleOrderID' },
                { field: 'SaleOrderCode', title: '订单编号', width: 100, align: 'center' },
                { field: 'BillTypeID' },
                { field: 'BillType_Name', title: '订单类型', width: 80, align: 'center' },
                { field: 'MaterialID' },
                { field: 'SaleDate', title: '销售日期', align: 'center', width: 80 },
                { field: 'FinishDate', title: '交货日期', align: 'center', width: 80 },
                { field: 'Material_Name', title: '物料', align: 'center', width: 100 },
                { field: 'SaleUnitID' },
                { field: 'SaleUnit_Name', title: '计量单位', align: 'center', width: 60 },
                 { field: 'ClientID' },
                { field: 'Client_Name', title: '客户', align: 'center', width: 100 },
                { field: 'SaleQty', title: '销售数量', align: 'center', width: 80 },
                { field: 'SalePrice', title: '销售单价', align: 'center', width: 80 },
                { field: 'SaleCost', title: '销售金额', align: 'center', width: 80 },
                { field: 'Routing', title: '生产工艺', align: 'center', width: 100 },
                { field: 'Creator' },
                { field: 'Creator_Name', title: '创建人', align: 'center', width: 80 },
                { field: 'CreateTime', title: '创建时间', align: 'center', width: 130 },
                { field: 'Editor' },
                { field: 'Editor_Name', title: '修改人', align: 'center', width: 80 },
                { field: 'EditTime', title: '修改时间', align: 'center', width: 130 },
                { field: 'FirstChecker' },
                { field: 'FirstChecker_Name', title: '初审人', align: 'center', width: 80 },
                { field: 'FirstCheckTime', title: '初审时间', align: 'center', width: 130 },
                { field: 'FirstCheckView', title: '初审意见', align: 'center', width: 100 },
                { field: 'SecondCheckerName', title: '复审人', align: 'center', width: 80 },
                { field: 'ReaderName', title: '分阅人', align: 'center', width: 60 },
                {
                    field: 'SaleState', title: '状态', width: 80, align: 'center',
                    formatter: function (value, row, index) { return soFormatter.soState.format(value); }
                },
                { field: 'Remark', title: '备注', width: 100, align: 'center' }
            ]],
            hidecols: ['SaleOrderID', 'BillTypeID', 'MaterialID', 'SaleUnitID', 'ClientID', 'Creator', 'Editor', 'FirstChecker'],
            singleSelect: false
        });
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return saleorder.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    addSaleOrder: function () {
        //弹出窗体
        gFunc.showPopWindow({
            title: '新增销售订单',
            width: saleorder.cardFormWidth,
            height: saleorder.cardFormHeight,
            url: saleorder.cardFormUrl,
            isModal: true,
            funLoadCallback: function () {
                //卡片初始化
            },
            funSubmitCallback: function () {
                return saleorder.doSave();
            }
        });
    },
    viewSaleOrder: function () {
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要查看的数据');
            return;
        }
        if (checkedRows.length > 1) {
            $.messager.alert('提示', '请选择一条数据查看');
            return;
        }
        //弹出窗体
        gFunc.showPopWindow({
            title: '查看销售订单',
            width: saleorder.cardFormWidth,
            height: saleorder.cardFormHeight,
            url: saleorder.cardFormUrl,
            isModal: true,
            funLoadCallback: function () {
                //初始化弹出界面
                saleorder.doSetForm(checkedRows[0], 2);
            }
        });
    },
    editSaleOrder: function () {
        var rows = saleorder.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        if (rows.length > 1) {
            $.messager.alert('提示', '只能修改一条数据');
            return;
        }
        //弹出窗体
        gFunc.showPopWindow({
            title: '修改销售订单',
            width: saleorder.cardFormWidth,
            height: saleorder.cardFormHeight,
            url: saleorder.cardFormUrl,
            isModal: true,
            funLoadCallback: function () {
                //初始化弹出界面
                saleorder.doSetForm(checkedRows[0], 1);
            },
            funSubmitCallback: function () {
                return saleorder.doSave();
            }
        });
    },
    deleteRowBatch: function () {
        var delCheckedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(delCheckedRows) || delCheckedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.SaleOrderID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('SaleOrderService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        saleorder.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = saleorder.formSearch.serializeToJson(true);
        //重新查询
        saleorder.grid.datagrid("reload", searchParams);
    },
    doSetForm: function (data, state) {
        gFunc.formFunc.clearValidations('editForm');//清除表单验证
        switch (state) {
            case 1://修改
            case 2://查看
                //赋值
                //设置只读
                var boolReadOnly = state == 2 ? true : false;

                break;
            case 0://添加
            default:
                break;
        }
    },
    doSave: function () {
        //验证数据
        var valRst = gFunc.formFunc.validate("editForm");
        console.log('valresult:' + valRst);
        if (!valRst) {
            return false;
        }
        //收集数据
        var formData = gFunc.formFunc.serializeToJson("editForm");

        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: saleorder.saveUrl,
            data: JSON.stringify(formData),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    //重新加载
                    saleorder.grid.datagrid('reload');
                    ajaxResult = true;
                    console.log('saleorder,ajax succeed');
                } else {
                    console.log('saleorder,ajax fail');
                    ajaxResult = false;
                }
            },
            error: function () {
                console.log('saleorder,ajax error');
                ajaxResult = false;
            }
        });
        console.log('saleorder,doSave over');
        return ajaxResult;
    },
    onClickSearchBillType: function () {
        showPopGridHelp(400, 300, true, helpInitializer.billType, saleorder.helpReceiver.searchBillType, null);
    },
    cardHandler: {
        onClickHelpBillType: function () {
            showPopGridHelp(400, 300, true, helpInitializer.billType, saleorder.helpReceiver.cardBillType, null);
        },
        onClickHelpMaterial: function () {
            showPopGridHelp(500, 400, true, helpInitializer.materials, saleorder.helpReceiver.cardMaterial, null);

        },
        onClickHelpUnit: function () {
            showPopGridHelp(400, 300, true, helpInitializer.measureUnit, saleorder.helpReceiver.cardUnit, null);

        },
        onClickHelpClient: function () {
            showPopGridHelp(400, 300, true, helpInitializer.client, saleorder.helpReceiver.cardClient, null);

        },
    },
    helpReceiver: {
        searchBillType: function (typeData) {
            //注意：必须先给name赋值，因为它会触发onChange事件，会把id冲掉
            saleorder.txtSearchBillTypeName.textbox('setValue', typeData.BillName);
            saleorder.txtSearchBillTypeID.val(typeData.BillID);
        },
        cardBillType: function (typeData) {
            saleorder.txtCardBillTypeName.textbox('setValue', typeData.BillName);
            saleorder.txtCardBillTypeID.val(typeData.BillID);
        },
        cardMaterial: function (mData) {
            saleorder.txtCardMName.textbox('setValue', mData.MaterialName);
            saleorder.txtCardMID.val(mData.MaterialID);
        },
        cardUnit: function (uData) {
            saleorder.txtCardUName.textbox('setValue', uData.UnitName);
            saleorder.txtCardUID.val(uData.UnitID);
        },
        cardClient: function (cData) {
            saleorder.txtCardClientName.textbox('setValue', cData.ClientName);
            saleorder.txtCardClientID.val(cData.ClientID);
        }
    }
}