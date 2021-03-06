﻿var clientlevelFormatter = {
    levelType: {
        src: [{ value: 'SOTotalAmmount', text: '销售订单总额' }],
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
    }
};

//客户列表对象
var clientlevel = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        clientlevel.initgrid();
        clientlevel.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        clientlevel.btnSearch.click(clientlevel.doSearch);
    },
    initgrid: function () {
        gFunc.initGridPublic(clientlevel.grid, {
            title: '客户分级设置',
            icon: 'icon-edit',
            key: 'LevelId',
            url: 'ClientLevelService.asmx/GetList',
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: clientlevel.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: clientlevel.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: clientlevel.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: clientlevel.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    clientlevel.grid.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'LevelId' },
                {
                    field: 'LevelName', title: '分级名称', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[50]'
                        }
                    }
                },
                {
                    field: 'LevelType', title: '分级类别', width: 100, align: 'center',
                    formatter: function (value, row, index) { return clientlevelFormatter.levelType.format(value); },
                    editor: {
                        type: 'combobox',
                        options: {
                            editable: false,
                            required: true,
                            valueField: 'value',
                            textField: 'text',
                            data: clientlevelFormatter.levelType.src
                        }
                    }
                },
                {
                    field: 'LevelMax', title: '上限', width: 100, align: 'center',
                    editor: {
                        type: 'numberbox',
                        options: {
                            required: true,
                            precision: 8,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'LevelMin', title: '下限', width: 100, align: 'center',
                    editor: {
                        type: 'numberbox',
                        options: {
                            required: true,
                            precision: 8,
                            validType: 'maxLength[20]'
                        }
                    }
                }
            ]],
            hidecols: ['LevelId'],
            singleSelect: false
        });
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return clientlevel.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = clientlevel.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = clientlevel.grid.datagrid('getRowIndex', rows[idx]);
            clientlevel.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var delCheckedRows = clientlevel.grid.datagrid('getChecked');
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
                        return gFunc.isNull(row.LevelId);//过滤条件：LevelId为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        clientlevel.grid.datagrid('deleteRow', clientlevel.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(delCheckedRows, function (row, idx) {
                    return !gFunc.isNull(row.LevelId);//过滤条件：LevelId不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.LevelId);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('ClientLevelService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        clientlevel.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = clientlevel.grid.datagrid('getChecked');
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
            var rowIdx = clientlevel.grid.datagrid('getRowIndex', row);
            if (!clientlevel.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            clientlevel.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            editingRows.push(row);
            clientlevel.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var data = JSON.stringify(editingRows);
        $.post('ClientLevelService.asmx/Save', data, function (result) {
            if (result && result.code) {
                //重新加载
                clientlevel.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        clientlevel.grid.datagrid('insertRow', {
            index: index,
            row: { LevelType: 'SOTotalAmmount', LevelMax: 0, LevelMin: 0 }
        });
        clientlevel.grid.datagrid('selectRow', index);
        clientlevel.grid.datagrid('beginEdit', index);
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = clientlevel.formSearch.serializeToJson(true);
        //重新查询
        clientlevel.grid.datagrid("reload", searchParams);
    }
}