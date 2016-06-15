/// <reference path="../help/help_grid.html" />
/// <reference path="../help/help_grid.html" />
/*————————————————————————公共扩展:begin——————————————————————*/
//全局函数:序列化表单内容为Json对象
$.fn.serializeToJson = function (boolNoSelEmpty) {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (boolNoSelEmpty == true) {
            if (this.value == '') {
                return;
            }
        }
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

//日期格式化扩展
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份
        "d+": this.getDate(),                    //日
        "h+": this.getHours(),                   //小时
        "m+": this.getMinutes(),                 //分
        "s+": this.getSeconds(),                 //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds()             //毫秒
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};

//格式化处理器
var formatHandler = {
    //性别
    gender: {
        src: [{ value: '1', text: '男' }, { value: '0', text: '女' }],
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
        },
        parse: function (text) {
            if (gFunc.isNull(text)) {
                return "";
            }
            var rst = "";
            for (var idx = 0; idx < this.src.length; idx++) {
                if (text == this.src[idx].text) {
                    rst = this.src[idx].value;
                    break;
                }
            }
            return rst;
        }
    },
    date: {
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            var date = gFunc.getDate(value);
            return date.Format('yyyy-MM-dd');
        },
        parse: function (text) {
            return gFunc.getDate(text);
        }
    },
    datetime: {
        format: function (value) {
            if (gFunc.isNull(value)) {
                return "";
            }
            var date = gFunc.getDate(value);
            return date.Format('yyyy-MM-dd hh:mm:ss');
        },
        parse: function (text) { return gFunc.getDate(text); }
    }
};

/*公共方法*/
var gFunc = {
    isNull: function (value) {
        return typeof (value) == 'undefined' || value == null;
    },
    /*
    解析日期：
        ①value为Date，直接返回；
        ②value为字符串，两种格式
            yyyy-MM-dd
            yyyy-MM-dd hh:mm:ss
    */
    getDate: function (value) {
        //console.log("getDate input " + value);
        if (gFunc.isNull(value)) {
            return new Date();
        }
        if (value instanceof Date) {
            return value;
        }
        var tempStrs = value.split(" ");
        var dateStrs = tempStrs[0].split("-");
        var year = parseInt(dateStrs[0], 10);
        var month = parseInt(dateStrs[1], 10) - 1;
        var day = parseInt(dateStrs[2], 10);
        var hour = 0, minute = 0, second = 0;
        if (tempStrs[1]) {
            timeStrs = tempStrs[1].split(":");
            hour = parseInt(timeStrs[0], 10);
            minute = parseInt(timeStrs[1], 10);
            second = parseInt(timeStrs[2], 10);
        }
        var date = new Date(year, month, day, hour, minute, second);
        return date;
    },
    getAge: function (value) {
        var birthDay = gFunc.getDate(value);
        if (!birthDay) {
            return null;
        }
        var dateNow = new Date();
        age = dateNow.getYear() - birthDay.getYear() + 1;
        return age;
    },
    getRootPath: function () {
        //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
        var curWwwPath = window.document.location.href;
        //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
        var pathName = window.document.location.pathname;
        var pos = curWwwPath.indexOf(pathName);
        //获取主机地址，如： http://localhost:8083
        var localhostPaht = curWwwPath.substring(0, pos);
        //获取带"/"的项目名，如：/uimcardprj
        var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
        return (localhostPaht + projectName);
    },
    /*
    初始化列表公共处理函数
    options对象成员说明：
        title:帮助标题
        icon:标题对应图标
        key:列表主键字段
        url:获取列表数据的url
        toolbar:列表工具栏配置
        columns:列表列配置,
        hidecols:列表中要隐藏的列数组，如各种id字段
    */
    initGridPublic: function (grid, options) {
        $(grid).datagrid({
            title: options.title,
            iconCls: options.icon,
            singleSelect: false,
            idField: options.key,//列表主键，必须
            url: options.url,
            remoteSort: false,
            rownumbers: true,
            pagination: true,
            pageSize: 10,
            fit: false,
            selectOnCheck: true,
            toolbar: options.toolbar,
            columns: options.columns,
            onBeforeLoad: function () {
                //加载前清除选中
                $(grid).datagrid('clearSelections').datagrid('clearChecked');
            },
            onLoadSuccess: function (data) {
                //$(grid).datagrid('clearSelections').datagrid('clearChecked');
            },
            onEndEdit: function (index, row, changes) {
                row.editing = false;
            },
            onBeforeEdit: function (index, row) {
                row.editing = true;
                $(this).datagrid('refreshRow', index);
                //if (!gFunc.isNull(row[options.key])) {
                //    //主键不为空，说明是已保存的数据，则唯一列禁止修改
                //    var cellEdit = $(grid).datagrid('getEditor', { index: index, field: 'MaterialClassCode' });
                //    cellEdit.target.prop('readonly', true);
                //}
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
        for (var i = 0; i < options.hidecols.length; i++) {
            $(grid).datagrid('hideColumn', options.hidecols[i]);//隐藏列
        }
    },
    /*
    弹出窗口
    options对象成员说明：
        width,：宽
        height：高
        title：标题
        url：窗口加载的页面
        isModal：是否模态
        funLoadCallback：弹出窗口加载后执行的操作（如给各个控件赋值等初始化页面内容操作）
        funSubmitCallback：点击确定按钮后的操作,它需要①返回true或false用来指示是否继续后续操作②如果结果依赖http请求，则http只能是同步请求
        target：触发弹出窗体的控件
    */
    showPopWindow: function (options) {
        var id = "_tmpWin_" + Math.floor(Math.random() * 10000 + 1);
        var win = $("<div id='" + id + "'></div>");

        win.addClass("myOpenWindow");
        win.appendTo($("body"));
        $(win).dialog({
            title: options.title,
            href: options.url,
            width: options.width,
            height: options.height,
            modal: options.isModal == true ? true : false,
            iconCls: null,
            buttons: [{
                text: '确定',
                iconCls: 'icon-ok',
                width: 75,
                handler: function () {
                    var selData = null;
                    //调用回调函数
                    var rst = options.funSubmitCallback();
                    console.log('base,funSubmitCallback result:' + rst);
                    if (rst) {
                        $(win).dialog("close");
                    }
                    console.log('base,btnok over');
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
                options.funLoadCallback();
                $("#" + id).next().children("a").first().focus();
                $(win).keydown(function (event) {
                    if (event.keyCode == 13) {
                        var b = options.funSubmitCallback();
                        if (b != false) {
                            $(win).dialog("close");
                        }
                    }
                });
            }
        });
        return id;
    },
    /*
    表单函数
    */
    formFunc: {
        // 验证控件
        _validateControls: [
            "easyui-validatebox",
            "easyui-textbox",
            "easyui-combobox",
            "easyui-combotree",
            "easyui-combogrid",
            "easyui-numberbox",
            "easyui-datebox",
            "easyui-datetimebox",
            "easyui-datetimespinner",
            "easyui-numberspinner",
            "easyui-timespinner",
            "easyui-filebox"],

        // 检查对象是否为Form
        _getForm: function (formId) {
            if (formId == null || typeof (formId) == "undefined") {
                return null;
            } else {
                var form = $("#" + formId);
                if (form == null || typeof (form) == "undefined" || form.length <= 0
                    || form[0].tagName.toUpperCase() != "FORM") {
                    return null;
                }
                return form;
            }
        },

        // 将Form序列化为JSON对象
        serializeToJson: function (formId) {
            var formObj = gFunc.formFunc._getForm(formId);
            if (formObj == null) {
                return null;
            }

            var o = {};
            var a = formObj.serializeArray();
            $.each(a, function () {
                if (o[this.name]) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        },

        // 清除表单中的所有验证
        clearValidations: function (formId) {
            var formObj = gFunc.formFunc._getForm(formId);
            if (formObj == null) {
                return false;
            }
            var ctrls = gFunc.formFunc._getFormValidateControls(formId);
            var cls = null;
            $(ctrls).each(function (index, item) {
                for (var j in gFunc.formFunc._validateControls) {
                    cls = gFunc.formFunc._validateControls[j];
                    if (item.hasClass(cls)) {
                        switch (cls) {
                            case "easyui-validatebox":
                                item.validatebox("disableValidation");
                                break;
                            case "easyui-textbox":
                                item.textbox("disableValidation");
                                break;
                            case "easyui-combobox":
                                item.combobox("disableValidation");
                                break;
                            case "easyui-combotree":
                                item.combotree("disableValidation");
                                break;
                            case "easyui-combogrid":
                                item.combogrid("disableValidation");
                                break;
                            case "easyui-numberbox":
                                item.numberbox("disableValidation");
                                break;
                            case "easyui-datebox":
                                item.datebox("disableValidation");
                                break;
                            case "easyui-datetimebox":
                                item.datetimebox("disableValidation");
                                break;
                            case "easyui-datetimespinner":
                                item.datetimespinner("disableValidation");
                                break;
                            case "easyui-numberspinner":
                                item.numberspinner("disableValidation");
                                break;
                            case "easyui-timespinner":
                                item.timespinner("disableValidation");
                                break;
                            case "easyui-filebox":
                                item.filebox("disableValidation");
                                break;
                            default:
                        }
                        break;
                    }
                }
            });
            return true;
        },

        // 重建表单中的所有验证
        reduceValidations: function (formId) {
            var formObj = gFunc.formFunc._getForm(formId);
            if (formObj == null) {
                return false;
            }
            var ctrls = gFunc.formFunc._getFormValidateControls(formId);
            var cls = null;
            $(ctrls).each(function (index, item) {
                for (var j in gFunc.formFunc._validateControls) {
                    cls = gFunc.formFunc._validateControls[j];
                    if (item.hasClass(cls)) {
                        switch (cls) {
                            case "easyui-validatebox":
                                item.validatebox("enableValidation");
                                break;
                            case "easyui-textbox":
                                item.textbox("enableValidation");
                                break;
                            case "easyui-combobox":
                                item.combobox("enableValidation");
                                break;
                            case "easyui-combotree":
                                item.combotree("enableValidation");
                                break;
                            case "easyui-combogrid":
                                item.combogrid("enableValidation");
                                break;
                            case "easyui-numberbox":
                                item.numberbox("enableValidation");
                                break;
                            case "easyui-datebox":
                                item.datebox("enableValidation");
                                break;
                            case "easyui-datetimebox":
                                item.datetimebox("enableValidation");
                                break;
                            case "easyui-datetimespinner":
                                item.datetimespinner("enableValidation");
                                break;
                            case "easyui-numberspinner":
                                item.numberspinner("enableValidation");
                                break;
                            case "easyui-timespinner":
                                item.timespinner("enableValidation");
                                break;
                            case "easyui-filebox":
                                item.filebox("enableValidation");
                                break;
                            default:
                        }
                        break;
                    }
                }
            });
            return true;
        },

        // 获得form中的所有验证控件
        _getFormValidateControls: function (formId) {
            var ctrls = [];
            var formObj = gFunc.formFunc._getForm(formId);
            if (formObj == null) {
                return null;
            }
            var idPre = "#" + formId + " .";
            $(gFunc.formFunc._validateControls).each(function (index, item) {
                var tmpArr = $(idPre + item);
                if (tmpArr.length > 0) ctrls = ctrls.concat(tmpArr);
            });
            return ctrls;
        },

        // 验证表单
        validate: function (formId) {
            var formObj = gFunc.formFunc._getForm(formId);
            if (formObj == null) {
                return false;
            }
            gFunc.formFunc.reduceValidations(formId);
            var rst = formObj.form("validate");
            return rst;
        },

        // 清除表单数据
        clearData: function (formId) {
            var formObj = gFunc.formFunc._getForm(formId);
            if (formObj == null) {
                return false;
            }
            formObj.form("clear");
            gFunc.formFunc.clearValidations(formId);
            return true;
        }
    }
};
/*————————————————————————公共扩展:end——————————————————————*/

/*————————————————————————easyui扩展:begin——————————————————————*/
//easyui:输入验证扩展
$.extend($.fn.validatebox.defaults.rules, {
    chs: {
        validator: function (value, param) {
            return /^[\u0391-\uFFE5]+$/.test(value);
        },
        message: '请输入汉字'
    },
    english: {// 验证英语
        validator: function (value) {
            return /^[A-Za-z]+$/i.test(value);
        },
        message: '请输入英文'
    },
    ip: {// 验证IP地址
        validator: function (value) {
            return /\d+\.\d+\.\d+\.\d+/.test(value);
        },
        message: 'IP地址格式不正确'
    },
    zip: {
        validator: function (value, param) {
            return /^[0-9]\d{5}$/.test(value);
        },
        message: '邮政编码不存在'
    },
    qq: {
        validator: function (value, param) {
            return /^[1-9]\d{4,10}$/.test(value);
        },
        message: 'QQ号码不正确'
    },
    mobile: {
        validator: function (value, param) {
            return /^(?:13\d|15\d|18\d)-?\d{5}(\d{3}|\*{3})$/.test(value);
        },
        message: '手机号码不正确'
    },
    tel: {
        validator: function (value, param) {
            return /^(\d{3}-|\d{4}-)?(\d{8}|\d{7})?(-\d{1,6})?$/.test(value);
        },
        message: '电话号码不正确'
    },
    mobileAndTel: {
        validator: function (value, param) {
            return /(^([0\+]\d{2,3})\d{3,4}\-\d{3,8}$)|(^([0\+]\d{2,3})\d{3,4}\d{3,8}$)|(^([0\+]\d{2,3}){0,1}13\d{9}$)|(^\d{3,4}\d{3,8}$)|(^\d{3,4}\-\d{3,8}$)/.test(value);
        },
        message: '请正确输入电话号码'
    },
    number: {
        validator: function (value, param) {
            return /^[0-9]+.?[0-9]*$/.test(value);
        },
        message: '请输入数字'
    },
    money: {
        validator: function (value, param) {
            return (/^(([1-9]\d*)|\d)(\.\d{1,2})?$/).test(value);
        },
        message: '请输入正确的金额'

    },
    mone: {
        validator: function (value, param) {
            return (/^(([1-9]\d*)|\d)(\.\d{1,2})?$/).test(value);
        },
        message: '请输入整数或小数'

    },
    integer: {
        validator: function (value, param) {
            return /^[+]?[1-9]\d*$/.test(value);
        },
        message: '请输入最小为1的整数'
    },
    integ: {
        validator: function (value, param) {
            return /^[+]?[0-9]\d*$/.test(value);
        },
        message: '请输入整数'
    },
    range: {
        validator: function (value, param) {
            if (/^[1-9]\d*$/.test(value)) {
                return value >= param[0] && value <= param[1]
            } else {
                return false;
            }
        },
        message: '输入的数字在{0}到{1}之间'
    },
    minLength: {
        validator: function (value, param) {
            return value.length >= param[0]
        },
        message: '至少输入{0}个字'
    },
    maxLength: {
        validator: function (value, param) {
            return value.length <= param[0]
        },
        message: '最多{0}个字'
    },
    //select即选择框的验证
    selectValid: {
        validator: function (value, param) {
            if (value == param[0]) {
                return false;
            } else {
                return true;
            }
        },
        message: '请选择'
    },
    idCode: {
        validator: function (value, param) {
            return /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/.test(value);
        },
        message: '请输入正确的身份证号'
    },
    loginName: {
        validator: function (value, param) {
            return /^[\u0391-\uFFE5\w]+$/.test(value);
        },
        message: '登录名称只允许汉字、英文字母、数字及下划线。'
    },
    equalTo: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '两次输入的字符不一至'
    },
    englishOrNum: {// 只能输入英文和数字
        validator: function (value) {
            return /^[a-zA-Z0-9_ ]{1,}$/.test(value);
        },
        message: '请输入英文、数字、下划线或者空格'
    },
    xiaoshu: {
        validator: function (value) {
            return /^(([1-9]+)|([0-9]+\.[0-9]{1,2}))$/.test(value);
        },
        message: '最多保留两位小数！'
    },
    ddPrice: {
        validator: function (value, param) {
            if (/^[1-9]\d*$/.test(value)) {
                return value >= param[0] && value <= param[1];
            } else {
                return false;
            }
        },
        message: '请输入1到100之间正整数'
    },
    jretailUpperLimit: {
        validator: function (value, param) {
            if (/^[0-9]+([.]{1}[0-9]{1,2})?$/.test(value)) {
                return parseFloat(value) > parseFloat(param[0]) && parseFloat(value) <= parseFloat(param[1]);
            } else {
                return false;
            }
        },
        message: '请输入0到100之间的最多俩位小数的数字'
    },
    rateCheck: {
        validator: function (value, param) {
            if (/^[0-9]+([.]{1}[0-9]{1,2})?$/.test(value)) {
                return parseFloat(value) > parseFloat(param[0]) && parseFloat(value) <= parseFloat(param[1]);
            } else {
                return false;
            }
        },
        message: '请输入0到1000之间的最多俩位小数的数字'
    }
});
//easyui:datagrid列编辑器扩展
$.extend($.fn.datagrid.defaults.editors, {
    //带帮助的输入框
    helpEdit: {
        init: function (container, options) {
            var editor = $('<div><input type="text" style="width:70px;" readonly="true"/><input type="button" style="width:20px;display: inline-block;vertical-align: top; padding: 1px; border: 1px solid #ccc;border-radius:0;font-size: 14px;font-weight: 300;text-decoration: none;background-color:lightblue;cursor:pointer;" value="..."/></div>');
            //注册按钮点击事件
            if (options && options.onclick) {
                editor.click(function () { options.onclick(editor[0]); });
            }
            editor.appendTo(container);
            return editor;
        },
        getValue: function (target) {
            //return $(target).val();
            return $($(target).children('input').get(0)).val();
        },
        setValue: function (target, value) {
            //$(target).val(value);
            $($(target).children('input').get(0)).val(value);
        },
        resize: function (target, width) {
            var input = $(target);
            input.width(width - (input.outerWidth() - input.width()));
            //intput.height(40);//解决IE下高度不正常问题
        }
    }
});

//easyui:datagrid方法扩展
$.extend($.fn.datagrid.methods, {
    /*
    根据编辑器获取所在行，返回jQuery对象（不是所在行数据）
    options:{element:ele}
    */
    getRowByEditor: function (grid, options) {
        var tr = $(options.element).closest('tr.datagrid-row');
        return tr;
    },
    /*
    根据编辑器控件获取行索引
    options:{element:ele}
    */
    getRowIndexByEditor: function (grid, options) {
        var tr = $(grid).datagrid('getRowByEditor', options);
        return parseInt(tr.attr('datagrid-row-index'));
    },
    /*
    更新行单元格
    options:{field:'Id', index:1, value:value}
    */
    updateRowCell: function (grid, options) {
        var row = $(grid).datagrid('getRows')[options.index];
        row[options.field] = options.value;
        //更新界面显示文本
        var view = $('.datagrid-view');
        for (var i = 0; i < view.length; i++) {
            if ($(view[i]).children($(grid).selector).length > 0) {
                var view = $(view[i]).children('.datagrid-view2');
                var td = $(view).find('.datagrid-body td[field="' + options.field + '"]')[options.index]
                var div = $(td).find('div')[0];
                $(div).text(options.value);
            }
        }
    },
    /**
    * 开打提示功能（基于1.3.3+版本）
    * @param {} jq
    * @param {} params 提示消息框的样式
    * @return {}
    */
    doCellTip: function (jq, params) {
        function showTip(showParams, td, e, dg) {
            //无文本，不提示。
            if ($(td).text() == "") return;
            params = params || {};
            var options = dg.data('datagrid');
            var styler = 'style="';
            if (showParams.width) {
                styler = styler + "width:" + showParams.width + ";";
            }
            if (showParams.maxWidth) {
                styler = styler + "max-width:" + showParams.maxWidth + ";";
            }
            if (showParams.minWidth) {
                styler = styler + "min-width:" + showParams.minWidth + ";";
            }
            styler = styler + ';word-wrap:break-word;"';
            showParams.content = '<div class="tipcontent"' + styler + '>' + showParams.content + '</div>';
            $(td).tooltip({
                content: showParams.content,
                trackMouse: true,
                position: params.position,
                onHide: function () {
                    $(this).tooltip('destroy');
                },
                onShow: function () {
                    var tip = $(this).tooltip('tip');
                    if (showParams.tipStyler) {
                        tip.css(showParams.tipStyler);
                    }
                    if (showParams.contentStyler) {
                        tip.find('div.tipcontent').css(showParams.contentStyler);
                    }
                }
            }).tooltip('show');

        };
        return jq.each(function () {
            var grid = $(this);
            var options = $(this).data('datagrid');
            if (!options.tooltip) {
                var panel = grid.datagrid('getPanel').panel('panel');
                panel.find('.datagrid-body').each(function () {
                    var delegateEle = $(this).find('> div.datagrid-body-inner').length ? $(this).find('> div.datagrid-body-inner')[0] : this;
                    $(delegateEle).undelegate('td', 'mouseover').undelegate('td', 'mouseout').undelegate('td', 'mousemove').delegate('td[field]', {
                        'mouseover': function (e) {
                            //if($(this).attr('field')===undefined) return;
                            var that = this;
                            var setField = null;
                            if (params.specialShowFields && params.specialShowFields.sort) {
                                for (var i = 0; i < params.specialShowFields.length; i++) {
                                    if (params.specialShowFields[i].field == $(this).attr('field')) {
                                        setField = params.specialShowFields[i];
                                    }
                                }
                            }
                            if (setField == null) {
                                options.factContent = $(this).find('>div').clone().css({
                                    'margin-left': '-5000px',
                                    'width': 'auto',
                                    'display': 'inline',
                                    'position': 'absolute'
                                }).appendTo('body');
                                var factContentWidth = options.factContent.width();
                                params.content = $(this).text();
                                if (params.onlyShowInterrupt) {
                                    if (factContentWidth > $(this).width()) {
                                        //debugger;
                                        showTip(params, this, e, grid);
                                    }
                                } else {
                                    showTip(params, this, e, grid);
                                }
                            } else {
                                panel.find('.datagrid-body').each(function () {
                                    var trs = $(this).find('tr[datagrid-row-index="' + $(that).parent().attr('datagrid-row-index') + '"]');
                                    trs.each(function () {
                                        var td = $(this).find('> td[field="' + setField.showField + '"]');
                                        if (td.length) {
                                            params.content = td.text();
                                        }
                                    });
                                });
                                showTip(params, this, e, grid);
                            }
                        },
                        'mouseout': function (e) {
                            if (options.factContent) {
                                options.factContent.remove();
                                options.factContent = null;
                            }
                        }
                    });
                });
            }
        });
    },
    /**
     * 关闭消息提示功能（基于1.3.3版本）
     * @param {} jq
     * @return {}
     */
    cancelCellTip: function (jq) {
        return jq.each(function () {
            var data = $(this).data('datagrid');
            if (data.factContent) {
                data.factContent.remove();
                data.factContent = null;
            }
            var panel = $(this).datagrid('getPanel').panel('panel');
            panel.find('.datagrid-body').undelegate('td', 'mouseover').undelegate('td', 'mouseout').undelegate('td', 'mousemove')
        });
    }
});

/*————————————————————————easyui扩展:end——————————————————————*/
