//订单信息格式化对象
var boFormatter = {
    //订单状态
    boState: {
        src: [{ value: '1', text: '编制' }, { value: '2', text: '提交初审' }, { value: '3', text: '初审通过' }, { value: '4', text: '初审不通过' }, { value: '5', text: '提交复审' }, { value: '6', text: '复审通过' }, { value: '7', text: '复审不通过' }, { value: '8', text: '关闭' }],
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            return formatHandler.combobox.format(value, boFormatter.boState.src);
        }
    },
};

//订单列表对象
var buyorder = {
    stateConf: {
        add: '0',
        edit: '1',
        view: '2'
    },
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtSearchSupplierName: $('#txtSearchSupplierName'),
    txtSearchSupplierID: $('#txtSearchSupplierID'),
    btnSearchSupplier: $('#btnSearchSupplier'),
    dateSearchBuyOrderDateBegin: $('#dateSearchBuyOrderDateBegin'),
    dateSearchBuyOrderDateEnd: $('#dateSearchBuyOrderDateEnd'),
    cardFormWidth: 700,
    cardFormHeight: 600,
    approvalFormWidth: 400,
    approvalFormHeight: 240,
    cardFormUrl: 'BuyOrderCard.aspx',
    searchUrl: 'BuyOrderService.asmx/GetList',
    init: function () {
        buyorder.initgrid();
        buyorder.bindingEvents();
        buyorder.formSearch.children('div').css({ 'float': 'left', 'padding-left': '8px' });
    },
    //绑定（注册）事件
    bindingEvents: function () {
        buyorder.btnSearch.click(buyorder.doSearch);
        buyorder.btnSearchSupplier.click(buyorder.onClickSearchSupplier);
        buyorder.txtSearchSupplierName.textbox({
            onChange: function (newValue, oldValue) {
                if (oldValue != newValue) {
                    //手动输入时，id设置空
                    buyorder.txtSearchSupplierID.val('');
                }
            }
        });
    },
    initgrid: function () {
        gFunc.initGridPublic(buyorder.grid, {
            title: '订单列表',
            icon: 'icon-edit',
            key: 'BuyOrderID',
            url: buyorder.searchUrl,
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: buyorder.addBuyOrder
            }, "-", {
                id: 'btnView',
                text: '查看',
                iconCls: 'icon-search',
                handler: buyorder.viewBuyOrder
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: buyorder.editBuyOrder
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: buyorder.deleteRowBatch
            }, "-", {
                id: 'btnSubmitToFirstCheck',
                text: '提交初审',
                iconCls: 'icon-search',
                handler: buyorder.submitToFirstCheck
            }, "-", {
                id: 'btnSubmitToSecondCheck',
                text: '提交复审',
                iconCls: 'icon-edit',
                handler: buyorder.submitToSecondCheck
            }, "-", {
                id: 'btnSubmitToReader',
                text: '设置分阅人',
                iconCls: 'icon-remove',
                handler: buyorder.submitToRead
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'BuyOrderID' },
                { field: 'BuyOrderCode', title: '订单编号', width: 100, align: 'center' },
                {
                    field: 'OrderState', title: '订单状态', width: 80, align: 'center',
                    formatter: function (value, row, index) { return boFormatter.boState.format(value); }
                },
                { field: 'SupplierID' },
                { field: 'Supplier_Name', title: '供应商', width: 80, align: 'center' },
                { field: 'BuyOrderDate', title: '采购日期', align: 'center', width: 80, formatter: formatHandler.date.format },
                { field: 'DeliveryDate', title: '到货日期', align: 'center', width: 80, formatter: formatHandler.date.format },
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
            hidecols: ['BuyOrderID', 'SupplierID', 'Creator', 'Editor', 'FirstChecker'],
            singleSelect: false
        });
    },
    //编辑
    addBuyOrder: function () {
        location.href = buyorder.cardFormUrl + '?state=0';
    },
    viewBuyOrder: function () {
        var checkedRows = buyorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要查看的数据');
            return;
        }
        if (checkedRows.length > 1) {
            $.messager.alert('提示', '请选择一条数据查看');
            return;
        }
        //location.href = buyorder.cardFormUrl + '?' + encodeURI('state=2&sodata=' + JSON.stringify(checkedRows[0]));//
        location.href = buyorder.cardFormUrl + '?' + encodeURI('state=2&boId=' + checkedRows[0].BuyOrderID);//
    },
    editBuyOrder: function () {
        var rows = buyorder.grid.datagrid('getChecked');
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
        if (rows[0].OrderState !== '1' && rows[0].OrderState !== '4' && rows[0].OrderState !== '7') {
            $.messager.alert('提示', '请选择编制、初审不通过或复审不通过状态的订单');
            return;
        }
        //location.href = buyorder.cardFormUrl + '?' + encodeURI('state=1&sodata=' + JSON.stringify(rows[0]));//
        location.href = buyorder.cardFormUrl + '?' + encodeURI('state=1&boId=' + rows[0].BuyOrderID);//
    },
    deleteRowBatch: function () {
        var delCheckedRows = buyorder.grid.datagrid('getChecked');
        if (gFunc.isNull(delCheckedRows) || delCheckedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }
        var illegalRows = $.grep(delCheckedRows, function (row, idx) {
            return row.OrderState !== '1' && row.OrderState !== '4'
                && row.OrderState !== '7' && row.OrderState !== '8';//过滤条件：非(编制、初审不通过、复审不通过、关闭)
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
                    ids.push(row.BuyOrderID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('BuyOrderService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        buyorder.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = buyorder.formSearch.serializeToJson(true);
        //重新查询
        buyorder.grid.datagrid("reload", searchParams);
    },
    //提交
    submitToFirstCheck: function () {
        //获取选中行
        var checkedRows = buyorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.OrderState !== '1' && row.OrderState !== '4';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择编制或初审不通过状态的订单');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, buyorder.helpReceiver.submitToFirstChecker, null);
    },
    submitToSecondCheck: function () {
        //获取选中行
        var checkedRows = buyorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.OrderState !== '3' && row.OrderState !== '7';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择初审通过或复审不通过状态的订单');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, buyorder.helpReceiver.submitToSecondChecker, null);
    },
    submitToRead: function () {
        //获取选中行
        var checkedRows = buyorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        //提交分阅
        showPopGridHelp(400, 300, true, helpInitializer.user, buyorder.helpReceiver.submitToReader, null);
    },
    //供应商帮助
    onClickSearchSupplier: function () {
        showPopGridHelp(400, 300, true, helpInitializer.supplier, buyorder.helpReceiver.searchSupplier, null);
    },
    helpReceiver: {
        searchSupplier: function (supplierData) {
            //注意：必须先给name赋值，因为它会触发onChange事件，会把id冲掉
            buyorder.txtSearchSupplierName.textbox('setValue', supplierData.SupplierName);
            buyorder.txtSearchSupplierID.val(supplierData.SupplierID);
        },
        submitToFirstChecker: function (userData) {
            if (gFunc.isNull(userData) || gFunc.isNull(userData.UserID)) {
                $.messager.alert('提示', '请选择初审人');
                return;
            }
            //获取选中行
            var checkedRows = buyorder.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var boIds = [];
            $.each(checkedRows, function (index, row) {
                boIds.push(row.BuyOrderID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'SaleOrderService.asmx/SubmitToFirstChecker',
                data: 'userId=' + userData.UserID + '&boIds=' + JSON.stringify(boIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        buyorder.grid.datagrid('reload');
                        //console.log('buyorder,ajax succeed');
                    } else {
                        //console.log('buyorder,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('buyorder,ajax error');
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
            var checkedRows = buyorder.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var boIds = [];
            $.each(checkedRows, function (index, row) {
                boIds.push(row.BuyOrderID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'SaleOrderService.asmx/SubmitToSecondChecker',
                data: 'userId=' + userData.UserID + '&boIds=' + JSON.stringify(boIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        buyorder.grid.datagrid('reload');
                        //console.log('buyorder,ajax succeed');
                    } else {
                        //console.log('buyorder,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('buyorder,ajax error');
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
            var checkedRows = buyorder.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var boIds = [];
            $.each(checkedRows, function (index, row) {
                boIds.push(row.BuyOrderID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'SaleOrderService.asmx/SubmitToReader',
                data: 'userId=' + userData.UserID + '&boIds=' + JSON.stringify(boIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        buyorder.grid.datagrid('reload');
                        //console.log('buyorder,ajax succeed');
                    } else {
                        //console.log('buyorder,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('buyorder,ajax error');
                    ajaxResult = false;
                }
            });
        },
    }
}