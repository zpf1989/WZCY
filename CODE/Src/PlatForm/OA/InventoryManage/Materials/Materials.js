
//物料分类列表对象
var materials = {
    grid: $('#grid'),
    formSearch: $('#searchForm'),
    btnSearch: $('#btnSearch'),
    txtSearchClassID: $('#txtSearchClassID'),
    txtSearchClassName: $('#txtSearchClassName'),
    btnSearchHelpMClass: $('#btnSearchHelpMClass'),
    txtSearchTypeID: $('#txtSearchTypeID'),
    txtSearchTypeName: $('#txtSearchTypeName'),
    btnSearchHelpMType: $('#btnSearchHelpMType'),
    btnCardHelpMClass: $('#btnCardHelpMClass'),
    btnCardHelpMType: $('#btnCardHelpMType'),
    btnCardHelpMUnit: $('#btnCardHelpMUnit'),
    txtCardClassName: $('#txtCardClassName'),
    txtCardClassID: $('#txtCardClassID'),
    txtCardTypeName: $('#txtCardTypeName'),
    txtCardTypeID: $('#txtCardTypeID'),
    txtCardUnitName: $('#txtCardUnitName'),
    txtCardUnitID: $('#txtCardUnitID'),
    txtCardPrice: $('#txtPrice'),
    txtCardWasterRate: $('#txtWasterRate'),
    cardFormWidth: 600,
    cardFormHeight: 360,
    cardFormUrl: 'MaterialsAdd.html',
    saveUrl: 'MaterialsService.asmx/Save',
    searchUrl: 'MaterialsService.asmx/GetList',
    init: function () {
        materials.initgrid();
        materials.bindingEvents();
        materials.formSearch.children('div').css({ 'float': 'left', 'padding-left': '8px' });
    },
    //绑定（注册）事件
    bindingEvents: function () {
        materials.btnSearch.click(materials.doSearch);
        materials.btnSearchHelpMClass.click(materials.onClickSearchMClass);
        materials.btnSearchHelpMType.click(materials.onClickSearchMType);
        materials.txtSearchClassName.textbox({
            onChange: function (newValue, oldValue) {
                if (oldValue != newValue) {
                    //手动输入时，id设置空
                    materials.txtSearchClassID.val("");
                }
            }
        });
        materials.txtSearchTypeName.textbox({
            onChange: function (newValue, oldValue) {
                if (oldValue != newValue) {
                    //手动输入时，id设置空
                    materials.txtSearchTypeID.val("");
                }
            }
        });
    },
    initgrid: function () {
        gFunc.initGridPublic(materials.grid, {
            title: '物料列表',
            icon: 'icon-edit',
            key: 'MaterialID',
            url: materials.searchUrl,
            toolbar: [{
                id: 'btnAdd',
                text: '新增',
                iconCls: 'icon-add',
                handler: materials.addMaterial
            }, "-", {
                id: 'btnView',
                text: '查看',
                iconCls: 'icon-search',
                handler: materials.viewMaterial
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
            singleSelect: false
        });
    },
    editMaterial: function () {
        var checkedRows = materials.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要修改的数据');
            return;
        }
        if (checkedRows.length > 1) {
            $.messager.alert('提示', '只能修改一条数据');
            return;
        }
        //弹出窗体
        gFunc.showPopWindow({
            title: '修改物料信息',
            width: materials.cardFormWidth,
            height: materials.cardFormHeight,
            url: materials.cardFormUrl,
            isModal: true,
            funLoadCallback: function () {
                //初始化弹出界面
                materials.doSetForm(checkedRows[0], 1);
            },
            funSubmitCallback: function () {
                return materials.doSave();
            }
        });
    },
    addMaterial: function () {
        //弹出窗体
        gFunc.showPopWindow({
            title: '添加物料信息',
            width: materials.cardFormWidth,
            height: materials.cardFormHeight,
            url: materials.cardFormUrl,
            isModal: true,
            funLoadCallback: function () {
                materials.txtCardPrice.numberbox('setValue', 0);
                materials.txtCardWasterRate.numberbox('setValue', 0);
            },
            funSubmitCallback: function () {
                return materials.doSave();
            }
        });
    },
    viewMaterial: function () {
        var checkedRows = materials.grid.datagrid('getChecked');
        if (gFunc.isNull(checkedRows) || checkedRows.length < 1) {
            $.messager.alert('提示', '请选择要查看的数据');
            return;
        }
        if (checkedRows.length > 1) {
            $.messager.alert('提示', '请选择一条数据查看');
            return;
        }
        //弹出窗体
        gFunc.showPopWindow({
            title: '查看物料信息',
            width: materials.cardFormWidth,
            height: materials.cardFormHeight,
            url: materials.cardFormUrl,
            isModal: true,
            funLoadCallback: function () {
                //初始化弹出界面
                materials.doSetForm(checkedRows[0], 2);
            }
        });
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
    doSearch: function () {
        //收集查询条件
        var searchParams = materials.formSearch.serializeToJson(true);
        //重新查询
        materials.grid.datagrid("reload", searchParams);
    },
    doSetForm: function (data, state) {
        gFunc.formFunc.clearValidations('editForm');//清除表单验证
        switch (state) {
            case 1://修改
            case 2://查看
                $('#txtMID').val(data.MaterialID);
                $('#txtCode').textbox('setValue', data.MaterialCode);
                $('#txtName').textbox('setValue', data.MaterialName);
                $('#txtSpecs').textbox('setValue', data.Specs);
                materials.txtCardClassName.textbox('setValue', data.MaterialClass_Name);
                materials.txtCardClassID.val(data.MaterialClassID);
                materials.txtCardTypeName.textbox('setValue', data.MaterialType_Name);
                materials.txtCardTypeID.val(data.MaterialTypeID);
                materials.txtCardUnitName.textbox('setValue', data.PrimaryUnit_Name);
                materials.txtCardUnitID.val(data.PrimaryUnitID);
                $('#txtPrice').numberbox('setValue', data.Price);
                $('#txtWasterRate').numberbox('setValue', data.WasterRate);
                $('#txtRemark').textbox('setValue', data.Remark);

                $('#txtCode').textbox('readonly', true);//编号不能修改
                var boolReadOnly = state == 2 ? true : false;
                $('#txtName').textbox('readonly', boolReadOnly);
                $('#txtSpeces').textbox('readonly', boolReadOnly);
                $('#txtPrice').numberbox('readonly', boolReadOnly);
                $('#txtWasterRate').numberbox('readonly', boolReadOnly);
                $('#txtRemark').textbox('readonly', boolReadOnly);
                if (boolReadOnly) {
                    materials.btnCardHelpMClass.attr({ 'disabled': 'disabled' });
                    materials.btnCardHelpMType.attr({ 'disabled': 'disabled' });
                    materials.btnCardHelpMUnit.attr({ 'disabled': 'disabled' });
                }
                break;
            case 0://添加
            default:
                break;
        }
    },
    initCardControls: function () {
        //注册卡片按钮事件
        materials.btnCardHelpMClass.click(materials.onClickCardMClass);
        materials.btnCardHelpMType.click(materials.onClickCardMType);
        materials.btnCardHelpMUnit.click(materials.onClickCardMUnit);
    },
    doSave: function () {
        //验证数据
        var valRst = gFunc.formFunc.validate("editForm");
        console.log('valresult:' + valRst);
        if (!valRst) {
            return false;
        }
        //收集数据
        var formData = gFunc.formFunc.serializeToJson("editForm");

        //提交保存
        //这里json序列化的目标一定是一个数组，否则，后台解析（解析为列表）时会出错
        var ajaxResult = false;
        $.ajax({
            type: 'post',
            url: materials.saveUrl,
            data: JSON.stringify(formData),
            async: false,//同步请求
            success: function (result) {
                if (result && result.code) {
                    //重新加载
                    materials.grid.datagrid('reload');
                    ajaxResult = true;
                    console.log('materials,ajax succeed');
                } else {
                    console.log('materials,ajax fail');
                    ajaxResult = false;
                }
            },
            error: function () {
                console.log('materials,ajax error');
                ajaxResult = false;
            }
        });
        console.log('materials,doSave over');
        return ajaxResult;
    },
    helpReceiver: {
        searchMClass: function (classData) {
            //注意：必须先给name赋值，因为它会触发onChange事件，会把id冲掉
            materials.txtSearchClassName.textbox('setValue', classData.MaterialClassName);
            materials.txtSearchClassID.val(classData.MaterialClassID);
        },
        searchMType: function (typeData) {
            materials.txtSearchTypeName.textbox('setValue', typeData.MaterialTypeName);
            materials.txtSearchTypeID.val(typeData.MaterialTypeID);
        },
        cardMClass: function (classData) {
            //注意：必须先给name赋值，因为它会触发onChange事件，会把id冲掉
            materials.txtCardClassName.textbox('setValue', classData.MaterialClassName);
            materials.txtCardClassID.val(classData.MaterialClassID);
        },
        cardMType: function (typeData) {
            materials.txtCardTypeName.textbox('setValue', typeData.MaterialTypeName);
            materials.txtCardTypeID.val(typeData.MaterialTypeID);
        },
        cardMUnit: function (unitData) {
            materials.txtCardUnitName.textbox('setValue', unitData.UnitName);
            materials.txtCardUnitID.val(unitData.UnitID);
        },
    },
    onClickSearchMClass: function () {
        showPopGridHelp(400, 300, true, helpInitializer.materialClass, materials.helpReceiver.searchMClass, null);
    },
    onClickSearchMType: function () {
        showPopGridHelp(400, 300, true, helpInitializer.materialType, materials.helpReceiver.searchMType, null);
    },
    onClickCardMClass: function () {
        showPopGridHelp(400, 300, true, helpInitializer.materialClass, materials.helpReceiver.cardMClass, null);
    },
    onClickCardMType: function () {
        showPopGridHelp(400, 300, true, helpInitializer.materialType, materials.helpReceiver.cardMType, null);
    },
    onClickCardMUnit: function () {
        showPopGridHelp(400, 300, true, helpInitializer.measureUnit, materials.helpReceiver.cardMUnit, null);
    }
}