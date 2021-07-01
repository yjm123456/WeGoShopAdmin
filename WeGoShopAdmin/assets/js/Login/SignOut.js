//退出程序
function LoginOut() {
    layer.confirm('您确定退出吗？', { btn: ['确定', '取消'], title: "提示" }, function () {
        LoginOutConfirm();
    })

};
function LoginOutConfirm() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../ApiControl/Login.ashx",
        data: {
            CheckParam: "SignOut"
        },
        success: function (data) {
            if (data.StatusCode == 1) {
                window.location.href = "../ControlStation.aspx";
            }
            else {
                document.getElementById("info").innerHTML = data.Error;
            }
        },
        error: function (jqXHR) {
            console.log(jqXHR);
        }
    });
}
function UpdatePassWord() {
    layer.open({
        type: 2, area: ['451px', '253px'], title: '修改密码',
        content: "../updatePassWord.aspx",
        btn: ["修改", "关闭"],
        yes: function (index, layero) {
            var _form = layer.getChildFrame('form', index);
            var NewPassword = _form.find('input')[0].value;
            var ConfirmPassword = _form.find('input')[1].value;
            if (/(.+){6,20}$/.test(NewPassword) == false) {
                layer.msg("密码必须6到20位");
                return;
            }
            if (NewPassword != ConfirmPassword) {
                layer.msg("两次输入的密码不一致!");
                return;
            }
            var UserId = document.getElementById("UserID").innerHTML;
            var UserRole = document.getElementById("UserRole").innerHTML;
            if(UserId==""||UserId==null||UserId==undefined)
            {
                layer.msg("未获取到用户登陆ID，请重新登陆!");
                return;
            }
            if (UserRole == "" || UserRole == null || UserRole == undefined) {
                layer.msg("未获取到用户角色，请重新登陆!");
                return;
            }
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../ApiControl/Login.ashx",
                data: {
                    CheckParam: "UpdatePassWord",
                    NewPassword: NewPassword,
                    Id: UserId,
                    UserRole: UserRole
                },
                success: function (data) {
                    if (data.StatusCode == 1) {
                        layer.close(index);
                        layer.confirm('修改成功，请重新登陆!', { btn: ['确定'], title: "提示" }, function () {
                            LoginOutConfirm();
                        })

                    }
                    else {
                        layer.msg("操作失败！" + data.Error);
                    }
                }
            });
        },
        success: function (layero, index) {

        }
    });
}