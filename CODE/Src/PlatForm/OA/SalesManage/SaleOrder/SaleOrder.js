//订单信息格式化对象
var soFormatter = {
    //订单状态
    soState: {
        src: [{ value: '1', text: '编制' }, { value: '2', text: '提交初审' }, { value: '3', text: '初审通过' }, { value: '4', text: '初审不通过' }, { value: '5', text: '提交复审' }, { value: '6', text: '复审通过' }, { value: '7', text: '复审不通过' }, { value: '8', text: '关闭' }],
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            return formatHandler.combobox.format(value, soFormatter.soState.src);
        }
    },
};

//订单列表对象
var saleorder = {
    stateConf: {
        add: '0',
        edit: '1',
        view: '2'
    },
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtSearchBillTypeName: $('#txtSearchBillTypeName'),
    txtSearchBillTypeID: $('#txtSearchBillTypeID'),
    btnSearchBillType: $('#btnSearchBillType'),
    dateSearchSaleDateBegin: $('#dateSearchSaleDateBegin'),
    dateSearchSaleDateEnd: $('#dateSearchSaleDateEnd'),
    cardFormWidth: 700,
    cardFormHeight: 600,
    approvalFormWidth: 400,
    approvalFormHeight: 240,
    cardFormUrl: 'SaleOrderAdd.aspx',
    searchUrl: 'SaleOrderService.asmx/GetList',
    init: function () {
        saleorder.initgrid();
        saleorder.bindingEvents();
        saleorder.formSearch.children('div').css({ 'float': 'left', 'padding-left': '8px' });
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
            }, "-", {
                id: 'btnSubmitToFirstCheck',
                text: '提交初审',
                iconCls: 'icon-search',
                handler: saleorder.submitToFirstCheck
            }, "-", {
                id: 'btnSubmitToSecondCheck',
                text: '提交复审',
                iconCls: 'icon-edit',
                handler: saleorder.submitToSecondCheck
            }, "-", {
                id: 'btnSubmitToReader',
                text: '设置分阅人',
                iconCls: 'icon-remove',
                handler: saleorder.submitToRead
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'SaleOrderID' },
                { field: 'SaleOrderCode', title: '订单编号', width: 100, align: 'center' },
                {
                    field: 'SaleState', title: '订单状态', width: 80, align: 'center',
                    formatter: function (value, row, index) { return soFormatter.soState.format(value); }
                },
                { field: 'BillTypeID' },
                { field: 'BillType_Name', title: '订单类型', width: 80, align: 'center' },
                { field: 'SaleDate', title: '销售日期', align: 'center', width: 80, formatter: formatHandler.date.format },
                { field: 'FinishDate', title: '交货日期', align: 'center', width: 80, formatter: formatHandler.date.format },
                { field: 'MaterialID' },
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
                { field: 'Remark', title: '备注', width: 100, align: 'center' }
            ]],
            hidecols: ['SaleOrderID', 'BillTypeID', 'MaterialID', 'SaleUnitID', 'ClientID', 'Creator', 'Editor', 'FirstChecker'],
            singleSelect: false
        });
    },
    //编辑
    addSaleOrder: function () {
        location.href = saleorder.cardFormUrl + '?state=0';
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
        //location.href = saleorder.cardFormUrl + '?' + encodeURI('state=2&sodata=' + JSON.stringify(checkedRows[0]));//
        location.href = saleorder.cardFormUrl + '?' + encodeURI('state=2&soId=' + checkedRows[0].SaleOrderID);//
    },
    editSaleOrder: function () {
        var rows = saleorder.grid.datagrid('getChecked');
        ////console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        if (rows.length > 1) {
            $.messager.alert('提示', '只能修改一条数据');
            return;
        }
        //
        if (rows[0].SaleState !== '1' && rows[0].SaleState !== '4' && rows[0].SaleState !== '7') {
            $.messager.alert('提示', '请选择编制、初审不通过或复审不通过状态的订单');
            return;
        }
        //location.href = saleorder.cardFormUrl + '?' + encodeURI('state=1&sodata=' + JSON.stringify(rows[0]));//
        location.href = saleorder.cardFormUrl + '?' + encodeURI('state=1&soId=' + rows[0].SaleOrderID);//
    },
    deleteRowBatch: function () {
        var delCheckedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(delCheckedRows) || delCheckedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }
        var illegalRows = $.grep(delCheckedRows, function (row, idx) {
            return row.SaleState !== '1' && row.SaleState !== '4'
                && row.SaleState !== '7' && row.SaleState !== '8';//过滤条件：非(编制、初审不通过、复审不通过、关闭)
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择编制、初审不通过、复审不通过或关闭状态的订单');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var ids = [];
                $.each(delCheckedRows, function (index, row) {
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
    //提交
    submitToFirstCheck: function () {
        //获取选中行
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.SaleState !== '1' && row.SaleState !== '4';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择编制或初审不通过状态的订单');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, saleorder.helpReceiver.submitToFirstChecker, null);
    },
    submitToSecondCheck: function () {
        //获取选中行
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.SaleState !== '3' && row.SaleState !== '7';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择初审通过或复审不通过状态的订单');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, saleorder.helpReceiver.submitToSecondChecker, null);
    },
    submitToRead: function () {
        //获取选中行
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        //提交分阅
        showPopGridHelp(400, 300, true, helpInitializer.user, saleorder.helpReceiver.submitToReader, null);
    },
    onClickSearchBillType: function () {
        showPopGridHelp(400, 300, true, helpInitializer.billType, saleorder.helpReceiver.searchBillType, null);
    },
    helpReceiver: {
        searchBillType: function (typeData) {
            //注意：必须先给name赋值，因为它会触发onChange事件，会把id冲掉
            saleorder.txtSearchBillTypeName.textbox('setValue', typeData.BillName);
            saleorder.txtSearchBillTypeID.val(typeData.BillID);
        },
        submitToFirstChecker: function (userData) {
            if (gFunc.isNull(userData) || gFunc.isNull(userData.UserID)) {
                $.messager.alert('提示', '请选择初审人');
                return;
            }
            //获取选中行
            var checkedRows = saleorder.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var soIds = [];
            $.each(checkedRows, function (index, row) {
                soIds.push(row.SaleOrderID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'SaleOrderService.asmx/SubmitToFirstChecker',
                data: 'userId=' + userData.UserID + '&soIds=' + JSON.stringify(soIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        saleorder.grid.datagrid('reload');
                        //console.log('saleorder,ajax succeed');
                    } else {
                        //console.log('saleorder,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('saleorder,ajax error');
                    ajaxResult = false;
                }
            });
        },
        submitToSecondChecker: function (userData) {
            if (gFunc.isNull(userData) || gFunc.isNull(userData.UserID)) {
                $.messager.alert('提示', '请选择复审人');
                return;
            }
            //获取选中行
            var checkedRows = saleorder.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var soIds = [];
            $.each(checkedRows, function (index, row) {
                soIds.push(row.SaleOrderID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'SaleOrderService.asmx/SubmitToSecondChecker',
                data: 'userId=' + userData.UserID + '&soIds=' + JSON.stringify(soIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        saleorder.grid.datagrid('reload');
                        //console.log('saleorder,ajax succeed');
                    } else {
                        //console.log('saleorder,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('saleorder,ajax error');
                    ajaxResult = false;
                }
            });
        },
        submitToReader: function (userData) {
            if (gFunc.isNull(userData) || gFunc.isNull(userData.UserID)) {
                $.messager.alert('提示', '请选择分阅人');
                return;
            }
            //获取选中行
            var checkedRows = saleorder.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var soIds = [];
            $.each(checkedRows, function (index, row) {
                soIds.push(row.SaleOrderID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'SaleOrderService.asmx/SubmitToReader',
                data: 'userId=' + userData.UserID + '&soIds=' + JSON.stringify(soIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        saleorder.grid.datagrid('reload');
                        //console.log('saleorder,ajax succeed');
                    } else {
                        //console.log('saleorder,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('saleorder,ajax error');
                    ajaxResult = false;
                }
            });
        },
    }
}