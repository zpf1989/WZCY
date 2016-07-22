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

    txtCardCreateDate: $('#txtCardCreateDate'),

    txtCardGMID: $('#txtGMID'),
    saveUrl: 'GoodsMovementService.asmx/Save',
    searchUrlGMItem: 'GoodsMovementItemService.asmx/GetGMItems',
    gmManageUrl: 'GoodsMovement.aspx',
    bindingEvents: function (options) {
        $('#btnCardHelpRecDept').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                $('#txtCardRecDeptName').textbox('setValue', data.DeptName);
                $('#txtCardRecDeptID').val(data.DeptID);
            }, null);
        });
        $('#btnCardHelpRecHandler').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardRecHandlerName').textbox('setValue', data.UserName);
                $('#txtCardRecHandler').val(data.UserID);
            }, null);
        });
        $('#btnCardHelpRecWH').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.wareHouse, function (data) {
                //设置仓库
                $('#txtCardRecWHName').textbox('setValue', data.WareHouseName);
                $('#txtCardRecWHID').val(data.WareHouseID);
                //设置库管员
                $('#txtCardRecWHEmpName').textbox('setValue', data.WareHouseMan_Name);
                $('#txtCardRecWHEmpID').val(data.WareHouseMan);
            }, null);
        });
        $('#btnCardHelpRecWHEmp').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardRecWHEmpName').textbox('setValue', data.UserName);
                $('#txtCardRecWHEmpID').val(data.UserID);
            }, null);
        });
        $('#btnCardHelpIssDept').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                $('#txtCardIssDeptName').textbox('setValue', data.DeptName);
                $('#txtCardIssDeptID').val(data.DeptID);
            }, null);
        });
        $('#btnCardHelpIssHandler').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardIssHandlerName').textbox('setValue', data.UserName);
                $('#txtCardIssHandler').val(data.UserID);
            }, null);
        });
        $('#btnCardHelpIssWH').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.wareHouse, function (data) {
                //设置仓库
                $('#txtCardIssWHName').textbox('setValue', data.WareHouseName);
                $('#txtCardIssWHID').val(data.WareHouseID);
                //设置库管员
                $('#txtCardIssWHEmpName').textbox('setValue', data.WareHouseMan_Name);
                $('#txtCardIssWHEmpID').val(data.WareHouseMan);
            }, null);
        });
        $('#btnCardHelpIssWHEmp').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardIssWHEmpName').textbox('setValue', data.UserName);
                $('#txtCardIssWHEmpID').val(data.UserID);
            }, null);
        });
        $('#btnCardHelpPurDept').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                $('#txtCardPurDeptName').textbox('setValue', data.DeptName);
                $('#txtCardPurDeptID').val(data.DeptID);
            }, null);
        });
        $('#btnCardHelpPurEmp').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardPurEmpName').textbox('setValue', data.UserName);
                $('#txtCardPurEmpID').val(data.UserID);
            }, null);
        });
        //供应商：帮助尚未完成？？
        //$('#btnCardHelpSupplier').click(function () {
        //    showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
        //        $('#txtCardSupplierName').textbox('setValue', data.DeptName);
        //        $('#txtCardSupplierID').val(data.DeptID);
        //    }, null);
        //});
        $('#btnCardHelpSalesDep').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                $('#txtCardSalesDepName').textbox('setValue', data.DeptName);
                $('#txtCardSalesDepID').val(data.DeptID);
            }, null);
        });
        $('#btnCardHelpSalesEmp').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardSalesEmpName').textbox('setValue', data.UserName);
                $('#txtCardSalesEmpID').val(data.UserID);
            }, null);
        });
        $('#btnCardHelpCustomer').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.client, function (data) {
                $('#txtCardCustomerName').textbox('setValue', data.ClientName);
                $('#txtCardCustomerID').val(data.ClientID);
            }, null);
        });
        $('#btnCardHelpProDep').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                $('#txtCardProDepName').textbox('setValue', data.DeptName);
                $('#txtCardProDepID').val(data.DeptID);
            }, null);
        });
        $('#btnCardHelpProEmp').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardProEmpName').textbox('setValue', data.UserName);
                $('#txtCardProEmpID').val(data.UserID);
            }, null);
        });
        $('#btnCardHelpConDep').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.dept, function (data) {
                $('#txtCardConDepName').textbox('setValue', data.DeptName);
                $('#txtCardConDepID').val(data.DeptID);
            }, null);
        });
        $('#btnCardHelpConEmp').click(function () {
            showPopGridHelp(400, 300, true, helpInitializer.user, function (data) {
                $('#txtCardConEmpName').textbox('setValue', data.UserName);
                $('#txtCardConEmpID').val(data.UserID);
            }, null);
        });

        gmCard.btnSave.click(gmCard.doSave);
        gmCard.btnBack.click(function () {
            location.href = gmCard.gmManageUrl + '?busType=' + options.busType + '&moveType=' + options.moveType;
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
    initCardForm: function (options) {
        //1、加载动态控件
        gmCard.initDynamicControl(options.busType, options.moveType);
        if (!gFunc.isNull(options.state)) {
            gmCard.cardState = options.state;
        }
        //2、绑定按钮事件
        gmCard.bindingEvents(options);

        //3、下拉框
        gmCard.cbCardBusinessType.combobox({
            data: gmFormatter.busType.src,
            valueField: 'value',
            textField: 'text'
        });
        gmCard.cbCardBusinessType.combobox('setValue', options.busType);
        gmCard.cbCardBusinessType.combobox('readonly', 'readonly');

        gmCard.cbCardMoveType.combobox({
            data: gmFormatter.moveType.src,
            valueField: 'value',
            textField: 'text'
        });
        gmCard.cbCardMoveType.combobox('setValue', options.moveType);
        gmCard.cbCardMoveType.combobox('readonly', 'readonly');
        //4、表单数据
        var cols = gmCard.getCols(options.busType);
        switch (options.state) {
            case cardStateConf.edit://修改
            case cardStateConf.view://查看
                var ajaxResult = false;
                $.ajax({
                    type: 'post',
                    url: 'GoodsMovementService.asmx/GetGoodsMovementById',
                    data: 'gmId=' + options.gmId,
                    async: false,//同步请求
                    success: function (result) {
                        if (result && result.code) {
                            ajaxResult = true;
                            gmCard.setFormData(result.data, options.state, cols);
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
                gmCard.txtCardCreateDate.datebox('setValue', new Date().Format('yyyy-MM-dd'));
                gmCard.initCardGrid("", cols);
                break;
        }
    },
    setFormData: function (data, state, gridCols) {
        //赋值
        ////console.log(data);
        gmCard.txtCardGMID.val(data.GoodsMovementID);
        $('#txtCardGMCode').textbox('setValue', data.GoodsMovementCode);
        //gmCard.cbCardBusinessType.combobox('setValue', data.BusinessType);
        //gmCard.cbCardMoveType.combobox('setValue', data.MoveTypeCode);
        gmCard.txtCardCreateDate.datebox('setValue', formatHandler.date.format(data.CreateDate));
        $('#txtCardBillState').textbox('setValue', gmFormatter.gmState.format(data.BillState));
        if (data.IsRed == '1') {
            $('#checkCardIsRed').attr({ 'checked': 'checked' });
        }
        $('#txtCardReceiptDate').textbox('setValue', formatHandler.date.format(data.ReceiptDate));

        $('#txtCardRecDeptName').textbox('setValue', data.RecDept_Name);
        $('#txtCardRecDeptID').val(data.RecDeptID);

        $('#txtCardRecHandlerName').textbox('setValue', data.RecHandler_Name);
        $('#txtCardRecHandler').val(data.RecHandler);
        console.log($('#txtCardRecHandler').val());

        $('#txtCardRecWHName').textbox('setValue', data.RecWH_Name);
        $('#txtCardRecWHID').val(data.RecWHID);

        $('#txtCardRecWHEmpName').textbox('setValue', data.RecWHEmp_Name);
        $('#txtCardRecWHEmpID').val(data.RecWHEmpID);

        $('#txtCardRecWHEmpName').textbox('setValue', data.RecWHEmp_Name);
        $('#txtCardRecWHEmpID').val(data.RecWHEmpID);

        $('#txtCardIssDate').datebox('setValue', formatHandler.date.format(data.IssDate));

        $('#txtCardIssDeptName').textbox('setValue', data.IssDept_Name);
        $('#txtCardIssDeptID').val(data.IssDeptID);

        $('#txtCardIssHandlerName').textbox('setValue', data.IssHandler_Name);
        $('#txtCardIssHandler').val(data.IssHandler);

        $('#txtCardIssWHName').textbox('setValue', data.IssWH_Name);
        $('#txtCardIssWHID').val(data.IssWHID);

        $('#txtCardIssWHEmpName').textbox('setValue', data.IssWHEmp_Name);
        $('#txtCardIssWHEmpID').val(data.IssWHEmpID);

        $('#txtCardPurDeptName').textbox('setValue', data.PurDept_Name);
        $('#txtCardPurDeptID').val(data.PurDeptID);

        $('#txtCardPurEmpName').textbox('setValue', data.PurEmp_Name);
        $('#txtCardPurEmpID').val(data.PurEmpID);

        $('txtCardSupplierName').textbox('setValue', data.Supplier_Name);
        $('txtCardSupplierID').val(data.SupplierID);

        $('#txtCardSalesDepName').textbox('setValue', data.SalesDep_Name);
        $('#txtCardSalesDepID').val(data.SalesDepID);

        $('#txtCardSalesEmpName').textbox('setValue', data.SalesEmp_Name);
        $('#txtCardSalesEmpID').val(data.SalesEmpID);

        $('#txtCardCustomerName').textbox('setValue', data.Customer_Name);
        $('#txtCardCustomerID').val(data.CustomerID);

        $('#txtCardProDepName').textbox('setValue', data.ProDep_Name);
        $('#txtCardProDepID').val(data.ProDepID);

        $('#txtCardProEmpName').textbox('setValue', data.ProEmp_Name);
        $('#txtCardProEmpID').val(data.ProEmpID);

        $('#txtCardConDepName').textbox('setValue', data.ConDep_Name);
        $('#txtCardConDepID').val(data.ConDepID);

        $('#txtCardConEmpName').textbox('setValue', data.ConEmp_Name);
        $('#txtCardConEmpID').val(data.ConEmpID);

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
        $('#txtCardGMCode').textbox('readonly', true);//编号只读
        ////console.log(typeof(state));
        var boolReadOnly = state == cardStateConf.view ? true : false;
        ////console.log('boolReadOnly:' + boolReadOnly);
        if (boolReadOnly) {
            ////console.log('gmCard.btnSave.hide();');
            gmCard.btnSave.hide();
        }
        gmCard.txtCardCreateDate.datebox('readonly', boolReadOnly);
        $('#txtCardIssDate').datebox('readonly', boolReadOnly);
        $('#txtCardReceiptDate').datebox('readonly', boolReadOnly);

        $('#txtCardRemark').textbox('readonly', boolReadOnly);
        if (boolReadOnly) {
            $('#checkCardIsRed').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpRecDept').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpRecHandler').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpRecWH').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpRecWHEmp').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpIssDept').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpIssHandler').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpIssWH').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpIssWHEmp').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpPurDept').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpPurEmp').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpSupplier').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpSalesDep').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpSalesEmp').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpCustomer').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpProDep').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpProEmp').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpConDep').attr({ 'disabled': 'disabled' });
            $('#btnCardHelpConEmp').attr({ 'disabled': 'disabled' });
        }

        //初始化列表
        var gmId = "";
        if (!gFunc.isNull(data)) {
            gmId = data.GoodsMovementID;
        }
        gmCard.initCardGrid(gmId, gridCols);
    },
    initCardGrid: function (gmId, cols) {
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
            columns: cols,
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
        //是否红单，特殊处理
        if ($('#checkCardIsRed').is(':checked')) {
            formData.IsRed = 1;
        } else {
            formData.IsRed = 0;
        }
        //alert(formData.IsRed);
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
                    location.href = gmCard.gmManageUrl + '?busType=' + formData.BusinessType + '&moveType=' + formData.MoveTypeCode;;
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
    },
    initDynamicControl: function (busType, moveType) {
        //初始化动态内容
        var strHtml = '';
        if (busType == '1') {//入库
            strHtml += '<tr><td class="card-table-label">接收日期</td><td class="card-table-centent"><input type="text" id="txtCardReceiptDate" name="ReceiptDate" class="easyui-datebox" editable="false" /></td><td class="card-table-label">接收部门</td><td class="card-table-centent"><span><input type="text" id="txtCardRecDeptName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpRecDept" class="btn-help" /><input id="txtCardRecDeptID" name="RecDeptID" hidden="hidden" /></span></td></tr><tr><td class="card-table-label">接收经办人</td><td class="card-table-centent"><span><input type="text" id="txtCardRecHandlerName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpRecHandler" class="btn-help" /><input id="txtCardRecHandler" name="RecHandler" hidden="hidden" /></span></td><td class="card-table-label">接收仓库</td><td class="card-table-centent"><span><input type="text" id="txtCardRecWHName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpRecWH" class="btn-help" /><input id="txtCardRecWHID" name="RecWHID" hidden="hidden" /></span></td></tr><tr><td class="card-table-label">接收仓库保管员</td><td class="card-table-centent"><span><input type="text" id="txtCardRecWHEmpName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpRecWHEmp" class="btn-help" /><input id="txtCardRecWHEmpID" name="RecWHEmpID" hidden="hidden" /></span></td></tr>';
            if (moveType == '100') {//采购入库
                strHtml += '<tr><td class="card-table-label">采购部门</td><td class="card-table-centent"><span><input type="text" id="txtCardPurDeptName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpPurDept" class="btn-help" /><input id="txtCardPurDeptID" name="PurDeptID" hidden="hidden" /></span></td><td class="card-table-label">采购人员</td><td class="card-table-centent"><span><input type="text" id="txtCardPurEmpName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpPurEmp" class="btn-help" /><input id="txtCardPurEmpID" name="PurEmpID" hidden="hidden" /></span></td></tr><tr><td class="card-table-label">供应商</td><td class="card-table-centent"><span><input type="text" id="txtCardSupplierName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpSupplier" class="btn-help" /><input id="txtCardSupplierID" name="SupplierID" hidden="hidden" /></span></td></tr>';
            } else if (moveType == '101') {//生产入库
                strHtml += '<tr><td class="card-table-label">生产部门</td><td class="card-table-centent"><span><input type="text" id="txtCardProDepName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpProDep" class="btn-help" /><input id="txtCardProDepID" name="ProDepID" hidden="hidden" /></span></td><td class="card-table-label">生产人</td><td class="card-table-centent"><span><input type="text" id="txtCardProEmpName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpProEmp" class="btn-help" /><input id="txtCardProEmpID" name="ProEmpID" hidden="hidden" /></span></td></tr>';
            }
        } else if (busType == '2') {
            strHtml += '<tr><td class="card-table-label">发出日期</td><td class="card-table-centent"><input type="text" id="txtCardIssDate" name="IssDate" class="easyui-datebox" editable="false" /></td><td class="card-table-label">发出部门</td><td class="card-table-centent"><span><input type="text" id="txtCardIssDeptName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpIssDept" class="btn-help" /><input id="txtCardIssDeptID" name="IssDeptID" hidden="hidden" /></span></td></tr><tr><td class="card-table-label">发出经办人</td><td class="card-table-centent"><span><input type="text" id="txtCardIssHandlerName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpIssHandler" class="btn-help" /><input id="txtCardIssHandler" name="IssHandler" hidden="hidden" /></span></td><td class="card-table-label">发出仓库</td><td class="card-table-centent"><span><input type="text" id="txtCardIssWHName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpIssWH" class="btn-help" /><input id="txtCardIssWHID" name="IssWHID" hidden="hidden" /></span></td></tr><tr><td class="card-table-label">发出仓库保管员</td><td class="card-table-centent"><span><input type="text" id="txtCardIssWHEmpName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpIssWHEmp" class="btn-help" /><input id="txtCardIssWHEmpID" name="IssWHEmpID" hidden="hidden" /></span></td></tr>';
            if (moveType == '102') {//领料出库
                strHtml += '<tr><td class="card-table-label">销售部门</td><td class="card-table-centent"><span><input type="text" id="txtCardSalesDepName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpSalesDep" class="btn-help" /><input id="txtCardSalesDepID" name="SalesDepID" hidden="hidden" /></span></td><td class="card-table-label">销售人员</td><td class="card-table-centent"><span><input type="text" id="txtCardSalesEmpName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpSalesEmp" class="btn-help" /><input id="txtCardSalesEmpID" name="SalesEmpID" hidden="hidden" /></span></td></tr><tr><td class="card-table-label">销售客户</td><td class="card-table-centent"><span><input type="text" id="txtCardCustomerName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpCustomer" class="btn-help" /><input id="txtCardCustomerID" name="CustomerID" hidden="hidden" /></span></td></tr>';
            } else if (moveType == '103') {//销售出库
                strHtml += '<tr><td class="card-table-label">领用部门</td><td class="card-table-centent"><span><input type="text" id="txtCardConDepName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpConDep" class="btn-help" /><input id="txtCardConDepID" name="ConDepID" hidden="hidden" /></span></td><td class="card-table-label">领用人</td><td class="card-table-centent"><span><input type="text" id="txtCardConEmpName" class="easyui-textbox help-text" readonly="readonly" /><input type="button" value="..." id="btnCardHelpConEmp" class="btn-help" /><input id="txtCardConEmpID" name="ConEmpID" hidden="hidden" /></span></td></tr>';
            }
        }
        $('#tbBasicInfo tr:eq(2)').after(strHtml);
        $.parser.parse($('#tbBasicInfo'));
    },
    getCols: function (busType) {
        var cols = [[
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
                }
        ]];
        //根据业务类型加载动态列
        if (busType == '1') {//入库
            cols[0].push({ field: 'TargInpQty', title: '应收数量', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'ActInpQty', title: '实收数量', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'RecUnitID' },
                {
                    field: 'RecUnit_Name', title: '接收计量单位', align: 'center', width: 100, editor: {
                        type: 'helpEdit',
                        options: {
                            onclick:
                                function (target) {
                                    if (gFunc.isNull(target)) { return; }
                                    showPopGridHelp(400, 300, true, helpInitializer.measureUnit, gmCard.helpReceiver.gmItemUnitRec, target);
                                }
                        }
                    }
                },
                { field: 'InpPlaPrice', title: '接收计划单价', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'InpPlaValue', title: '接收计划金额', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'InpActPrice', title: '接收实际单价', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'InpActValue', title: '接收实际金额', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } });
        } else if (busType == '2') {//出库
            cols[0].push({ field: 'TargOutQty', title: '应发数量', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'ActOutQty', title: '实发数量', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
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
                { field: 'OutPlaPrice', title: '发出计划单价', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'OutPlaValue', title: '发出计划金额', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'OutActPrice', title: '发出实际单价', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } },
                { field: 'OutActValue', title: '发出实际金额', align: 'center', width: 80, editor: { type: 'numberbox', options: { precision: 2, validType: 'maxLength[20]' } } });
        }
        cols[0].push({
            field: 'Remark', title: '备注', width: 100, align: 'center',
            editor: {
                type: 'textbox',
                options: {
                    validType: 'maxLength[1024]'
                }
            }
        });

        return cols;
    }
}