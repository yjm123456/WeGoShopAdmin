<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerSideInfo.aspx.cs" Inherits="WeGoShopAdmin.ServerSide.ServerSideInfo" %>

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
                                <dd><a href="#">服务器信息</a></dd>
                            </dl>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="layui-body">
                <!-- 内容主体区域 -->
                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
                    <legend>服务器信息</legend>
                </fieldset>
                <div class="layui-card">
                    <div class="layui-card-body">

                        <div class="layui-form-item">
                            <%-- <div class="layui-inline">
                                <label class="layui-form-label">日期范围</label>
                            </div>
                            <div class="layui-inline ">
                                <input type="text" name="StartDate" id="StartDate" lay-verify="StartDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input"/>
                            </div>
                            <div class="layui-inline">-</div>
                            <div class="layui-inline ">
                                <input type="text" name="EndDate" id="EndDate" lay-verify="EndDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input"/>
                            </div>--%>

                            <div class="layui-inline">
                                <label class="layui-form-label">IP地址</label>
                            </div>
                            <div class="layui-inline ">
                                <input type="text" name="IPPort" id="IPPort" autocomplete="off" class="layui-input" />
                            </div>
                            <asp:TextBox ID="BusInfo" name="BusInfo" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="SalesManId" name="SalesManId" runat="server" Style="display: none"></asp:TextBox>
                            <div class="layui-inline">
                                <button id="search" type="button" class="layui-btn" onclick="app.loadData()">查询</button>
                                <button id="add" type="button" class="layui-btn" onclick="app.Insert()">新增</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="layui-card">
                    <div class="layui-card-body layui-card-table-body">
                        <table id="ServerSideInfo"></table>
                    </div>
                </div>
            </div>

            <div class="layui-footer">
                <!-- 底部固定区域 -->
                © Copyright 2021 浙江冰点网络科技有限公司
            </div>
        </div>
    </form>

    <script type="text/html" id="operation">
        <a class="layui-btn layui-btn-sm " lay-event="Details" onclick="app.Details('{{ d.Id }}')">查看绑定详情</a>
        <a class="layui-btn layui-btn-sm layui-btn-normal" lay-event="Details" onclick="app.InsertDetails('{{ d.Id }}')">新增绑定信息</a>
        <a class="layui-btn layui-btn-sm layui-btn-warm" lay-event="Update" onclick="app.Update('{{ d.Id }}')">修改</a>
        <a class="layui-btn layui-btn-sm layui-btn-danger" lay-event="Delete" onclick="app.Delete('{{ d.Id }}')">删除</a>
    </script>
    
<script src="../LayUI/layui.js"></script>
<script src="../assets/js/site.js" asp-append-version="true"></script>
    <script>
        var app = {
            init: function () {
                var searchParams = layui.form.val("searchForm");
                layui.table.render({
                    id: "ServerSideInfo",
                    height: 600,
                    where: { queryParams: searchParams },
                    elem: '#ServerSideInfo',
                    url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=Select", //数据接口
                    page: true, //开启分页
                    defaultToolbar: [],
                    cols: [[ //表头
                        { field: 'ShopUserName', title: '商户名称', width: 150 },
                        { field: 'CPUInfomation', title: 'CPU信息', width: 460 },
                        { field: 'RAMInformation', title: '运行内存信息', width: 130 },
                        { field: 'DiskInformation', title: '磁盘信息', width: 100 },
                        { field: 'ServerIP', title: 'IP地址', width: 140 },
                        { field: 'PingPort', title: '端口', width: 120 },
                        { field: 'PassWord', title: '登陆密码', width: 120 },
                        { field: 'BandWidth', title: '带宽', width: 100 },
                        { field: 'StartDate', title: '购买时间', width: 120 },
                        { field: 'EndDate', title: '到期时间', width: 120 },
                        { field: 'Remark', title: '备注', width: 220 },
                        { fixed: 'right', title: '操作', width: 340, align: 'left', toolbar: '#operation' }
                    ]]
                });

            },
            Details: function (id) {
                layer.open({
                    type: 2, area: ['1330px', '723px'], title: '详细信息',
                    content: "details.aspx?id=" + id,
                    btn: ["关闭"],
                    yes: function (index, layero) {
                        layer.close(index);
                    },
                    success: function (layero, index) {
                    }
                });
            },
            loadData: function () {
                var searchParams = layui.form.val("searchForm");
                layui.table.reload("ServerSideInfo", {
                    page: { curr: 1 },
                    where: { queryParams: searchParams }
                });
            },
            Insert: function (Id) {
                var busInfo = document.getElementById("BusInfo").value;
                if (busInfo == "0") {
                    layer.msg("新增失败<br/>请先通过审核并生成商户号！");
                    return;
                }
                layer.open({
                    type: 2, area: ['790px', '548px'], title: '新增服务器基本信息',
                    content: "InsertServcerSide.aspx",
                    btn: ["确定", "关闭"],
                    yes: function (index, layero) {
                        var _form = layer.getChildFrame('form', index);
                        var InsertData = JSON.stringify(_form.serializeArray());
                        if (!_form.validForm()) {
                            return false;
                        }
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=InsertServerSide",
                            data: InsertData,
                            success: function (data) {
                                if (data.StatusCode == 1) {
                                    layer.close(index);
                                    layer.msg("添加成功！");
                                    app.loadData();
                                    return;
                                }
                                else {
                                    layer.msg("操作失败！" + data.Error);
                                    return;
                                }
                            }
                        });
                    },
                    success: function (layero, index) {

                    }
                });
            },
            Update: function (Id) {
                var busInfo = document.getElementById("BusInfo").value;
                if (busInfo == "0") {
                    layer.msg("修改失败<br/>请先通过审核并生成商户号！");
                    return;
                }
                layer.open({
                    type: 2, area: ['790px', '548px'], title: '修改服务器基本信息',
                    content: "UpdateServcerSide.aspx?Id=" + Id,
                    btn: ["确定", "关闭"],
                    yes: function (index, layero) {
                        var _form = layer.getChildFrame('form', index);
                        var InsertData = JSON.stringify(_form.serializeArray());
                        if (!_form.validForm()) {
                            return false;
                        }
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=UpdateServerSide",
                            data: InsertData,
                            success: function (data) {
                                if (data.StatusCode == 1) {
                                    layer.close(index);
                                    layer.msg("修改成功！");
                                    app.loadData();
                                    return;
                                }
                                else {
                                    layer.msg("操作失败！" + data.Error);
                                    return;
                                }
                            }
                        });
                    },
                    success: function (layero, index) {

                    }
                });
            },
            Delete: function (Id) {
                layer.confirm('数据删除后不可恢复，您确定删除该条数据吗？', { btn: ['确定', '取消'], title: "提示" }, function () {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=DeleteServerSide",
                        data: {
                            Id: Id
                        },
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                    layer.msg("删除成功！");
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
            },
            InsertDetails: function (Id) {
                var busInfo = document.getElementById("BusInfo").value;
                if (busInfo == "0") {
                    layer.msg("新增失败<br/>请先通过审核并生成商户号！");
                    return;
                }
                layer.open({
                    type: 2, area: ['790px', '702px'], title: '新增服务器详细信息',
                    content: "InsertDetail.aspx?id=" + Id,
                    btn: ["确定", "关闭"],
                    yes: function (index, layero) {
                        var _form = layer.getChildFrame('form', index);
                        var InsertData = JSON.stringify(_form.serializeArray());
                        if (!_form.validForm()) {
                            return false;
                        }
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=InsertServerSideDetails",
                            data: InsertData,
                            success: function (data) {
                                if (data.StatusCode == 1) {
                                    layer.close(index);
                                    layer.msg("添加成功！");
                                    app.loadData();
                                    return;
                                }
                                else {
                                    layer.msg("操作失败！" + data.Error);
                                    return;
                                }
                            }
                        });
                    },
                    success: function (layero, index) {

                    }
                });
            }

        };
        layui.use(["table", 'form', 'layedit', 'laydate', 'element'], function () {

            app.init();

        });
    </script>
    <script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="../assets/js/Login/SignOut.js"></script>
</body>
</html>
