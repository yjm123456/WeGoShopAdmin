<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WeGoShopAdmin.ShopManage.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<link type="image/x-icon" href="~/big_logo.png" rel="icon" />网站logo标签--%>
    <title>商户管理系统</title>
    <script src="../assets/js/jquery.min.js"></script>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <fieldset class="layui-elem-field">
            <legend>基本信息</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 210px;">
                        <div id="LoginForm" lay-filter="addForm" class="layui-form">
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">商户名称</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="CompanyName" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="UserId" Visible="true" autocomplete="off" class="layui-input" Style="display: none" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <div class="layui-inline">
                                    <label class="layui-form-label">邮箱</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="Email" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">组织机构</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <select runat="server" id="InstitutionalType" disabled="disabled" asp-for="InstitutionalType" lay-search="true" lay-verify="required" lay-filter="InstitutionalType">
                                                <option value="default">&ndash; 选择机构 &ndash;</option>
                                                <option value="1">企业法人</option>
                                                <option value="2">个体工商户</option>
                                                <option value="3">企业媒体</option>
                                                <option value="4">事业单位媒体</option>
                                                <option value="5">政府</option>
                                                <option value="6">事业单位</option>
                                                <option value="7">非营利组织(慈善基金会、大使馆、国外政府机构)</option>
                                                <option value="8">民办非企业单位</option>
                                                <option value="9">社会团体</option>
                                                <option value="10">其他组织</option>
                                            </select>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">机构代码</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="OrganizingInstitution" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">开户名称</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="AccountName" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">开户行</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="DepositBank" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">银行卡号</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="BankAccount" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">备注</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="Remark" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </fieldset>

        <fieldset class="layui-elem-field">
            <legend>登陆信息</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 110px;">
                        <div id="LoginForm" lay-filter="addForm" class="layui-form">
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">登陆账户</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="UserName" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">子账户</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="UserNameChildren" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">当前版本</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="Version" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label">到期时间</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="DueTime" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <%--  <div class="layui-inline">
                                    <label class="layui-form-label required"></label>
                                    <div class="layui-input-inline">
                                        <input type="button" class="layui-btn" onclick="AddTable()" value="更换版本" />
                                    </div>
                                </div>--%>
                            </div>
                            <%--   <div class="layui-form-item">
                               
                                <div class="layui-inline">
                                    <label class="layui-form-label required"></label>
                                    <div class="layui-input-inline">
                                        <input type="button" class="layui-btn" onclick="AddTable()" value="续费" />
                                    </div>
                                </div>

                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>

        <fieldset class="layui-elem-field">
            <legend>证件信息</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 50px;">
                        <div id="CertificateForm" lay-filter="CertificateForm" class="layui-form">
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">身份证</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="IdentityCard" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label">机构代码</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="OrganizationCode" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </fieldset>

        <fieldset class="layui-elem-field">
            <legend>证件照片</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 80px;">
                        <div id="CertificateForm" lay-filter="CertificateForm" class="layui-form">
                            <div class="layui-form-item">

                                <div class="layui-inline">
                                    <label class="layui-form-label required">身份证照片</label>
                                    <div class="layui-input-inline">

                                        <input type="button" class="layui-btn" onclick="SelectIdentityCard()" value="点击查看" />
                                    </div>
                                </div>

                                <div class="layui-inline">
                                    <label class="layui-form-label required">营业执照</label>
                                    <div class="layui-input-inline">

                                        <input type="button" class="layui-btn" onclick="SelectBusinessLicense()" value="点击查看" />
                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">合同回执</label>
                                    <div class="layui-input-inline">

                                        <input type="button" class="layui-btn" onclick="SelectContractResponseImg()" value="点击查看" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset class="layui-elem-field">
            <legend>其他信息</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 100px;">
                        <div id="OtherForm" lay-filter="OtherForm" class="layui-form">
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">合同编号</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="ContractNo" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">推荐人</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="SalesMan" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">商户号</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="BusinessId" Style="width: 320px;" autocomplete="off" class="layui-input" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required"></label>
                                    <div class="layui-input-inline">
                                        <input type="button" class="layui-btn" onclick="CreateBusinessId()" value="自动生成" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>
        <script type="text/template" id="tmplSelectIdentityCard">
            <div class="layui-card">
                <div class="layui-card-body">
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <label class="layui-form-label">身份证正面</label>
                            <div class="layui-input-inline">
                                <div class="layui-upload-list" id="IdentityDemo"></div>
                            </div>
                        </div>
                        <div class="layui-inline">
                            <label class="layui-form-label">身份证背面</label>
                            <div class="layui-input-inline">
                                <div class="layui-upload-list" id="IdentityDemo2"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </script>

        <script type="text/template" id="tmplSelectBusinessLicense">
            <div class="layui-card">
                <div class="layui-card-body">
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                <div class="layui-upload-list" id="BusinessLicenseImgurl"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </script>

        <script type="text/template" id="tmplSelectContractResponse">
            <div class="layui-card">
                <div class="layui-card-body">
                    <div class="layui-form-item">
                        <div class="layui-inline">
                            <div class="layui-input-inline">
                                <div class="layui-upload-list" id="ContractResponseImgurl"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </script>

        <script src="../assets/js/jquery.min.js"></script>
        <script src="../LayUI/layui.all.js"></script>
        <script type="text/javascript">
            function SelectIdentityCard() {
                layer.open({
                    title: "查看身份证信息",
                    type: 1,
                    area: ['590px', '708px'],
                    content: layui.laytpl($("#tmplSelectIdentityCard").html()).render({ "Id": 1 }),
                    btn: ["关闭"],
                    success: function (layero, index) {
                        //$.ajax({
                        //    type: "POST",
                        //    dataType: "json",
                        //    url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                        //    data: {
                        //        CheckParam: "GetImgUrl",
                        //        Id: userId
                        //    },
                        //    success: function (data) {
                        //        if (data.StatusCode == 1) {
                        //            var BusinessInfo = data.data;
                        //            var IdentityCard1Url = BusinessInfo.imgurl1;
                        //        }
                        //        else {
                        //            layer.msg("操作失败！" + data.Error);
                        //        }
                        //    }
                        //});
                        $('#IdentityDemo').append('<img src="../CompanyInfos/Example/IdentityCard1.png" alt="IdentityCard1" class="layui-upload-img">');
                        $('#IdentityDemo2').append('<img src="../CompanyInfos/Example/IdentityCard2.png" alt="IdentityCard2" class="layui-upload-img">');
                        //$('#IdentityDemo').append('<img src="' + result + '" alt="' + file.name + '" class="layui-upload-img">');
                    }
                });
            }
            function SelectBusinessLicense() {
                layer.open({
                    title: "查看营业执照信息",
                    type: 1,
                    area: ['490px', '618px'],
                    content: layui.laytpl($("#tmplSelectBusinessLicense").html()).render({ "Id": 1 }),
                    btn: ["关闭"],
                    success: function (layero, index) {
                        //$.ajax({
                        //    type: "POST",
                        //    dataType: "json",
                        //    url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                        //    data: {
                        //        CheckParam: "GetImgUrl",
                        //        Id: userId
                        //    },
                        //    success: function (data) {
                        //        if (data.StatusCode == 1) {
                        //            var BusinessInfo = data.data;
                        //            var IdentityCard1Url = BusinessInfo.imgurl1;
                        //        }
                        //        else {
                        //            layer.msg("操作失败！" + data.Error);
                        //        }
                        //    }
                        //});
                        $('#BusinessLicenseImgurl').append('<img src="../CompanyInfos/Example/BusinessLicense.png" alt="BusinessLicense" class="layui-upload-img">');
                        //$('#IdentityDemo').append('<img src="' + result + '" alt="' + file.name + '" class="layui-upload-img">');
                    }
                });
            }
            function SelectContractResponseImg() {
                layer.open({
                    title: "查看合同回执信息",
                    type: 1,
                    area: ['335px', '538px'],
                    content: layui.laytpl($("#tmplSelectContractResponse").html()).render({ "Id": 1 }),
                    btn: ["关闭"],
                    success: function (layero, index) {
                        //$.ajax({
                        //    type: "POST",
                        //    dataType: "json",
                        //    url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                        //    data: {
                        //        CheckParam: "GetImgUrl",
                        //        Id: userId
                        //    },
                        //    success: function (data) {
                        //        if (data.StatusCode == 1) {
                        //            var BusinessInfo = data.data;
                        //            var IdentityCard1Url = BusinessInfo.imgurl1;
                        //        }
                        //        else {
                        //            layer.msg("操作失败！" + data.Error);
                        //        }
                        //    }
                        //});
                        $('#ContractResponseImgurl').append('<img src="../CompanyInfos/Example/ContractResult.png" alt="BusinessLicense" class="layui-upload-img">');
                        //$('#IdentityDemo').append('<img src="' + result + '" alt="' + file.name + '" class="layui-upload-img">');
                    }
                });
            }


            function CreateBusinessId() {
                var userId = document.getElementById("UserId").value;
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "../ApiControl/ShopManageApi/ShopManageInfo.ashx",
                    data: {
                        CheckParam: "CreateBusinessId",
                        Id: userId
                    },
                    success: function (data) {
                        if (data.StatusCode == 1) {
                            document.getElementById("BusinessId").value = data.Msg;
                            layer.msg("生成商户号成功，请进行商户号配置！");
                        }
                        else {
                            layer.msg("操作失败！" + data.Error);
                        }
                    }
                });
            }
        </script>
    </form>
</body>
</html>
