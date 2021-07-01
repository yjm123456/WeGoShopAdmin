<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BalanceManage.aspx.cs" Inherits="WeGoShopAdmin.Balance.BalanceManage" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
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
                    <legend>我的账户</legend>
                </fieldset>
                <div class="layui-card-body">
                    <div style="width: 100%; height: 110px; margin: 0 5px;">
                        <div class="balanceBack">
                            <div class="f1">
                                <ul>
                                    <li class="title">账户余额(元)</li>
                                    <li class="BalanceCss">
                                        <asp:Label ID="Balance" runat="server" Text="Label"></asp:Label></li>
                                    <li class="buttonParam">
                                        <button type="button" class="layui-btn layui-btn-primary layui-btn-sm" onclick="Deposit()">提现</button>
                                        <li>
                                </ul>
                            </div>
                            <div class="f1">
                                <ul>
                                    <li class="title">本月结算金额(元)</li>
                                    <li class="BalanceCss">
                                        <asp:Label ID="SettleAmount" runat="server" Text="Label"></asp:Label></li>
                                    <li class="buttonParam"></li>
                                </ul>
                            </div>
                            <div class="f1">
                                <ul>
                                    <li class="title">参与分成比例</li>
                                    <li class="BalanceCss">
                                        <asp:Label ID="DistributionRate" runat="server" Text="Label"></asp:Label>%
                                    </li>
                                    <li class="buttonParam"></li>
                                </ul>
                            </div>

                        </div>
                        <div class="dibu">
                            (注：每月一号系统结算，结算规则为：[合伙人名下所有商户数量*版本价格*合伙人参与分成比例]结算金额加入账户余额中)
                        </div>
                    </div>
                </div>

                <div class="layui-card">
                    <div class="layui-card-body">
                        <fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
                            <legend>资金明细</legend>
                        </fieldset>

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

                            <asp:TextBox ID="BusInfo" name="BusInfo" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="SalesManId" name="SalesManId" runat="server" Style="display: none"></asp:TextBox>
                            <div class="layui-inline">
                                <button id="search" type="button" class="layui-btn" onclick="app.loadData()">查询</button>
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

    <script type="text/template" id="tmplUpdate">
        <div class="layui-card">
            <div class="layui-card-body">

                <form id="updateForm" lay-filter="updateForm" class="layui-form" style="height: 300px;">
                    <input type="hidden" Id="Id" name="Id" value="{{ d.Id }}" />

                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <label class="layui-form-label ">账户余额</label>
                            <div class="layui-input-inline">
                                <label class="layui-form-label ">{{ d.Balance }}</label>
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <label class="layui-form-label required">提现金额</label>
                            <div class="layui-input-inline">
                                <input type="text" class="layui-input" lay-verify="required|positivedecimal" placeholder="请输入提现金额" name="NewBalance" autocomplete="off" maxlength="6" />
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <label class="layui-form-label required">提现渠道</label>
                            <div class="layui-input-inline">
                                <select id="DepositWay" asp-for="DepositWay" lay-verify="required" lay-search="true" lay-filter="DepositWay">
                                    <option value="0">请选择 </option>
                                    <option value="1">支付宝</option>
                                    <option value="2">微信</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <label class="layui-form-label required">提现账号</label>
                            <div class="layui-input-inline">
                                <input type="text" class="layui-input" lay-verify="required" placeholder="支付宝/微信账号" name="DepositAccount" autocomplete="off" maxlength="30" />
                            </div>

                        </div>
                    </div>
                     <div class="layui-form-item">
                        <div class="layui-inline">
                            <label class="layui-form-label required">收款人</label>
                            <div class="layui-input-inline">
                                <input type="text" class="layui-input" lay-verify="required" placeholder="请输入收款人姓名" name="PayeeName" autocomplete="off" maxlength="30" />
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </script>
    
    <script src="../LayUI/layui.js"></script>
    <script src="../assets/js/site.js" asp-append-version="true"></script>
    <script>
        var app = {
            init: function () {
                var searchParams = layui.form.val("searchForm");
                layui.table.render({
                    id: "ServerSideInfo",
                    height: 420,
                    where: { queryParams: searchParams },
                    elem: '#ServerSideInfo',
                    url: "../ApiControl/Balance/BalanceInfo.ashx?CheckParam=SelectSaleManBalanceDetails", //数据接口
                    page: true, //开启分页
                    defaultToolbar: [],
                    cols: [[ //表头
                        { field: 'SaleManName', title: '合伙人', width: 150 },
                        { field: 'ReceiptNo', title: '单据号', width: 180 },
                        { field: 'CreateTime', title: '操作日期', width: 130 },
                        { field: 'InitBalance', title: '期初金额', width: 170 },
                        { field: 'thisOperateBalance', title: '操作金额', width: 170 },//正数红色，负数绿色
                        { field: 'OperationCardNo', title: '操作账户', width: 130 },
                        { field: 'LastBalance', title: '期末金额', width: 170 },
                        { field: 'Creator', title: '经办人', width: 140 },//系统结算，超管调整资金
                        { field: 'Remark', title: '备注', width: 280 },//系统结算，超管调整资金备注
                    ]],
                    done: function (res, curr, count) {
                        var that = this.elem.next();
                        res.data.forEach(function (item, index) {
                            if (item.thisOperateBalance < 0) {
                                var tr = that.find(".layui-table-box tbody tr[data-index='" + index + "']");
                                tr.css("color", "#008000");
                            }
                            else if (item.thisOperateBalance > 0) {
                                var tr = that.find(".layui-table-box tbody tr[data-index='" + index + "']");
                                tr.css("color", "red");
                            }
                            else {
                                var tr = that.find(".layui-table-box tbody tr[data-index='" + index + "']");
                                tr.css("color", "gray");
                            }
                        });
                    }
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

        };
        layui.use(["table", 'form', 'layedit', 'laydate', 'element'], function () {
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
        function Deposit() {
            var Balance = document.getElementById("Balance").innerHTML;
            var id = document.getElementById("UserID").innerHTML;
            if (Balance < 10) {
                layer.msg("超过10元才可提现");
            }
            layer.open({
                title: "账户提现申请",
                type: 1,
                content: layui.laytpl($("#tmplUpdate").html()).render({ "Id": id, "Balance": Balance }),
                btn: ["保存", "关闭"],
                yes: function (index, layero) {
                    if ($("#updateForm").validForm() == false) {
                        return false;
                    }
                    var data = layui.form.val("updateForm");
                    if (Number(data.NewBalance) > Number(Balance)) {
                        layer.msg("提现金额不能大于" + Balance + "，请重新输入！");
                        return false;
                    }
                    var depositWay = document.getElementById("DepositWay").value;
                    if (depositWay==0) {
                        layer.msg("请选择提现渠道！");
                        return false;
                    }
                    data.DepositWay = depositWay;
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        url: "../ApiControl/Finance/SaleManDepositInfo.ashx?CheckParam=DepositApplication",
                        data: data,
                        success: function (data) {
                            if (data.StatusCode == 1) {
                                layer.close(index);
                                layer.confirm('提现申请成功，请耐心等待审核。是否查看提现申请记录？', { btn: ['是', '否'], title: "提示" }, function () {
                                    window.location.href = "../Balance/SaleManDeposit.aspx";
                                })
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
        }
    </script>
    <script src="../assets/js/Register/jquery-1.8.2.min.js"></script>
    <script src="../assets/js/Login/SignOut.js"></script>
</body>
</html>
