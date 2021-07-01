<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoPermission.aspx.cs" Inherits="WeGoShopAdmin.NoPermission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <title>后台商城管理系统</title>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
</head>
<body class="layui-layout-body">
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
                        <img src="../images/IndexImage/default.jpg" class="layui-nav-img">
                        <asp:Label ID="UserName" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="UserID" runat="server" style="display:none" Text="Label"></asp:Label>
                            <asp:Label ID="UserRole" runat="server" style="display:none" Text="Label"></asp:Label>
                    </a>
                          <dl class="layui-nav-child">
                            <dd><a  onclick="UpdatePassWord()">修改密码</a></dd>
                        </dl>
                </li>
                <li class="layui-nav-item"><a onclick="LoginOut()">退出</a></li>
            </ul>
        </div>

        <div class="layui-side layui-bg-black">
            <div class="layui-side-scroll">
                <!-- 左侧导航区域（可配合layui已有的垂直导航） -->
                <ul class="layui-nav layui-nav-tree" lay-filter="test">
                    <li class="layui-nav-item layui-nav-itemed">
                        <a  href="javascript:;">商户模块</a>
                        <dl class="layui-nav-child">
                            <dd><a href="../ShopManage/ShopManageInfo.aspx">商户信息</a></dd>
                           
                        </dl>
                    </li>
                    <%-- <li class="layui-nav-item">
          <a href="javascript:;">解决方案</a>
          <dl class="layui-nav-child">
            <dd><a href="javascript:;">列表一</a></dd>
            <dd><a href="javascript:;">列表二</a></dd>
            <dd><a href="">超链接</a></dd>
          </dl>
        </li>--%>
                    <li class="layui-nav-item layui-nav-itemed"><a href="javascript:;">云市场</a>
                        <dl class="layui-nav-child">
                            <dd><a href="../ServerSide/ServerSideInfo.aspx">服务器信息</a></dd>
                        </dl>
                    </li>
                    <%--<li class="layui-nav-item"><a href="">发布商品</a></li>--%>
                </ul>
            </div>
        </div>

        <div class="layui-body">
            <!-- 内容主体区域 -->
            <fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
                <legend>禁止访问</legend>
            </fieldset>
            <div class="layui-card">
                <div class="layui-card-body">
                   您暂无权限查看！点击<a href="Index.aspx">返回首页</a>
                </div>
            </div>
            <div class="layui-card">
                <div class="layui-card-body layui-card-table-body">
                    <table id="SalesManInfoTable"></table>
                </div>
            </div>
        </div>

        <div class="layui-footer">
            <!-- 底部固定区域 -->
            © Copyright 2021 浙江冰点网络科技有限公司
        </div>
    </div>
    <script src="../LayUI/layui.js"></script>
    <script>
        layui.use(["table", 'form', 'layedit', 'laydate', 'element'], function () {

            app.init();

        });
     
    </script>
    <script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="../assets/js/Login/SignOut.js"></script>
</body>
</html>
