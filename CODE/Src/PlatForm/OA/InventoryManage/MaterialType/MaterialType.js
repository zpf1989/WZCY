
//物料分类列表对象
var types = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        types.initgrid();
        types.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        types.btnSearch.click(types.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(types.grid, {
            title: '物料类型列表',
            icon: 'icon-edit',
            key: 'MaterialTypeID',
            url: 'MaterialTypeService.asmx/GetList',
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: types.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: types.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: types.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: types.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    types.grid.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'MaterialTypeID' },
                {
                    field: 'MaterialTypeCode', title: '类型编号', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'MaterialTypeName', title: '类型名称', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[60]'
                        }
                    }
                },
                {
                    field: 'Remark', title: '备注', width: 120, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[255]'
                        }
                    }
                }
            ]],
            hidecols: ['MaterialTypeID'],
            singleSelect: false
        });
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return types.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = types.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = types.grid.datagrid('getRowIndex', rows[idx]);
            types.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var checkedRows = types.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                //1、删除选中行中新增的部分（这部分直接客户端删除即可）
                while (true) {
                    //因为删除一行后，checkedNewRows会变化，所以需要从checkedRows中重新筛选新增行，并且，每次只删除checkedRows[0]即可
                    //$.grep是jquery的函数，用于过滤数组元素
                    var checkedNewRows = $.grep(checkedRows, function (row, idx) {
                        return gFunc.isNull(row.MaterialTypeID);//过滤条件：UserID为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        types.grid.datagrid('deleteRow', types.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(checkedRows, function (row, idx) {
                    return !gFunc.isNull(row.MaterialTypeID);//过滤条件：UserID不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.MaterialTypeID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('MaterialTypeService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        types.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = types.grid.datagrid('getChecked');
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
            var rowIdx = types.grid.datagrid('getRowIndex', row);
            if (!types.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            types.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            editingRows.push(row);
            types.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        $.post('MaterialTypeService.asmx/Save', JSON.stringify(editingRows), function (result) {
            if (result && result.code) {
                //重新加载
                types.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        types.grid.datagrid('insertRow', { index: index, row: {} });
        types.grid.datagrid('selectRow', index);
        types.grid.datagrid('beginEdit', index);
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = types.formSearch.serializeToJson(true);
        //重新查询
        types.grid.datagrid("reload", searchParams);
    }
}