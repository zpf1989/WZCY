
//弹出帮助相关代码

/*————————————————————————帮助相关:start——————————————————————*/
var helpInitializer = {
    dept: function (grid) {
        if (!grid) {
            return;
        }
        grid.datagrid({
            url: gFunc.getRootPath() + '/OA/SysManage/Department/DepartmentService.asmx/GetForGridHelp',
            title: '部门帮助',
            singleSelect: true,
            pagination: true,
            pageSize: 10,
            fit: true,
            columns: [[
                { field: 'DeptId', title: '主键', width: 50, align: 'center' },
                { field: 'DeptCode', title: '部门编号', width: 100, align: 'center' },
                { field: 'DeptName', title: '部门名称', width: 60, align: 'center' },
                { field: 'Remark', title: '备注', width: 60, align: 'center' },
            ]]
        });
        grid.datagrid('hideColumn', 'DeptId');//隐藏id列
    },
    role: function (grid) {
        if (!grid) {
            return;
        }
        grid.datagrid({
            url: gFunc.getRootPath() + '/OA/SysManage/RoleManage/RoleManageService.asmx/GetForGridHelp',
            title: '角色帮助',
            singleSelect: true,
            pagination: true,
            pageSize: 10,
            fit: true,
            columns: [[
                { field: 'RoleId', title: '主键', width: 50, align: 'center' },
                { field: 'RoleCode', title: '角色编号', width: 100, align: 'center' },
                { field: 'RoleName', title: '角色名称', width: 60, align: 'center' }
            ]]
        });
        grid.datagrid('hideColumn', 'RoleId');//隐藏id列
    }
};

/*
弹出列表帮助
width,：宽
height：高
isModal：是否模态
funLoadCallback：弹出窗口加载后执行的操作（如加载列表数据等初始化页面内容操作）
funSubmitCallback：点击确定按钮后的操作，用于父界面获取弹出框选择的值（通过注册回调函数传递）
target：触发弹出窗体的控件
*/
function showPopGridHelp(width, height, isModal, funLoadCallback, funSubmitCallback, target) {

    isModal = isModal == true ? true : false;
    var id = "_tmpWin_" + Math.floor(Math.random() * 10000 + 1);
    var win = $("<div id='" + id + "'></div>");

    win.addClass("myOpenWindow");
    win.appendTo($("body"));
    //var helpUrl = gFunc.getRootPath();
    $(win).dialog({
        title: "",
        href: gFunc.getRootPath() + '/help/help_grid_public.html',
        width: width,
        height: height,
        modal: isModal,
        iconCls: null,
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            width: 75,
            handler: function () {
                var selData = null;
                var row = $('#help_grid').datagrid('getSelected');
                if (funSubmitCallback && target) {
                    funSubmitCallback(row, target);
                }
                $(win).dialog("close");
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
            funLoadCallback($('#help_grid'));
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
}
/*————————————————————————帮助相关:end——————————————————————*/

