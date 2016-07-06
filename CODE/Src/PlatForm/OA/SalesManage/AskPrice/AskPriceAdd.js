var cardStateConf = {
    add: '0',
    edit: '1',
    view: '2'
};
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
var apCard = {
    log: function (msg) {
        //console.log(msg);
    },
    gridAPItem: $('#gridAPItem'),
    formAP: $('#editForm'),
    cardState: cardStateConf.add,
    btnSave: $('#btnSave'),
    btnBack: $('#btnBack'),
    btnCardHelpClient: $('#btnCardHelpClient'),
    txtCardClientID: $('#txtCardClientID'),
    txtCardClientName: $('#txtCardClientName'),
    lblClientContact: $('#lblClientContact'),
    lblClientTel: $('#lblClientTel'),
    lblClientAddress: $('#lblClientAddress'),
    btnCardHelpPayType: $('#btnCardHelpPayType'),
    txtCardPayTypeID: $('#txtCardPayTypeID'),
    txtCardPayTypeName: $('#txtCardPayTypeName'),
    txtCardAPID: $('#txtCardAPID'),
    cbCardAPType: $('#cbCardAPType'),
    saveUrl: 'AskPriceService.asmx/Save',
    searchUrlAPItem: 'AskPriceItemService.asmx/GetAPItems',
    apManageUrl: 'AskPrice.aspx',
    bindingEvents: function () {
        apCard.btnCardHelpClient.click(apCard.eventHandler.btnHelpClient);
        apCard.btnCardHelpPayType.click(apCard.eventHandler.btnHelpPayType);

        apCard.btnSave.click(apCard.doSave);
        apCard.btnBack.click(function () {
            location.href = apCard.apManageUrl;
        });
    },
    gridHandler: {
        insertRow: function () {
            if (apCard.cardState == cardStateConf.view) {
                return;
            }
            var index = 0;
            apCard.gridAPItem.datagrid('insertRow', {
                index: index,
                row: {
                    APID: apCard.txtCardAPID.val(),
                    APItemID: '',
                    PlanPrice: 0,
                    Qty: 0,
                    ActualPrice: 0,
                }
            });
            apCard.gridAPItem.datagrid('selectRow', index).datagrid('beginEdit', index);
        },
        editRowBatch: function () {
            if (apCard.cardState == cardStateConf.view) {
                return;
            }
            var rows = apCard.gridAPItem.datagrid('getChecked');
            ////console.log(rows.length);
            if (gFunc.isNull(rows) || rows.length < 1) {
                $.messager.alert('提示', '请选择要修改的数据');
                return;
            }
            for (var idx = 0; idx < rows.length; idx++) {
                if (rows[idx].editing) {
                    continue;
                }
                var index = apCard.gridAPItem.datagrid('getRowIndex', rows[idx]);
                apCard.gridAPItem.datagrid('beginEdit', index);
            }
        },
        deleteRowBatch: function () {
            if (apCard.cardState == cardStateConf.view) {
                return;
            }
            var checkedRows = apCard.gridAPItem.datagrid('getChecked');
            if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
                $.messager.alert('提示', '请选择要删除的数据');
                return;
            }

            $.messager.confirm('询问', '删除数据将不能恢复，继续删除吗？', function (result) {
                if (result) {
                    //1、删除选中行中新增的部分（这部分直接客户端删除即可）
                    while (true) {
                        //因为删除一行后，checkedNewRows会变化，所以需要从checkedRows中重新筛选新增行，并且，每次只删除checkedRows[0]即可
                        //$.grep是jquery的函数，用于过滤数组元素
                        var checkedNewRows = $.grep(checkedRows, function (row, idx) {
                            return gFunc.isNull(row.APItemID);//过滤条件：APItemID为空
                        });
                        if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                            apCard.gridAPItem.datagrid('deleteRow', apCard.gridAPItem.datagrid('getRowIndex', checkedNewRows[0]));
                        } else {
                            break;
                        }
                    };
                    //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                    var checkedSavedRows = $.grep(checkedRows, function (row, idx) {
                        return !gFunc.isNull(row.APItemID);//过滤条件：UserID不为空
                    });
                    if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                        return;
                    }
                    var ids = [];
                    $.each(checkedSavedRows, function (index, row) {
                        ids.push(row.APItemID);
                    });
                    //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                    $.post('AskPriceItemService.asmx/Delete', JSON.stringify(ids), function (result) {
                        if (result && result.code) {
                            //重新加载
                            apCard.gridAPItem.datagrid('reload');
                        }
                    });
                }
            });
        }
    },
    eventHandler: {
        btnHelpPayType: function () {
            showPopGridHelp(400, 300, true, helpInitializer.payType, apCard.helpReceiver.cardPayType, null);

        },
        btnHelpClient: function () {
            showPopGridHelp(400, 300, true, helpInitializer.client, apCard.helpReceiver.cardClient, null);
        }
    },
    helpReceiver: {
        cardPayType: function (typeData) {
            apCard.txtCardPayTypeName.textbox('setValue', typeData.PayTypeName);
            apCard.txtCardPayTypeID.val(typeData.PayTypeID);
        },
        cardClient: function (cData) {
            apCard.txtCardClientName.textbox('setValue', cData.ClientName);
            apCard.lblClientContact.html(cData.Contactor);
            apCard.lblClientTel.html(cData.ClientTel);
            apCard.lblClientAddress.html(cData.ClientAddress);
            apCard.txtCardClientID.val(cData.ClientID);
        },
        apItemMaterial: function (mData, target) {
            if (gFunc.isNull(mData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, mData.MaterialCode);
            //给关联列赋值
            var index = apCard.getRowIndexByEditor(target);
            var row = apCard.gridAPItem.datagrid('getRows')[index];
            apCard.gridAPItem.datagrid('updateRowCell', { field: 'MaterialID', index: index, value: mData.MaterialID });
            apCard.gridAPItem.datagrid('updateRowCell', { field: 'Material_Name', index: index, value: mData.MaterialName });
            apCard.gridAPItem.datagrid('updateRowCell', { field: 'Material_Specs', index: index, value: mData.Specs });
        },
        apItemUnit: function (uData, target) {
            if (gFunc.isNull(uData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, uData.UnitName);
            //给关联列赋值
            var index = apCard.getRowIndexByEditor(target);
            var row = apCard.gridAPItem.datagrid('getRows')[index];
            apCard.gridAPItem.datagrid('updateRowCell', { field: 'UnitID', index: index, value: uData.UnitID });
        }
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return apCard.gridAPItem.datagrid('getRowIndexByEditor', { element: target });
    },
    initCardForm: function (apId, state) {
        apCard.bindingEvents();
        apCard.initComboBox();
        if (!gFunc.isNull(state)) {
            apCard.cardState = state;
        }
        switch (state) {
            case cardStateConf.edit://修改
            case cardStateConf.view://查看
                var ajaxResult = false;
                $.ajax({
                    type: 'post',
                    url: 'AskPriceService.asmx/GetAskPriceById',
                    data: 'apId=' + apId,
                    async: false,//同步请求
                    success: function (result) {
                        if (result && result.code) {
                            ajaxResult = true;
                            apCard.setFormData(result.data, state);
                            ////console.log('askprice,ajax succeed');
                        } else {
                            console.log('askprice,ajax fail:' + result.msg);
                            ajaxResult = false;
                        }
                    },
                    error: function () {
                        //console.log('askprice,ajax error');
                        ajaxResult = false;
                    }
                });
                break;
            case cardStateConf.add://添加
            default:
                apCard.initCardGrid("");
                break;
        }
    },
    setFormData: function (data, state) {
        //赋值
        ////console.log(data);
        apCard.txtCardAPID.val(data.APID);
        $('#txtCardAPCode').textbox('setValue', data.APCode);
        apCard.cbCardAPType.combobox('setValue', data.APType);
        $('#txtCardAskDate').datebox('setValue', data.AskDate);
        apCard.txtCardClientID.val(data.ClientID);
        apCard.txtCardClientName.textbox('setValue', data.Client_Name);
        apCard.lblClientAddress.html(data.Client_Address);
        apCard.lblClientContact.html(data.Client_Contact);
        apCard.lblClientTel.html(data.Client_Tel);
        apCard.txtCardPayTypeName.textbox('setValue', data.PayType_Name);
        apCard.txtCardPayTypeID.html(data.PayTypeID);
        $('#txtCardTrackDescription').textbox('setValue', data.TrackDescription);
        $('#txtCardClientSurvey').textbox('setValue', data.ClientSurvey);
        $('#txtCardAPRemark').textbox('setValue', data.APRemark);

        $('#lblCardCreatorName').html(data.Creator_Name);
        $('#lblCardCreateTime').html(data.CreateTime);
        $('#lblCardEditorName').html(data.Editor_Name);
        $('#lblCardEditTime').html(data.EditTime);
        $('#lblCardFirstCheckerName').html(data.FirstChecker_Name);
        $('#lblCardFirstCheckTime').html(data.FirstCheckTime);
        $('#lblCardFirstCheckView').html(data.FirstCheckView);
        $('#lblCardSecondCheckerName').html(data.SecondCheckerName);
        $('#lblCardReaderName').html(data.ReaderName);
        $('#lblCardState').html(apFormatter.apState.format(data.State));

        //设置只读
        $('#txtCardAPCode').textbox('readonly', true);//编号只读
        ////console.log(typeof(state));
        var boolReadOnly = state == cardStateConf.view ? true : false;
        ////console.log('boolReadOnly:' + boolReadOnly);
        if (boolReadOnly) {
            ////console.log('apCard.btnSave.hide();');
            apCard.btnSave.hide();
        }
        apCard.cbCardAPType.combobox('readonly', boolReadOnly);
        $('#txtCardAskDate').datebox('readonly', boolReadOnly);
        $('#txtCardClientName').textbox('readonly', boolReadOnly);
        $('#txtCardTrackDescription').textbox('readonly', boolReadOnly);
        $('#txtCardClientSurvey').textbox('readonly', boolReadOnly);
        $('#txtCardAPRemark').textbox('readonly', boolReadOnly);

        if (boolReadOnly) {
            apCard.btnCardHelpClient.attr({ 'disabled': 'disabled' });
            apCard.btnCardHelpPayType.attr({ 'disabled': 'disabled' });
        }

        //子表

        //初始化列表
        var apId = "";
        if (!gFunc.isNull(data)) {
            apId = data.APID;
        }
        apCard.initCardGrid(apId);
    },
    initComboBox: function () {
        apCard.cbCardAPType.combobox({
            data: apFormatter.apType.src,
            valueField: 'value',
            textField: 'text'
        });
    },
    initCardGrid: function (apId) {
        gFunc.initGridPublic(apCard.gridAPItem, {
            title: '',
            icon: 'icon-edit',
            key: 'APItemID',
            url: (apCard.cardState != cardStateConf.add) ? (apCard.searchUrlAPItem + '?apId=' + apId) : "",
            toolbar: [{
                id: 'btnAddItem',
                text: '新增',
                iconCls: 'icon-add',
                handler: apCard.gridHandler.insertRow
            }, "-", {
                id: 'btnEditItem',
                text: '修改',
                iconCls: 'icon-edit',
                handler: apCard.gridHandler.editRowBatch
            }, "-", {
                id: 'btnDeleteItem',
                text: '删除',
                iconCls: 'icon-remove',
                handler: apCard.gridHandler.deleteRowBatch
            }, "-", {
                id: 'btnCancelEditItem',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    if (apCard.cardState == cardStateConf.view) {
                        return;
                    }
                    apCard.gridAPItem.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'APID' },
                { field: 'APItemID' },
                { field: 'MaterialID' },
                {
                    field: 'Material_Code', title: '产品编号', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.materials, apCard.helpReceiver.apItemMaterial, target);
                            }
                        }
                    }
                },
                { field: 'Material_Name', title: '产品名称', align: 'center', width: 100 },
                { field: 'Material_Specs', title: '规格型号', align: 'center', width: 80 },
                {
                    field: 'Routing', title: '工艺', align: 'center', width: 80, editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[100]'
                        }
                    }
                },
                {
                    field: 'PlanPrice', title: '价格', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[18]'
                        }
                    }
                },
                {
                    field: 'Qty', title: '数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[18]'
                        }
                    }
                },
                { field: 'UnitID' },
                {
                    field: 'Unit_Name', title: '计量单位', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.measureUnit, apCard.helpReceiver.apItemUnit, target);
                            }
                        }
                    }
                },
                {
                    field: 'ActualPrice', title: '金额', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[18]'
                        }
                    }
                },
                {
                    field: 'IsTax', title: '是否含税', width: 60, align: 'center',
                    formatter: function (value, row, index) {
                        return formatHandler.trueOrFalse.format(value);
                    },
                    editor: {
                        type: 'combobox',
                        options: {
                            editable: false,
                            valueField: 'value',
                            textField: 'text',
                            data: formatHandler.trueOrFalse.src
                        }
                    }
                },
                {
                    field: 'IsShipping', title: '是否含运费', 80: 100, align: 'center',
                    formatter: function (value, row, index) {
                        return formatHandler.trueOrFalse.format(value);
                    },
                    editor: {
                        type: 'combobox',
                        options: {
                            editable: false,
                            valueField: 'value',
                            textField: 'text',
                            data: formatHandler.trueOrFalse.src
                        }
                    }
                }
            ]],
            hidecols: ['APID', 'APItemID', 'MaterialID', 'UnitID'],
            singleSelect: false
        });
    },
    doSave: function () {
        //1、基本信息验证
        var valRst = gFunc.formFunc.validate("editForm");
        ////console.log('valresult:' + valRst);
        if (!valRst) {
            $.messager.alert('提示', '数据校验失败，请检查输入！');
            return false;
        }
        //2、列表验证
        var editingRows = [];
        var allRows = apCard.gridAPItem.datagrid('getRows');
        if (!gFunc.isNull(allRows) && allRows.length > 0) {
            //逐行校验，只校验正在编辑的行
            for (var idx = 0; idx < allRows.length; idx++) {
                var row = allRows[idx];
                if (!row.editing) {
                    continue;
                }
                var rowIdx = apCard.gridAPItem.datagrid('getRowIndex', row);
                if (!apCard.gridAPItem.datagrid('validateRow', rowIdx)) {
                    $.messager.alert('提示', '数据校验失败，请检查输入！');
                    return false;
                }
                apCard.gridAPItem.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
                editingRows.push(row);
                apCard.gridAPItem.datagrid('beginEdit', rowIdx);
            }
        }

        //3、组合数据
        var formData = gFunc.formFunc.serializeToJson("editForm");
        formData.Items = editingRows;
        ////console.log(JSON.stringify(formData));
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: apCard.saveUrl,
            data: JSON.stringify(formData),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('saleorder,ajax succeed');
                    location.href = apCard.apManageUrl;
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
        //console.log('saleorder,doSave over');
        return ajaxResult;
    }
}