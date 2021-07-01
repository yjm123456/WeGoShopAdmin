<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WeGoShopAdmin.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <title>后台商城管理系统</title>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <style>
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
                        <a href="javascript:;">
                            帮助中心
                        </a>
                        <dl class="layui-nav-child">
                            <dd><a href="IndexAbout/ShoppingCenter.aspx" target="_blank">商城模板</a></dd>
                            <dd>
                                <a href="images/IndexImage/用户手册.pdf" title="用户手册" target="_blank">用户手册</a>
                            </dd>
                        </dl>
                    </li>
                    <li class="layui-nav-item"><a  href="IndexAbout/ChangeLog.aspx" target="_blank">更新日志<span class="layui-badge-dot" style="position:relative;top:-2px;left:-4px;"></span></a></li>
                    <li class="layui-nav-item">
                        <a href="javascript:;">
                            <img src="images/IndexImage/default.jpg" class="layui-nav-img" />
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
                                                                <h3>服务器数</h3>
                                                                <p>1</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>商户加入数</h3>
                                                                <p>0</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>商户到期数</h3>
                                                                <p>0</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>合伙人数量</h3>
                                                                <p>2</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>开户待审核</h3>
                                                                <p>0</p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>商户即将到期</h3>
                                                                <p>0</p>
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
                                                            <a href="../SalesManManage/SalesManManageInfo.aspx">

                                                                <i class="layui-icon">
                                                                    <img src="images/shopManage/Partner.png" />
                                                                </i>

                                                                <cite>合伙人信息</cite>
                                                            </a>
                                                        </li>
                                                       <li class="layui-col-xs3">
                                                            <a href="../ShopManage/ShopManageInfo.aspx?Version=4">
                                                                <i class="layui-icon">
                                                                    <img src="images/shopManage/PersonalShop.png" />
                                                                </i>
                                                                <cite>自建商户数据</cite>
                                                            </a>
                                                            <a href="../InformationSide/InformationManage.aspx">

                                                                <i class="layui-icon">
                                                                    <img src="images/Information/infoLogo.png" />
                                                                </i>
                                                                
                                                                <cite>消息通知</cite>
                                                            </a>
                                                           

                                                        </li>
                                                          <li class="layui-col-xs3">
                                                            <a href="../Balance/SaleManDeposit.aspx">
                                                                <i class="layui-icon">
                                                                    <img src="images/Account/DepositApplication.png" />
                                                                </i>
                                                                <cite>提现申请</cite>
                                                            </a>
                                                                <a href="../Balance/SaleManDepositRecord.aspx">
                                                                <i class="layui-icon">
                                                                    <img src="images/Account/SaleManDepositRecord.png" />
                                                                </i>
                                                                <cite>提现记录</cite>
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
                                        <div class="layui-card-header">销售业绩</div>
                                        <div class="layui-card-body">
                                            <div class="" lay-filter="LAY-index-dataview" id="LAY-index-dataview" style="width: 100%; height: 450px;">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="layui-col-md4">
                            <div class="layui-card">
                                <div class="layui-card-header">版本信息</div>
                                <div class="layui-card-body layui-text">
                                    <table class="layui-table">
                                        <colgroup>
                                            <col width="100">
                                            <col>
                                        </colgroup>
                                        <tbody>
                                            <tr>
                                                <td>当前版本</td>
                                                <td>
                                                    <b>V1.0.0</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>技术支持</td>
                                                <td>
                                                    <a href="" target="_blank">浙江冰点网络科技有限公司</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>更新通知</td>
                                                <td style="padding-bottom: 0; padding: 0px 0px;">
                                                    <div style="padding: 9px 15px; height: 102px; overflow-y: auto;">
                                                        <p id="content">暂无</p>
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
                                        <li class="layui-col-xs4">近三月销售排名  </li>
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
    <script src="LayUI/layui.js"></script>
    <script type="text/javascript">
        layui.use(["table", 'form', 'layedit', 'laydate', 'upload', 'element'], function () {


        });
    </script>
    <script src="assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="assets/js/Login/SignOut.js"></script>
</body>
</html>
