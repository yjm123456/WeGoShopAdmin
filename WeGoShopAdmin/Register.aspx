<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WeGoShopAdmin.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>商户注册审核</title>
    <link href="assets/css/Register/normalize.css" rel="stylesheet" />
    <link href="assets/css/Register/jquery.idealforms.min.css" rel="stylesheet" />
    <link href="assets/css/Register/jquery-ui.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font: normal 15px/1.5 Arial, Helvetica, Free Sans, sans-serif;
            color: #222;
            background: url(images/pattern.png);
            overflow-y: scroll;
            padding: 60px 0 0 0;
        }

        #my-form {
            width: 755px;
            margin: 0 auto;
            border: 1px solid #ccc;
            padding: 3em;
            border-radius: 3px;
            box-shadow: 0 0 2px rgba(0,0,0,.2);
        }

        #comments {
            width: 350px;
            height: 100px;
        }

        #submitInput {
            font-family: sans-serif;
            height: 33px;
            line-height: 33px;
            padding: 0 .8em;
            padding: 0 1.2em;
            margin-right: 1em;
            margin-bottom: 1em;
        }
    </style>

</head>
<body>


    <div class="row">
        <div class="eightcol last">

            <!-- Begin Form -->

            <form id="my-form">

                <section name="基本信息">

                    <div>
                        <label>公司名称:</label>
                        <input id="CompanyName" name="CompanyName" data-ideal="required" type="text" />

                    </div>
                    <div>
                        <label>组织机构:</label>
                        <select id="InstitutionalType" name="InstitutionalType">
                            <option value="default">&ndash; 选择机构 &ndash;</option>
                            <option value="1">企业法人</option>
                            <option value="2">个体工商户</option>
                            <option value="3">企业媒体</option>
                            <option value="4">事业单位媒体</option>
                            <option value="5">政府</option>
                            <option value="6">事业单位</option>
                            <option value="7">非营利组织(慈善基金会、大使馆、国外政府机构)</option>
                            <option value="8">民办非企业单位</option>
                            <option value="9">社会团体</option>
                            <option value="10">其他组织</option>
                        </select>
                    </div>
                    <div>
                        <label>机构代码:</label><input id="OrganizingInstitution" data-ideal="required" name="OrganizingInstitution" type="text" />
                    </div>

                    <div>
                        <label>法人:</label><input id="LiasonManName" name="LiasonManName" data-ideal="required" type="text" />
                    </div>
                    <div>
                        <label>法人电话:</label><input id="Phone" name="Phone" data-ideal="required number" type="text" />
                    </div>
                    <div>
                        <label>法人身份证:</label><input id="IdentityCard" name="IdentityCard" data-ideal="required" type="text" />
                    </div>
                    <div>
                        <label>对公账户名称:</label><input id="AccountName" name="AccountName" type="text" />
                    </div>
                    <div>
                        <label>开户行:</label><input id="DepositBank" name="DepositBank" type="text" />
                    </div>
                    <div>
                        <label>银行卡号:</label><input id="BankAccount" name="BankAccount" type="text" />
                    </div>
                </section>



                <section name="联系方式">

                    <div>
                        <label>邮箱:</label>

                        <input id="Email" name="Email" data-ideal="required email" type="email" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <button id="SendMail" type="button">获取验证码</button>
                    </div>
                    <div>
                        <label>验证码:</label>
                        <input id="VerificationCode" data-ideal="required" name="VerificationCode" type="text" />
                    </div>
                    <div>
                        <label>固定电话:</label><input type="tel" name="FixPhone" id="FixPhone" data-ideal="phone" />
                    </div>
                    <div>
                        <label>推荐人:</label>
                        <select id="SaleManId" name="SaleManId">
                            <option value="">&ndash; 选择推荐人 &ndash;</option>
                            <option value="12389679481782">张三</option>
                            <option value="2384798237189">李四</option>
                        </select>
                    </div>
                    <div>
                        <label>备注:</label><textarea id="comments" name="comments"></textarea>
                    </div>

                </section>


                <section name="选择环境">
                    <div data-ideal="required">
                        <label>使用范围:</label>
                        <label>
                            <input type="checkbox" id="sc1" name="Scope" value="1" />电商</label>
                        <label>
                            <input type="checkbox" id="sc2" name="Scope" value="2" />线下店铺</label>
                        <label>
                            <input type="checkbox" id="sc3" name="Scope" value="3" />运营团队</label>
                    </div>
                    <div>
                        <label>选择版本:</label>
                        <label>
                            <input type="radio" name="Version" value="0" checked />试用</label>
                        <label>
                            <input type="radio" name="Version" value="1" />基础版</label>
                        <label>
                            <input type="radio" name="Version" value="2" />标准版</label>
                        <label>
                            <input type="radio" name="Version" value="3" />旗舰版</label>
                    </div>
                </section>
                <div>
                    <hr />
                </div>

                <div>
                    <%--<button id="submitFrom" type="submit">提交</button>--%>
                    <%-- <button id="reset" type="button">重置</button>--%>
                    <input id="submitInput" type="button" value="提交" onclick="Insert()" />
                    <button id="return" type="button">返回</button>
                </div>

            </form>

            <!-- End Form -->

        </div>

    </div>
    <script src="assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="assets/js/Register/jquery-ui.min.js"></script>
    <script src="assets/js/Register/jquery.idealforms.js"></script>
    <script src="assets/js/Register/jquery.query.js"></script>
    <script type="text/javascript">


        // 截取url传参的参数name和value
        function getParams(url) {
            try {
                var obj = {}, arr = url.split('&');
                for (var i = 0; i < arr.length; i++) {
                    var subArr = arr[i].split('=');
                    var key = decodeURIComponent(subArr[0]);
                    var value = decodeURIComponent(subArr[1]);
                    obj[key] = value;
                }
                return obj;

            } catch (err) {
                return null;
            }
        }

        var options = {
            focusFirstInvalid: function () {
                var $first = $myform.getInvalid().first().find('input:first, select, textarea')
                var tabName = $first.parents('.ideal-tabs-content').data('ideal-tabs-content-name')
                if (this.$tabs.length) {
                    this.switchTab(tabName)
                }
                $first.focus()
                return this
            },
            onFail: function () {
                alert("您还有" + $myform.getInvalid().length + ' 个验证框需要填写.');
                $myform.focusFirstInvalid();
            },

            inputs: {
                'password': {
                    filters: 'required pass',
                },
                'username': {
                    filters: 'required username',
                    data: {
                        //ajax: { url:'validate.php' }
                    }
                },
                'file': {
                    filters: 'extension',
                    data: { extension: ['jpg'] }
                },
                'comments': {
                    filters: 'min max',
                    data: { min: 10, max: 200 }
                },
                'InstitutionalType': {
                    filters: 'exclude',
                    data: { exclude: ['default'] },
                    errors: {
                        exclude: '请选择机构.'
                    }
                },
                'Scope': {
                    filters: 'min max',
                    data: { min: 1, max: 5 },
                    errors: {
                        min: '请选择 <strong>1</strong> 个模块.',
                        max: 'No more than <strong>3</strong> options allowed.'
                    }
                }
            }

        };

        var $myform = $('#my-form').idealforms(options).data('idealforms');

        //$('#reset').click(function () {
        //    $myform.reset().fresh().focusFirst()
        //});
        $('#return').click(function () {
            window.location.href = "javascript:window.location.href='Home.aspx'";
        });

        function Insert() {
            var checkLength = $myform.getInvalid().length;
            if (checkLength > 0) {
                options.onFail();
                return;
            }
            var VerificationCode = $(" input[ name='VerificationCode' ] ").val()
            if (VerificationCode == null && VerificationCode == undefined && VerificationCode == "") {
                alert("请输入验证码！");
                return;
            }
            var arr = $('#my-form').serialize();
            var param = JSON.stringify(getParams(arr));
            console.log(param);
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx?CheckParam=Insert",
                data: param,
                success: function (data) {
                    if (data.StatusCode == 1) {
                        if (confirm('审核已提交，初始密码为123456，您可登陆数据中心查看审核进度并修改密码！')) {
                            window.location.href = "ControlStation.aspx";
                        }
                        else {

                            window.location.href = "ControlStation.aspx";
                        }
                    }
                    else {

                        alert("操作失败！" + data.Error);
                    }
                }
            });
        }


        $('#SendMail').click(function () {
            var Email = document.getElementById("Email").value;
            if (Email == "" || Email == null || Email == undefined) {
                alert("请输入邮箱！");
                return;
            }
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                data: {
                    CheckParam: "SendMail",
                    Email: Email
                },
                success: function (data) {
                    if (data.StatusCode == 1) {
                        alert("发送成功！");
                    }
                    else {
                        alert("操作失败！" + data.Error);
                    }
                }
            });
        });
        $myform.focusFirst();

        getParamData();
        function getParamData() {
            var Version = $.query.get("Version");
            if (Version != null) {
                //获取选中值
                $("input[name='Version'][value=" + Version + "]").attr("checked", true);
            }
        }
    </script>
    <div style="text-align: center;">
        <p>版权所有©2021.浙江冰点网络科技有限公司保留所有权利。更多详情敬请期待</p>
    </div>
    <style>
        .copyrights {
            text-indent: -9999px;
            height: 0;
            line-height: 0;
            font-size: 0;
            overflow: hidden;
        }
    </style>
</body>
</html>
