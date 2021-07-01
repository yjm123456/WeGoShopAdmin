<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexSalesMan.aspx.cs" Inherits="WeGoShopAdmin.IndexSalesMan" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <title>后台商城管理系统</title>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <style>
        .layui-table-cell {
            width: auto;
        }

        label.required::after {
            content: '* ';
            color: #FF5722;
            margin-right: -9px;
        }

        .layui-fluid {
            padding: 15px;
        }

        .layui-card {
            margin-bottom: 15px;
            border-radius: 2px;
            background-color: #fff;
            box-shadow: 0 1px 2px 0 rgba(0,0,0,.05);
        }

        .layadmin-shortcut li {
            text-align: center;
            margin-top: 10px;
        }

        .layadmin-backlog li {
            text-align: center;
            margin-top: 10px;
        }

        .layadmin-backlog .layadmin-backlog-body {
            display: block;
            padding: 10px 15px;
            background-color: #f8f8f8;
            color: #999;
            border-radius: 2px;
            transition: all .3s;
            -webkit-transition: all .3s;
        }

        .layadmin-carousel {
            background-color: #fff;
        }

        .layadmin-shortcut li .layui-icon {
            display: inline-block;
            width: 100%;
            height: 60px;
            line-height: 60px;
            text-align: center;
            border-radius: 2px;
            font-size: 30px;
            background-color: #F8F8F8;
            color: #333;
            transition: all .3s;
            -webkit-transition: all .3s;
        }

        .layadmin-backlog .layadmin-backlog-body {
            display: block;
            padding: 10px 15px;
            background-color: #f8f8f8;
            color: #999;
            border-radius: 2px;
            transition: all .3s;
            -webkit-transition: all .3s;
        }

        .layadmin-backlog-body p cite {
            font-style: normal;
            font-size: 30px;
            font-weight: 300;
            color: #009688;
        }

        .layui-carousel > [carousel-item] > * {
            display: none;
            position: absolute;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: #fff;
            transition-duration: .3s;
            -webkit-transition-duration: .3s;
        }

        .layui-card-line {
            border-bottom: 1px solid #f6f6f6;
            color: #333;
        }

        .topsalecontent {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            background: #FF7E00;
        }

        .salecontent {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            background: #BDBDBD;
        }

        .topsalecontent p {
            width: 30px;
            height: 30px;
            color: #fff;
            text-align: center;
            line-height: 30px;
        }

        .salecontent p {
            width: 30px;
            height: 30px;
            color: #fff;
            text-align: center;
            line-height: 30px;
        }
    </style>
</head>
<script src="LayUI/layui.js"></script>
<script src="LayUI/layui.all.js"></script>
<script src="assets/js/jquery.min.js"></script>
<script src="assets/js/Register/jquery-1.8.2.min.js"></script>
<script src="assets/js/Register/jquery-ui.min.js"></script>
<script src="assets/js/site.js" asp-append-version="true"></script>
<script src="assets/js/xm-select.js"></script>
<script src="assets/js/Login/SignOut.js"></script>
<body class="layui-layout-body">
    <form id="searchForm" runat="server">
        <div class="layui-layout layui-layout-admin">
            <div class="layui-header">
                <div class="layui-logo">商户管理系统</div>
                <!-- 头部区域（可配合layui已有的水平导航） -->
                <ul class="layui-nav layui-layout-left">
                    <li class="layui-nav-item"><a href="../Index.aspx">控制台</a></li>

                </ul>
                <ul class="layui-nav layui-layout-right">
                    <li class="layui-nav-item">
                        <a href="javascript:;">帮助中心
                        </a>
                        <dl class="layui-nav-child">
                            <dd><a href="IndexAbout/ShoppingCenter.aspx" target="_blank">商城模板</a></dd>
                            <dd>
                                <a href="images/IndexImage/用户手册.pdf" title="用户手册" target="_blank">用户手册</a>
                            </dd>
                        </dl>
                    </li>
                    <li class="layui-nav-item"><a href="IndexAbout/ChangeLog.aspx" target="_blank">更新日志<span class="layui-badge-dot" style="position: relative; top: -2px; left: -4px;"></span></a></li>
                    <li class="layui-nav-item">
                        <a href="javascript:;">
                            <img src="images/IndexImage/default.jpg" class="layui-nav-img" />
                            欢迎您，<asp:Label ID="UserName" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="UserID" runat="server" Style="display: none" Text="Label"></asp:Label>
                            <asp:Label ID="UserRole" runat="server" Style="display: none" Text="Label"></asp:Label>
                        </a>
                        <dl class="layui-nav-child">
                            <dd><a onclick="UpdatePassWord()">修改密码</a></dd>
                            <dd><a onclick="LoginOut()">退出</a></dd>
                        </dl>
                    </li>
                </ul>
            </div>

            <div class="layui-side layui-bg-black">
                <div class="layui-side-scroll">
                    <!-- 左侧导航区域（可配合layui已有的垂直导航） -->
                    <ul class="layui-nav layui-nav-tree" lay-filter="test">
                        <li class="layui-nav-item layui-nav-itemed">
                            <a href="javascript:;">商户模块</a>
                            <dl class="layui-nav-child">
                                <dd><a href="ShopManage/ShopManageInfo.aspx?Version=0">商户信息</a></dd>

                            </dl>
                        </li>
                        <li class="layui-nav-item layui-nav-itemed">
                            <a href="javascript:;">云市场</a>
                            <dl class="layui-nav-child">
                                <dd><a href="ServerSide/ServerSideInfo.aspx">服务器信息</a></dd>
                            </dl>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="layui-body">
                <div class="layui-fluid">
                    <div class="layui-row layui-col-space15">
                        <div class="layui-col-md8">
                            <div class="layui-row layui-col-space15">
                                <div class="layui-col-md6">
                                    <div class="layui-card">
                                        <div class="layui-card-header">今日数据</div>
                                        <div class="layui-card-body">
                                            <div class="layui-carousel layadmin-carousel layadmin-backlog" lay-anim=""
                                                lay-indicator="inside" lay-arrow="none" style="width: 100%; height: 210px; margin: 0 5px;">
                                                <div carousel-item="">
                                                    <ul class="layui-row layui-col-space10 layui-this">
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>商户加入数量</h3>
                                                                <p>0</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>商户即将到期</h3>
                                                                <p>0</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>商户到期数量</h3>
                                                                <p>1</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>服务器数量</h3>
                                                                <p>1</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>开户待审核</h3>
                                                                <p>1</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>待收款提现单</h3>
                                                                <p>1</p>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="layui-col-md6">
                                    <div class="layui-card">
                                        <div class="layui-card-header">重点关注</div>
                                        <div class="layui-card-body">
                                            <div class="layui-carousel layadmin-carousel layadmin-shortcut" lay-anim=""
                                                lay-indicator="inside" lay-arrow="none" style="width: 100%; height: 210px; margin: 0 5px;">
                                                <div carousel-item="">
                                                    <ul class="layui-row layui-col-space10 layui-this">

                                                        <li class="layui-col-xs3">
                                                            <a href="../ShopManage/ShopManageInfo.aspx?Version=0">
                                                                <i class="layui-icon">
                                                                    <img src="images/shopManage/ShopInfo.png" />
                                                                </i>
                                                                <cite>商户信息</cite>
                                                            </a>
                                                            <a href="../Balance/SaleManDepositRecord.aspx">
                                                                <i class="layui-icon">
                                                                    <img src="images/Account/SaleManDepositRecord.png" />
                                                                </i>
                                                                <cite>提现记录</cite>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs3">
                                                            <a href="../ShopManage/ShopManageInfo.aspx?Version=4">
                                                                <i class="layui-icon">
                                                                    <img src="images/shopManage/PersonalShop.png" />
                                                                </i>
                                                                <cite>商户创建数据</cite>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs3">
                                                            <a href="../Balance/BalanceManage.aspx">
                                                                <i class="layui-icon">
                                                                    <img src="images/Account/Balance.png" />
                                                                </i>
                                                                <cite>账户收入</cite>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs3">
                                                            <a href="../Balance/SaleManDeposit.aspx">
                                                                <i class="layui-icon">
                                                                    <img src="images/Account/DepositApplication.png" />
                                                                </i>
                                                                <cite>提现申请</cite>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="layui-col-md12">
                                    <div class="layui-card">
                                        <div class="layui-card-header">个人信息</div>
                                        <div class="layui-card-body">
                                            <div class="" lay-filter="LAY-index-dataview" id="lay-index-dataview" style="width: 100%; height: 450px;">
                                                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                                                    <legend>我的昵称</legend>
                                                </fieldset>
                                                <blockquote class="layui-elem-quote layui-quote-nm">
                                                    <asp:Label ID="SaleManName" runat="server" Text="Label"></asp:Label>
                                                   &nbsp; &nbsp; &nbsp;<button type="button" class="layui-btn layui-btn-primary layui-btn-sm" style="text-align:right;margin-right:auto" onclick="UpdateSaleManName()">修改</button>
                                                </blockquote>
                                                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                                                    <legend>联系电话</legend>
                                                </fieldset>
                                                <blockquote class="layui-elem-quote layui-quote-nm">
                                                    <asp:Label ID="Phone" runat="server" Text="Label"></asp:Label> &nbsp; &nbsp; &nbsp;<button type="button" class="layui-btn layui-btn-primary layui-btn-sm" style="text-align:right;margin-right:auto" onclick="UpdatePhone()">修改</button>
                                              
                                                </blockquote>
                                                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                                                    <legend>联系地址</legend>
                                                </fieldset>
                                                <blockquote class="layui-elem-quote layui-quote-nm">
                                                    <asp:Label ID="Address" runat="server" Text="Label"></asp:Label> &nbsp; &nbsp; &nbsp;<button type="button" class="layui-btn layui-btn-primary layui-btn-sm" style="text-align:right;margin-right:auto" onclick="UpdateAddress()">修改</button>
                                              
                                                </blockquote>
                                                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                                                    <legend>邮箱</legend>
                                                </fieldset>
                                                <blockquote class="layui-elem-quote layui-quote-nm">
                                                    <asp:Label ID="Email" runat="server" Text="Label"></asp:Label> &nbsp; &nbsp; &nbsp;<button type="button" class="layui-btn layui-btn-primary layui-btn-sm" style="text-align:right;margin-right:auto" onclick="UpdateSaleManEmail()">修改</button>
                                              
                                                </blockquote>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="layui-col-md4">
                            <div class="layui-card">
                                <div class="layui-card-header">其他信息</div>
                                <div class="layui-card-body layui-text">
                                    <table class="layui-table">
                                        <colgroup>
                                            <col width="100">
                                            <col>
                                        </colgroup>
                                        <tbody>
                                            <tr>
                                                <td>我的等级</td>
                                                <td>
                                                    <b>
                                                        <asp:Label ID="Level" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>参与分成</td>
                                                <td>
                                                    <b>
                                                        <asp:Label ID="DistributionRate" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>最新通知</td>
                                                <td style="padding-bottom: 0; padding: 0px 0px;">
                                                    <div style="padding: 9px 15px; height: 102px; overflow-y: auto;">
                                                        <p id="content">
                                                            <asp:Label ID="Information" runat="server" Text="Label"></asp:Label>
                                                        </p>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="layui-card">
                                <div class="layui-card-header">
                                    <ul class="layui-row layui-this">
                                        <li class="layui-col-xs4">商户发展排名</li>
                                    </ul>
                                </div>
                                <div class="layui-card-body" id="topsales" style="height: 450px;">
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="layui-footer">
                <!-- 底部固定区域 -->
                © Copyright 2021 浙江冰点网络科技有限公司
            </div>
        </div>
    </form>
    <script type="text/template" id="tmplUpdate">
        <div class="layui-card">
            <div class="layui-card-body">

                <form id="updateForm" lay-filter="updateForm" class="layui-form" style="width: 200px; height: 110px;">
                    <input type="hidden" id="Id" name="Id" value="{{ d.Id }}" />

                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                请重新设置您的昵称    
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                <input type="text" class="layui-input" lay-verify="required" placeholder="请输入昵称" name="SaleManName" autocomplete="off" maxlength="30" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </script>

     <script type="text/template" id="tmplUpdatePhone">
        <div class="layui-card">
            <div class="layui-card-body">

                <form id="updatePhoneForm" lay-filter="updatePhoneForm" class="layui-form" style="width: 200px; height: 110px;">
                    <input type="hidden" name="Id" value="{{ d.Id }}" />

                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                请重新设置您的联系电话   
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                <input type="text" class="layui-input" lay-verify="required|phone" placeholder="请输入手机号" name="Phone" autocomplete="off" maxlength="30" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </script>
     <script type="text/template" id="tmplUpdateAddress">
        <div class="layui-card">
            <div class="layui-card-body">

                <form id="updateAddressForm" lay-filter="updateAddressForm" class="layui-form" style="width: 200px; height: 110px;">
                    <input type="hidden" name="Id" value="{{ d.Id }}" />

                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                请重新设置您的联系地址  
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                <input type="text" class="layui-input"  placeholder="请输入联系地址" name="Address" autocomplete="off" maxlength="30" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </script>
     <script type="text/template" id="tmplUpdateEmail">
        <div class="layui-card">
            <div class="layui-card-body">

                <form id="updateEmailForm" lay-filter="updateEmailForm" class="layui-form" style="width: 200px; height: 110px;">
                    <input type="hidden"  name="Id" value="{{ d.Id }}" />

                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                请重新设置您的邮箱    
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                <input type="text" class="layui-input" lay-verify="required|email" placeholder="请输入邮箱" name="Email" autocomplete="off" maxlength="30" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </script>

    <script src="LayUI/layui.js"></script>
    <script type="text/javascript">

        layui.use(["table", 'form', 'layedit', 'laydate', 'upload', 'element'], function () {


            var userName = document.getElementById("UserName").innerHTML;

            if (userName = "" || userName == "" || userName == null) {
                UpdateSaleManName();

            }
        });

        function UpdateSaleManName() {
            var userId = document.getElementById("UserID").innerHTML;
            layer.open({
                title: "修改昵称",
                type: 1,
                content: layui.laytpl($("#tmplUpdate").html()).render({ "Id": userId }),
                btn: ["确定", "关闭"],
                yes: function (index, layero) {
                    if ($("#updateForm").validForm() == false) {
                        return false;
                    }
                    var data = layui.form.val("updateForm");
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx?CheckParam=UpdateSaleManName",
                        data: data,
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                layer.close(index);
                                layer.confirm('修改成功！', { btn: ['确定'], title: "提示" }, function () {
                                    location.reload();
                                })
                            }
                            else {
                                layer.msg("操作失败！" + data.Error);
                                return;
                            }
                        }
                    });

                },
                btn2: function (index, layero) {
                },
                success: function (layero, index) {

                }
            });
        }
        function UpdateSaleManEmail() {
            var userId = document.getElementById("UserID").innerHTML;
            layer.open({
                title: "修改邮箱",
                type: 1,
                content: layui.laytpl($("#tmplUpdateEmail").html()).render({ "Id": userId }),
                btn: ["确定", "关闭"],
                yes: function (index, layero) {
                    if ($("#updateEmailForm").validForm() == false) {
                        return false;
                    }
                    var data = layui.form.val("updateEmailForm");
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx?CheckParam=UpdateSaleManEmail",
                        data: data,
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                layer.close(index);
                                layer.confirm('修改成功！', { btn: ['确定'], title: "提示" }, function () {
                                    location.reload();
                                })
                            }
                            else {
                                layer.msg("操作失败！" + data.Error);
                                return;
                            }
                        }
                    });

                },
                btn2: function (index, layero) {
                },
                success: function (layero, index) {

                }
            });
        }
        function UpdatePhone() {
            var userId = document.getElementById("UserID").innerHTML;
            layer.open({
                title: "修改联系电话",
                type: 1,
                content: layui.laytpl($("#tmplUpdatePhone").html()).render({ "Id": userId }),
                btn: ["确定", "关闭"],
                yes: function (index, layero) {
                    if ($("#updatePhoneForm").validForm() == false) {
                        return false;
                    }
                    var data = layui.form.val("updatePhoneForm");
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx?CheckParam=UpdatePhone",
                        data: data,
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                layer.close(index);
                                layer.confirm('修改成功！', { btn: ['确定'], title: "提示" }, function () {
                                    location.reload();
                                })
                            }
                            else {
                                layer.msg("操作失败！" + data.Error);
                                return;
                            }
                        }
                    });

                },
                btn2: function (index, layero) {
                },
                success: function (layero, index) {

                }
            });
        }
        function UpdateAddress() {
            var userId = document.getElementById("UserID").innerHTML;
            layer.open({
                title: "修改地址",
                type: 1,
                content: layui.laytpl($("#tmplUpdateAddress").html()).render({ "Id": userId }),
                btn: ["确定", "关闭"],
                yes: function (index, layero) {
                    if ($("#updateAddressForm").validForm() == false) {
                        return false;
                    }
                    var data = layui.form.val("updateAddressForm");
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx?CheckParam=UpdateSaleManAddress",
                        data: data,
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                layer.close(index);
                                layer.confirm('修改成功！', { btn: ['确定'], title: "提示" }, function () {
                                    location.reload();
                                })
                            }
                            else {
                                layer.msg("操作失败！" + data.Error);
                                return;
                            }
                        }
                    });

                },
                btn2: function (index, layero) {
                },
                success: function (layero, index) {

                }
            });
        }
    </script>
</body>
</html>

