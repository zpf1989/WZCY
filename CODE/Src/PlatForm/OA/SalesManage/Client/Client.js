//客户列表对象
var client = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        client.initgrid();
        client.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        client.btnSearch.click(client.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(client.grid, {
            title: '客户列表',
            icon: 'icon-edit',
            key: 'ClientID',
            url: 'ClientService.asmx/GetList',
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: client.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: client.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: client.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: client.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    client.grid.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'ClientID' },
                { field: 'PayTypeID' },
                {
                    field: 'ClientCode', title: '客户编号', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[60]'
                        }
                    }
                },
                {
                    field: 'ClientName', title: '客户名称', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[255]'
                        }
                    }
                },
                {
                    field: 'ClientTel', title: '联系电话', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'ClientAddress', title: '联系地址', width: 120, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[255]'
                        }
                    }
                },
                {
                    field: 'Contactor', title: '联系人', width: 60, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'Receiver', title: '收货人', width: 60, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'ReceiverTel', title: '收货人联系方式', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'PayType_Name', title: '付款方式', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.payType, client.helpReceiver.payType, target);
                            }
                        }
                    }
                },
                {
                    field: 'BillingInfo', title: '客服开票信息', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[1024]'
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
                },
            ]],
            hidecols: ['ClientID', 'PayTypeID'],
            singleSelect: false
        });
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return client.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = client.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = client.grid.datagrid('getRowIndex', rows[idx]);
            client.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var delCheckedRows = client.grid.datagrid('getChecked');
        if (gFunc.isNull(delCheckedRows) || delCheckedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                //1、删除选中行中新增的部分（这部分直接客户端删除即可）
                while (true) {
                    //因为删除一行后，checkedNewRows会变化，所以需要从delCheckedRows中重新筛选新增行，并且，每次只删除delCheckedRows[0]即可
                    //$.grep是jquery的函数，用于过滤数组元素
                    var checkedNewRows = $.grep(delCheckedRows, function (row, idx) {
                        return gFunc.isNull(row.ClientID);//过滤条件：ClientID为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        client.grid.datagrid('deleteRow', client.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(delCheckedRows, function (row, idx) {
                    return !gFunc.isNull(row.ClientID);//过滤条件：ClientID不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.ClientID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('ClientService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        client.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = client.grid.datagrid('getChecked');
        if (gFunc.isNull(selectedRows) || selectedRows.length < 1) {
            return;
        }
        var editingRows = [];
        //逐行校验
        for (var idx = 0; idx < selectedRows.length; idx++) {
            var row = selectedRows[idx];
            if (!row.editing) {
                continue;
            }
            var rowIdx = client.grid.datagrid('getRowIndex', row);
            if (!client.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            client.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            if (gFunc.isNull(row.ClientMan)) {
                row.ClientMan = '';
            }
            editingRows.push(row);
            client.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var data = JSON.stringify(editingRows);
        $.post('ClientService.asmx/Save', data, function (result) {
            if (result && result.code) {
                //重新加载
                client.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        client.grid.datagrid('insertRow', {
            index: index,
            row: {}
        });
        client.grid.datagrid('selectRow', index);
        client.grid.datagrid('beginEdit', index);
    },
    helpReceiver: {
        payType: function (payTypeData, target) {
            if (gFunc.isNull(payTypeData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, payTypeData.PayTypeName);
            //给关联列赋值
            var index = client.getRowIndexByEditor(target);
            var row = client.grid.datagrid('getRows')[index];
            client.grid.datagrid('updateRowCell', { field: 'PayTypeID', index: index, value: payTypeData.PayTypeID });
        }
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = client.formSearch.serializeToJson(true);
        //重新查询
        client.grid.datagrid("reload", searchParams);
    }
}