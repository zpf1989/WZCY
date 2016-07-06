//询价单信息格式化对象
var apFormatter = {
    //询价单状态
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
        }
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

//询价单列表对象
var askprice = {
    stateConf: {
        add: '0',
        edit: '1',
        view: '2'
    },
    grid: $('#grid'),
    gridSOItem: $('#gridAPItem'),
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
    cardFormUrl: 'AskPriceAdd.aspx',
    searchUrl: 'AskPriceService.asmx/GetList',
    init: function (funcType) {
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
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: askprice.addAskPrice
            }, "-", {
                id: 'btnView',
                text: '查看',
                iconCls: 'icon-search',
                handler: askprice.viewAskPrice
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: askprice.editAskPrice
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: askprice.deleteRowBatch
            }, "-", {
                id: 'btnSubmitToFirstCheck',
                text: '提交初审',
                iconCls: 'icon-search',
                handler: askprice.submitToFirstCheck
            }, "-", {
                id: 'btnSubmitToSecondCheck',
                text: '提交复审',
                iconCls: 'icon-edit',
                handler: askprice.submitToSecondCheck
            }, "-", {
                id: 'btnSubmitToReader',
                text: '设置分阅人',
                iconCls: 'icon-remove',
                handler: askprice.submitToRead
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
    //编辑
    addAskPrice: function () {
        location.href = askprice.cardFormUrl + '?state=0';
    },
    viewAskPrice: function () {
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要查看的数据');
            return;
        }
        if (checkedRows.length > 1) {
            $.messager.alert('提示', '请选择一条数据查看');
            return;
        }
        //location.href = askprice.cardFormUrl + '?' + encodeURI('state=2&apdata=' + JSON.stringify(checkedRows[0]));//
        location.href = askprice.cardFormUrl + '?' + encodeURI('state=2&apId=' + checkedRows[0].APID);//
    },
    editAskPrice: function () {
        var rows = askprice.grid.datagrid('getChecked');
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
        if (rows[0].State !== '1' && rows[0].State !== '4' && rows[0].State !== '7') {
            $.messager.alert('提示', '请选择编制、初审不通过或复审不通过状态的询价单');
            return;
        }
        //location.href = askprice.cardFormUrl + '?' + encodeURI('state=1&apdata=' + JSON.stringify(rows[0]));//
        location.href = askprice.cardFormUrl + '?' + encodeURI('state=1&apId=' + rows[0].APID);//
    },
    deleteRowBatch: function () {
        var delCheckedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(delCheckedRows) || delCheckedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }
        var illegalRows = $.grep(delCheckedRows, function (row, idx) {
            return row.State !== '1' && row.State !== '4'
                && row.State !== '7' && row.State !== '8';//过滤条件：非(编制、初审不通过、复审不通过、关闭)
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择编制、初审不通过、复审不通过或关闭状态的询价单');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var ids = [];
                $.each(delCheckedRows, function (index, row) {
                    ids.push(row.APID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('AskPriceService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        askprice.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = askprice.formSearch.serializeToJson(true);
        console.log(JSON.stringify(searchParams));
        //重新查询
        askprice.grid.datagrid("reload", searchParams);
    },
    //提交
    submitToFirstCheck: function () {
        //获取选中行
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.State !== '1' && row.State !== '4';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择编制或初审不通过状态的询价单');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, askprice.helpReceiver.submitToFirstChecker, null);
    },
    submitToSecondCheck: function () {
        //获取选中行
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        var illegalRows = $.grep(checkedRows, function (row, idx) {
            return row.State !== '3' && row.State !== '7';
        });
        if (illegalRows.length > 0) {
            $.messager.alert('提示', '请选择初审通过或复审不通过状态的询价单');
            return;
        }
        //提交初审
        showPopGridHelp(400, 300, true, helpInitializer.user, askprice.helpReceiver.submitToSecondChecker, null);
    },
    submitToRead: function () {
        //获取选中行
        var checkedRows = askprice.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择数据');
            return;
        }
        //提交分阅
        showPopGridHelp(400, 300, true, helpInitializer.user, askprice.helpReceiver.submitToReader, null);
    },
    helpReceiver: {
        submitToFirstChecker: function (userData) {
            if (gFunc.isNull(userData) || gFunc.isNull(userData.UserID)) {
                $.messager.alert('提示', '请选择初审人');
                return;
            }
            //获取选中行
            var checkedRows = askprice.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var apIds = [];
            $.each(checkedRows, function (index, row) {
                apIds.push(row.APID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'AskPriceService.asmx/SubmitToFirstChecker',
                data: 'userId=' + userData.UserID + '&apIds=' + JSON.stringify(apIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        askprice.grid.datagrid('reload');
                        ////console.log('askprice,ajax succeed');
                    } else {
                        ////console.log('askprice,ajax fail');
                        ajaxResult = false;
                    }
                },
                error: function () {
                    //console.log('askprice,ajax error');
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
            var checkedRows = askprice.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var apIds = [];
            $.each(checkedRows, function (index, row) {
                apIds.push(row.APID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'AskPriceService.asmx/SubmitToSecondChecker',
                data: 'userId=' + userData.UserID + '&apIds=' + JSON.stringify(apIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        askprice.grid.datagrid('reload');
                        //console.log('askprice,ajax succeed');
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
        },
        submitToReader: function (userData) {
            if (gFunc.isNull(userData) || gFunc.isNull(userData.UserID)) {
                $.messager.alert('提示', '请选择分阅人');
                return;
            }
            //获取选中行
            var checkedRows = askprice.grid.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择数据');
                return;
            }
            var apIds = [];
            $.each(checkedRows, function (index, row) {
                apIds.push(row.APID);
            });
            var ajaxResult = false;
            $.ajax({
                type: 'post',
                url: 'AskPriceService.asmx/SubmitToReader',
                data: 'userId=' + userData.UserID + '&apIds=' + JSON.stringify(apIds),
                async: false,//同步请求
                success: function (result) {
                    if (result && result.code) {
                        ajaxResult = true;
                        askprice.grid.datagrid('reload');
                        //console.log('askprice,ajax succeed');
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
        },
    }
}