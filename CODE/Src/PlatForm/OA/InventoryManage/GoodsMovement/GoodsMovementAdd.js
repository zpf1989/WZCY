var cardStateConf = {
    add: '0',
    edit: '1',
    view: '2'
};
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
var gmCard = {
    log: function (msg) {
        console.log(msg);
    },
    gridGMItem: $('#gridGMItem'),
    formSO: $('#editForm'),
    cardState: cardStateConf.add,
    btnSave: $('#btnSave'),
    btnBack: $('#btnBack'),

    cbCardBusinessType: $('#cbCardBusinessType'),
    cbCardMoveType: $('#cbCardMoveType'),

    btnCardHelpRecDept: $('#btnCardHelpRecDept'),
    txtCardRecDeptName: $('#txtCardRecDeptName'),
    txtCardRecDeptID: $('#txtCardRecDeptID'),

    btnCardHelpRecHandler: $('#btnCardHelpRecHandler'),
    txtCardRecHandlerName: $('#txtCardRecHandlerName'),
    txtCardRecHandler: $('#txtCardRecHandler'),

    btnCardHelpRecWH: $('#btnCardHelpRecWH'),
    txtCardRecWHName: $('#txtCardRecWHName'),
    txtCardRecWHID: $('#txtCardRecWHID'),

    btnCardHelpRecWHEmp: $('#btnCardHelpRecWHEmp'),
    txtCardRecWHEmpName: $('#txtCardRecWHEmpName'),
    txtCardRecWHEmpID: $('#txtCardRecWHEmpID'),

    btnCardHelpIssDept: $('#btnCardHelpIssDept'),
    txtCardIssDeptName: $('#txtCardIssDeptName'),
    txtCardIssDeptID: $('#txtCardIssDeptID'),

    btnCardHelpIssHandler: $('#btnCardHelpIssHandler'),
    txtCardIssHandlerName: $('#txtCardIssHandlerName'),
    txtCardIssHandler: $('#txtCardIssHandler'),

    btnCardHelpIssWH: $('#btnCardHelpIssWH'),
    txtCardIssWHName: $('#txtCardIssWHName'),
    txtCardIssWHID: $('#txtCardIssWHID'),

    btnCardHelpIssWHEmp: $('#btnCardHelpIssWHEmp'),
    txtCardIssWHEmpName: $('#txtCardIssWHEmpName'),
    txtCardIssWHEmpID: $('#txtCardIssWHEmpID'),

    btnCardHelpPurDept: $('#btnCardHelpPurDept'),
    txtCardPurDeptName: $('#txtCardPurDeptName'),
    txtCardPurDeptID: $('#txtCardPurDeptID'),

    btnCardHelpPurEmp: $('#btnCardHelpPurEmp'),
    txtCardPurEmpName: $('#txtCardPurEmpName'),
    txtCardPurEmpID: $('#txtCardPurEmpID'),

    btnCardHelpSupplier: $('#btnCardHelpSupplier'),
    txtCardSupplierName: $('#txtCardSupplierName'),
    txtCardSupplierID: $('#txtCardSupplierID'),

    btnCardHelpSalesDep: $('#btnCardHelpSalesDep'),
    txtCardSalesDepName: $('#txtCardSalesDepName'),
    txtCardSalesDepID: $('#txtCardSalesDepID'),

    btnCardHelpSalesEmp: $('#btnCardHelpSalesEmp'),
    txtCardSalesEmpName: $('#txtCardSalesEmpName'),
    txtCardSalesEmpID: $('#txtCardSalesEmpID'),

    btnCardHelpCustomer: $('#btnCardHelpCustomer'),
    txtCardCustomerName: $('#txtCardCustomerName'),
    txtCardCustomerID: $('#txtCardCustomerID'),

    btnCardHelpProDep: $('#btnCardHelpProDep'),
    txtCardProDepName: $('#txtCardProDepName'),
    txtCardProDepID: $('#txtCardProDepID'),

    btnCardHelpProEmp: $('#btnCardHelpProEmp'),
    txtCardProEmpName: $('#txtCardProEmpName'),
    txtCardProEmpID: $('#txtCardProEmpID'),

    btnCardHelpConDep: $('#btnCardHelpConDep'),
    txtCardConDepName: $('#txtCardConDepName'),
    txtCardConDepID: $('#txtCardConDepID'),

    btnCardHelpConEmp: $('#btnCardHelpConEmp'),
    txtCardConEmpName: $('#txtCardConEmpName'),
    txtCardConEmpID: $('#txtCardConEmpID'),

    txtCardGMID: $('#txtGMID'),
    saveUrl: 'GoodsMovementService.asmx/Save',
    searchUrlGMItem: 'GoodsMovementItemService.asmx/GetGMItems',
    gmManageUrl: 'GoodsMovement.aspx',
    bindingEvents: function () {
        gmCard.btnCardHelpRecDept.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                gmCard.txtCardRecDeptName.textbox('setValue', data.DeptName);
                gmCard.txtCardRecDeptID.val(data.DeptID);
            }, null);
        });
        gmCard.btnCardHelpRecHandler.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardRecHandlerName.textbox('setValue', data.UserName);
                gmCard.txtCardRecHandler.val(data.UserID);
            }, null);
        });
        gmCard.btnCardHelpRecWH.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.wareHouse, function (data) {
                //设置仓库
                gmCard.txtCardRecWHName.textbox('setValue', data.WareHouseName);
                gmCard.txtCardRecWHID.val(data.WareHouseId);
                //设置库管员
                gmCard.txtCardRecWHEmpName.textbox('setValue', data.WareHouseMan_Name);
                gmCard.txtCardRecWHEmpID.val(data.WareHouseId);
            }, null);
        });
        gmCard.btnCardHelpRecWHEmp.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardRecWHEmpName.textbox('setValue', data.UserName);
                gmCard.txtCardRecWHEmpID.val(data.UserID);
            }, null);
        });
        gmCard.btnCardHelpIssDept.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                gmCard.txtCardIssDeptName.textbox('setValue', data.DeptName);
                gmCard.txtCardIssDeptID.val(data.DeptID);
            }, null);
        });
        gmCard.btnCardHelpIssHandler.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardIssHandlerName.textbox('setValue', data.UserName);
                gmCard.txtCardIssHandler.val(data.UserID);
            }, null);
        });
        gmCard.btnCardHelpIssWH.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.wareHouse, function (data) {
                //设置仓库
                gmCard.txtCardIssWHName.textbox('setValue', data.WareHouseName);
                gmCard.txtCardIssWHID.val(data.WareHouseId);
                //设置库管员
                gmCard.txtCardIssWHEmpName.textbox('setValue', data.WareHouseMan_Name);
                gmCard.txtCardIssWHEmpID.val(data.WareHouseId);
            }, null);
        });
        gmCard.btnCardHelpIssWHEmp.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardIssWHEmpName.textbox('setValue', data.UserName);
                gmCard.txtCardIssWHEmpID.val(data.UserID);
            }, null);
        });
        gmCard.btnCardHelpPurDept.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                gmCard.txtCardPurDeptName.textbox('setValue', data.DeptName);
                gmCard.txtCardPurDeptID.val(data.DeptID);
            }, null);
        });
        gmCard.btnCardHelpPurEmp.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardPurEmpName.textbox('setValue', data.UserName);
                gmCard.txtCardPurEmpID.val(data.UserID);
            }, null);
        });
        //gmCard.btnCardHelpSupplier.click();
        gmCard.btnCardHelpSalesDep.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                gmCard.txtCardSalesDepName.textbox('setValue', data.DeptName);
                gmCard.txtCardSalesDepID.val(data.DeptID);
            }, null);
        });
        gmCard.btnCardHelpSalesEmp.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardSalesEmpName.textbox('setValue', data.UserName);
                gmCard.txtCardSalesEmpID.val(data.UserID);
            }, null);
        });
        gmCard.btnCardHelpCustomer.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.client, function (data) {
                gmCard.txtCardCustomerName.textbox('setValue', data.ClientName);
                gmCard.txtCardCustomerID.val(data.ClientID);
            }, null);
        });
        gmCard.btnCardHelpProDep.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                gmCard.txtCardProDepName.textbox('setValue', data.DeptName);
                gmCard.txtCardProDepID.val(data.DeptID);
            }, null);
        });
        gmCard.btnCardHelpProEmp.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardProEmpName.textbox('setValue', data.UserName);
                gmCard.txtCardProEmpID.val(data.UserID);
            }, null);
        });
        gmCard.btnCardHelpConDep.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                gmCard.txtCardConDepName.textbox('setValue', data.DeptName);
                gmCard.txtCardConDepID.val(data.DeptID);
            }, null);
        });
        gmCard.btnCardHelpConEmp.click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                gmCard.txtCardConEmpName.textbox('setValue', data.UserName);
                gmCard.txtCardConEmpID.val(data.UserID);
            }, null);
        });

        gmCard.btnSave.click(gmCard.doSave);
        gmCard.btnBack.click(function () {
            location.href = gmCard.gmManageUrl;
        });
    },
    gridHandler: {
        insertRow: function () {
            if (gmCard.cardState == cardStateConf.view) {
                return;
            }
            var index = 0;
            gmCard.gridGMItem.datagrid('insertRow', {
                index: index,
                row: {
                    GoodsMovementID: gmCard.txtCardGMID.val(),
                    GoodsMovementItemID: '',
                    TargInpQty: 0,
                    ActInpQty: 0,
                    TargOutQty: 0,
                    ActOutQty: 0,
                    InpPlaPrice: 0,
                    InpPlaValue: 0,
                    InpActPrice: 0,
                    InpActValue: 0,
                    OutPlaPrice: 0,
                    OutPlaValue: 0,
                    OutActPrice: 0,
                    OutActValue: 0,
                    ReturnQuantity: 0,
                    Remark: ''
                }
            });
            gmCard.gridGMItem.datagrid('selectRow', index).datagrid('beginEdit', index);
        },
        editRowBatch: function () {
            if (gmCard.cardState == cardStateConf.view) {
                return;
            }
            var rows = gmCard.gridGMItem.datagrid('getChecked');
            //console.log(rows.length);
            if (gFunc.isNull(rows) || rows.length < 1) {
                $.messager.alert('提示', '请选择要修改的数据');
                return;
            }
            for (var idx = 0; idx < rows.length; idx++) {
                if (rows[idx].editing) {
                    continue;
                }
                var index = gmCard.gridGMItem.datagrid('getRowIndex', rows[idx]);
                gmCard.gridGMItem.datagrid('beginEdit', index);
            }
        },
        deleteRowBatch: function () {
            if (gmCard.cardState == cardStateConf.view) {
                return;
            }
            var checkedRows = gmCard.gridGMItem.datagrid('getChecked');
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
                            return gFunc.isNull(row.GoodsMovementItemID);//
                        });
                        if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                            gmCard.gridGMItem.datagrid('deleteRow', gmCard.gridGMItem.datagrid('getRowIndex', checkedNewRows[0]));
                        } else {
                            break;
                        }
                    };
                    //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                    var checkedSavedRows = $.grep(checkedRows, function (row, idx) {
                        return !gFunc.isNull(row.GoodsMovementItemID);//过滤条件：UserID不为空
                    });
                    if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                        return;
                    }
                    var ids = [];
                    $.each(checkedSavedRows, function (index, row) {
                        ids.push(row.GoodsMovementItemID);
                    });
                    //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                    $.post('GoodsMovementItemService.asmx/Delete', JSON.stringify(ids), function (result) {
                        if (result && result.code) {
                            //重新加载
                            gmCard.gridGMItem.datagrid('reload');
                        }
                    });
                }
            });
        }
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return gmCard.gridGMItem.datagrid('getRowIndexByEditor', { element: target });
    },
    initCardForm: function (gmId, state) {
        gmCard.bindingEvents();

        //下拉框
        gmCard.cbCardBusinessType.combobox({
            data: gmFormatter.busType.src,
            valueField: 'value',
            textField: 'text'
        });
        gmCard.cbCardMoveType.combobox({
            data: gmFormatter.moveType.src,
            valueField: 'value',
            textField: 'text'
        });

        if (!gFunc.isNull(state)) {
            gmCard.cardState = state;
        }
        switch (state) {
            case cardStateConf.edit://修改
            case cardStateConf.view://查看
                var ajaxResult = false;
                $.ajax({
                    type: 'post',
                    url: 'GoodsMovementService.asmx/GetGoodsMovementById',
                    data: 'gmId=' + gmId,
                    async: false,//同步请求
                    success: function (result) {
                        if (result && result.code) {
                            ajaxResult = true;
                            gmCard.setFormData(result.data, state);
                            ////console.log('askprice,ajax succeed');
                        } else {
                            console.log('goodsmovement,ajax fail:' + result.msg);
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
                gmCard.initCardGrid("");
                break;
        }
    },
    setFormData: function (data, state) {
        //赋值
        ////console.log(data);
        gmCard.txtCardGMID.val(data.GoodsMovementID);
        $('#txtCardGMCode').textbox('setValue', data.GoodsMovementCode);
        gmCard.cbCardBusinessType.combobox('setValue', data.CardBusinessType);
        gmCard.cbCardMoveType.combobox('setValue', data.CardMoveType);
        $('#txtCardCreateDate').datebox('setValue', gmFormatter.gmState.format(data.CreateDate));
        $('#txtCardBillState').textbox('setValue', gmFormatter.gmState.format(data.BillState));
        if (data.IsRed == '1') {
            $('#checkCardIsRed').attr({ 'checked': 'checked' });
        }
        $('#txtCardReceiptDate').textbox('setValue', gmFormatter.gmState.format(data.ReceiptDate));

        gmCard.txtCardRecDeptName.textbox('setValue', data.RecDept_Name);
        gmCard.txtCardRecDeptID.val(data.RecDeptID);

        gmCard.txtCardRecHandlerName.textbox('setValue', data.RecHandler_Name);
        gmCard.txtCardRecHandlerID.val(data.RecHandlerID);

        gmCard.txtCardRecWHName.textbox('setValue', data.RecWH_Name);
        gmCard.txtCardRecWHID.val(data.RecWHID);

        gmCard.txtCardRecWHEmpName.textbox('setValue', data.RecWHEmp_Name);
        gmCard.txtCardRecWHEmpID.val(data.RecWHEmpID);

        gmCard.txtCardRecWHEmpName.textbox('setValue', data.RecWHEmp_Name);
        gmCard.txtCardRecWHEmpID.val(data.RecWHEmpID);

        $('#txtCardIssDate').datebox('setValue', gmFormatter.gmState.format(data.IssDate));

        gmCard.txtCardIssDeptName.textbox('setValue', data.IssDept_Name);
        gmCard.txtCardIssDeptID.val(data.IssDeptID);

        gmCard.txtCardIssHandlerName.textbox('setValue', data.IssHandler_Name);
        gmCard.txtCardIssHandler.val(data.IssHandler);

        gmCard.txtCardIssWHName.textbox('setValue', data.IssWH_Name);
        gmCard.txtCardIssWHID.val(data.IssWHID);

        gmCard.txtCardIssWHEmpName.textbox('setValue', data.IssWHEmp_Name);
        gmCard.txtCardIssWHEmpID.val(data.IssWHEmpID);

        gmCard.txtCardPurDeptName.textbox('setValue', data.PurDept_Name);
        gmCard.txtCardPurDeptID.val(data.PurDeptID);

        gmCard.txtCardPurEmpName.textbox('setValue', data.PurEmp_Name);
        gmCard.txtCardPurEmpID.val(data.PurEmpID);

        gmCard.txtCardSupplierName.textbox('setValue', data.Supplier_Name);
        gmCard.txtCardSupplierID.val(data.SupplierID);

        gmCard.txtCardSalesDepName.textbox('setValue', data.SalesDep_Name);
        gmCard.txtCardSalesDepID.val(data.SalesDepID);

        gmCard.txtCardSalesEmpName.textbox('setValue', data.SalesEmp_Name);
        gmCard.txtCardSalesEmpID.val(data.SalesEmpID);

        gmCard.txtCardCustomerName.textbox('setValue', data.Customer_Name);
        gmCard.txtCardCustomerID.val(data.SalesEmpID);

        gmCard.txtCardProDepName.textbox('setValue', data.ProDep_Name);
        gmCard.txtCardProDepID.val(data.ProDepID);

        gmCard.txtCardProEmpName.textbox('setValue', data.ProEmp_Name);
        gmCard.txtCardProEmpID.val(data.ProEmpID);

        gmCard.txtCardConDepName.textbox('setValue', data.ConDep_Name);
        gmCard.txtCardConDepID.val(data.ConDepID);

        gmCard.txtCardConEmpName.textbox('setValue', data.ConEmp_Name);
        gmCard.txtCardConEmpID.val(data.ConEmpID);

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
        $('#GoodsMovementCode').textbox('readonly', true);//编号只读
        ////console.log(typeof(state));
        var boolReadOnly = state == cardStateConf.view ? true : false;
        ////console.log('boolReadOnly:' + boolReadOnly);
        if (boolReadOnly) {
            ////console.log('gmCard.btnSave.hide();');
            gmCard.btnSave.hide();
        }
        $('#txtCardCreateDate').datebox('readonly', boolReadOnly);
        $('#txtCardIssDate').datebox('readonly', boolReadOnly);

        $('#txtCardRemark').textbox('readonly', boolReadOnly);
        if (boolReadOnly) {
            $('#checkCardIsRed').attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpRecDept.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpRecHandler.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpRecWH.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpRecWHEmp.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpIssDept.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpIssHandler.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpIssWH.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpIssWHEmp.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpPurDept.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpPurEmp.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpSupplier.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpSalesDep.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpSalesEmp.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpCustomer.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpProDep.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpProEmp.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpConDep.attr({ 'disabled': 'disabled' });
            gmCard.btnCardHelpConEmp.attr({ 'disabled': 'disabled' });
        }

        //初始化列表
        var gmId = "";
        if (!gFunc.isNull(data)) {
            gmId = data.GoodsMovementID;
        }
        //gmCard.initCardGrid(gmId);
    },
    initCardGrid: function (gmId) {
        gFunc.initGridPublic(gmCard.gridGMItem, {
            title: '',
            icon: 'icon-edit',
            key: 'GoodsMovementItemID',
            url: (gmCard.cardState != cardStateConf.add) ? (gmCard.searchUrlGMItem + '?gmId=' + gmId) : "",
            toolbar: [{
                id: 'btnAddItem',
                text: '新增',
                iconCls: 'icon-add',
                handler: gmCard.gridHandler.insertRow
            }, "-", {
                id: 'btnEditItem',
                text: '修改',
                iconCls: 'icon-edit',
                handler: gmCard.gridHandler.editRowBatch
            }, "-", {
                id: 'btnDeleteItem',
                text: '删除',
                iconCls: 'icon-remove',
                handler: gmCard.gridHandler.deleteRowBatch
            }, "-", {
                id: 'btnCancelEditItem',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    if (gmCard.cardState == cardStateConf.view) {
                        return;
                    }
                    gmCard.gridGMItem.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'GoodsMovementItemID' },
                { field: 'GoodsMovementID' },
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
                                showPopGridHelp(400, 300, true, helpInitializer.materials, gmCard.helpReceiver.gmItemMaterial, target);
                            }
                        }
                    }
                },
                {
                    field: 'TargInpQty', title: '应收数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'ActInpQty', title: '实收数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                { field: 'RecUnitID' },
                {
                    field: 'RecUnit_Name', title: '接收计量单位', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.measureUnit, gmCard.helpReceiver.gmItemUnitRec, target);
                            }
                        }
                    }
                },
                {
                    field: 'TargOutQty', title: '应发数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'ActOutQty', title: '实发数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                { field: 'IssUnitID' },
                {
                    field: 'IssUnit_Name', title: '发出计量单位', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.measureUnit, gmCard.helpReceiver.gmItemUnitIss, target);
                            }
                        }
                    }
                },
                {
                    field: 'InpPlaPrice', title: '接收计划单价', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'InpPlaValue', title: '接收计划金额', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'InpActPrice', title: '接收实际单价', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'InpActValue', title: '接收实际金额', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'OutPlaPrice', title: '发出计划单价', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'OutPlaValue', title: '发出计划金额', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'OutActPrice', title: '发出实际单价', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'OutActValue', title: '发出实际金额', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'ReturnQuantity', title: '退回数量', align: 'center', width: 80,
                    editor: {
                        type: 'numberbox',
                        options: {
                            precision: 2,
                            validType: 'maxLength[20]'
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
            hidecols: ['GoodsMovementID', 'GoodsMovementItemID', 'MaterialID', 'RecUnitID', 'IssUnitID'],
            singleSelect: false
        });
    },
    helpReceiver: {
        gmItemMaterial: function (mData, target) {
            if (gFunc.isNull(mData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, mData.MaterialName);
            //给关联列赋值
            var index = gmCard.getRowIndexByEditor(target);
            var row = gmCard.gridGMItem.datagrid('getRows')[index];
            gmCard.gridGMItem.datagrid('updateRowCell', { field: 'MaterialID', index: index, value: mData.MaterialID });
        },
        gmItemUnitRec: function (uData, target) {
            gmCard.helpReceiver.gmItemUnit(uData, target, 'RecUnitID');
        },
        gmItemUnitIss: function (uData, target) {
            gmCard.helpReceiver.gmItemUnit(uData, target, 'IssUnitID');
        },
        gmItemUnit: function (uData, target, idField) {
            if (gFunc.isNull(uData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, uData.UnitName);
            //给关联列赋值
            var index = gmCard.getRowIndexByEditor(target);
            var row = gmCard.gridGMItem.datagrid('getRows')[index];
            gmCard.gridGMItem.datagrid('updateRowCell', { field: idField, index: index, value: uData.UnitID });
        }
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
        var allRows = gmCard.gridGMItem.datagrid('getRows');
        if (!gFunc.isNull(allRows) && allRows.length > 0) {
            //逐行校验，只校验正在编辑的行
            for (var idx = 0; idx < allRows.length; idx++) {
                var row = allRows[idx];
                if (!row.editing) {
                    continue;
                }
                var rowIdx = gmCard.gridGMItem.datagrid('getRowIndex', row);
                if (!gmCard.gridGMItem.datagrid('validateRow', rowIdx)) {
                    $.messager.alert('提示', '数据校验失败，请检查输入！');
                    return false;
                }
                gmCard.gridGMItem.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
                editingRows.push(row);
                gmCard.gridGMItem.datagrid('beginEdit', rowIdx);
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
            url: gmCard.saveUrl,
            data: JSON.stringify(formData),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    ajaxResult = true;
                    //console.log('goodsmovement,ajax succeed');
                    location.href = gmCard.gmManageUrl;
                } else {
                    //console.log('goodsmovement,ajax fail');
                    ajaxResult = false;
                }
            },
            error: function () {
                //console.log('goodsmovement,ajax error');
                ajaxResult = false;
            }
        });
        //console.log('goodsmovement,doSave over');
        return ajaxResult;
    }
}