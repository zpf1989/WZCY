//订单信息格式化对象
var apFormatter = {
    //订单状态
    apState: {
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
        },
    },
    //询价单类别
    apType: {
        src: [{ value: 'OffsetPrint', text: '胶印新产品' }, { value: 'SilkScreen', text: '丝印新产品' }],
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
var askprice = {
    stateConf: {
        add: 0,
        edit: 1,
        view: 2
    },
    grid: $('#grid'),
    gridAPItem: $('#gridAPItem'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtSearchAPCode: $('#txtSearchAPCode'),
    cbAPType: $('#cbAPType'),
    dateSearchAskDateBegin: $('#dateSearchAskDateBegin'),
    dateSearchAskDateEnd: $('#dateSearchAskDateEnd'),
    cardFormWidth: 700,
    cardFormHeight: 600,
    approvalFormWidth: 400,
    approvalFormHeight: 240,
    searchUrl: 'AskPriceService.asmx/GetList',
    checkUrl: 'AskPriceCheck.html',
    init: function () {
        askprice.initgrid();
        askprice.bindingEvents();
        askprice.formSearch.children('div').css({ 'float': 'left', 'padding-left': '8px' });
        askprice.cbAPType.combobox({
            data: apFormatter.apType.src,
            valueField: 'value',
            textField: 'text'
        });
    },
    //绑定（注册）事件
    bindingEvents: function () {
        askprice.btnSearch.click(askprice.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(askprice.grid, {
            title: '询价单列表',
            icon: 'icon-edit',
            key: 'APID',
            url: askprice.searchUrl,
            toolbar: [{
                id: 'btnFirstCheck',
                text: '初审',
                iconCls: 'icon-add',
                handler: askprice.firstCheck
            }, "-", {
                id: 'btnSecondCheck',
                text: '复审',
                iconCls: 'icon-search',
                handler: askprice.secondCheck
            }, "-", {
                id: 'btnRead',
                text: '分阅',
                iconCls: 'icon-edit',
                handler: askprice.read
            }, "-", {
                id: 'btnClose',
                text: '关闭',
                iconCls: 'icon-edit',
                handler: askprice.close
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'APID' },
                { field: 'APCode', title: '询价单编号', width: 100, align: 'center' },
                {
                    field: 'State', title: '询价单状态', width: 80, align: 'center',
                    formatter: function (value, row, index) { return apFormatter.apState.format(value); }
                },
                {
                    field: 'APType', title: '询价单类型', width: 80, align: 'center',
                    formatter: function (value, row, index) { return apFormatter.apType.format(value); }
                },
                { field: 'AskDate', title: '询价日期', align: 'center', width: 80, formatter: formatHandler.date.format },
                { field: 'ClientID' },
                { field: 'Client_Name', title: '客户名称', align: 'center', width: 100 },
                { field: 'Client_Contact', title: '客户联系人', align: 'center', width: 100 },
                { field: 'Client_Tel', title: '客户电话', align: 'center', width: 100 },
                { field: 'Client_Address', title: '客户地址', align: 'center', width: 100 },
                { field: 'PayTypeID' },
                { field: 'PayType_Name', title: '付款方式', align: 'center', width: 80 },
                { field: 'TrackDescription', title: '跟踪情况', align: 'center', width: 120 },
                { field: 'ClientSurvey', title: '客户调查', align: 'center', width: 120 },
                { field: 'APRemark', title: '备注', align: 'center', width: 100 },
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
                { field: 'ReaderName', title: '分阅人', align: 'center', width: 60 }
            ]],
            hidecols: ['APID', 'ClientID', 'PayTypeID', 'Creator', 'Editor', 'FirstChecker'],
            singleSelect: false
        });
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = askprice.formSearch.serializeToJson(true);
        //重新查询
        askprice.grid.datagrid("reload", searchParams);
    },
    //流程
    firstCheck: function () {
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要初审的数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, index) {
            return row.State !== '2';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择提交初审状态的订单');
            return;
        }
        //初审时，校验是否有权限（应该校验初审人是否为当前用户，后续实现）

        //初审，弹出窗体
        gFunc.showPopWindow({
            title: '询价单初审',
            width: askprice.approvalFormWidth,
            height: askprice.approvalFormHeight,
            url: askprice.checkUrl,
            isModal: true,
            funLoadCallback: function () {

            },
            funSubmitCallback: function () {
                return askprice.onCheckSubmit("first");
            }
        });
    },
    secondCheck: function () {
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要复审的数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, index) {
            return row.State !== '5';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择提交复审状态的订单');
            return;
        }
        //复审时，校验是否有权限（应该校验复审人是否为当前用户，后续实现）

        //复审，弹出窗体
        gFunc.showPopWindow({
            title: '询价单复审',
            width: askprice.approvalFormWidth,
            height: askprice.approvalFormHeight,
            url: askprice.checkUrl,
            isModal: true,
            funLoadCallback: function () {

            },
            funSubmitCallback: function () {
                return askprice.onCheckSubmit("second");
            }
        });
    },
    read: function () {
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要分阅的数据');
            return;
        }
        //分阅时，校验是否有权限（应该校验分阅人是否为当前用户，后续实现）

        //分阅
        var apIds = [];
        $.each(checkedRows, function (index, row) {
            apIds.push(row.APID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: 'AskPriceService.asmx/Read',
            data: 'apIds=' + JSON.stringify(apIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('askprice,ajax succeed');
                    askprice.grid.datagrid('reload');
                } else {
                    //console.log('askprice,ajax fail');
                    ajaxResult = false;
                }
            },
            error: function () {
                //console.log('askprice,ajax error');
                ajaxResult = false;
            }
        });
        return ajaxResult;
    },
    close: function () {
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        //关闭
        var apIds = [];
        $.each(checkedRows, function (index, row) {
            apIds.push(row.APID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: 'AskPriceService.asmx/Close',
            data: 'apIds=' + JSON.stringify(apIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('askprice,ajax succeed');
                    askprice.grid.datagrid('reload');
                } else {
                    //console.log('askprice,ajax fail');
                    ajaxResult = false;
                }
            },
            error: function () {
                //console.log('askprice,ajax error');
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
        var checkedRows = askprice.grid.datagrid('getChecked');
        var apIds = [];
        $.each(checkedRows, function (index, row) {
            apIds.push(row.APID);
        });
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        var url = "";
        if (checkType == "first") {
            url = 'AskPriceService.asmx/FirstCheck';
        } else if (checkType == "second") {
            url = 'AskPriceService.asmx/SecondCheck';
        } else if (checkType == "read") {
            url = 'AskPriceService.asmx/Read';
        }
        if (url == "") {
            //console.log('onCheckSubmit url cannot be null');
            return;
        }
        $.ajax({
            type: 'post',
            url: url,
            data: 'result=' + formData.checkresult + '&checkView=' + formData.checkview + '&apIds=' + JSON.stringify(apIds),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('askprice,ajax succeed');
                    askprice.grid.datagrid('reload');
                } else {
                    //console.log('askprice,ajax fail');
                    ajaxResult = false;
                }
            },
            error: function () {
                //console.log('askprice,ajax error');
                ajaxResult = false;
            }
        });
        return ajaxResult;
    }
}