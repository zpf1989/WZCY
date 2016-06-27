var soCard = {
    gridSOItem: $('#gridSOItem'),
    formSO: $('#editForm'),
    cardState: saleorder.stateConf.add,
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
    txtCardSOID: $('#txtSOID'),
    saveUrl: 'SaleOrderService.asmx/Save',
    searchUrlSOItem: 'SaleOrderItemService.asmx/GetOrderItems',
    bindingEvents: function () {
        soCard.btnCardBillType.click(soCard.eventHandler.btnHelpBillType);
        soCard.btnCardMaterial.click(soCard.eventHandler.btnHelpMaterial);
        soCard.btnCardUnit.click(soCard.eventHandler.btnHelpUnit);
        soCard.btnCardClient.click(soCard.eventHandler.btnHelpClient);
    },
    gridHandler: {
        insertRow: function () {
            if (soCard.cardState == saleorder.stateConf.view) {
                return;
            }
            var index = 0;
            soCard.gridSOItem.datagrid('insertRow', {
                index: index,
                row: {
                    SaleOrderID: soCard.txtCardSOID.val(),
                    SaleOrderItemID: '',
                    PlanQty: 0,
                    ActualQty: 0,
                    PlanCost: 0,
                    Remark: ''
                }
            });
            soCard.gridSOItem.datagrid('selectRow', index).datagrid('beginEdit', index);
        },
        editRowBatch: function () {
            if (soCard.cardState == saleorder.stateConf.view) {
                return;
            }
            var rows = soCard.gridSOItem.datagrid('getChecked');
            console.log(rows.length);
            if (gFunc.isNull(rows) || rows.length < 1) {
                $.messager.alert('提示', '请选择要修改的数据');
                return;
            }
            for (var idx = 0; idx < rows.length; idx++) {
                if (rows[idx].editing) {
                    continue;
                }
                var index = soCard.gridSOItem.datagrid('getRowIndex', rows[idx]);
                soCard.gridSOItem.datagrid('beginEdit', index);
            }
        },
        deleteRowBatch: function () {
            if (soCard.cardState == saleorder.stateConf.view) {
                return;
            }
            var checkedRows = soCard.gridSOItem.datagrid('getChecked');
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
                            return gFunc.isNull(row.SaleOrderItemID);//过滤条件：UserID为空
                        });
                        if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                            soCard.gridSOItem.datagrid('deleteRow', soCard.gridSOItem.datagrid('getRowIndex', checkedNewRows[0]));
                        } else {
                            break;
                        }
                    };
                    //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                    var checkedSavedRows = $.grep(checkedRows, function (row, idx) {
                        return !gFunc.isNull(row.SaleOrderItemID);//过滤条件：UserID不为空
                    });
                    if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                        return;
                    }
                    var ids = [];
                    $.each(checkedSavedRows, function (index, row) {
                        ids.push(row.SaleOrderItemID);
                    });
                    //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                    $.post('SaleOrderItemService.asmx/Delete', JSON.stringify(ids), function (result) {
                        if (result && result.code) {
                            //重新加载
                            soCard.gridSOItem.datagrid('reload');
                        }
                    });
                }
            });
        }
    },
    eventHandler: {
        btnHelpBillType: function () {
            showPopGridHelp(400, 300, true, helpInitializer.billType, soCard.helpReceiver.cardBillType, null);
        },
        btnHelpMaterial: function () {
            showPopGridHelp(500, 400, true, helpInitializer.materials, soCard.helpReceiver.cardMaterial, null);

        },
        btnHelpUnit: function () {
            showPopGridHelp(400, 300, true, helpInitializer.measureUnit, soCard.helpReceiver.cardUnit, null);

        },
        btnHelpClient: function () {
            showPopGridHelp(400, 300, true, helpInitializer.client, soCard.helpReceiver.cardClient, null);

        }
    },
    helpReceiver: {
        cardBillType: function (typeData) {
            soCard.txtCardBillTypeName.textbox('setValue', typeData.BillName);
            soCard.txtCardBillTypeID.val(typeData.BillID);
        },
        cardMaterial: function (mData) {
            soCard.txtCardMName.textbox('setValue', mData.MaterialName);
            soCard.txtCardMID.val(mData.MaterialID);
        },
        cardUnit: function (uData) {
            soCard.txtCardUName.textbox('setValue', uData.UnitName);
            soCard.txtCardUID.val(uData.UnitID);
        },
        cardClient: function (cData) {
            soCard.txtCardClientName.textbox('setValue', cData.ClientName);
            soCard.txtCardClientID.val(cData.ClientID);
        },
        soItemMaterial: function (mData, target) {
            if (gFunc.isNull(mData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, mData.MaterialName);
            //给关联列赋值
            var index = soCard.getRowIndexByEditor(target);
            var row = soCard.gridSOItem.datagrid('getRows')[index];
            soCard.gridSOItem.datagrid('updateRowCell', { field: 'MaterialID', index: index, value: mData.MaterialID });
        },
        soItemUnit: function (uData, target) {
            if (gFunc.isNull(uData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, uData.UnitName);
            //给关联列赋值
            var index = soCard.getRowIndexByEditor(target);
            var row = soCard.gridSOItem.datagrid('getRows')[index];
            soCard.gridSOItem.datagrid('updateRowCell', { field: 'PrimaryUnitID', index: index, value: uData.UnitID });
        }
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return soCard.gridSOItem.datagrid('getRowIndexByEditor', { element: target });
    },
    initCardForm: function (data, state) {
        //gFunc.formFunc.clearValidations('editForm');//清除表单验证
        soCard.bindingEvents();
        soCard.cardState = state;
        switch (state) {
            case saleorder.stateConf.edit://修改
            case saleorder.stateConf.view://查看
                //赋值
                soCard.txtCardSOID.val(data.SaleOrderID);
                $('#txtCardSOCode').textbox('setValue', data.SaleOrderCode);
                soCard.txtCardBillTypeName.textbox('setValue', data.BillType_Name);
                soCard.txtCardBillTypeID.val(data.BillTypeID);
                soCard.txtCardMName.textbox('setValue', data.Material_Name);
                soCard.txtCardMID.val(data.MaterialID);
                soCard.txtCardUName.textbox('setValue', data.SaleUnit_Name);
                soCard.txtCardUID.val(data.SaleUnitID);
                soCard.txtCardClientName.textbox('setValue', data.Client_Name);
                soCard.txtCardClientID.val(data.ClientID);
                $('#txtCardSaleDate').datebox('setValue', data.SaleDate);
                $('#txtCardFinishDate').datebox('setValue', data.FinishDate);
                $('#txtCardSOState').textbox('setValue', soFormatter.soState.format(data.SaleState));
                $('#txtCardRouting').textbox('setValue', data.Routing);
                $('#txtCardSaleQty').numberbox('setValue', data.SaleQty);
                $('#txtCardPrice').numberbox('setValue', data.PriceSalePrice);
                $('#txtCardSaleCost').numberbox('setValue', data.SaleCost);
                $('#txtCardCreatorName').textbox('setValue', data.Creator_Name);
                $('#txtCardCreateTime').textbox('setValue', data.CreateTime);
                $('#txtCardEditorName').textbox('setValue', data.Editor_Name);
                $('#txtCardEditTime').textbox('setValue', data.EditTime);
                $('#txtCardFirstCheckerName').textbox('setValue', data.FirstChecker_Name);
                $('#txtCardFirstCheckTime').textbox('setValue', data.FirstCheckTime);
                $('#txtCardFirstCheckView').textbox('setValue', data.FirstCheckView);
                $('#txtCardSecondCheckerName').textbox('setValue', data.SecondCheckerName);
                $('#txtCardReaderName').textbox('setValue', data.ReaderName);
                $('#txtCardRemark').textbox('setValue', data.Remark);
                //设置只读
                $('#txtCardSOCode').textbox('readonly', true);//编号只读
                var boolReadOnly = state == soCard.cardState.view ? true : false;
                $('#txtCardSaleDate').datebox('readonly', boolReadOnly);
                $('#txtCardFinishDate').datebox('readonly', boolReadOnly);
                $('#txtCardRouting').textbox('readonly', boolReadOnly);
                $('#txtCardSaleQty').numberbox('readonly', boolReadOnly);
                $('#txtCardPrice').numberbox('readonly', boolReadOnly);
                $('#txtCardSaleCost').numberbox('readonly', boolReadOnly);
                $('#txtCardRemark').textbox('readonly', boolReadOnly);
                if (boolReadOnly) {
                    soCard.btnCardBillType.attr({ 'disabled': 'disabled' });
                    soCard.btnCardMaterial.attr({ 'disabled': 'disabled' });
                    soCard.btnCardClient.attr({ 'disabled': 'disabled' });
                    soCard.btnCardUnit.attr({ 'disabled': 'disabled' });
                }
                break;
            case saleorder.stateConf.add://添加
            default:
                break;
        }
        //数值类型如果空则赋初值0
        if (!$('#txtCardSaleQty').textbox('getValue')) {
            //console.log('txtCardSaleQty,before set 0 ');
            $('#txtCardSaleQty').textbox('setValue', 0.00);
            //console.log('txtCardSaleQty,after set 0 ');
        }
        if (!$('#txtCardPrice').textbox('getValue')) {
            $('#txtCardPrice').textbox('setValue', 0.00);
        }
        if (!$('#txtCardSaleCost').textbox('getValue')) {
            $('#txtCardSaleCost').textbox('setValue', 0.00);
        }

        //初始化列表
        var soId = "";
        if (!gFunc.isNull(data)) {
            soId = data.SaleOrderID;
        }
        soCard.initCardGrid(soId);
    },
    initCardGrid: function (soID) {
        gFunc.initGridPublic(soCard.gridSOItem, {
            title: '',
            icon: 'icon-edit',
            key: 'SaleOrderItemID',
            url: (soCard.cardState != saleorder.stateConf.add) ? (soCard.searchUrlSOItem + '?SOID=' + soID) : "",
            toolbar: [{
                id: 'btnAddItem',
                text: '新增',
                iconCls: 'icon-add',
                handler: soCard.gridHandler.insertRow
            }, "-", {
                id: 'btnEditItem',
                text: '修改',
                iconCls: 'icon-edit',
                handler: soCard.gridHandler.editRowBatch
            }, "-", {
                id: 'btnDeleteItem',
                text: '删除',
                iconCls: 'icon-remove',
                handler: soCard.gridHandler.deleteRowBatch
            }, "-", {
                id: 'btnCancelEditItem',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    if (soCard.cardState == saleorder.stateConf.view) {
                        return;
                    }
                    soCard.gridSOItem.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'SaleOrderID' },
                { field: 'SaleOrderItemID' },
                { field: 'MaterialID' },
                {
                    field: 'Material_Name', title: '物料', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.materials, soCard.helpReceiver.soItemMaterial, target);
                            }
                        }
                    }
                },
                {
                    field: 'PlanQty', title: '计划数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'ActualQty', title: '实际数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'PlanCost', title: '金额', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                { field: 'PrimaryUnitID' },
                {
                    field: 'PrimaryUnit_Name', title: '计量单位', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.measureUnit, soCard.helpReceiver.soItemUnit, target);
                            }
                        }
                    }
                },
                {
                    field: 'Remark', title: '备注', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[1024]'
                        }
                    }
                }
            ]],
            hidecols: ['SaleOrderID', 'SaleOrderItemID', 'MaterialID', 'PrimaryUnitID'],
            singleSelect: false
        });
    },
    doSave: function () {
        //1、基本信息验证
        var valRst = gFunc.formFunc.validate("editForm");
        //console.log('valresult:' + valRst);
        if (!valRst) {
            $.messager.alert('提示', '数据校验失败，请检查输入！');
            return false;
        }
        //2、列表验证
        var editingRows = [];
        var allRows = soCard.gridSOItem.datagrid('getRows');
        if (!gFunc.isNull(allRows) && allRows.length > 0) {
            //逐行校验，只校验正在编辑的行
            for (var idx = 0; idx < allRows.length; idx++) {
                var row = allRows[idx];
                if (!row.editing) {
                    continue;
                }
                var rowIdx = soCard.gridSOItem.datagrid('getRowIndex', row);
                if (!soCard.gridSOItem.datagrid('validateRow', rowIdx)) {
                    $.messager.alert('提示', '数据校验失败，请检查输入！');
                    return false;
                }
                soCard.gridSOItem.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
                editingRows.push(row);
                soCard.gridSOItem.datagrid('beginEdit', rowIdx);
            }
        }

        //3、组合数据
        var formData = gFunc.formFunc.serializeToJson("editForm");
        formData.Items = editingRows;
        //console.log(JSON.stringify(formData));
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: soCard.saveUrl,
            data: JSON.stringify(formData),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
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
    }
};