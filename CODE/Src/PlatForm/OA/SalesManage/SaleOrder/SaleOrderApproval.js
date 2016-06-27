//订单信息格式化对象
var soFormatter = {
    //订单状态
    soState: {
        src: [{ value: '1', text: '编制' }, { value: '2', text: '提交初审' }, { value: '3', text: '初审通过' }, { value: '4', text: '初审不通过' }, { value: '5', text: '提交复审' }, { value: '6', text: '复审通过' }, { value: '7', text: '复审不通过' }, { value: '8', text: '关闭' }],
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
    stateConf: {
        add: 0,
        edit: 1,
        view: 2
    },
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtSearchBillTypeName: $('#txtSearchBillTypeName'),
    txtSearchBillTypeID: $('#txtSearchBillTypeID'),
    btnSearchBillType: $('#btnSearchBillType'),
    btnSearchBillType: $('#btnSearchBillType'),
    dateSearchSaleDateBegin: $('#dateSearchSaleDateBegin'),
    dateSearchSaleDateEnd: $('#dateSearchSaleDateEnd'),
    cardFormWidth: 700,
    cardFormHeight: 600,
    approvalFormWidth: 400,
    approvalFormHeight: 240,
    searchUrl: 'SaleOrderService.asmx/GetList',
    checkUrl: 'SaleOrderCheck.html',
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
                id: 'btnView',
                text: '查看',
                iconCls: 'icon-remove',
                handler: saleorder.viewSaleOrder
            }, '-', {
                id: 'btnFirstCheck',
                text: '初审',
                iconCls: 'icon-add',
                handler: saleorder.firstCheck
            }, "-", {
                id: 'btnSecondCheck',
                text: '复审',
                iconCls: 'icon-search',
                handler: saleorder.secondCheck
            }, "-", {
                id: 'btnRead',
                text: '分阅',
                iconCls: 'icon-edit',
                handler: saleorder.read
            }, "-", {
                id: 'btnClose',
                text: '关闭',
                iconCls: 'icon-edit',
                handler: saleorder.close
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
                soCard.initCardForm(checkedRows[0], saleorder.stateConf.view);
            }
        });
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = saleorder.formSearch.serializeToJson(true);
        //重新查询
        saleorder.grid.datagrid("reload", searchParams);
    },
    //流程
    firstCheck: function () {
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要初审的数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, index) {
            return row.SaleState !== '2';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择提交初审状态的订单');
            return;
        }
        //初审时，校验是否有权限（应该校验初审人是否为当前用户，后续实现）

        //初审，弹出窗体
        gFunc.showPopWindow({
            title: '销售订单初审',
            width: saleorder.approvalFormWidth,
            height: saleorder.approvalFormHeight,
            url: saleorder.checkUrl,
            isModal: true,
            funLoadCallback: function () {

            },
            funSubmitCallback: function () {
                return saleorder.onCheckSubmit("first");
            }
        });
    },
    secondCheck: function () {
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要复审的数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, index) {
            return row.SaleState !== '5';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择提交复审状态的订单');
            return;
        }
        //复审时，校验是否有权限（应该校验复审人是否为当前用户，后续实现）

        //复审，弹出窗体
        gFunc.showPopWindow({
            title: '销售订单复审',
            width: saleorder.approvalFormWidth,
            height: saleorder.approvalFormHeight,
            url: saleorder.checkUrl,
            isModal: true,
            funLoadCallback: function () {

            },
            funSubmitCallback: function () {
                return saleorder.onCheckSubmit("second");
            }
        });
    },
    read: function () {
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要分阅的数据');
            return;
        }
        //分阅时，校验是否有权限（应该校验分阅人是否为当前用户，后续实现）

        //分阅
        var soIds = [];
        $.each(checkedRows, function (index, row) {
            soIds.push(row.SaleOrderID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: 'SaleOrderService.asmx/Read',
            data: 'soIds=' + JSON.stringify(soIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    console.log('saleorder,ajax succeed');
                    saleorder.grid.datagrid('reload');
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
        return ajaxResult;
    },
    close: function () {
        var checkedRows = saleorder.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        //关闭
        var soIds = [];
        $.each(checkedRows, function (index, row) {
            soIds.push(row.SaleOrderID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: 'SaleOrderService.asmx/Close',
            data: 'soIds=' + JSON.stringify(soIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    console.log('saleorder,ajax succeed');
                    saleorder.grid.datagrid('reload');
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
        var checkedRows = saleorder.grid.datagrid('getChecked');
        var soIds = [];
        $.each(checkedRows, function (index, row) {
            soIds.push(row.SaleOrderID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        var url = "";
        if (checkType == "first") {
            url = 'SaleOrderService.asmx/FirstCheck';
        } else if (checkType == "second") {
            url = 'SaleOrderService.asmx/SecondCheck';
        } else if (checkType == "read") {
            url = 'SaleOrderService.asmx/Read';
        }
        if (url == "") {
            console.log('onCheckSubmit url cannot be null');
            return;
        }
        $.ajax({
            type: 'post',
            url: url,
            data: 'result=' + formData.checkresult + '&checkView=' + formData.checkview + '&soIds=' + JSON.stringify(soIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    console.log('saleorder,ajax succeed');
                    saleorder.grid.datagrid('reload');
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
        return ajaxResult;
    },
    onClickSearchBillType: function () {
        showPopGridHelp(400, 300, true, helpInitializer.billType, saleorder.helpReceiver.searchBillType, null);
    },
    helpReceiver: {
        searchBillType: function (typeData) {
            //注意：必须先给name赋值，因为它会触发onChange事件，会把id冲掉
            saleorder.txtSearchBillTypeName.textbox('setValue', typeData.BillName);
            saleorder.txtSearchBillTypeID.val(typeData.BillID);
        }
    }
}