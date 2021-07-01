// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function dateConvert(value) {
    if (!value) {
        return value;
    }

    return new Date(value).format('yyyy-MM-dd hh:mm:ss')
}
/**
 * 提示框
 * @param {any} msg
 */
function showMsg(msg) {
    layer.msg(msg, {
        time: 1500,
    });
}
/**
 * 保存数据到本地
 * @param {any} name
 * @param {any} data
 */
function exportRaw(name, data) {
    var urlObject = window.URL || window.webkitURL || window;
    var export_blob = new Blob([data]);
    var save_link = document.createElementNS("http://www.w3.org/1999/xhtml", "a");
    save_link.href = urlObject.createObjectURL(export_blob);
    save_link.download = name;
    var ev = document.createEvent("MouseEvents");
    ev.initMouseEvent("click", true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
    save_link.dispatchEvent(ev);
}
//时间格式化字符串
Date.prototype.format = function (format) {
    /*
    * eg:format="yyyy-MM-dd hh:mm:ss";
    */
    if (!format) {
        format = "yyyy-MM-dd hh:mm:ss";
    }

    var o = {
        "M+": this.getMonth() + 1, // month
        "d+": this.getDate(), // day
        "h+": this.getHours(), // hour
        "m+": this.getMinutes(), // minute
        "s+": this.getSeconds(), // second
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
        "S": this.getMilliseconds()
        // millisecond
    };

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};

//扩展jquery
(function () {
    $.extend({
        /**
         * post请求下载文件
         * options:{
         *   url:'',  //下载地址
         *   data:{name:value}, //要发送的数据
         *   method:'post'
         * }
         */
        postDownLoadFile: function (options) {
            var config = $.extend(true, { method: 'post' }, options);
            var $iframe = $('<iframe id="down-file-iframe" />');
            var $form = $('<form target="down-file-iframe" method="' + config.method + '" />');
            $form.attr('action', config.url);
            for (var key in config.data) {
                $form.append('<input type="hidden" name="' + key + '" value="' + config.data[key] + '" />');
            }
            $iframe.append($form);
            $(document.body).append($iframe);
            $form[0].submit();
            $iframe.remove();
        },
        downloadFile: function (options) {
            var config = $.extend(true, { type: 'get' }, options);
            var $iframe = $('<iframe id="down-file-iframe" />');
            var $form = $('<form target="down-file-iframe" method="' + config.type + '" />');
            var url = options.url
            $form.attr('action', url);
            var params = $.param(options.data);
            if (params && params.length) {
                var arr = params.split('&');;
                for (var i = 0; i < arr.length; i++) {
                    var entry = arr[i];
                    entry = entry.split('=');
                    if (entry.length > 1) {
                        $form.append('<input type="hidden" name="' + entry[0] + '" value="' + entry[1] + '" />');
                    }
                }
            }
            $iframe.append($form);
            $(document.body).append($iframe);
            $form[0].submit();
            $iframe.remove();
        }
    })

    //异步请求展示loading
    function ajaxLoading() {
        if (!this.loading) {
            return;
        }
        var defaultOptions = {
            shade: 0.3, //遮罩
            icon: 2 //风格
        };
        if (typeof this.loading == "object") {
            $.extend(true, defaultOptions, options);
        }
        this.loading = defaultOptions;
        this.loading.index = layer.load(defaultOptions.icon, defaultOptions);
    }

    function closeAjaxLoading(index) {
        if (this.loading && this.loading.index) {
            layer.close(this.loading.index);
        }
    }

    var handlers = {
        "401": function (xhr) {            
            layer.alert('登录过期，请重新登录', {
                skin: 'layui-layer-molv', //样式类名  自定义样式
                closeBtn: 1,    // 是否显示关闭按钮
                anim: 1, //动画类型
                btn: ['登录'], //按钮
                icon: 6,    // icon
                yes: function () {
                    top.location.href = '/account/login';
                }
            });
        },
        "403": function (xhr) {
            layer.msg(xhr.responseText);
        },
        "418": function (xhr) {
            layer.msg(xhr.responseText);
        },
        "500": function (xhr) {
            layer.msg("系统出现错误，请稍后重试");
        }
    };

    /* ajax默认参数设置 */
    $.ajaxSetup({
        beforeSend: function (xhr) {
            ajaxLoading.call(this);
        },
        complete: function (xhr) {
            closeAjaxLoading.call(this);

            var handler = handlers[xhr.status];
            if (handler) handler(xhr);
        },
        error: function (xhr) {
            var log = {
                readyState: xhr.readyState,
                status: xhr.status,
                responseText: xhr.responseText
            };
            console.error(log);
        }
    });

    function buildParams(prefix, value, add) {
        var name;
        if (Array.isArray(value)) {
            $.each(value, function (i, v) {
                if (typeof value === "object" && value != null) {
                    name = prefix + "[" + i + "]";
                } else {
                    name = prefix + "[]";
                }
                buildParams(name, v, add);
            });
        } else if (typeof (value) == "object") {
            for (name in value) {
                buildParams(prefix + "." + name , value[name], add);
            }
        } else {
            add(prefix, value);
        }
    }

    /*
    重写jquery的urlparams方法以适配asp.net core的参数绑定
    */
    $.param = function (a) {
        var prefix,
            args = [];

        function add(key, value) {
            args[args.length] = encodeURIComponent(key) + "=" +
                encodeURIComponent(value == null ? "" : value);
        }
        if (Array.isArray(a)) {
            for (prefix in a) {
                buildParams("[" + prefix + "]", a[prefix], add);
            }
        } else {
            for (prefix in a) {
                buildParams(prefix, a[prefix], add);
            }
        }

        return args.join("&");
    }  

    //扩展序列化
    $.fn.serializeObject = function () {
        var o = {};
        var arr = this.serializeArray();
        $.each(arr, function () {
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
    }

    
    var tableRowChangeEvents = {
        inputChange: function (options) {
            var _this = $(this);
            var $check = _this.closest("tr").find("input[name='" + options.checkName + "']");
            $check[0].checked = _this.val() && true;
            layui.form.render('checkbox');
        },
        checkChange: function (options,isChecked) {
            var check = $(this).prev();
            var input = check.closest("tr").find("input[name='" + options.inputName + "']");
            var value = isChecked ? (input.val() || input.attr('max')) : "";
            input.val(value);
        }
    }

    /*
     * 表格多选框或者输入框改变事件
     * options:{
     *  checkName,//全选框的名称
     *  inputName//输入框的名称
     * }
     */
    $.fn.syncAfterRowChange = function (options) {
        var _options = $.extend({}, options),
            _this = $(this),
            inputSelector = "tbody input[name='" + _options.inputName + "']",
            checkSelector = "tbody input[name='" + _options.checkName + "']+",
            checkAllSelector = "thead input[name='" + _options.checkName + "']+";

        _this.on("click", checkSelector, function () {
            var check = $(this).prev();
            tableRowChangeEvents.checkChange.call(this, _options, check.is(":checked"));

        }).on("change", inputSelector, function () {

            tableRowChangeEvents.inputChange.call(this, _options);

        }).on("click", checkAllSelector, function () {
            var check = $(this).prev();
            var $checks = _this.find(checkSelector);
            $checks.each(function (i, e) {
                var tr = $(this).closest("tr");
                if (tr.is(":visible")) {
                    tableRowChangeEvents.checkChange.call(this, _options, check.is(":checked"));
                }
            });

        });
    }
})();

 
layui.use(["form"], function () {
    var $ = layui.$,
        form = layui.form,
        device = layui.device();
    var submit = function () {
        var stop = null //验证不通过状态
            , verify = form.config.verify //验证规则
            , DANGER = 'layui-form-danger' //警示样式
            , elem = $(this) //当前所在表单域
            , verifyElem = elem.find('*[lay-verify]'); //获取需要校验的元素


        //开始校验
        layui.each(verifyElem, function (_, item) {
            var othis = $(this)
                , vers = othis.attr('lay-verify').split('|')
                , verType = othis.attr('lay-verType') //提示方式
                , value = othis.val();

            othis.removeClass(DANGER); //移除警示样式

            //遍历元素绑定的验证规则
            layui.each(vers, function (_, thisVer) {
                var isTrue //是否命中校验
                    , errorText = '' //错误提示文本
                    , isFn = typeof verify[thisVer] === 'function';

                //匹配验证规则
                if (verify[thisVer]) {
                    var isTrue = isFn ? errorText = verify[thisVer](value, item) : !verify[thisVer][0].test(value);
                    errorText = errorText || verify[thisVer][1];

                    if (thisVer === 'required') {
                        errorText = othis.attr('lay-reqText') || errorText;
                    }

                    //如果是必填项或者非空命中校验，则阻止提交，弹出提示
                    if (isTrue) {
                        //提示层风格
                        if (verType === 'tips') {
                            layer.tips(errorText, function () {
                                if (typeof othis.attr('lay-ignore') !== 'string') {
                                    if (item.tagName.toLowerCase() === 'select' || /^checkbox|radio$/.test(item.type)) {
                                        return othis.next();
                                    }
                                }
                                return othis;
                            }(), { tips: 1 });
                        } else if (verType === 'alert') {
                            layer.alert(errorText, { title: '提示', shadeClose: true });
                        } else {
                            layer.msg(errorText, { icon: 5, shift: 6 });
                        }

                        //非移动设备自动定位焦点
                        if (!device.android && !device.ios) {
                            setTimeout(function () {
                                item.focus();
                            }, 7);
                        }

                        othis.addClass(DANGER);
                        return stop = true;
                    }
                }
            });
            if (stop) return stop;
        });
        if (stop) return false;
    };

    $(document).on("submit", ".layui-form", submit);

    $.fn.validForm = function () {
        if (submit.call(this) === false) {
            return false;
        }
        return true;
    };

    //自定义验证规则
    form.verify({
        number: function (value, item) {
            if (value && isNaN(value)) return '请输入数字';
        },
        extrequired: function (value, item) {
            var $ = layui.$;
            var verifyName = $(item).attr('name')
                , verifyType = $(item).attr('type')
                , formElem = $(item).parents('.layui-form')//获取当前所在的form元素，如果存在的话
                , verifyElem = formElem.find('input[name=' + verifyName + ']')//获取需要校验的元素
                , isTrue = verifyElem.is(':checked')//是否命中校验
                , focusElem = verifyElem.next().find('i.layui-icon');//焦点元素
            if (!isTrue || !value) {
                //定位焦点
                focusElem.css(verifyType == 'radio' ? { "color": "#FF5722" } : { "border-color": "#FF5722" });
                //对非输入框设置焦点
                focusElem.first().attr("tabIndex", "1").css("outline", "0").blur(function () {
                    focusElem.css(verifyType == 'radio' ? { "color": "" } : { "border-color": "" });
                }).focus();
                return '必填项不能为空';
            }
        },
        positiveinteger: function (value, item) { //非空的正整数           
            var reg = /^[1-9]\d*$/;
            if (value.length > 0 && reg.test(value) === false)
                return '只能输入正整数';
        },
        positivedecimal: function (value, item) { //非负数           
            var reg = /^(?!(0[0-9]{0,}$))[0-9]{1,}[.]{0,}[0-9]{0,}$/;
            if (value.length > 0 && reg.test(value) === false)
                return '输入大于零的数';
        },
        nonnegativeinteger: function (value, item) {//非负整数
            var reg = /^((\d+)|(0+))$/; // /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/;
            if (value.length > 0 && reg.test(value) === false)
                return '输入大于等于零的整数';
        },
        nonnegativenumber: function (value, item) {
            if (value.length == 0) {
                return;
            }
            if (/^[0-9]\d*$/.test(value) || /^[0-9]\d*\.\d*$|^0\.\d*[0-9]\d*$/.test(value)) {
                return;
            }
            return '请输入一个非负数';
        },
        numberRange: function (value, item) {
            if (!value) return;            
            var num = parseFloat(value);
            var max = parseFloat(item.max);
            var min = parseFloat(item.min);
            if (num < min || num > max) {
                return '请输入一个介于' + min + "与" + max + '之间的数字';
            } 
        },
        intRange: function (value, item) {
            if (!value) return;
            var num = parseInt(value);
            var max = parseInt(item.max);
            var min = parseInt(item.min);
            if (num < min || num > max) {
                return '请输入一个介于' + min + "与" + max + '之间的数字';
            }
        },
        min: function (value, item) {
            var num = parseFloat(value);
            var min = parseFloat(item.min);
            if (num < min) {
                return '请输入一个大于' + min + "的数字";
            } 
        }
    });
});

//layui配置
(function () {
    $.extend(layui.table.config, {
        defaultToolbar: [],
        limits: [20, 30, 40, 50, 60, 70, 80, 90],
        limit: 20,
        height: "full-160"
    });
})();

/*数据表格*/
//全选按钮
$(document).on("click", "thead.layui-table-header th.primary-check input[type='checkbox']+", function () {
    var $this = $(this);
    var checkbox = $this.prev();
    var table = $this.closest("table");
    var $childs = table.find("tbody tr").filter(":visible").find("td.primary-check input:checkbox");
    if (checkbox.is(":checked")) {
        $childs.each(function () {
            this.checked = true;
        });
    } else {
        $childs.each(function () {
            this.checked = false;
        });
    }

    //触发元素的click结束事件,必须要在渲染之前调用
    $this.trigger("clickend");

    layui.form.render("checkbox");
}).on("click", "tbody.layui-table-body td.primary-check input[type='checkbox']+", function () {
    var $this = $(this);
    var checkbox = $this.prev();
    var table = $this.closest("table");
    var $checkAll = table.find("thead th.primary-check input[type='checkbox']");
    if (!checkbox.is(":checked")) {
        $checkAll[0].checked = false; 
    }
    //触发元素的click结束事件,必须要在渲染之前调用
    $this.trigger("clickend");

    layui.form.render("checkbox");
});

//表格点击行选中行事件
$(document).on("click", ".layui-table-body>table>tbody>tr>td>div:not(.laytable-cell-radio,.laytable-cell-checkbox)", function () {
    var $e = $(this).closest("tr").find("input:radio,input:checkbox");
    if ($e.length) {
        $e.next()[0].click();
    }
});

window.atjubo = (function () {
    var _this = this;

    _this.laytableValidator = function (options) {
        var _options = $.extend(true, {}, options);

        var laytableValidatorAsserts = {
            "isSelectRow": function () {
                var id = _options.id;
                var rows = layui.table.checkStatus(id);
                if (rows && rows.data.length) {
                    return true;
                }
                layer.msg("请选择需要操作的记录");
                return false;
            }
        };

        $.extend(this, laytableValidatorAsserts);
        return this;
    }

    return this;
}());

//普通表格（冻结表头，全选功能）
$.fn.normalTable = function (options) {
    var _options = $.extend({
        onCheck: function () {//选中一行

        },
        onUnCheck: function () {//取消一行

        },
        onCheckAll: function () {//全选

        },
        onUnCheckAll: function () {//取消全选

        }
    }, options);

    var table = $(this);
    var header = table.find(".atjubo-table-header");
    var body = table.find(".atjubo-table-body");

    table.options = _options;

    header.on("click", "th.primary-check input[type='checkbox']+", function () {
        var $this = $(this);
        var checkbox = $this.prev();
        var checked = checkbox.is(":checked");

        var rowCheckboxs = body.find("tr").filter(":visible").find("td.primary-check input:checkbox");
        rowCheckboxs.each(function () {
            this.checked = checked;
        });
        layui.form.render("checkbox");

        if (checked) {
            checkbox.trigger("checkAll");
        } else {
            checkbox.trigger("unCheckAll");
        }
    });

    header.find("th.primary-check input[type='checkbox']").on("checkAll", function () {
        _options.onCheckAll.call(this, table);
    }).on("unCheckAll", function () {
        _options.onUnCheckAll.call(this, table);
    });

    body.on("click", "td.primary-check input[type='checkbox']+", function () {
        var $this = $(this);
        var checkbox = $this.prev();
        var checked = checkbox.is(":checked");

        var allCheckbox = header.find("th.primary-check input[type='checkbox']");
        if (!checked && allCheckbox.length) {
            allCheckbox[0].checked = false;
        }
        layui.form.render("checkbox");

        if (checked) {
            checkbox.trigger("check");
        } else {
            checkbox.trigger("unCheck");
        }
    });

    body.on("check", "td.primary-check input[type='checkbox']", function () {
        _options.onCheck.call(this, table);
    }).on("unCheck", "td.primary-check input[type='checkbox']", function () {
        _options.onUnCheck.call(this, table);
    });
}