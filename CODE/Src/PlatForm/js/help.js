/// <reference path="../OA/InventoryManage/MaterialType/MaterialTypeService.asmx" />
/// <reference path="../OA/InventoryManage/MaterialType/MaterialTypeService.asmx" />

//弹出帮助相关代码

/*————————————————————————帮助相关:start——————————————————————*/
var helpInitializer = {
    dept: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "部门帮助",
            url: gFunc.getRootPath() + '/OA/SysManage/Department/DepartmentService.asmx/GetForGridHelp',
            columns: [[
                { field: 'DeptId' },
                { field: 'DeptCode', title: '部门编号', width: 100, align: 'center' },
                { field: 'DeptName', title: '部门名称', width: 60, align: 'center' },
                { field: 'Remark', title: '备注', width: 60, align: 'center' },
            ]],
            hidecols: ['DeptId'],
            singleSelect: true
        });
    },
    role: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "角色帮助",
            url: gFunc.getRootPath() + '/OA/SysManage/RoleManage/RoleManageService.asmx/GetForGridHelp',
            columns: [[
                { field: 'RoleID' },
                { field: 'RoleCode', title: '角色编号', width: 100, align: 'center' },
                { field: 'RoleName', title: '角色名称', width: 60, align: 'center' }
            ]],
            hidecols: ['RoleID'],
            singleSelect: true
        });
    },
    user: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "用户帮助",
            url: gFunc.getRootPath() + '/OA/SysManage/UserManage/UserManageService.asmx/GetListForHelp',
            columns: [[
                { field: 'UserID' },
                { field: 'DeptID' },
                { field: 'RoleID' },
                { field: 'UserCode', title: '用户编号', width: 100, align: 'center' },
                { field: 'UserName', title: '用户名称', width: 60, align: 'center' },
                { field: 'Dept_Name', title: '所属部门', width: 60, align: 'center' },
                { field: 'Role_Name', title: '用户角色', width: 60, align: 'center' },
            ]],
            hidecols: ['UserID', 'DeptID', 'RoleID'],
            singleSelect: true
        });
    },
    materialClass: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "物料分类帮助",
            url: gFunc.getRootPath() + '/OA/InventoryManage/MaterialClass/MaterialClassService.asmx/GetListForHelp',
            columns: [[
                { field: 'MaterialClassID' },
                { field: 'MaterialClassCode', title: '分类编号', width: 100, align: 'center' },
                { field: 'MaterialClassName', title: '分类名称', width: 100, align: 'center' }
            ]],
            hidecols: ['MaterialClassID'],
            singleSelect: true
        });
    },
    materialType: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "物料类型帮助",
            url: gFunc.getRootPath() + '/OA/InventoryManage/MaterialType/MaterialTypeService.asmx/GetListForHelp',
            columns: [[
                { field: 'MaterialTypeID' },
                { field: 'MaterialTypeCode', title: '类型编号', width: 100, align: 'center' },
                { field: 'MaterialTypeName', title: '类型名称', width: 100, align: 'center' }
            ]],
            hidecols: ['MaterialTypeID'],
            singleSelect: true
        });
    },
    measureUnit: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "计量单位帮助",
            url: gFunc.getRootPath() + '/OA/InventoryManage/MeasureUnits/MeasureUnitsService.asmx/GetListForHelp',
            columns: [[
                { field: 'UnitID' },
                { field: 'UnitCode', title: '单位编号', width: 100, align: 'center' },
                { field: 'UnitName', title: '单位名称', width: 100, align: 'center' }
            ]],
            hidecols: ['UnitID'],
            singleSelect: true
        });
    },
    payType: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "付款方式帮助",
            url: gFunc.getRootPath() + '/OA/SalesManage/PayType/PayTypeService.asmx/GetListForHelp',
            columns: [[
                { field: 'PayTypeID' },
                { field: 'PayTypeCode', title: '付款方式编号', width: 100, align: 'center' },
                { field: 'PayTypeName', title: '付款方式名称', width: 100, align: 'center' }
            ]],
            hidecols: ['PayTypeID'],
            singleSelect: true
        });
    },
    billType: function (grid) {
        if (!grid) {
            return;
        }
        gFunc.initGridPublic(grid, {
            title: "单据类型帮助",
            url: gFunc.getRootPath() + '/OA/SalesManage/BillType/BillTypeService.asmx/GetListForHelp?BillType=sale',
            columns: [[
                { field: 'BillID' },
                { field: 'BillCode', title: '单据类型编号', width: 100, align: 'center' },
                { field: 'BillName', title: '单据类型名称', width: 100, align: 'center' },
                { field: 'Remark', title: '备注', width: 100, align: 'center' }
            ]],
            hidecols: ['BillID'],
            singleSelect: true
        });
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
                if (funSubmitCallback) {
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

