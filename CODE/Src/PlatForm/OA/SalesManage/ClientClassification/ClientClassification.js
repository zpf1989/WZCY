//客户列表对象
var cc = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        cc.initgrid();
        cc.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        cc.btnSearch.click(cc.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(cc.grid, {
            title: '客户等级列表',
            icon: 'icon-edit',
            key: 'Id',
            url: 'ClientClassificationService.asmx/GetList',
            toolbar: [{
                id: 'btnClassify',
                text: '自动分级',
                iconCls: 'icon-save',
                handler: cc.classify
            }, "-", {
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: cc.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: cc.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: cc.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: cc.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    cc.grid.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'Id' },
                {
                    field: 'ClientName', title: '客户名称', width: 100, align: 'center',
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.client, cc.helpReceiver.client, target);
                            }
                        }
                    }
                },
                {
                    field: 'LevelName', title: '客户等级', width: 100, align: 'center',
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.clientLevel, cc.helpReceiver.clientLevel, target);
                            }
                        }
                    }
                },
                {
                    field: 'LevelTypeName', title: '分级类别', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            validType: 'maxLength[30]'
                        }
                    }
                },
                { field: 'Amount', title: '总额', width: 100, align: 'center' }
            ]],
            hidecols: ['Id'],
            singleSelect: false
        });
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return cc.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = cc.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = cc.grid.datagrid('getRowIndex', rows[idx]);
            cc.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var delCheckedRows = cc.grid.datagrid('getChecked');
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
                        return gFunc.isNull(row.Id);//过滤条件：Id为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        cc.grid.datagrid('deleteRow', cc.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(delCheckedRows, function (row, idx) {
                    return !gFunc.isNull(row.Id);//过滤条件：Id不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.Id);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('ClientClassificationService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        cc.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = cc.grid.datagrid('getChecked');
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
            var rowIdx = cc.grid.datagrid('getRowIndex', row);
            if (!cc.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            cc.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            if (gFunc.isNull(row.Amount)) {
                row.Amount = 0;
            }
            editingRows.push(row);
            cc.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var data = JSON.stringify(editingRows);
        $.post('ClientClassificationService.asmx/Save', data, function (result) {
            if (result && result.code) {
                //重新加载
                cc.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        cc.grid.datagrid('insertRow', {
            index: index,
            row: { Amount: 0 }
        });
        cc.grid.datagrid('selectRow', index);
        cc.grid.datagrid('beginEdit', index);
    },
    classify: function () {
        $.messager.confirm('询问', '自动分级将清空当前分级结果，是否继续？', function (result) {
            if (result) {
                $.post('ClientClassificationService.asmx/Classify', null, function (result) {
                    if (result && result.code) {
                        //重新加载
                        cc.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    helpReceiver: {
        client: function (clientData, target) {
            if (gFunc.isNull(clientData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, clientData.ClientName);
        },
        clientLevel: function (levelData, target) {
            if (gFunc.isNull(levelData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, levelData.LevelName);
        }
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = cc.formSearch.serializeToJson(true);
        //重新查询
        cc.grid.datagrid("reload", searchParams);
    }
}