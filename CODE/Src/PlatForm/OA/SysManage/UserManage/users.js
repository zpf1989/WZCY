//用户信息格式化对象
var userInfoFormatter = {
    //用户状态
    userState: {
        src: [{ value: '1', text: '生效' }, { value: '0', text: '失效' }],
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
    },
    //操作权限
    operator: {
        src: [//value、text对应关系待定
            { value: '1', text: '增加' },
            { value: '2', text: '修改' },
            { value: '3', text: '删除' },
            { value: '4', text: '查看' },
            { value: '5', text: '审批' }
        ],
        //将"1,2,3"转化为"增加,修改,删除"
        format: function (value) {
            var txt = "";
            if (gFunc.isNull(value)) {
                return txt;
            }
            //遍历数组[1,2,3]
            $.each(value.split(','), function (index, val) {
                //在src数组中，查找与val匹配的项(jquery全局函数grep对数组过滤)
                var keyVal = $.grep(userInfoFormatter.operator.src, function (item, idx) {
                    return item.value == val;//这里是筛选条件
                });
                if (!gFunc.isNull(keyVal) && keyVal.length > 0) {
                    txt += keyVal[0].text + "|";
                }
            });
            if (txt.lastIndexOf('|') == txt.length - 1) {
                txt = txt.substr(0, txt.length - 1);
            }
            return txt;
        }
    }
};

//用户状态格式化
var userSateFormatter = {

};

//用户列表对象
var users = {
    grid: $('#gridUser'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    init: function () {
        users.initgrid();
        users.bindingEvents();
    },
    //绑定（注册）事件
    bindingEvents: function () {
        users.btnSearch.click(users.doSearch);
    },
    initgrid: function () {
        users.grid.datagrid({
            title: '用户列表',
            iconCls: 'icon-edit',
            width: 1000,
            height: 450,
            singleSelect: false,
            idField: 'UserID',//列表主键，必须
            url: 'UserManageService.asmx/GetList',
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
                handler: users.insertRow
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: users.editRowBatch
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: users.deleteRowBatch
            }, "-", {
                id: 'btnSave',
                text: '保存',
                iconCls: 'icon-save',
                handler: users.saveRowBatch
            }, "-", {
                id: 'btnCancel',
                text: '取消',
                iconCls: 'icon-cancel',
                handler: function () {
                    users.grid.datagrid('rejectChanges');
                    users.grid.datagrid('clearChecked');
                    users.grid.datagrid('clearSelections');
                }
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'UserID' },
                {
                    field: 'UserCode', title: '用户编号', width: 100, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                {
                    field: 'UserName', title: '用户姓名', width: 80, align: 'center',
                    editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            validType: 'maxLength[20]'
                        }
                    }
                },
                { field: 'DeptID', title: '部门ID' },
                {
                    field: 'Dept_Name', title: '所属部门', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.dept, users.helpReceiver.dept, target);
                            }
                        }
                    }
                },
                { field: 'RoleID' },
                {
                    field: 'Role_Name', title: '角色', align: 'center', width: 100,
                    editor: {
                        type: 'helpEdit',
                        options: {
                            required: true,
                            onclick: function (target) {
                                if (gFunc.isNull(target)) {
                                    return;
                                }
                                showPopGridHelp(400, 300, true, helpInitializer.role, users.helpReceiver.role, target);
                            }
                        }
                    }
                },
                {
                    field: 'UserState', title: '状态', width: 80, align: 'center',
                    formatter: function (value, row, index) { return userInfoFormatter.userState.format(value); },
                    editor: {
                        type: 'combobox',
                        options: {
                            editable: false,
                            required: true,
                            valueField: 'value',
                            textField: 'text',
                            data: userInfoFormatter.userState.src
                        }
                    }
                },
                {
                    field: 'Operator', title: '操作权限', width: 150, align: 'center',
                    formatter: function (value, row, index) { return userInfoFormatter.operator.format(value); },
                },
                {
                    field: 'CreateTime', title: '创建时间', width: 130, align: 'center',
                    formatter: formatHandler.datetime.format
                }
            ]],
            onLoadSuccess: function (data) {
                users.grid.datagrid('clearChecked');
                users.grid.datagrid('clearSelections');
            },
            onEndEdit: function (index, row, changes) {
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
        users.grid.datagrid('hideColumn', 'UserID');//隐藏id列
        users.grid.datagrid('hideColumn', 'DeptID');//隐藏部门id列
        users.grid.datagrid('hideColumn', 'RoleID');//隐藏角色id列
    },
    getRowIndexByEditor: function (target) {
        if (gFunc.isNull(target)) {
            return null;
        }
        return users.grid.datagrid('getRowIndexByEditor', { element: target });
    },
    editRowBatch: function () {
        var rows = users.grid.datagrid('getChecked');
        console.log(rows.length);
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        for (var idx = 0; idx < rows.length; idx++) {
            if (rows[idx].editing) {
                continue;
            }
            var index = users.grid.datagrid('getRowIndex', rows[idx]);
            users.grid.datagrid('beginEdit', index);
        }
    },
    deleteRowBatch: function () {
        var checkedRows = users.grid.datagrid('getChecked');
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
                        return gFunc.isNull(row.UserID);//过滤条件：UserID为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        users.grid.datagrid('deleteRow', users.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(checkedRows, function (row, idx) {
                    return !gFunc.isNull(row.UserID);//过滤条件：UserID不为空
                });
                if (gFunc.isNull(checkedSavedRows) || checkedSavedRows.length < 1) {
                    return;
                }
                var ids = [];
                $.each(checkedSavedRows, function (index, row) {
                    ids.push(row.UserID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('UserManageService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        users.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    saveRowBatch: function () {
        //获取所有正在编辑的行
        var selectedRows = users.grid.datagrid('getChecked');
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
            var rowIdx = users.grid.datagrid('getRowIndex', row);
            if (!users.grid.datagrid('validateRow', rowIdx)) {
                $.messager.alert('提示', '数据校验失败，请检查输入！');
                return;
            }
            users.grid.datagrid('endEdit', rowIdx);//执行这句，否则下面的row数据不全
            editingRows.push(row);
            users.grid.datagrid('beginEdit', rowIdx);
        }
        if (editingRows.length < 1) {
            return;
        }
        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        $.post('userservice.asmx/Save', JSON.stringify(editingRows), function (result) {
            if (result && result.code) {
                //重新加载
                users.grid.datagrid('reload');
            }
        });
    },
    insertRow: function () {
        var index = 0;
        users.grid.datagrid('insertRow', {
            index: index,
            row: {//默认值
                UserState: 1,
                CreateTime: (new Date())
            }
        });
        users.grid.datagrid('selectRow', index);
        users.grid.datagrid('beginEdit', index);
    },
    helpReceiver: {
        dept: function (deptData, target) {
            if (gFunc.isNull(deptData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, deptData.DeptName);
            //给关联列赋值
            var index = users.getRowIndexByEditor(target);
            var row = users.grid.datagrid('getRows')[index];
            users.grid.datagrid('updateRowCell', { field: 'DeptID', index: index, value: deptData.DeptID });
        },
        role: function (roleData, target) {
            if (gFunc.isNull(roleData) || gFunc.isNull(target)) {
                return;
            }
            //调用easyui扩展编辑器的赋值方法
            $.fn.datagrid.defaults.editors.helpEdit.setValue(target, roleData.RoleName);
            //给关联列赋值
            var index = users.getRowIndexByEditor(target);
            var row = users.grid.datagrid('getRows')[index];
            users.grid.datagrid('updateRowCell', { field: 'RoleID', index: index, value: roleData.RoleID });
        }
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = users.formSearch.serializeToJson(true);
        //重新查询
        users.grid.datagrid("reload", searchParams);
    }
}