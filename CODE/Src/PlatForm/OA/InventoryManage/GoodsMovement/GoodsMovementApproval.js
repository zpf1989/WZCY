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
        src: [{ value: '100', text: '采购入库' }, { value: '101', text: '生产入库' }, { value: '102', text: '领料出库' }, { value: '103', text: '销售出库' }, { value: '104', text: '其他出库' }],
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            return formatHandler.combobox.format(value, gmFormatter.moveType.src);
        }
    }
};

var gm = {
    stateConf: {
        add: 0,
        edit: 1,
        view: 2
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
    searchUrl: 'GoodsMovementService.asmx/GetList',
    checkUrl: 'GoodsMovementCheck.html',
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
    initgrid: function () {
        gFunc.initGridPublic(gm.grid, {
            title: '单据列表',
            icon: 'icon-edit',
            key: 'GoodsMovementID',
            url: gm.searchUrl,
            toolbar: [{
                id: 'btnFirstCheck',
                text: '初审',
                iconCls: 'icon-add',
                handler: gm.firstCheck
            }, "-", {
                id: 'btnSecondCheck',
                text: '复审',
                iconCls: 'icon-search',
                handler: gm.secondCheck
            }, "-", {
                id: 'btnRead',
                text: '分阅',
                iconCls: 'icon-edit',
                handler: gm.read
            }, "-", {
                id: 'btnClose',
                text: '关闭',
                iconCls: 'icon-edit',
                handler: gm.close
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
                    field: 'IsRed', title: '是否红单', width: 80, align: 'center',
                    formatter: function (value, row, index) { return formatHandler.trueOrFalse.format(value); }
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
    //绑定（注册）事件
    bindingEvents: function () {
        gm.btnSearch.click(gm.doSearch);
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = gm.formSearch.serializeToJson(true);
        //重新查询
        gm.grid.datagrid("reload", searchParams);
    },
    //流程
    firstCheck: function () {
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要初审的数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, index) {
            return row.BillState !== '2';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择提交初审状态的订单');
            return;
        }
        //初审时，校验是否有权限（应该校验初审人是否为当前用户，后续实现）

        //初审，弹出窗体
        gFunc.showPopWindow({
            title: '单据初审',
            width: gm.approvalFormWidth,
            height: gm.approvalFormHeight,
            url: gm.checkUrl,
            isModal: true,
            funLoadCallback: function () {

            },
            funSubmitCallback: function () {
                return gm.onCheckSubmit("first");
            }
        });
    },
    secondCheck: function () {
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要复审的数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, index) {
            return row.BillState !== '5';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择提交复审状态的订单');
            return;
        }
        //复审时，校验是否有权限（应该校验复审人是否为当前用户，后续实现）

        //复审，弹出窗体
        gFunc.showPopWindow({
            title: '单据复审',
            width: gm.approvalFormWidth,
            height: gm.approvalFormHeight,
            url: gm.checkUrl,
            isModal: true,
            funLoadCallback: function () {

            },
            funSubmitCallback: function () {
                return gm.onCheckSubmit("second");
            }
        });
    },
    read: function () {
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要分阅的数据');
            return;
        }
        //分阅时，校验是否有权限（应该校验分阅人是否为当前用户，后续实现）

        //分阅
        var gmIds = [];
        $.each(checkedRows, function (index, row) {
            gmIds.push(row.GoodsMovementID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: 'GoodsMovementService.asmx/Read',
            data: 'gmIds=' + JSON.stringify(gmIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('gm,ajax succeed');
                    gm.grid.datagrid('reload');
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
        return ajaxResult;
    },
    close: function () {
        var checkedRows = gm.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        //关闭
        var gmIds = [];
        $.each(checkedRows, function (index, row) {
            gmIds.push(row.GoodsMovementID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: 'GoodsMovementService.asmx/Close',
            data: 'gmIds=' + JSON.stringify(gmIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('gm,ajax succeed');
                    gm.grid.datagrid('reload');
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
        return ajaxResult;
    },
    onCheckSubmit: function (checkType) {
        //1、验证
        var valRst = gFunc.formFunc.validate("checkForm");
        if (!valRst) {
            $.messager.alert('提示', '数据校验失败，请检查输入！');
            return false;
        }
        var formData = gFunc.formFunc.serializeToJson("checkForm");
        //提交审批结果
        var checkedRows = gm.grid.datagrid('getChecked');
        var gmIds = [];
        $.each(checkedRows, function (index, row) {
            gmIds.push(row.GoodsMovementID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        var url = "";
        if (checkType == "first") {
            url = 'GoodsMovementService.asmx/FirstCheck';
        } else if (checkType == "second") {
            url = 'GoodsMovementService.asmx/SecondCheck';
        } else if (checkType == "read") {
            url = 'GoodsMovementService.asmx/Read';
        }
        if (url == "") {
            //console.log('onCheckSubmit url cannot be null');
            return;
        }
        $.ajax({
            type: 'post',
            url: url,
            data: 'result=' + formData.checkresult + '&checkView=' + formData.checkview + '&gmIds=' + JSON.stringify(gmIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('gm,ajax succeed');
                    gm.grid.datagrid('reload');
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
        return ajaxResult;
    }
};