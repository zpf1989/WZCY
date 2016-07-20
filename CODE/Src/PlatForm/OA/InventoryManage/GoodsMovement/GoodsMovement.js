//单据信息格式化对象
var gmFormatter = {
    //单据状态
    gmState: {
        src: [{ value: '1', text: '编制' }, { value: '2', text: '提交初审' }, { value: '3', text: '初审通过' }, { value: '4', text: '初审不通过' }, { value: '5', text: '提交复审' }, { value: '6', text: '复审通过' }, { value: '7', text: '复审不通过' }, { value: '8', text: '关闭' }],
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            return formatHandler.combobox.format(value, gmFormatter.gmState.src);
        }
    },
    //业务类型
    busType: {
        src: [{ value: '1', text: '入库业务' }, { value: '2', text: '出库业务' }],
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            return formatHandler.combobox.format(value, gmFormatter.busType.src);
        }
    },
    //移动类型
    moveType: {
        src: [{ value: '100', text: '采购入库' }, { value: '101', text: '生产入库' }, { value: '102', text: '领料入库' }, { value: '103', text: '销售入库' }, { value: '104', text: '其他入库' }],
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            return formatHandler.combobox.format(value, gmFormatter.moveType.src);
        }
    }
};

//单据列表对象
var gm = {
    stateConf: {
        add: '0',
        edit: '1',
        view: '2'
    },
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtSearchGMCode: $('#txtSearchGMCode'),
    cbBusinessType: $('#cbBusinessType'),
    cbMoveTypeCode: $('#cbMoveTypeCode'),
    dateSearchCreateDateBegin: $('#dateSearchCreateDateBegin'),
    dateSearchCreateDateEnd: $('#dateSearchCreateDateEnd'),
    cardFormWidth: 700,
    cardFormHeight: 600,
    approvalFormWidth: 400,
    approvalFormHeight: 240,
    cardFormUrl: 'GoodsMovementAdd.aspx',
    searchUrl: 'GoodsMovementService.asmx/GetList',
    init: function () {
        gm.initgrid();
        gm.bindingEvents();
        gm.formSearch.children('div').css({ 'float': 'left', 'padding-left': '8px' });
        gm.cbBusinessType.combobox({
            data: gmFormatter.busType.src,
            valueField: 'value',
            textField: 'text'
        });
        gm.cbMoveTypeCode.combobox({
            data: gmFormatter.moveType.src,
            valueField: 'value',
            textField: 'text'
        });
    },
    //绑定（注册）事件
    bindingEvents: function () {
        gm.btnSearch.click(gm.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(gm.grid, {
            title: '单据列表',
            icon: 'icon-edit',
            key: 'GoodsMovementID',
            url: gm.searchUrl,
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: gm.addGM
            }, "-", {
                id: 'btnView',
                text: '查看',
                iconCls: 'icon-search',
                handler: gm.viewGM
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: gm.editGM
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: gm.deleteRowBatch
            }, "-", {
                id: 'btnSubmitToFirstCheck',
                text: '提交初审',
                iconCls: 'icon-search',
                handler: gm.submitToFirstCheck
            }, "-", {
                id: 'btnSubmitToSecondCheck',
                text: '提交复审',
                iconCls: 'icon-edit',
                handler: gm.submitToSecondCheck
            }, "-", {
                id: 'btnSubmitToReader',
                text: '设置分阅人',
                iconCls: 'icon-remove',
                handler: gm.submitToRead
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'GoodsMovementID' },
                { field: 'GoodsMovementCode', title: '单据编号', width: 100, align: 'center' },
                {
                    field: 'BillState', title: '单据状态', width: 80, align: 'center',
                    formatter: function (value, row, index) { return gmFormatter.gmState.format(value); }
                },
                {
                    field: 'BusinessType', title: '业务类型', width: 80, align: 'center',
                    formatter: function (value, row, index) { return gmFormatter.busType.format(value); }
                },
                {
                    field: 'MoveTypeCode', title: '移动类型', width: 80, align: 'center',
                    formatter: function (value, row, index) { return gmFormatter.moveType.format(value); }
                },
                { field: 'CreateDate', title: '单据日期', align: 'center', width: 80, formatter: formatHandler.date.format },
                { field: 'ReceiptDate', title: '接收日期', align: 'center', width: 80, formatter: formatHandler.date.format },
                { field: 'RecDept_Name', title: '接收部门', align: 'center', width: 100 },
                { field: 'RecHandler_Name', title: '接收经办人', align: 'center', width: 100 },
                { field: 'RecWH_Name', title: '接收仓库', align: 'center', width: 100 },
                { field: 'RecWHEmp_Name', title: '接收仓库保管员', align: 'center', width: 120 },
                { field: 'IssDate', title: '发出日期', align: 'center', width: 80, formatter: formatHandler.date.format },
                { field: 'IssDept_Name', title: '发出部门', align: 'center', width: 100 },
                { field: 'IssHandler_Name', title: '发出经办人', align: 'center', width: 100 },
                { field: 'IssWH_Name', title: '发出仓库', align: 'center', width: 100 },
                { field: 'IssWHEmp_Name', title: '发出仓库保管员', align: 'center', width: 120 },
                { field: 'PurDept_Name', title: '采购部门', align: 'center', width: 100 },
                { field: 'PurEmp_Name', title: '采购人员', align: 'center', width: 100 },
                { field: 'Supplier_Name', title: '供应商', align: 'center', width: 100 },
                { field: 'SalesDep_Name', title: '销售部门', align: 'center', width: 100 },
                { field: 'SalesEmp_Name', title: '销售人员', align: 'center', width: 100 },
                { field: 'Customer_Name', title: '销售客户', align: 'center', width: 100 },
                { field: 'ProDep_Name', title: '生产部门', align: 'center', width: 100 },
                { field: 'ProEmp_Name', title: '生产人', align: 'center', width: 100 },
                { field: 'ConDep_Name', title: '领用部门', align: 'center', width: 100 },
                { field: 'ConEmp_Name', title: '领用人', align: 'center', width: 100 },
                { field: 'Creator_Name', title: '创建人', align: 'center', width: 80 },
                { field: 'CreateTime', title: '创建时间', align: 'center', width: 130 },
                { field: 'Editor_Name', title: '修改人', align: 'center', width: 80 },
                { field: 'EditTime', title: '修改时间', align: 'center', width: 130 },
                { field: 'FirstChecker_Name', title: '初审人', align: 'center', width: 80 },
                { field: 'FirstCheckTime', title: '初审时间', align: 'center', width: 130 },
                { field: 'FirstCheckView', title: '初审意见', align: 'center', width: 100 },
                { field: 'SecondCheckerName', title: '复审人', align: 'center', width: 80 },
                { field: 'ReaderName', title: '分阅人', align: 'center', width: 60 },
                { field: 'Remark', title: '备注', width: 100, align: 'center' }
            ]],
            hidecols: ['GoodsMovementID'],
            singleSelect: false
        });
    },
    //编辑
    addGM: function () {
        location.href = gm.cardFormUrl + '?state=0';
    },
    viewGM: function () {
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要查看的数据');
            return;
        }
        if (checkedRows.length > 1) {
            $.messager.alert('提示', '请选择一条数据查看');
            return;
        }
        location.href = gm.cardFormUrl + '?' + encodeURI('state=2&gmId=' + checkedRows[0].GoodsMovementID);//
    },
    editGM: function () {
        var rows = gm.grid.datagrid('getChecked');
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
        if (rows[0].BillState !== '1' && rows[0].BillState !== '4' && rows[0].BillState !== '7') {
            $.messager.alert('提示', '请选择编制、初审不通过或复审不通过状态的单据');
            return;
        }
        location.href = gm.cardFormUrl + '?' + encodeURI('state=1&gmId=' + rows[0].GoodsMovementID);//
    },
    deleteRowBatch: function () {
        var delCheckedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(delCheckedRows) || delCheckedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }
        var illegalRows = $.grep(delCheckedRows, function (row, idx) {
            return row.BillState !== '1' && row.BillState !== '4'
                && row.BillState !== '7' && row.BillState !== '8';//过滤条件：非(编制、初审不通过、复审不通过、关闭)
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择编制、初审不通过、复审不通过或关闭状态的单据');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var ids = [];
                $.each(delCheckedRows, function (index, row) {
                    ids.push(row.GoodsMovementID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('GoodsMovementService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        gm.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = gm.formSearch.serializeToJson(true);
        //重新查询
        gm.grid.datagrid("reload", searchParams);
    },
    //提交
    submitToFirstCheck: function () {
        //获取选中行
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.BillState !== '1' && row.BillState !== '4';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择编制或初审不通过状态的单据');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, gm.helpReceiver.submitToFirstChecker, null);
    },
    submitToSecondCheck: function () {
        //获取选中行
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.BillState !== '3' && row.BillState !== '7';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择初审通过或复审不通过状态的单据');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, gm.helpReceiver.submitToSecondChecker, null);
    },
    submitToRead: function () {
        //获取选中行
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        //提交分阅
        showPopGridHelp(400, 300, true, helpInitializer.user, gm.helpReceiver.submitToReader, null);
    },
    helpReceiver: {
        submitToFirstChecker: function (userData) {
            if (gFunc.isNull(userData) || gFunc.isNull(userData.UserID)) {
                $.messager.alert('提示', '请选择初审人');
                return;
            }
            //获取选中行
            var checkedRows = gm.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var gmIds = [];
            $.each(checkedRows, function (index, row) {
                gmIds.push(row.GoodsMovementID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'GoodsMovementService.asmx/SubmitToFirstChecker',
                data: 'userId=' + userData.UserID + '&gmIds=' + JSON.stringify(gmIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        gm.grid.datagrid('reload');
                        //console.log('gm,ajax succeed');
                    } else {
                        //console.log('gm,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('gm,ajax error');
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
            var checkedRows = gm.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var gmIds = [];
            $.each(checkedRows, function (index, row) {
                gmIds.push(row.GoodsMovementID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'GoodsMovementService.asmx/SubmitToSecondChecker',
                data: 'userId=' + userData.UserID + '&gmIds=' + JSON.stringify(gmIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        gm.grid.datagrid('reload');
                        //console.log('gm,ajax succeed');
                    } else {
                        //console.log('gm,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('gm,ajax error');
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
            var checkedRows = gm.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var gmIds = [];
            $.each(checkedRows, function (index, row) {
                gmIds.push(row.GoodsMovementID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'GoodsMovementService.asmx/SubmitToReader',
                data: 'userId=' + userData.UserID + '&gmIds=' + JSON.stringify(gmIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        gm.grid.datagrid('reload');
                        //console.log('gm,ajax succeed');
                    } else {
                        //console.log('gm,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('gm,ajax error');
                    ajaxResult = false;
                }
            });
        },
    }
}