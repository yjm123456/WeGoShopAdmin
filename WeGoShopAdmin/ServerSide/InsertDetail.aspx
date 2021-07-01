<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertDetail.aspx.cs" Inherits="WeGoShopAdmin.ServerSide.InsertDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户管理系统</title>
    <script src="../assets/js/jquery.min.js"></script>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <link href="../assets/css/EmailInput.css" rel="stylesheet" />
    <link href="../LayUI/css/saas.main.css" rel="stylesheet" />
</head>
<body>
    <form id="addForm" runat="server">
        <fieldset class="layui-elem-field">
            <legend>详细信息新增</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 524px;">
                        <div id="LoginForm" lay-filter="addForm" class="layui-form">
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">域名</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="ServerPathId" Visible="true" autocomplete="off" class="layui-input" Style="display: none" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="DomainName" autocomplete="off" class="layui-input" lay-verify="required" Style="width: 514px;" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">公众号名称</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="BindWechatStationName" autocomplete="off" lay-verify="required" class="layui-input" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">公众号类型</label>
                                    <div class="layui-input-inline">
                                        <select id="BindWechatStationType" name="BindWechatStationType" lay-verify="required" asp-for="BindWechatStationType" lay-search="true" lay-filter="BindWechatStationType">
                                            <option value="1">订阅号</option>
                                            <option value="2">服务号</option>
                                            <option value="3">小程序</option>
                                            <option value="4">企业微信</option>
                                        </select>
                                    </div>
                                </div>


                            </div>
                            <div class="layui-form-item">

                                <div class="layui-inline">
                                    <label class="layui-form-label required">绑定电话</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="Phone" autocomplete="off" lay-verify="required|phone" class="layui-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label ">绑定邮箱</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <div class="emailCheckclass">
                                                <asp:TextBox ID="Email" autocomplete="off" class="layui-input" runat="server"></asp:TextBox>
                                                <ul class="email-sug" id="email-sug-wrapper">
                                                </ul>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">

                                <div class="layui-inline">
                                    <label class="layui-form-label ">公众号账户</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="BindWechatStationUserName" autocomplete="off" class="layui-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label">账户密码</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="BindWechatStationPassWord" autocomplete="off" type="password" class="layui-input" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">AppId</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="BindWechatStationAppId" autocomplete="off" class="layui-input" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label ">AppSecret</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="BindWechatStationAppSecret" autocomplete="off" type="password" class="layui-input" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">备注</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="Remark" autocomplete="off" class="layui-input" Style="width: 514px;" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <script src="../assets/js/jquery.min.js"></script>
        <script src="../LayUI/layui.all.js"></script>
        <script src="../assets/js/site.js"></script>
        <script src="../assets/js/EmailInput.js"></script>
        <script src="../assets/js/xm-select.js"></script>
        <script type="text/javascript">
            var txt = document.getElementById("Email");
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

            layui.use(['form', 'layedit', 'laydate'], function () {
                var form = layui.form
                    , layer = layui.layer
                    , layedit = layui.layedit
                    , laydate = layui.laydate;

                //日期
                laydate.render({
                    elem: '#StartDate'
                });
                laydate.render({
                    elem: '#EndDate'
                });
            });
        </script>
    </form>
</body>
</html>
