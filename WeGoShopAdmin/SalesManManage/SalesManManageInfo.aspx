<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesManManageInfo.aspx.cs" Inherits="WeGoShopAdmin.SalesManManage.SalesManManageInfo" %>

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

         .f1 {
             width: 195px;
             float: left;
         }

         .BalanceCss {
             font-size: 24px;
             font-weight: bold;
             text-align: center;
             color: orange;
         }

         .title {
             font-size: 10px;
             height: 28px;
             font-weight: bold;
             text-align: center;
             color: black;
         }

         .buttonParam {
             font-size: 10px;
             height: 28px;
             margin-top: 5px;
             font-weight: bold;
             text-align: center;
             color: black;
         }

         label.required::after {
             content: '* ';
             color: #FF5722;
             margin-right: -9px;
         }

         .balanceBack {
             width: 99%;
             background-color: #F2F2F2;
             height: 110PX;
             border: 2px #E6E6E6 solid;
         }

         .dibu {
             width: 40%;
             height: 10PX;
             font-size: 5px;
             font-weight: bold;
             text-align: center;
             color: red;
         }
     </style>
</head>
<script src="../LayUI/layui.all.js"></script>
<script src="../assets/js/jquery.min.js"></script>
<script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
<script src="../assets/js/Register/jquery-ui.min.js"></script>
<script src="../assets/js/xm-select.js"></script>
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
                            <a href="javascript:;">商户模块</a>
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
                    <legend>合伙人信息</legend>
                </fieldset>
                <div class="layui-card">
                    <div class="layui-card-body">
                         <div class="layui-form-item">
                             <div class="layui-inline">
                                 <label class="layui-form-label">合伙人名称</label>
                             </div>
                             <div class="layui-inline atjubo-date">
                                 <input type="text" name="SaleManName" id="StartDate" lay-verify="SaleManName" placeholder="请输入合伙人名称" autocomplete="off" class="layui-input">
                             </div>
                             <div class="layui-inline">
                                 <button id="search" type="button" class="layui-btn" onclick="app.loadData()">查询</button>
                                 <button id="Insert" type="button" class="layui-btn" onclick="app.Insert()">新增</button>
                             </div>
                         </div>
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
    </form>
    
<script type="text/template" id="tmplUpdate">
    <div class="layui-card">
        <div class="layui-card-body">

            <form id="updateForm" lay-filter="updateForm" class="layui-form" style="height:200px;">
                <input type="hidden" name="Id" value="{{ d.Id }}" />

                <div class="layui-form-item">
                    <div class="layui-inline">
                        <label class="layui-form-label ">账户余额</label>
                        <div class="layui-input-inline">
                            <label class="layui-form-label ">{{ d.Balance }}</label>
                             <input type="hidden" name="InitBalance" value="{{ d.Balance }}" />
                        </div>
                    </div>
                </div>
                <div class="layui-form-item">
                    <div class="layui-inline">
                        <label class="layui-form-label required">调整金额</label>
                        <div class="layui-input-inline">
                            <input type="text" class="layui-input" lay-verify="required|number" placeholder="正数加余额，负数减余额" name="NewBalance" autocomplete="off" maxlength="6" />
                        </div>
                    </div>
                </div>
                 <div class="layui-form-item">
                    <div class="layui-inline">
                        <label class="layui-form-label">调整备注</label>
                        <div class="layui-input-inline">
                            <input type="text" class="layui-input" placeholder="余额调整备注说明" name="Remark" autocomplete="off" maxlength="6" />
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</script>
     <script type="text/html" id="switchTpl">
  <!-- 这里的 checked 的状态只是演示 -->
  <input type="checkbox" name="UserState" value="{{d.Id}}" lay-skin="switch" lay-text="启用|禁用" lay-filter="UserState" {{ d.State == 1 ? 'checked' : '' }}/>
</script>
    <script type="text/html" id="operation">
        <a class="layui-btn layui-btn-sm layui-btn-warm" lay-event="Details" onclick="app.Update('{{ d.Id }}','{{ d.State }}')">修改</a>
        <a class="layui-btn layui-btn-sm layui-btn-warm" lay-event="Details" onclick="app.UpdateBalance('{{ d.Id }}','{{ d.Balance }}','{{ d.State }}')">调整余额</a>
    </script>
    <script src="../LayUI/layui.js"></script>
    <script src="../assets/js/site.js" asp-append-version="true"></script>
    <script>
        var app = {
            init: function () {
                var searchParams = layui.form.val("searchForm");
                layui.table.render({
                    id: "SalesManInfoTable",
                    height: 600,
                    where: { queryParams: searchParams },
                    elem: '#SalesManInfoTable',
                    url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx?CheckParam=Select", //数据接口
                    page: true, //开启分页
                    defaultToolbar: [],
                    cols: [[ //表头
                        { field: 'SaleManName', title: '合伙人姓名', width: 160 },
                        { field: 'LoginName', title: '登陆名', minWidth: 150 },
                        { field: 'Phone', title: '联系方式', width: 140 },
                        { field: 'Address', title: '联系地址', minWidth: 200 },
                        { field: 'Email', title: '邮箱', minWidth: 160 },
                        { field: 'Level', title: '等级', minWidth: 70 },
                        { field: 'Balance', title: '账户余额', minWidth: 80 },
                        { field: 'DistributionRate', title: '参与分成比例（%）', minWidth: 70 },
                        { field: 'State', title: '合伙人状态', width: 120, templet: '#switchTpl', unresize: true },
                        { fixed: 'right', title: '操作', width: 160, align: 'left', toolbar: '#operation' }
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
                        url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx",
                        data: {
                            CheckParam: "UpdateSalesManState",
                            Id: ShopId,
                            UserState: State
                        },
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                if (userState == true) {
                                    layer.tips("该合伙人已启用", obj.othis);
                                    app.loadData();
                                }
                                else {
                                    layer.tips("该合伙人已禁用", obj.othis);
                                    app.loadData();
                                }
                            }
                            else {
                                layer.tips("操作失败！" + data.Error, obj.othis); app.loadData();
                            }
                        }
                    });
                });
            },
            Insert: function (Id) {
                layer.open({
                    type: 2, area: ['790px', '685px'], title: '新增合伙人',
                    content: "InsertSalesMan.aspx",
                    btn: ["确定", "关闭"],
                    yes: function (index, layero) {
                        var _form = layer.getChildFrame('form', index);
                        var InsertData = JSON.stringify(_form.serializeArray());
                        if (!_form.validForm()) {
                            return false;
                        };
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx?CheckParam=InsertSaleManInfo",
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
            Update: function (Id, State) {
                if (State != 1)
                {
                    layer.msg("账户未启用，无法修改！");
                    return;
                }
                layer.open({
                    type: 2, area: ['790px', '685px'], title: '修改合伙人信息',
                    content: "UpdateSalesMan.aspx?Id=" + Id,
                    btn: ["确定", "关闭"],
                    yes: function (index, layero) {
                        var _form = layer.getChildFrame('form', index);
                        var UpdateData = JSON.stringify(_form.serializeArray());
                        if (!_form.validForm()) {
                            return false;
                        }
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx?CheckParam=UpdateSaleManInfo",
                            data: UpdateData,
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
            UpdateBalance: function (Id, Balance,State) {
                if (State != 1) {
                    layer.msg("账户未启用，无法调整！");
                    return;
                }
                layer.open({
                    title: "调整余额",
                    type: 1,
                    content: layui.laytpl($("#tmplUpdate").html()).render({ "Id": Id, "Balance": Balance }),
                    btn: ["保存", "关闭"],
                    yes: function (index, layero) {
                        if ($("#updateForm").validForm() == false) {
                            return false;
                        }
                        var updateData = layui.form.val("updateForm");
                        if (updateData.NewBalance == "") {
                            layer.msg("请输入调整金额");
                            return false;
                        }
                        updateData.CheckParam= "UpdateSalesManBalance",
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/SalesManInfoApi/SalesManInfo.ashx",
                            data: updateData,
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
                        layui.form.render("select", "updateForm");
                        layui.form.val("updateForm");
                    }
                });
            },
            loadData: function () {
                var searchParams = layui.form.val("searchForm");
                layui.table.reload("SalesManInfoTable", {
                    page: { curr: 1 },
                    where: { queryParams: searchParams }
                });
            }
        };
        layui.use(["table", 'form', 'layedit', 'laydate', 'element'], function () {
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

            ////日期
            //laydate.render({
            //    elem: '#StartDate',
            //    value: year + "-" + month + "-1"
            //});
            //laydate.render({
            //    elem: '#EndDate',
            //    value: year + "-" + month + "-" + endDateparam
            //});
            app.init();

        });

    </script>
    <script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="../assets/js/Login/SignOut.js"></script>
</body>
</html>
