var employees = {
    grid: $('#gridEmployee'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        employees.initgrid();
        employees.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        employees.btnSearch.click(employees.doSearch);
    },
    initgrid: function () {
        employees.grid.datagrid({
            title: '雇员列表',
            iconCls: 'icon-edit',
            width: 1000,
            height: 450,
            singleSelect: false,
            idField: 'EmpId',//列表主键，必须
            url: 'EmployeeService.asmx/GetList',
            remoteSort: false,
            rownumbers: true,
            pagination: true,
            pageSize: 10,
            fit: false,
            selectOnCheck: true,
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: employees.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: employees.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: employees.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: employees.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    employees.grid.datagrid('rejectChanges');
                    employees.grid.datagrid('clearChecked');
                    employees.grid.datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'EmpId', title: 'ID', width: 60, visible: false },
                {
                    field: 'EmpCode', title: '编号', width: 80, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'EmpName', title: '姓名', width: 80, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'EmpGender', title: '性别', width: 80, align: 'center',
                    formatter: function (value, row, index) { return formatHandler.gender.format(value); },
                    editor: {
                        type: 'combobox',
                        options: {
                            editable: false,
                            required: true,
                            valueField: 'value',
                            textField: 'text',
                            data: formatHandler.gender.src
                        }
                    }
                },
                {
                    field: 'EmpBirthDay', title: '出生日期', width: 120, align: 'center',
                    formatter: formatHandler.date.format,
                    editor: {
                        type: 'datebox',
                        options: {
                            editable: false,//禁止手输
                            required: true,
                            formatter: formatHandler.date.format,
                            parser: formatHandler.date.parse
                        }
                    }
                },
                {
                    field: 'EmpAge', title: '年龄', width: 80, align: 'center',
                },
                {
                    field: 'EmpSalary', title: '薪资', width: 80, align: 'center',
                    editor: {
                        type: 'numberbox',
                        options: { precision: 2 }
                    }
                },
                { field: 'DeptId', title: '所属组织ID' },
                {
                    field: 'DeptName', title: '所属组织', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.dept, employees.helpReceiver.dept, target);
                            }
                        }
                    }
                }
            ]],
            onLoadSuccess: function (data) {
                employees.grid.datagrid('clearChecked');
                employees.grid.datagrid('clearSelections');
            },
            onEndEdit: function (index, row, changes) {
                //自动计算年龄
                row.EmpAge = gFunc.getAge(row.EmpBirthDay);
                row.editing = false;
            },
            onBeforeEdit: function (index, row) {
                row.editing = true;
                $(this).datagrid('refreshRow', index);
            },
            onAfterEdit: function (index, row) {
                row.editing = false;
                $(this).datagrid('refreshRow', index);
            },
            onCancelEdit: function (index, row) {
                row.editing = false;
                $(this).datagrid('refreshRow', index);
            }
        });
        employees.grid.datagrid('hideColumn', 'EmpId');//隐藏id列
        employees.grid.datagrid('hideColumn', 'DeptId');//隐藏部门id列
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return employees.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = employees.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = employees.grid.datagrid('getRowIndex', rows[idx]);
            employees.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var checkedRows = employees.grid.datagrid('getChecked');
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
                        return gFunc.isNull(row.EmpId);//过滤条件：EmpId为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        employees.grid.datagrid('deleteRow', employees.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(checkedRows, function (row, idx) {
                    return !gFunc.isNull(row.EmpId);//过滤条件：EmpId不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.EmpId);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('EmployeeService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        employees.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = employees.grid.datagrid('getChecked');
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
            var rowIdx = employees.grid.datagrid('getRowIndex', row);
            if (!employees.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            employees.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            editingRows.push(row);
            employees.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        $.post('EmployeeService.asmx/Save', JSON.stringify(editingRows), function (result) {
            if (result && result.code) {
                //重新加载
                employees.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        employees.grid.datagrid('insertRow', {
            index: index,
            row: {//默认值
                EmpGender: 1,
                EmpBirthDay: (new Date()),
                EmpSalary: 0,
                EmpAge: 0
            }
        });
        employees.grid.datagrid('selectRow', index);
        employees.grid.datagrid('beginEdit', index);
    },
    helpReceiver: {
        dept: function (deptData, target) {
            if (gFunc.isNull(deptData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, deptData.DeptName);
            //给关联列赋值
            var index = employees.getRowIndexByEditor(target);
            var row = employees.grid.datagrid('getRows')[index];
            employees.grid.datagrid('updateRowCell', { field: 'DeptId', index: index, value: deptData.DeptId });

        }
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = employees.formSearch.serializeToJson(true);
        //重新查询
        employees.grid.datagrid("reload", searchParams);
    }
}