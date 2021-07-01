<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassWord.aspx.cs" Inherits="WeGoShopAdmin.UpdatePassWord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <script src="LayUI/layui.js"></script>
    <script src="LayUI/layui.all.js"></script>
    <title></title>
</head>
<body>

    <form id="resetpasswordfrom" lay-filter="resetpasswordfrom" class="layui-form"
              method="post" action="/account/updatepassword/">
          <div class="layui-form-item">
                <div class="layui-inline">
                    </div>
              </div>
            <div class="layui-form-item">
                <div class="layui-inline">
                    <label class="layui-form-label">输入密码</label>
                    <div class="layui-input-inline">
                    <input type="password" placeholder="请输入输入密码" autocomplete="off" name="NewPassword"
                           lay-verify="NewPassword" id="NewPassword" class="layui-input" maxlength="30" /></div>
                </div>
            </div>
            <div class="layui-form-item">
                <label class="layui-form-label">确认密码</label>
                <div class="layui-input-inline">
                    <input type="password" placeholder="请输入输入密码" name="ConfirmPassword" id="ConfirmPassword"
                           lay-verify="ConfirmPassword" autocomplete="off" class="layui-input" maxlength="30" />
                </div>
            </div>
           
        </form>
</body>
    <script type="text/javascript">
        layui.use('form', function () {
            var form = layui.form, layer = layui.layer, $ = layui.$;
            //自定义验证规则
            form.verify({
                NewPassword: [/(.+){6,20}$/, '密码必须6到20位'],
                ConfirmPassword: function (value) {
                    var repassvalue = $("#NewPassword").val();
                    if (value != repassvalue) {
                        return '两次输入的密码不一致!';
                    }
                }
            });
            //监听提交
            form.on('submit(resetpasswordfrom)', function (data) {
                //$.ajax({
                //    asyn: false,
                //    type: "POST",
                //    url: "/account/updatepassword/",
                //    dataType: "JSON",
                //    data: {  NewPassword: $("#NewPassword").val() },
                //    success: function (result) {
                //        if (result.isSuccess) {
                //            layer.msg("修改密码成功", { icon: 2 });
                //            window.location.href="/account/login/";
                //        }
                //        else {
                //            layer.msg("修改密码失败", { icon: 2 });
                //        }
                //    }
                //});
                //return false;
            });
        });
    </script>
</html>
