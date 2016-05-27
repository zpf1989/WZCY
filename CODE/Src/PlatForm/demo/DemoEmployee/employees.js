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
            idField: 'id',
            url: 'EmployeeService.asmx/GetList',
            remoteSort: false,
            rownumbers: true,
            pagination: true,
            pageSize: 10,
            fit: false,
            selectOnCheck: true,
            //toolbar:'#toolbar',
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: employees.insertRow
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
                },
                {
                    field: 'action', title: '操作', width: 120, align: 'center',
                    formatter: function (value, row, index) {
                        if (row.editing) {
                            return '<input type="button" onclick="employees.saveRow(this)" value="保存"/>';
                            //+ '<input type="button" onclick="employees.cancelRow(this)" value="取消"/>';
                        } else {
                            return '<input type="button" onclick="employees.editRow(this)" value="修改"/>' +
                                '<input type="button" onclick="employees.deleteRow(this)" value="删除"/>';
                        }
                    }
                }
            ]],
            onLoadSuccess: function (data) {
                //tooltips，先禁用tooltips，不完善
                //employees.grid.datagrid('doCellTip', {
                //    onlyShowInterrupt: true,
                //    position: 'bottom',
                //    maxWidth: '300px',
                //    tipStyler: {
                //        borderColor: '#000',
                //        boxShadow: '1px 1px 3px #000'
                //    }
                //});
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
            return;
        }
        return employees.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRow: function (target) {
        if (gFunc.isNull(target)) {
            return;
        }
        var index = employees.getRowIndexByEditor(target);
        employees.grid.datagrid('selectRow', index);
        employees.grid.datagrid('beginEdit', index);
    },
    deleteRow: function (target) {
        if (gFunc.isNull(target)) {
            return;
        }
        var index = employees.getRowIndexByEditor(target);
        var row = employees.grid.datagrid('getRows')[index];
        $.messager.confirm('提示', '确定要删除吗?', function (r) {
            if (r) {
                if (gFunc.isNull(row.EmpId)) {
                    //主键为空，说明是新增的，客户端直接删除即可
                    employees.grid.datagrid('clearSelections');
                    employees.grid.datagrid('deleteRow', index);
                    return;
                }
                $.post('EmployeeService.asmx/Delete', JSON.stringify([row.EmpId]), function (result) {
                    if (result) {
                        if (result.code) {
                            //重新加载
                            employees.grid.datagrid('reload');
                        } else if (result.msg) {
                            $.messager.alert('删除失败', result.msg);
                        }
                    }
                });
            }
        });
    },
    deleteRowBatch: function () {
        var rows = employees.grid.datagrid('getSelections');
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                var ids = [];
                for (var idx = 0; idx < rows.length; idx++) {
                    if (gFunc.isNull(rows[idx].EmpId)) {
                        employees.grid.datagrid('deleteRow', employees.grid.datagrid('getRowIndex', rows[idx]));
                    } else {
                        ids.push(rows[idx].EmpId);
                    }
                }
                $.post('EmployeeService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        employees.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRow: function (target) {
        if (gFunc.isNull(target)) {
            return;
        }
        var index = employees.getRowIndexByEditor(target);
        if (!employees.grid.datagrid('validateRow', index)) {
            return;
        }
        employees.grid.datagrid('endEdit', index);//执行这句，否则下面的row数据不全（试试其他办法，Org可以获取到）
        var row = employees.grid.datagrid('getRows')[index];

        //提交到服务端
        $.post('EmployeeService.asmx/Save', JSON.stringify([row]), function (result) {
            if (result && result.code) {
                //重新加载
                employees.grid.datagrid('reload');
                return;
            }
            if (!result || !result.code || result.code != "1") {
                if (result.msg) {
                    $.messager.alert('错误', result.msg);
                }
                //修改状态
                employees.grid.datagrid('beginEdit', index);
            }

        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = employees.grid.datagrid('getSelections');
        if (gFunc.isNull(selectedRows) || selectedRows.length < 1) {
            return;
        }
        var editingRows = [];
        var checkSucceed = true;
        //逐行校验
        for (var idx = 0; idx < selectedRows.length; idx++) {
            var row = selectedRows[idx];
            if (!row.editing) {
                continue;
            }
            var rowIdx = employees.grid.datagrid('getRowIndex', row);
            if (!employees.grid.datagrid('validateRow', rowIdx)) {
                checkSucceed = false;
                continue;
            }
            employees.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            editingRows.push(row);
        }
        if (!checkSucceed || editingRows.length < 1) {
            return;
        }
        //提交保存
        $.post('EmployeeService.asmx/Save', JSON.stringify(editingRows), function (result) {
            if (result && result.code) {
                //重新加载
                employees.grid.datagrid('reload');
            }
        });
    },
    cancelRow: function (target) {
        if (gFunc.isNull(target)) {
            return;
        }
        employees.grid.datagrid('cancelEdit', employees.getRowIndexByEditor(target));
    },
    insertRow: function () {
        var row = employees.grid.datagrid('getSelected');
        if (row) {
            var index = employees.grid.datagrid('getRowIndex', row);
        } else {
            index = 0;
        }
        employees.grid.datagrid('insertRow', {
            index: index,
            row: {//默认值
                EmpGender: 1,
                EmpBirthDay: (new Date()),
                EmpSalary: 0,
                EmpAge: 0,
                EmpCode: 'code' + index,
                EmpName: 'name' + index
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
            $(target).val(deptData.DeptName);
            var index = employees.getRowIndexByEditor(target);
            var row = employees.grid.datagrid('getRows')[index];
            //给关联列赋值
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