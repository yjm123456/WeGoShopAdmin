<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlStation.aspx.cs" Inherits="WeGoShopAdmin.ControlStation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户管理系统</title>
    <link href="assets/css/ControlStation/style.css" rel="stylesheet" />
    <link href="assets/css/EmailInput.css" rel="stylesheet" />
</head>
<body>
    <div class="zxcf_nav_wper">
        <div class="zxcf_nav clearfix px1000">
            <div class="zxcf_nav_l fl">
                <img src="images/zxcf_logo.png" alt="">
            </div>
            <div class="zxcf_nav_r fr">
                <img src="images/lg_pic01.png" alt="">
                <span>
                    <a href="Home.aspx">返回首页</a></span>

            </div>
        </div>
    </div>
    <!-- end  -->
    <div class="login_con_wper">
        <div class="login_con px1000 ">
            <div class="lg_section clearfix">
                <div class="lg_section_l fl">
                    <img src="images/lg_bg02.png">
                </div>
                <!-- end l -->
                <div class="lg_section_r fl">
                    <h2 class="lg_sec_tit clearfix">
                        <span class="fl">登录</span>
                    </h2>
                    <form>
                        <fieldset>
                            <p class="mt20">
                                <div class="emailCheckclass">
                                    <input type="text" id="UserName" name="UserName" autocomplete="off" placeholder="邮箱"  class="lg_input01 lg_input" />
                                    <ul class="email-sug" id="email-sug-wrapper">
                                    </ul>
                                </div>
                            </p>
                            <p class="mt20">
                                <input type="password" id="PassWord" name="PassWord" placeholder="密码" class="lg_input02 lg_input" />
                            </p>
                            <p class="clearfix lg_check"><span class="fl"></span></p>
                            <p><a href="#" class="lg_btn" onclick="posttoIndex()">立即登陆</a></p>
                            <span id="info" style="color: red"></span>
                        </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="zscf_bottom_wper">
        <div class="zscf_bottom px1000 clearfix">
            <p class="fl">© 2021 浙江冰点网络工作室 </p>
            <p class="fr">
                <a href="#">
                    <img src="images/360.png" alt=""></a>
                <a href="#">
                    <img src="images/kexin.png" alt=""></a>
                <%--<a href="#"><img src="images/norton.png" alt=""></a>--%>
            </p>
        </div>
    </div>
    <!-- footer end -->
    <style>
        .copyrights {
            text-indent: -9999px;
            height: 0;
            line-height: 0;
            font-size: 0;
            overflow: hidden;
        }
        .email-sug {
            width: 295px !important;
        }
            .email-sug li {
                 width: 295px !important;
            }
    </style>
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/EmailInput.js"></script>
    <script type="text/javascript">
        var txt = document.getElementById("UserName");
        var nowSelectTipIndex = 0;

        //获取输入文本
        txt.oninput = function (e) {
            //按下的是内容，则重置选中状态，坐标清零，避免光标位置已经计算存入。
            if (!(e.keyCode == 40 || e.keyCode == 38 || e.keyCode == 13)) {
                nowSelectTipIndex = 0;
            }
            judge();
            add();

        }

        function posttoIndex() {
            var userName = document.getElementById("UserName").value;
            if (userName == null || userName == undefined || userName == '') {
                document.getElementById("info").innerHTML = "请输入用户名!";
                return;
            }
            var passWord = document.getElementById("PassWord").value;
            if (passWord == null || passWord == undefined || passWord == '') {
                document.getElementById("info").innerHTML = "请输入密码!";
                return;
            }
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "ApiControl/Login.ashx",
                data: {
                    CheckParam: "SignIn",
                    UserName: document.getElementById("UserName").value,
                    PassWord: document.getElementById("PassWord").value
                },
                success: function (data) {
                    if (data.StatusCode == 1) {
                        window.location.href = "Index.aspx";
                    }
                    else {
                        document.getElementById("info").innerHTML = data.Error;
                        setTimeout(function () {
                            document.getElementById("info").innerHTML = "";
                        }, 3 * 1000)
                    }
                },
                error: function (jqXHR) {
                    console.log(jqXHR);
                }
            });
        }

    </script>
</body>
</html>
