<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaleManDepositRecord.aspx.cs" Inherits="WeGoShopAdmin.Balance.SaleManDepositRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
    <title>后台商城管理系统</title>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <style type="text/css">
        .layui-table-cell {
            　　 width: auto;
        }
    </style>
</head>
<script src="../LayUI/layui.all.js"></script>
<script src="../assets/js/jquery.min.js"></script>
<script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
<script src="../assets/js/Register/jquery-ui.min.js"></script>
<script src="../assets/js/xm-select.js"></script>
<script src="../assets/js/site.js" asp-append-version="true"></script>
<script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
<script src="../assets/js/Login/SignOut.js"></script>
<body class="layui-layout-body">
    <form id="searchForm" runat="server" class="layui-form atjubo-form-search" lay-filter="searchForm" onsubmit="return false;">
        <div class="layui-layout layui-layout-admin">
            <div class="layui-header">
                <div class="layui-logo">商户管理系统</div>
                <!-- 头部区域（可配合layui已有的水平导航） -->
                <ul class="layui-nav layui-layout-left">
                    <li class="layui-nav-item"><a href="../Index.aspx">控制台</a></li>
                </ul>
                <ul class="layui-nav layui-layout-right">
                    <li class="layui-nav-item">
                        <a href="javascript:;">
                            帮助中心
                        </a>
                        <dl class="layui-nav-child">
                            <dd><a href="../IndexAbout/ShoppingCenter.aspx" target="_blank">商城模板</a></dd>
                            <dd>
                                <a href="../images/IndexImage/用户手册.pdf" title="用户手册" target="_blank">用户手册</a>
                            </dd>
                        </dl>
                    </li>
                    <li class="layui-nav-item"><a  href="../IndexAbout/ChangeLog.aspx" target="_blank">更新日志<span class="layui-badge-dot" style="position:relative;top:-2px;left:-4px;"></span></a></li>
                    <li class="layui-nav-item">
                        <a href="javascript:;">
                            <img src="../images/IndexImage/default.jpg" class="layui-nav-img" />
                            <asp:Label ID="UserName" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="UserID" runat="server" style="display:none" Text="Label"></asp:Label>
                            <asp:Label ID="UserRole" runat="server" style="display:none" Text="Label"></asp:Label>
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
                            <a class="" href="javascript:;">商户模块</a>
                            <dl class="layui-nav-child">
                                <dd><a href="../ShopManage/ShopManageInfo.aspx?Version=0">商户信息</a></dd>
                                
                            </dl>
                        </li>
                        <li class="layui-nav-item layui-nav-itemed"><a href="javascript:;">云市场</a>
                            <dl class="layui-nav-child">
                                <dd><a href="../ServerSide/ServerSideInfo.aspx">服务器信息</a></dd>
                            </dl>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="layui-body">
                <!-- 内容主体区域 -->
                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
                    <legend>提现记录</legend>
                </fieldset>
                <div class="layui-card">
                    <div class="layui-card-body">

                          <div class="layui-form-item">
                            <div class="layui-inline">
                                <label class="layui-form-label">日期范围</label>
                            </div>
                            <div class="layui-inline ">
                                <input type="text" name="StartDate" id="StartDate" lay-verify="StartDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input" />
                            </div>
                            <div class="layui-inline">-</div>
                            <div class="layui-inline ">
                                <input type="text" name="EndDate" id="EndDate" lay-verify="EndDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input" />
                            </div>
                                <div id="IsHidden1" class="layui-inline"> <label class="layui-form-label">合伙人</label></div>
                            <div id="IsHidden2" class="layui-inline ">
                                <select id="SalesManId" name="SalesManId" asp-for="SalesManId" lay-search="true" lay-filter="SalesManId">
                                </select>
                             </div>
                            <asp:TextBox ID="DepositState" name="DepositState" runat="server" Style="display: none">2</asp:TextBox>
                            <asp:TextBox ID="SaleManId" name="SaleManId" runat="server" Style="display: none"></asp:TextBox>
                            <div class="layui-inline">
                                <button id="search" type="button" class="layui-btn" onclick="app.loadData()">查询</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="layui-card">
                    <div class="layui-card-body layui-card-table-body">
                        <table id="SaleManDepositInfo"></table>
                    </div>
                </div>
            </div>

            <div class="layui-footer">
                <!-- 底部固定区域 -->
                © Copyright 2021 浙江冰点网络科技有限公司
            </div>
        </div>
    </form>

    
<script src="../LayUI/layui.js"></script>
<script src="../assets/js/site.js" asp-append-version="true"></script>
    <script>
        var app = {
            init: function () {
                var userRole = document.getElementById("UserRole").innerHTML;
                if (userRole == "SalesMan") {
                    document.getElementById("IsHidden1").remove();
                    document.getElementById("IsHidden2").remove();
                }
                var searchParams = layui.form.val("searchForm");
                layui.table.render({
                    id: "SaleManDepositInfo",
                    height: 600,
                    where: { queryParams: searchParams },
                    elem: '#SaleManDepositInfo',
                    url: "../ApiControl/Finance/SaleManDepositInfo.ashx?CheckParam=Select", //数据接口
                    page: true, //开启分页
                    defaultToolbar: [],
                    cols: [[ //表头
                        { field: 'SaleManName', title: '合伙人姓名', width: 150 },
                        { field: 'CreateTime', title: '申请时间', width: 120 },
                        { field: 'Phone', title: '联系方式', width: 150 },
                        { field: 'DepositMoney', title: '提现金额', width: 130 },
                        { field: 'DepositCardNo', title: '提现账号', width: 200 },
                        { field: 'DepositWay', title: '账号类型', width: 120 },
                        { field: 'ReceivableName', title: '收款人', width: 120 },
                        { field: 'DepositState', title: '提现状态', width: 120 },
                        { field: 'Remark', title: '备注', width: 320 }
                    ]]
                });

            },
            loadData: function () {
                var searchParams = layui.form.val("searchForm");
                layui.table.reload("SaleManDepositInfo", {
                    page: { curr: 1 },
                    where: { queryParams: searchParams }
                });
            },
            ConfirmAccount: function (Id) {
                layer.confirm('确认到账后该单据结束，您确定收到款项了吗？', { btn: ['确定', '取消'], title: "提示" }, function () {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/SaleManDeposit/SaleManDepositInfo.ashx?CheckParam=ConfirmAccount",
                        data: {
                            Id: Id
                        },
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                    layer.msg("确认收款成功！");
                                    app.loadData();
                                    return;
                                }
                                else {
                                    layer.msg("操作失败！" + data.Error);
                                    return;
                                }
                        },
                        error: function (jqXHR) {
                            console.log(jqXHR);
                        }
                    });
                })
            }

        };
        layui.use(["table", 'form', 'layedit', 'laydate', 'element'], function () {
            var $ = layui.jquery, form = layui.form;
            var htmls = '<option value="0">全部</option>';
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx",
                data: {
                    CheckParam: "SelectIdAndName"
                },
                success: function (data) {
                    for (var x in data) {
                        htmls += '<option value="' + x + '">' + data[x] + '</option>'
                    }
                    $('#SalesManId').append(htmls);
                    // 添加完记得render下  否则不会刷新到页面中
                    form.render('select');
                }
            });
            var year = new Date().getFullYear();
            var month = new Date().getMonth() + 1;
            var endDateparam = new Date().getDate();
            if (month < 10) {
                month = "0" + month;
            }
            if (endDateparam < 10) {
                endDateparam = "0" + endDateparam;
            }
            var form = layui.form
                , layer = layui.layer
                , layedit = layui.layedit
                , laydate = layui.laydate;

            //日期
            var laydate = layui.laydate;
            var dateEntryStart = laydate.render({
                elem: '#StartDate', format: 'yyyy-MM-dd',
                value: year + "-" + month + "-01",
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
                value: year + "-" + month + "-" + endDateparam,
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
            app.init();

        });
    </script>
    <script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="../assets/js/Login/SignOut.js"></script>
</body>
</html>
