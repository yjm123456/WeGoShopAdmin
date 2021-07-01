<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertInformation.aspx.cs" Inherits="WeGoShopAdmin.InformationSide.InsertInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户管理系统</title>
    <script src="../assets/js/jquery.min.js"></script>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <link href="../LayUI/css/saas.main.css" rel="stylesheet" />
</head>
<body>
    <form id="addForm" runat="server">
        <fieldset class="layui-elem-field">
            <legend>新增消息通知</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 325px;">
                        <div id="LoginForm" lay-filter="addForm" class="layui-form">

                            <div class="layui-form-item">

                                <div class="layui-inline">
                                    <label class="layui-form-label required">发送目标</label>
                                    <div class="layui-input-inline">
                                        <select id="SendTarget" name="SendTarget" lay-verify="required" asp-for="SendTarget" lay-search="true" lay-filter="SendTarget">
                                            <option value="0">草稿箱</option>
                                            <option value="1">商户</option>
                                            <option value="2">合伙人</option>
                                        </select>
                                    </div>
                                </div>


                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">备注</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                        <textarea style="width:514px;height:270px;" name="Content" autocomplete="off" class="layui-textarea" maxlength="300"></textarea>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <script src="../assets/js/jquery.min.js"></script>
        <script src="../LayUI/layui.all.js"></script>
        <script src="../assets/js/site.js"></script>
        <script src="../assets/js/xm-select.js"></script>
        <script type="text/javascript">
            layui.use(['form', 'layedit', 'laydate'], function () {
                var form = layui.form
                    , layer = layui.layer
                    , layedit = layui.layedit
                    , laydate = layui.laydate;

                //日期
                laydate.render({
                    elem: '#StartDate'
                });
                laydate.render({
                    elem: '#EndDate'
                });
            });
        </script>
    </form>
</body>
</html>
