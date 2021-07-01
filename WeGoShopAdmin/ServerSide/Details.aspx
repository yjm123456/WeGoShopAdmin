<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WeGoShopAdmin.ServerSide.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户管理系统</title>
    <script src="../assets/js/jquery.min.js"></script>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <fieldset class="layui-elem-field">
            <legend>服务器详细信息</legend>
            <div class="layui-field-box">
                <asp:TextBox ID="ServerPathId" Visible="true" autocomplete="off" class="layui-input" Style="display: none" runat="server"></asp:TextBox>
                <div class="layui-card">
                    <div class="layui-card-body layui-card-table-body">
                        <table id="ServerSideInfoDetails"></table>
                    </div>
                </div>
            </div>
        </fieldset>
           <script type="text/html" id="operation">
        <a class="layui-btn layui-btn-sm layui-btn-danger" lay-event="Delete" onclick="app.Delete('{{ d.Id }}')">删除</a>
    </script>
        <script src="../assets/js/jquery.min.js"></script>
        <script src="../LayUI/layui.all.js"></script>
        <script type="text/javascript">
            var app = {

                Delete: function (Id) {
                    layer.confirm('数据删除后不可恢复，您确定删除该条数据吗？', { btn: ['确定', '取消'], title: "提示" }, function () {
                        $.ajax({
                            type: "POST",
                            dataType: "json",
                            url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=DeleteServerSideDetails",
                            data: {
                                Id: Id
                            },
                            success: function (data) {
                                if (data.StatusCode == 1) {
                                    layer.msg("删除成功！");
                                    app.init();
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
                init: function () {
                    var id = document.getElementById("ServerPathId").value;
                    layui.table.render({
                        id: "ServerSideInfoDetails",
                        height: 500,
                        elem: '#ServerSideInfoDetails',
                        url: "../ApiControl/ServerPath/ServerPathInfo.ashx?CheckParam=SelectDetails&ServerPathId=" + id, //数据接口
                        page: true, //开启分页
                        defaultToolbar: [],
                        cols: [[ //表头
                            { field: 'DomainName', title: '域名', width: 150 },
                            { field: 'BindWechatStationName', title: '公众号名称', width: 130 },
                            { field: 'BindWechatStationUserName', title: '公众号账户', width: 130 },
                            { field: 'BindWechatStationPassWord', title: '账户密码', width: 130 },
                            { field: 'BindWechatStationType', title: '公众号类型', width: 100 },
                            { field: 'BindWechatStationAppId', title: 'AppId', width: 190 },
                            { field: 'BindWechatStationAppSecret', title: 'AppSecret', width: 230 },
                            { field: 'Remark', title: '备注', width: 110 },
                            { fixed: 'right', title: '操作', width: 80, align: 'left', toolbar: '#operation' }
                        ]]
                    });

                }
               
            };
            layui.use(["table", 'form', 'layedit', 'laydate'], function () {
              
                app.init();

            });
        </script>
    </form>
</body>
</html>
