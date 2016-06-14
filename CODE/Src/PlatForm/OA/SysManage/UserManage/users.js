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
            { value: '1', text: '查看' },
            { value: '2', text: '修改' },
            { value: '3', text: '删除' },
            { value: '4', text: '打印' },
            { value: '5', text: '导出' }
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
        gFunc.initGridPublic(users.grid, {
            title: '用户列表',
            icon: 'icon-edit',
            key: 'UserID',
            url: 'UserManageService.asmx/GetList',
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
                    users.grid.datagrid('rejectChanges').datagrid('clearChecked').datagrid('clearSelections');
                }
            }, "-", {
                id: 'btnOperator',
                text: '操作权限',
                iconCls: 'icon-edit',
                handler: users.setOperatorBatch
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
            hidecols: ['UserID', 'DeptID', 'RoleID'],
        });
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
        var delCheckedRows = users.grid.datagrid('getChecked');
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
                        return gFunc.isNull(row.UserID);//过滤条件：UserID为空
                    });
                    if (!gFunc.isNull(checkedNewRows) && checkedNewRows.length > 0) {
                        users.grid.datagrid('deleteRow', users.grid.datagrid('getRowIndex', checkedNewRows[0]));
                    } else {
                        break;
                    }
                };
                //2、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var checkedSavedRows = $.grep(delCheckedRows, function (row, idx) {
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
        $.post('UserManageService.asmx/Save', JSON.stringify(editingRows), function (result) {
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
    //批量设置操作权限
    setOperatorBatch: function () {
        var optCheckedRows = users.grid.datagrid('getChecked');
        if (gFunc.isNull(optCheckedRows) || optCheckedRows.length < 1) {
            $.messager.alert('提示', '请选择要设置操作权限的数据');
            return;
        }

        var id = "_tmpWin_" + Math.floor(Math.random() * 10000 + 1);
        var win = $("<div id='" + id + "'></div>");

        win.addClass("myOpenWindow");
        win.appendTo($("body"));
        $(win).dialog({
            title: "操作权限分配",
            href: 'operator.html',
            width: 200,
            height: 200,
            modal: true,
            iconCls: null,
            buttons: [{
                text: '确定',
                iconCls: 'icon-ok',
                width: 75,
                handler: function () {
                    //获取选择项列表
                    var optlist = $("input[name='optlist']:checked");
                    var optvalues = [];
                    $.each(optlist, function (index, opt) {
                        optvalues.push(opt.value);
                    });
                    //获取选择用户列表
                    var ids = [];
                    $.each(optCheckedRows, function (index, row) {
                        ids.push(row.UserID);
                    });
                    //提交到后台
                    $.post('UserManageService.asmx/SetOpt', JSON.stringify([ids, optvalues]), function (result) {
                        if (result && result.code) {
                            //重新加载
                            $(win).dialog("close");
                            users.grid.datagrid('reload');
                        } else {
                            $.messager.alert('提示', '操作权限设置失败');
                        }
                    });
                }
            }, {
                text: '取消',
                iconCls: 'icon-cancel',
                width: 75,
                handler: function () {
                    $(win).dialog("close");
                }
            }],
            onClose: function () {
                $(win).dialog("destroy");
            },
            onLoad: function () {
                $("#" + id).next().children("a").first().focus();
                $(win).keydown(function (event) {
                    if (event.keyCode == 13) {
                        var b = funSubmitCallback();
                        if (b != false) {
                            $(win).dialog("close");
                        }
                    }
                });

            }

        });
        return id;
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