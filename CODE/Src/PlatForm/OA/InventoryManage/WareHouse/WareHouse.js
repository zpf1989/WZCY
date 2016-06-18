//仓库列表对象
var warehouse = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        warehouse.initgrid();
        warehouse.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        warehouse.btnSearch.click(warehouse.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(warehouse.grid, {
            title: '仓库列表',
            icon: 'icon-edit',
            key: 'WareHouseID',
            url: 'WareHouseService.asmx/GetList',
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: warehouse.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: warehouse.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: warehouse.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: warehouse.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    warehouse.grid.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'WareHouseID' },
                {
                    field: 'WareHouseCode', title: '仓库编号', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[60]'
                        }
                    }
                },
                {
                    field: 'WareHouseName', title: '仓库名称', width: 80, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[255]'
                        }
                    }
                },
                { field: 'WareHouseMan' },
                {
                    field: 'WareHouseMan_Name', title: '仓库主管', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.user, warehouse.helpReceiver.user, target);
                            }
                        }
                    }
                },
                {
                    field: 'Address', title: '地址', width: 150, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[255]'
                        }
                    }
                },
                {
                    field: 'Tel', title: '电话', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                {
                    field: 'Remark', title: '备注', width: 120, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[1024]'
                        }
                    }
                },
            ]],
            hidecols: ['WareHouseID', 'WareHouseMan'],
            singleSelect: false
        });
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return warehouse.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = warehouse.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = warehouse.grid.datagrid('getRowIndex', rows[idx]);
            warehouse.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var delCheckedRows = warehouse.grid.datagrid('getChecked');
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
                        return gFunc.isNull(row.WareHouseID);//过滤条件：WareHouseID为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        warehouse.grid.datagrid('deleteRow', warehouse.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(delCheckedRows, function (row, idx) {
                    return !gFunc.isNull(row.WareHouseID);//过滤条件：WareHouseID不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.WareHouseID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('WareHouseService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        warehouse.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = warehouse.grid.datagrid('getChecked');
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
            var rowIdx = warehouse.grid.datagrid('getRowIndex', row);
            if (!warehouse.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            warehouse.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            if (gFunc.isNull(row.WareHouseMan)) {
                row.WareHouseMan = '';
            }
            editingRows.push(row);
            warehouse.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var data = JSON.stringify(editingRows);
        $.post('WareHouseService.asmx/Save', data, function (result) {
            if (result && result.code) {
                //重新加载
                warehouse.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        warehouse.grid.datagrid('insertRow', {
            index: index,
            row: {}
        });
        warehouse.grid.datagrid('selectRow', index);
        warehouse.grid.datagrid('beginEdit', index);
    },
    helpReceiver: {
        user: function (userData, target) {
            if (gFunc.isNull(userData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, userData.UserName);
            //给关联列赋值
            var index = warehouse.getRowIndexByEditor(target);
            var row = warehouse.grid.datagrid('getRows')[index];
            warehouse.grid.datagrid('updateRowCell', { field: 'WareHouseMan', index: index, value: userData.UserID });
        }
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = warehouse.formSearch.serializeToJson(true);
        //重新查询
        warehouse.grid.datagrid("reload", searchParams);
    }
}