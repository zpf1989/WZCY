var cardStateConf = {
    add: '0',
    edit: '1',
    view: '2'
};
//订单信息格式化对象
var boFormatter = {
    //订单状态
    boState: {
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
var boCard = {
    log: function (msg) {
        console.log(msg);
    },
    gridBOItem: $('#gridBOItem'),
    formBO: $('#editForm'),
    cardState: cardStateConf.add,
    btnSave: $('#btnSave'),
    btnBack: $('#btnBack'),
    btnCardSupplier: $('#btnCardHelpSupplier'),
    txtCardSupplierName: $('#txtCardSupplierName'),
    txtCardBillSupplierID: $('#txtCardBOSupplierID'),
    txtCardBOID: $('#txtBOID'),
    saveUrl: 'BuyOrderService.asmx/Save',
    searchUrlBOItem: 'BuyOrderItemSrv.asmx/GetOrderItems',
    boManageUrl: 'BuyOrderList.aspx',
    bindingEvents: function () {
        boCard.btnCardSupplier.click(boCard.eventHandler.btnHelpSupplier);
        boCard.btnSave.click(boCard.doSave);
        boCard.btnBack.click(function () {
            location.href = boCard.boManageUrl;
        });
    },
    gridHandler: {
        insertRow: function () {
            if (boCard.cardState == cardStateConf.view) {
                return;
            }
            var index = 0;
            boCard.gridBOItem.datagrid('insertRow', {
                index: index,
                row: {
                    BuyOrderID: boCard.txtCardBOID.val(),
                    BuyOrderItemID: '',
                    BuyQty: 0,
                    BuyCost: 0,
                    Remark: ''
                }
            });
            boCard.gridBOItem.datagrid('selectRow', index).datagrid('beginEdit', index);
        },
        editRowBatch: function () {
            if (boCard.cardState == cardStateConf.view) {
                return;
            }
            var rows = boCard.gridBOItem.datagrid('getChecked');
            //console.log(rows.length);
            if (gFunc.isNull(rows) || rows.length < 1) {
                $.messager.alert('提示', '请选择要修改的数据');
                return;
            }
            for (var idx = 0; idx < rows.length; idx++) {
                if (rows[idx].editing) {
                    continue;
                }
                var index = boCard.gridBOItem.datagrid('getRowIndex', rows[idx]);
                boCard.gridBOItem.datagrid('beginEdit', index);
            }
        },
        deleteRowBatch: function () {
            if (boCard.cardState == cardStateConf.view) {
                return;
            }
            var checkedRows = boCard.gridBOItem.datagrid('getChecked');
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
                            return gFunc.isNull(row.BuyOrderItemID);//过滤条件：UserID为空
                        });
                        if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                            boCard.gridBOItem.datagrid('deleteRow', boCard.gridBOItem.datagrid('getRowIndex', checkedNewRows[0]));
                        } else {
                            break;
                        }
                    };
                    //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                    var checkedSavedRows = $.grep(checkedRows, function (row, idx) {
                        return !gFunc.isNull(row.BuyOrderItemID);//过滤条件：UserID不为空
                    });
                    if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                        return;
                    }
                    var ids = [];
                    $.each(checkedSavedRows, function (index, row) {
                        ids.push(row.BuyOrderItemID);
                    });
                    //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                    $.post('BuyOrderItemSrv.asmx/Delete', JSON.stringify(ids), function (result) {
                        if (result && result.code) {
                            //重新加载
                            boCard.gridBOItem.datagrid('reload');
                        }
                    });
                }
            });
        }
    },
    eventHandler: {
        btnHelpSupplier: function () {
            showPopGridHelp(500, 300, true, helpInitializer.supplier, boCard.helpReceiver.cardSupplier, null);
        },
        btnHelpMaterial: function () {
            showPopGridHelp(500, 400, true, helpInitializer.materials, boCard.helpReceiver.cardMaterial, null);

        },
        btnHelpUnit: function () {
            showPopGridHelp(400, 300, true, helpInitializer.measureUnit, boCard.helpReceiver.cardUnit, null);

        }
    },
    helpReceiver: {
        cardSupplier: function (supplierData) {
            boCard.txtCardSupplierName.textbox('setValue', supplierData.SupplierName);
            boCard.txtCardBillSupplierID.val(supplierData.SupplierID);
        },
        boItemMaterial: function (mData, target) {
            if (gFunc.isNull(mData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, mData.MaterialName);
            //给关联列赋值
            var index = boCard.getRowIndexByEditor(target);
            var row = boCard.gridBOItem.datagrid('getRows')[index];
            boCard.gridBOItem.datagrid('updateRowCell', { field: 'MaterialID', index: index, value: mData.MaterialID });
        },
        boItemUnit: function (uData, target) {
            if (gFunc.isNull(uData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, uData.UnitName);
            //给关联列赋值
            var index = boCard.getRowIndexByEditor(target);
            var row = boCard.gridBOItem.datagrid('getRows')[index];
            boCard.gridBOItem.datagrid('updateRowCell', { field: 'PrimaryUnitID', index: index, value: uData.UnitID });
        }
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return boCard.gridBOItem.datagrid('getRowIndexByEditor', { element: target });
    },
    initCardForm: function (boId, state) {
        boCard.bindingEvents();
        if (!gFunc.isNull(state)) {
            boCard.cardState = state;
        }
        switch (state) {
            case cardStateConf.edit://修改
            case cardStateConf.view://查看
                var ajaxResult = false;
                $.ajax({
                    type: 'post',
                    url: 'BuyOrderService.asmx/GetBuyOrderById',
                    data: 'BOID=' + boId,
                    async: false,//同步请求
                    success: function (result) {
                        if (result && result.code) {
                            ajaxResult = true;
                            boCard.setFormData(result.data, state);
                            ////console.log('askprice,ajax succeed');
                        } else {
                            console.log('buyorder,ajax fail:' + result.msg);
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
                boCard.initCardGrid("");
                break;
        }
    },
    setFormData: function (data, state) {
        //赋值
        ////console.log(data);
        boCard.txtCardBOID.val(data.BuyOrderID);
        $('#txtCardBOCode').textbox('setValue', data.BuyOrderCode);
        boCard.txtCardSupplierName.textbox('setValue', data.Supplier_Name);
        boCard.txtCardBillSupplierID.val(data.SupplierID);
        $('#txtCardBuyOrderDate').datebox('setValue', data.BuyOrderDate);
        $('#txtCardDeliveryDate').datebox('setValue', data.DeliveryDate);
        $('#txtCardBOState').textbox('setValue', boFormatter.boState.format(data.OrderState));
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
        $('#txtCardBOCode').textbox('readonly', true);//编号只读
        ////console.log(typeof(state));
        var boolReadOnly = state == cardStateConf.view ? true : false;
        ////console.log('boolReadOnly:' + boolReadOnly);
        if (boolReadOnly) {
            ////console.log('boCard.btnSave.hide();');
            boCard.btnSave.hide();
        }
        $('#txtCardBuyOrderDate').datebox('readonly', boolReadOnly);
        $('#txtCardDeliveryDate').datebox('readonly', boolReadOnly);
        $('#txtCardRemark').textbox('readonly', boolReadOnly);
        if (boolReadOnly) {
            boCard.btnCardSupplier.attr({ 'disabled': 'disabled' });
        }

        //初始化列表
        var boId = "";
        if (!gFunc.isNull(data)) {
            boId = data.BuyOrderID;
        }
        boCard.initCardGrid(boId);
    },
    initCardGrid: function (boID) {
        gFunc.initGridPublic(boCard.gridBOItem, {
            title: '',
            icon: 'icon-edit',
            key: 'BuyOrderItemID',
            url: (boCard.cardState != cardStateConf.add) ? (boCard.searchUrlBOItem + '?BOID=' + boID) : "",
            toolbar: [{
                id: 'btnAddItem',
                text: '新增',
                iconCls: 'icon-add',
                handler: boCard.gridHandler.insertRow
            }, "-", {
                id: 'btnEditItem',
                text: '修改',
                iconCls: 'icon-edit',
                handler: boCard.gridHandler.editRowBatch
            }, "-", {
                id: 'btnDeleteItem',
                text: '删除',
                iconCls: 'icon-remove',
                handler: boCard.gridHandler.deleteRowBatch
            }, "-", {
                id: 'btnCancelEditItem',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    if (boCard.cardState == cardStateConf.view) {
                        return;
                    }
                    boCard.gridBOItem.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'BuyOrderID' },
                { field: 'BuyOrderItemID' },
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
                                showPopGridHelp(400, 300, true, helpInitializer.materials, boCard.helpReceiver.boItemMaterial, target);
                            }
                        }
                    }
                },
                {
                    field: 'BuyQty', title: '采购数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'BuyCost', title: '采购金额', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                { field: 'BuyUnitID' },
                {
                    field: 'BuyUnitID_Name', title: '计量单位', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.measureUnit, boCard.helpReceiver.boItemUnit, target);
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
            hidecols: ['BuyOrderID', 'BuyOrderItemID', 'MaterialID', 'BuyUnitID'],
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
        var allRows = boCard.gridBOItem.datagrid('getRows');
        if (!gFunc.isNull(allRows) && allRows.length > 0) {
            //逐行校验，只校验正在编辑的行
            for (var idx = 0; idx < allRows.length; idx++) {
                var row = allRows[idx];
                if (!row.editing) {
                    continue;
                }
                var rowIdx = boCard.gridBOItem.datagrid('getRowIndex', row);
                if (!boCard.gridBOItem.datagrid('validateRow', rowIdx)) {
                    $.messager.alert('提示', '数据校验失败，请检查输入！');
                    return false;
                }
                boCard.gridBOItem.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
                editingRows.push(row);
                boCard.gridBOItem.datagrid('beginEdit', rowIdx);
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
            url: boCard.saveUrl,
            data: JSON.stringify(formData),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('saleorder,ajax succeed');
                    location.href = boCard.boManageUrl;
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