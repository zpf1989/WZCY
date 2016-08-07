//客户列表对象
var supplier = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        supplier.initgrid();
        supplier.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        supplier.btnSearch.click(supplier.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(supplier.grid, {
            title: '供应商列表',
            icon: 'icon-edit',
            key: 'SupplierID',
            url: 'SupplierSvr.asmx/GetList',
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: supplier.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: supplier.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: supplier.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: supplier.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    supplier.grid.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'SupplierID' },
                {
                    field: 'SupplierCode', title: '供应商编号', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[60]'
                        }
                    }
                },
                {
                    field: 'SupplierName', title: '供应商名称', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[255]'
                        }
                    }
                },
                {
                    field: 'Contactor', title: '联系人', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[60]'
                        }
                    }
                },
                {
                    field: 'Tel', title: '联系电话', width: 120, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'Fax', title: '传真', width: 120, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'Remark', title: '备注', width: 150, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[255]'
                        }
                    }
                },
            ]],
            hidecols: ['SupplierID'],
            singleSelect: false
        });
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return supplier.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = supplier.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = supplier.grid.datagrid('getRowIndex', rows[idx]);
            supplier.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var delCheckedRows = supplier.grid.datagrid('getChecked');
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
                        return gFunc.isNull(row.SupplierID);//过滤条件：SupplierID为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        supplier.grid.datagrid('deleteRow', supplier.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(delCheckedRows, function (row, idx) {
                    return !gFunc.isNull(row.SupplierID);//过滤条件：SupplierID不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.SupplierID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('SupplierSvr.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        supplier.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = supplier.grid.datagrid('getChecked');
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
            var rowIdx = supplier.grid.datagrid('getRowIndex', row);
            if (!supplier.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            supplier.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            if (gFunc.isNull(row.ClientMan)) {
                row.ClientMan = '';
            }
            editingRows.push(row);
            supplier.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var data = JSON.stringify(editingRows);
        $.post('SupplierSvr.asmx/Save', data, function (result) {
            if (result && result.code) {
                //重新加载
                supplier.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        supplier.grid.datagrid('insertRow', {
            index: index,
            row: {}
        });
        supplier.grid.datagrid('selectRow', index);
        supplier.grid.datagrid('beginEdit', index);
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = supplier.formSearch.serializeToJson(true);
        //重新查询
        supplier.grid.datagrid("reload", searchParams);
    }
}