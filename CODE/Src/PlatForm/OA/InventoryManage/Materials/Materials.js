
//物料分类列表对象
var materials = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtMClassID: $('#mClassID'),
    txtMClassName: $('#mClassName'),
    btnHelpMClass: $('#btnHelpMClass'),
    txtMTypeID: $('#mTypeID'),
    txtMTypeName: $('#mTypeName'),
    btnHelpMType: $('#btnHelpMType'),
    init: function () {
        materials.initgrid();
        materials.bindingEvents();
        materials.formSearch.children('div').css({ 'float': 'left', 'padding-left': '8px' });
    },
    //绑定（注册）事件
    bindingEvents: function () {
        materials.btnSearch.click(materials.doSearch);
        materials.btnHelpMClass.click(materials.onClickMaterialClass);
        materials.btnHelpMType.click(materials.onClickMaterialType);
        materials.txtMClassName.textbox({
            onChange: function (newValue, oldValue) {
                if (oldValue != newValue) {
                    //手动输入时，id设置空
                    materials.txtMClassID.val("");
                }
            }
        });
        materials.txtMTypeName.textbox({
            onChange: function (newValue, oldValue) {
                if (oldValue != newValue) {
                    //手动输入时，id设置空
                    materials.txtMTypeID.val("");
                }
            }
        });
    },
    initgrid: function () {
        gFunc.initGridPublic(materials.grid, {
            title: '物料列表',
            icon: 'icon-edit',
            key: 'MaterialID',
            url: 'MaterialsService.asmx/GetList',
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: materials.addMaterial
            }, "-", {
                id: 'btnEdit',
                text: '修改',
                iconCls: 'icon-edit',
                handler: materials.editMaterial
            }, "-", {
                id: 'btnDelete',
                text: '删除',
                iconCls: 'icon-remove',
                handler: materials.deleteRowBatch
            }],
            columns: [[
                { field: 'ck', title: '', width: 100, align: 'center', checkbox: true },
                { field: 'MaterialID' },
                { field: 'MaterialClassID' },
                { field: 'MaterialTypeID' },
                { field: 'PrimaryUnitID' },
                { field: 'Creator' },
                { field: 'MaterialCode', title: '物料编号', width: 100, align: 'center' },
                { field: 'MaterialName', title: '物料名称', width: 100, align: 'center' },
                { field: 'Specs', title: '规格型号', width: 100, align: 'center' },
                { field: 'MaterialClass_Name', title: '物料分类', width: 100, align: 'center' },
                { field: 'MaterialType_Name', title: '物料类型', width: 100, align: 'center' },
                { field: 'PrimaryUnit_Name', title: '基本计量单位', width: 120, align: 'center' },
                { field: 'Price', title: '价格', width: 100, align: 'center' },
                { field: 'WasterRate', title: '废品率', width: 100, align: 'center' },
                { field: 'Remark', title: '备注', width: 100, align: 'center' },
                { field: 'Creator_Name', title: '创建人', width: 100, align: 'center' },
                 { field: 'CreateTime', title: '创建时间', width: 100, align: 'center' }
            ]],
            hidecols: ['MaterialID', 'MaterialClassID', 'MaterialTypeID', 'PrimaryUnitID', 'Creator'],
        });
    },
    editMaterial: function () {
        var rows = materials.grid.datagrid('getChecked');
        if (gFunc.isNull(rows) || rows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        if (rows.length > 1) {
            $.messager.alert('提示', '只能修改一条数据');
            return;
        }
        //弹出窗体
    },
    deleteRowBatch: function () {
        var checkedRows = materials.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要删除的数据');
            return;
        }

        $.messager.confirm('询问', '确定要删除所选数据吗？', function (result) {
            if (result) {
                //1、删除选中行中以保存的部分（这部分提交到服务端删除，然后刷新列表）
                var ids = [];
                $.each(checkedRows, function (index, row) {
                    ids.push(row.MaterialID);
                });
                //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
                $.post('MaterialsService.asmx/Delete', JSON.stringify(ids), function (result) {
                    if (result && result.code) {
                        //重新加载
                        materials.grid.datagrid('reload');
                    }
                });
            }
        });
    },
    addMaterial: function () {
        //弹出窗体
    },
    doSearch: function () {
        //收集查询条件
        var searchParams = materials.formSearch.serializeToJson(true);
        //重新查询
        materials.grid.datagrid("reload", searchParams);
    },
    helpReceiver: {
        materialClass: function (classData) {
            //注意：必须先给name赋值，因为它会触发onChange事件，会把id冲掉
            materials.txtMClassName.textbox('setValue', classData.MaterialClassName);
            materials.txtMClassID.val(classData.MaterialClassID);
        },
        materialType: function (classData) {
            materials.txtMTypeName.textbox('setValue', classData.MaterialTypeName);
            materials.txtMTypeID.val(classData.MaterialTypeID);
        },
    },
    onClickMaterialClass: function () {
        showPopGridHelp(400, 300, true, helpInitializer.materialClass, materials.helpReceiver.materialClass, null);
    },
    onClickMaterialType: function () {
        showPopGridHelp(400, 300, true, helpInitializer.materialType, materials.helpReceiver.materialType, null);
    }
}