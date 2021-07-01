<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexShop.aspx.cs" Inherits="WeGoShopAdmin.IndexShop" %>

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
                    <li class="layui-nav-item"><a href="../IndexShop.aspx">控制台</a></li>

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
                            <asp:Label ID="LiansonMan" runat="server" Text="Label"></asp:Label>
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
                                <dd><a href="ShopManage/ShopManageInfo.aspx">商户信息</a></dd>
                                
                            </dl>
                        </li>
                        <li class="layui-nav-item layui-nav-itemed"><a href="javascript:;">云市场</a>
                            <dl class="layui-nav-child">
                                <dd><a href="ServerSide/ServerSideInfo.aspx">服务器信息</a></dd>
                            </dl>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="layui-body">
                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 3px;">
                    <legend>进度：<span id="percent"></span></legend>
                </fieldset>
                <div class="layui-progress layui-progress-big" lay-filter="demo">
                    <div class="layui-progress-bar"></div>
                </div>
                <div class="layui-fluid">
                    <div class="layui-row layui-col-space15">

                        <div class="layui-col-md8">
                            <div class="layui-row layui-col-space15">
                                <div class="layui-col-md6">
                                    <div class="layui-card">
                                        <div class="layui-card-header">
                                            <asp:Label ID="CompanyName" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <div class="layui-card-body">
                                            <div class="layui-carousel layadmin-carousel layadmin-backlog" lay-anim=""
                                                lay-indicator="inside" lay-arrow="none" style="width: 100%; height: 210px; margin: 0 5px;">
                                                <div carousel-item="">
                                                    <ul class="layui-row layui-col-space10 layui-this">
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>合同编号</h3>
                                                                <p>
                                                                    <asp:Label ID="ContractNo" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>


                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>组织机构</h3>
                                                                <p>
                                                                    <asp:Label ID="InstitutionalType" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>机构代码</h3>
                                                                <p>
                                                                    <asp:Label ID="OrignaztionType" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>联系电话</h3>
                                                                <p>
                                                                    <asp:Label ID="Phone" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>审核状态</h3>
                                                                <p>
                                                                    <asp:Label ID="AuditState" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>商户状态</h3>
                                                                <p>
                                                                    <asp:Label ID="ShopUserState" runat="server" Text="Label"></asp:Label>
                                                                </p>
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
                                        <div class="layui-card-header">登陆信息</div>
                                        <div class="layui-card-body">
                                            <div class="layui-carousel layadmin-carousel layadmin-backlog" lay-anim=""
                                                lay-indicator="inside" lay-arrow="none" style="width: 100%; height: 210px; margin: 0 5px;">
                                                <div carousel-item="">
                                                    <ul class="layui-row layui-col-space10 layui-this">
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body" id="LoginAddress" target="view_window" href="#"/ >
                                                                
                                                                <h3>登陆邮箱</h3>
                                                                <p>
                                                                 <span style="color:blue">  <asp:Label ID="Email" runat="server" Text="Label"></asp:Label></span>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>到期时间</h3>
                                                                <p>
                                                                    <asp:Label ID="DueTime" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>可用时间</h3>
                                                                <p>
                                                                    <asp:Label ID="CanUseTime" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>营业执照信息</h3>
                                                                <p>
                                                                    <asp:Label ID="BusinessLicenseResult" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>
                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>身份证信息</h3>
                                                                <p>
                                                                    <asp:Label ID="IdentityCardResult" runat="server" Text="Label"></asp:Label>
                                                                </p>
                                                            </a>
                                                        </li>

                                                        <li class="layui-col-xs4">
                                                            <a class="layadmin-backlog-body">
                                                                <h3>合同回执</h3>
                                                                <p>
                                                                    <asp:Label ID="ContractResult" runat="server" Text="Label"></asp:Label>
                                                                </p>
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
                                        <div class="layui-card-header">证件信息</div>
                                        <div class="layui-card-body">
                                            <div class="" lay-filter="LAY-index-dataview" id="LAY-index-dataview" style="width: 100%; height: 350px;">

                                                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                                                    <legend>营业执照</legend>
                                                </fieldset>

                                                <div class="layui-upload">
                                                    <button type="button" class="layui-btn" id="BLUpload">上传图片</button>
                                                    <div class="layui-upload-list">
                                                        <img class="layui-upload-img" id="BLDemo">
                                                        <p id="BLdemoText"></p>
                                                    </div>
                                                </div>
                                                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                                                    <legend>合同回执</legend>
                                                </fieldset>

                                                <div class="layui-upload">
                                                    <button type="button" class="layui-btn" id="CRUpload">上传图片</button>
                                                    <div class="layui-upload-list">
                                                        <img class="layui-upload-img" id="CRDemo">
                                                        <p id="CRdemoText"></p>
                                                    </div>
                                                </div>


                                                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                                                    <legend>身份证正背面</legend>
                                                </fieldset>

                                                <div class="layui-upload">
                                                    <button type="button" class="layui-btn" id="IdentityUpload">多图片上传</button>
                                                    <blockquote class="layui-elem-quote layui-quote-nm" style="margin-top: 10px;">
                                                        预览图：
                                                <div class="layui-upload-list" id="IdentityDemo"></div>
                                                    </blockquote>
                                                </div>


                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="layui-col-md4">
                            <div class="layui-card">
                                <div class="layui-card-header">当前版本</div>
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
                                                    <b>
                                                        <asp:Label ID="Version" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>商户号</td>
                                                <td>
                                                    <a href="" target="_blank">
                                                        <asp:Label ID="BusinessId" runat="server" Text="Label"></asp:Label></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>通知</td>
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
                                        <li class="layui-col-xs4">服务器信息  </li>
                                    </ul>
                                </div>
                                <div class="layui-card-body" id="topsales" style="height: 350px;">


                                    <asp:TextBox ID="BusInfo" runat="server" Style="display: none"></asp:TextBox>
                                    <table id="ServerSideInfo"></table>
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
    <script src="LayUI/layui.all.js"></script>
    <script type="text/javascript">
        var app = {
            init: function () {
                var searchParams = layui.form.val("searchForm");
                var busId = document.getElementById("BusInfo").value;
                layui.table.render({
                    id: "ServerSideInfo",
                    height: 550,
                    where: { queryParams: searchParams },
                    elem: '#ServerSideInfo',
                    url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=Select&queryParams[IPPort]=&queryParams[SalesManId]=&queryParams[BusInfo]=" + busId, //数据接口
                    page: true, //开启分页
                    defaultToolbar: [],
                    cols: [[ //表头
                        { field: 'ShopUserName', title: '商户名称', width: 150 },
                        { field: 'ServerIP', title: 'IP地址', width: 140 },
                        { field: 'PingPort', title: '端口', width: 120 },
                        { field: 'BandWidth', title: '带宽', width: 100 },
                        { field: 'EndDate', title: '到期时间', width: 120 }
                    ]]
                });

            }
        };
        layui.use(["table", 'form', 'layedit', 'laydate', 'upload', 'element'], function () {
            var $ = layui.jquery
                , element = layui.element
                , upload = layui.upload; //Tab的切换功能，切换事件监听等，需要依赖element模块
            InitLoginAddress();
            function InitLoginAddress() {
                var id = document.getElementById("BusinessId").innerHTML;
                var hrefRes = "";
                //异步获取服务器域名与端口信息
                $.ajax({
                    type: "Post",
                    dataType: "json",
                    url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                    data: {
                        CheckParam: "GetLoginUrl",
                        Id:id
                    },
                    success: function (data) {
                        if (data.StatusCode == 1) {
                            hrefRes = data.Msg;
                            $("#LoginAddress").attr("href", hrefRes);
                        }
                        else {
                            console.log(data.Error);
                            return;
                        }
                    }
                });
            };
            app.init();
            //进度条触发事件
            //触发事件
            var active = {
                setPercent0: function () {
                    //设置20%进度
                    element.progress('demo', '20%');
                    document.getElementById("percent").innerHTML = "20%&nbsp;（上传所有证件信息可加快过审哦~）";
                },
                setPercent1: function () {
                    //设置40%进度
                    element.progress('demo', '40%');
                    document.getElementById("percent").innerHTML = "40%&nbsp;（上传所有证件信息可加快过审哦~）";
                },
                setPercent2: function () {
                    //设置60%进度
                    element.progress('demo', '60%');
                    document.getElementById("percent").innerHTML = "60%&nbsp;（上传所有证件信息可加快过审哦~）";
                },
                setPercent3: function () {
                    //设置80%进度
                    element.progress('demo', '80%');
                    document.getElementById("percent").innerHTML = "80%&nbsp;（上传所有证件信息可加快过审哦~）";
                },
                setPercent4: function () {
                    //设置100%进度
                    element.progress('demo', '100%');
                    document.getElementById("percent").innerHTML = "100%&nbsp;（您已通过审核）";
                }
            };
            //获取图片地址
            function GetImgurl() {
                document.getElementById("percent").innerHTML = "0%";
                var Email = document.getElementById("Email").innerHTML;
                $.ajax({
                    type: "Get",
                    dataType: "json",
                    url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                    data: {
                        CheckParam: "GetImgUrl",
                        UserName: Email
                    },
                    success: function (data) {
                        if (data.IdCard1Url != null && data.IdCard1Url != "" && data.IdCard1Url != undefined) {
                            active.setPercent0();
                        }
                        if (data.IdCardUrl2 != null && data.IdCardUrl2 != "" && data.IdCardUrl2 != undefined) {
                            active.setPercent1();
                        }
                        if (data.BusinessLicenseUrl != null && data.BusinessLicenseUrl != "" && data.BusinessLicenseUrl != undefined) {
                            active.setPercent2();
                        }
                        if (data.ContractResultUrl != null && data.ContractResultUrl != "" && data.ContractResultUrl != undefined) {
                            active.setPercent3();
                        }
                        var auditState = document.getElementById("AuditState").innerHTML;
                        if (auditState.indexOf("审核通过") != -1) {
                            active.setPercent4();
                        }
                    }
                });
            }
            GetImgurl();


            //上传图片触发事件
            //普通图片上传
            var uploadInst = upload.render({
                elem: '#BLUpload'
              , url: 'https://httpbin.org/post' //改成您自己的上传接口
              , before: function (obj) {
                  //预读本地文件示例，不支持ie8
                  obj.preview(function (index, file, result) {
                      $('#BLDemo').attr('src', result); //图片链接（base64）
                  });
              }
              , done: function (res) {
                  //如果上传失败
                  if (res.code > 0) {
                      return layer.msg('上传失败');
                  }
                  //上传成功
              }
              , error: function () {
                  //演示失败状态，并实现重传
                  var demoText = $('#BLdemoText');
                  demoText.html('<span style="color: #FF5722;">上传失败</span> <a class="layui-btn layui-btn-xs demo-reload">重试</a>');
                  demoText.find('.demo-reload').on('click', function () {
                      uploadInst.upload();
                  });
              }
            });
            var uploadInstcr = upload.render({
                elem: '#CRUpload'
             , url: 'https://httpbin.org/post' //改成您自己的上传接口
             , before: function (obj) {
                 //预读本地文件示例，不支持ie8
                 obj.preview(function (index, file, result) {
                     $('#CRDemo').attr('src', result); //图片链接（base64）
                 });
             }
             , done: function (res) {
                 //如果上传失败
                 if (res.code > 0) {
                     return layer.msg('上传失败');
                 }
                 //上传成功
             }
             , error: function () {
                 //演示失败状态，并实现重传
                 var demoText = $('#CRdemoText');
                 demoText.html('<span style="color: #FF5722;">上传失败</span> <a class="layui-btn layui-btn-xs demo-reload">重试</a>');
                 demoText.find('.demo-reload').on('click', function () {
                     uploadInstcr.upload();
                 });
             }
            });
            //多图片上传
            upload.render({
                elem: '#IdentityUpload'
              , url: 'https://httpbin.org/post' //改成您自己的上传接口
              , multiple: true
              , before: function (obj) {
                  //预读本地文件示例，不支持ie8
                  obj.preview(function (index, file, result) {
                      $('#IdentityDemo').append('<img src="' + result + '" alt="' + file.name + '" class="layui-upload-img">')
                  });
              }
              , done: function (res) {
                  layer.msg("上传成功！");
              }
            });

        });
       
    </script>
    <script src="assets/js/jquery.min.js"></script>
    <script src="assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="assets/js/Register/jquery-ui.min.js"></script>
    <script src="assets/js/xm-select.js"></script>
    <script src="../assets/js/site.js" asp-append-version="true"></script>
    <script src="assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="assets/js/Login/SignOut.js"></script>
</body>
</html>
