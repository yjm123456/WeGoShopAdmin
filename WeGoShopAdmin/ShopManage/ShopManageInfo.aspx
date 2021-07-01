<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopManageInfo.aspx.cs" Inherits="WeGoShopAdmin.ShopManage.ShopManageInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>后台商城管理系统</title>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
</head>
<body class="layui-layout-body">
    <style type="text/css">
        .layui-table-cell {
            　　 width: auto;
        }
    </style>
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
                            <dd><a href="#">商户信息</a></dd>
                            
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
                <legend>商户信息</legend>
            </fieldset>
            <div class="layui-card">
                <div class="layui-card-body">
                         <div class="layui-form-item">
                            <%--<div class="layui-inline">
                                <label class="layui-form-label">创建日期</label>
                            </div>
                            <div class="layui-inline ">
                                <input type="text" name="StartDate" id="StartDate" lay-verify="StartDate"  placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input"/>
                            </div>
                            <div class="layui-inline">-</div>
                            <div class="layui-inline ">
                                <input type="text" name="EndDate" id="EndDate" lay-verify="EndDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input"/>
                            </div>--%>
                             <div class="layui-inline">
                                <label class="layui-form-label">公司名称</label>
                            </div>
                            <div class="layui-inline ">
                                <input type="text" name="CompanyName" id="CompanyName"   autocomplete="off" class="layui-input"/>
                            </div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <div class="layui-inline">
                              <label class="layui-form-label">登陆名</label>
                            </div>
                            <div class="layui-inline ">
                                <input type="text" name="LoginUserName" id="LoginUserName"  autocomplete="off" class="layui-input"/>
                            </div>
                               <div  class="layui-inline">
                                <label class="layui-form-label">商户版本</label>
                            </div>
                            <div class="layui-inline ">
                               <select id="Version" name="Version" asp-for="Version" lay-search="true" runat="server" lay-filter="Version">
                                  
                                </select>
                            </div>
                            <div id="IsHidden1" class="layui-inline"> <label class="layui-form-label">推荐人</label></div>
                            <div id="IsHidden2" class="layui-inline ">
                                <select id="SalesManId" name="SalesManId" asp-for="SalesManId" lay-search="true" lay-filter="SalesManId">
                                </select>
                             </div>
                             <asp:TextBox ID="BusInfo" name="BusInfo" runat="server" style="display:none"></asp:TextBox>
                            
                        </div>
                      <div class="layui-form-item">
                               <div  class="layui-inline">
                                <label class="layui-form-label">商户状态</label>
                            </div>
                            <div class="layui-inline ">
                               <select id="ShopUserState" name="ShopUserState" asp-for="ShopUserState" lay-search="true" runat="server" lay-filter="Version">
                                  <option value="-1">&ndash; 全部 &ndash; </option>
                                  <option value="0">未启动 </option>
                                   <option value="1">正常</option>
                                   <option value="999">已禁用</option>
                                </select>
                            </div>
                            <div " class="layui-inline"> <label class="layui-form-label">审核状态</label></div>
                            <div  class="layui-inline ">
                                <select id="AuditState" name="AuditState" asp-for="AuditState" lay-search="true" lay-filter="AuditState">
                                     <option value="-1">&ndash; 全部 &ndash; </option>
                                  <option value="0">未审核 </option>
                                   <option value="1">审核通过</option>
                                   <option value="2">审核不通过</option>
                                </select>
                             </div>
                            
                            <div class="layui-inline">
                                <button id="search" type="button" class="layui-btn" onclick="app.loadData()">查询</button>
                            </div>
                        </div>
                </div>
            </div>
            <div class="layui-card">
                <div class="layui-card-body layui-card-table-body">
                    <table id="ShopManageInfo"></table>
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
    <script type="text/html" id="switchTpl">
  <!-- 这里的 checked 的状态只是演示 -->
  <input type="checkbox" name="UserState" value="{{d.Id}}" lay-skin="switch" lay-text="启用|禁用" lay-filter="UserState" {{ d.ShopUserState == 1 ? 'checked' : '' }}/>
</script>
<script type="text/html" id="operation">
     {{# if(d.VerifyState > 0){ }}
    <a class="layui-btn layui-btn-sm " lay-event="Details" onclick="app.Details('{{ d.Id }}')">查看详情</a>
    <a class="layui-btn layui-btn-sm layui-btn-normal" lay-event="ResetPwd" onclick="app.ResetPwd('{{ d.Id }}')">重置密码</a>
    {{# } }}
    {{# if(d.VerifyState == 0){ }}
    <a class="layui-btn layui-btn-sm " lay-event="Details" onclick="app.Details('{{ d.Id }}')">查看详情</a>
    <a class="layui-btn layui-btn-sm layui-btn-normal" lay-event="ResetPwd" onclick="app.ResetPwd('{{ d.Id }}')">重置密码</a>
    <a class="layui-btn layui-btn-sm layui-btn-danger" lay-event="Check" onclick="app.Check('{{ d.Id }}')">审核</a>
    {{# } }}
</script>
    <script>
        var app = {
            init: function () {


                var searchParams = layui.form.val("searchForm");
                layui.table.render({
                    id: "ShopManageInfo",
                    height: 600,
                    where: { queryParams: searchParams },
                    elem: '#ShopManageInfo',
                    url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx?CheckParam=Select", //数据接口
                    page: true, //开启分页
                    defaultToolbar: [],
                    cols: [[ //表头
                        { field: 'CompanyName', title: '公司名称', width: 180 },
                        { field: 'UserName', title: '登陆名', width: 130 },
                        { field: 'LiasonManName', title: '联系人', width: 120 },
                        { field: 'Phone', title: '联系电话', width: 140 },
                        { field: 'SaleManName', title: '推荐人', width: 120 },
                        { field: 'Version', title: '当前版本', width: 100 },
                        { field: 'ApplicationRange', title: '使用范围', width: 140 },
                        { field: 'VerifyStateDescription', title: '审核状态', width: 120 },
                        { field: 'ShopUserState', title: '商户状态', width: 120, templet: '#switchTpl', unresize: true },
                        { field: 'CreateTime', title: '创建时间', width: 150 },
                        { field: 'HasUsedTime', title: '可用时间', width: 120 },
                        { field: 'Remark', title: '备注', width: 200 },


                        //{ field: 'WechatQCPlayTypeAudit', title: '是否开启年审', width: 120 },
                        //{ field: 'WechatQCPlayType', title: '微信商户号', width: 120 },
                        //{ field: 'WechatQCPlayType', title: '秘钥', width: 120 },
                        //{ field: 'WechatQCPlayType', title: '支付宝商户号', width: 120 },
                        //{ field: 'WechatQCPlayType', title: '秘钥', width: 120 },
                        //{ field: 'fixPhone', title: '固定电话', width: 120 },
                        //{ field: 'email', title: '邮箱', width: 120 },
                        //{ field: 'identityCard', title: '身份证号', width: 120 },
                        //{ field: 'institutionalType', title: '组织机构', width: 120 },
                        //{ field: 'organizingInstitution', title: '组织机构代码', minWidth: 130 },
                        //{ field: 'accountName', title: '开户名称', width: 120 },
                        //{ field: 'depositBank', title: '开户行', width: 120 },
                        //{ field: 'bankAccount', title: '银行卡号', width: 120 },
                        { fixed: 'right', title: '操作', width: 240, align: 'left', toolbar: '#operation' }
                    ]]
                });
                //监听商户状态操作
                layui.form.on('switch(UserState)', function (obj) {
                    var userState = obj.elem.checked;
                    var ShopId = this.value;
                    var State = 1;
                    if (userState == false) {
                        State = 1000;
                    }
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                        data: {
                            CheckParam: "UpdateShopUserState",
                            Id: ShopId,
                            UserState: State
                        },
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                if (userState == true) {
                                    layer.tips("该商户已启用", obj.othis);
                                }
                                else {
                                    layer.tips("该商户已禁用", obj.othis);
                                }
                            }
                            else {
                                layer.tips("操作失败！" + data.Error, obj.othis); app.loadData();
                            }
                        }
                    });
                });
            },
            Details: function (id) {
                layer.open({
                    type: 2, area: ['790px', '893px'], title: '查看详细信息',
                    content: "details.aspx?id=" + id,
                    btn: ["关闭"],
                    yes: function (index, layero) {
                        layer.close(index);
                    },
                    success: function (layero, index) {
                    }
                });
            },
            ResetPwd: function (id) {
                layer.confirm('重置后密码为初始密码"123456"，确定重置该账户密码吗？', { btn: ['是', '否'], title: "提示" }, function () {

                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                        data: {
                            Id: id,
                            CheckParam: "ResetPwd"
                        },
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                layer.msg("重置成功！");
                            }
                            else {
                                layer.msg("操作失败，" + layer.Error);
                            }
                        }
                    });
                })
            },
            Check: function (id) {
                layer.open({
                    type: 2, area: ['790px', '893px'], title: '审核详细信息',
                    content: "audit.aspx?id=" + id,
                    btn: ["审核通过", "审核不通过", "关闭"],
                    yes: function (index, layero) {
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                            data: {
                                CheckParam: "UpdateVerifyState",
                                Id: id,
                                VerifyState: 1
                            },
                            success: function (data) {
                                if (data.StatusCode == 1) {

                                    layer.close(index);
                                    layer.msg("审核状态已通过！");
                                    app.loadData();

                                }
                                else {
                                    layer.msg("操作失败！" + data.Error);
                                }
                            }
                        });
                    },
                    btn2: function (index, layero) {
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                            data: {
                                CheckParam: "UpdateVerifyState",
                                Id: id,
                                VerifyState: 2
                            },
                            success: function (data) {
                                if (data.StatusCode == 1) {
                                    layer.msg("审核状态改为失败！");
                                    app.loadData();
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
            },
            loadData: function () {
                var searchParams = layui.form.val("searchForm");
                layui.table.reload("ShopManageInfo", {
                    page: { curr: 1 },
                    where: { queryParams: searchParams }
                });

            }

        };
        layui.use(["table", 'form', 'layedit', 'laydate', 'element'], function () {
            var $ = layui.jquery, form = layui.form;
            //加载版本选择下拉框
            var versionHtmls = '<option value="-1">全部</option>';
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                data: {
                    CheckParam: "SelectVersion"
                },
                success: function (data) {
                    for (var x in data) {
                        versionHtmls += '<option value="' + x + '">' + data[x] + '</option>'
                    }
                    $('#Version').append(versionHtmls);
                    // 添加完记得render下  否则不会刷新到页面中
                    form.render('select');
                }
            });

            var busId = document.getElementById("UserRole").innerHTML;
            if (busId == "admin") {
                //加载合伙人下拉框
                var htmls = '<option value="0">全部</option><option value="1">无合伙人</option>';
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
            }
            else if (busId!="admin") {
                document.getElementById("IsHidden1").remove();
                document.getElementById("IsHidden2").remove();
                document.getElementById("Version").selectedIndex = "4";
            }
            //var year = new Date().getFullYear();
            //var month = new Date().getMonth() + 1;
            //var endDateparam = new Date().getDate();
            //if (month < 10) {
            //    month = "0" + month;
            //}
            //if (endDateparam < 10) {
            //    endDateparam = "0" + endDateparam;
            //}
            var form = layui.form
                , layer = layui.layer
                , layedit = layui.layedit
                , laydate = layui.laydate;

            //日期
            laydate.render({
                elem: '#StartDate',
                //value: year + "-" + month + "-01"
            });
            laydate.render({
                elem: '#EndDate',
                // value: year + "-" + month + "-" + endDateparam
            });
            app.init();
        });
    </script>
    
    <script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="../assets/js/Login/SignOut.js"></script>
</body>
</html>
