<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertSalesMan.aspx.cs" Inherits="WeGoShopAdmin.SalesManManage.InsertSalesMan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户管理系统</title>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <link href="../assets/css/EmailInput.css" rel="stylesheet" />
    <link href="../LayUI/css/saas.main.css" rel="stylesheet" />
</head>
<body>
    <form id="addForm" runat="server">
        <fieldset class="layui-elem-field">
            <legend>新增合伙人</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 506px;">
                        <div id="LoginForm" lay-filter="addForm" class="layui-form">
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">合伙人昵称</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="SaleManName" autocomplete="off" class="layui-input" placeholder="合伙人昵称可随机生成，最好交由合伙人自行填写" Style="width: 514px;" runat="server"></asp:TextBox>

                                            <input type="button" class="layui-btn" onclick="CreatePartnerNickName()" value="随机生成" />
                                        </div>

                                    </div>
                                </div>

                            </div>


                            <div class="layui-form-item">

                                <div class="layui-inline">
                                    <label class="layui-form-label required">邮箱</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">

                                            <div class="emailCheckclass">
                                                <asp:TextBox ID="Email" autocomplete="off" class="layui-input" lay-verify="required|email" runat="server"></asp:TextBox>
                                                <ul class="email-sug" id="email-sug-wrapper">
                                                </ul>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">手机号</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="Phone" autocomplete="off" class="layui-input" PLACEHOLDER="用于合伙人登陆，请正确填写" lay-verify="required|phone" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">等级</label>
                                    <div class="layui-input-inline">
                                        <select id="Level" name="Level" asp-for="Level" lay-search="true" lay-filter="Level">
                                </select>
                                    </div>
                                </div>

                                <div class="layui-inline">
                                    <label class="layui-form-label">期初余额</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="Balance" autocomplete="off" class="layui-input" lay-verify="required|positivedecimal" runat="server">0.00</asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">联系地址</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="Address" autocomplete="off" class="layui-input" Style="width: 514px;" runat="server"></asp:TextBox>
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
                var $ = layui.jquery, form = layui.form;
                var laydate = layui.laydate;
                //加载合伙人下拉框
                var htmls = '';
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx",
                    data: {
                        CheckParam: "SelectLevelIdAndName"
                    },
                    success: function (data) {
                        for (var x in data) {
                            htmls += '<option value="' + x + '">' + data[x] + '</option>'
                        }
                        $('#Level').append(htmls);
                        // 添加完记得render下  否则不会刷新到页面中
                        form.render('select');
                    }
                });
                var dateEntryStart = laydate.render({
                    elem: '#StartDate', format: 'yyyy-MM-dd',
                    trigger: 'click',
                    btns: ['clear', 'confirm'],
                    // showBottom: false,
                    done: function (value, date) {
                        dateEntryEnd.config.min = {
                            year: date.year,
                            month: date.month - 1,
                            date: date.date,
                            hours: date.hours,
                            minutes: date.minutes,
                            seconds: date.seconds
                        };
                        // 作为 结束选择 的 开始时间
                        dateEntryEnd.config.value = value;
                    }
                });
                var dateEntryEnd = laydate.render({
                    elem: '#EndDate',
                    format: 'yyyy-MM-dd',
                    trigger: 'click',//  触发方式
                    btns: ['clear', 'confirm'],// 底部按钮
                    // showBottom: false,
                    done: function (value, date) {// 选择完成回调
                        dateEntryStart.config.max = {
                            year: date.year,
                            month: date.month - 1,
                            date: date.date,
                            hours: date.hours,
                            minutes: date.minutes,
                            seconds: date.seconds
                        };
                        dateEntryStart.config.value = value;
                    }
                });
            });

            function CreatePartnerNickName()
            {
                var nickName = "Partner_" + Math.random()*100000000000000000;
                document.getElementById("SaleManName").value = nickName;
            }
        </script>
    </form>
</body>
</html>
