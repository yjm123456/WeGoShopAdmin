<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertServcerSide.aspx.cs" Inherits="WeGoShopAdmin.ServerSide.InsertServcerSide" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商户管理系统</title>
    <link href="../LayUI/css/layui.css" rel="stylesheet" />
    <link href="../LayUI/css/saas.main.css" rel="stylesheet" />
</head>
<body>
    <form id="addForm" runat="server">
        <fieldset class="layui-elem-field">
            <legend>基本信息新增</legend>
            <div class="layui-field-box">

                <div class="layui-card" style="border-radius: 0; margin-bottom: 0px;">
                    <div class="layui-card-body" style="height: 330px;">
                        <div id="LoginForm" lay-filter="addForm" class="layui-form">
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">CPU信息</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="CPUInfo" autocomplete="off" class="layui-input"  placeholder="请输入正确的CPU信息：例：Intel(R) Xeon(R) Platinum 8269CY CPU @ 2.50GHz 2.50GHz" lay-verify="required" style="width:514px;"  runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                               
                            </div>
                               <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">购买时间</label>
                                    <div class="layui-input-inline">
                                          <input type="text" name="StartDate" id="StartDate"   lay-verify="required"  placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input"/>
                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label required">到期时间</label>
                                    <div class="layui-input-inline">
                                            <asp:TextBox ID="EndDate" autocomplete="off" class="layui-input"   lay-verify="required"  placeholder="yyyy-MM-dd" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                          
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">IP地址</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="IPPort" autocomplete="off" class="layui-input" PLACEHOLDER="例：11.222.333.444" lay-verify="required"  runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                               <div class="layui-inline">
                                    <label class="layui-form-label required">登陆密码</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="PassWord" type="password" autocomplete="off" class="layui-input" lay-verify="required"   runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label required">端口</label>
                                    <div class="layui-input-inline">
                                            <asp:TextBox ID="PingPort" autocomplete="off" class="layui-input"  lay-verify="required" placeholder="多个端口请用英文逗号隔开" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="layui-inline">
                                    <label class="layui-form-label">带宽</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="BandWidth" autocomplete="off" class="layui-input"  runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div> 

                            </div>
                           <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label ">运行内存</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                           <asp:TextBox ID="RAMInfo" autocomplete="off" class="layui-input" PLACEHOLDER="请标注内存与通道数"  runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="layui-inline">
                                    <label class="layui-form-label ">磁盘信息</label>
                                    <div class="layui-input-inline">
                                        <asp:TextBox ID="DiskInfo" autocomplete="off" class="layui-input" PLACEHOLDER="例：固态/机械 512G" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                             <div class="layui-form-item">
                                <div class="layui-inline">
                                    <label class="layui-form-label">备注</label>
                                    <div class="layui-input-inline">
                                        <div class="layui-input-inline">
                                            <asp:TextBox ID="Remark" autocomplete="off" class="layui-input" style="width:514px;"  runat="server"></asp:TextBox>
                                              <asp:TextBox ID="BusinessId" autocomplete="off" class="layui-input"  runat="server" style="display:none"></asp:TextBox>
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
           layui.use(['form', 'layedit', 'laydate'], function(){
               var laydate = layui.laydate;
               var dateEntryStart = laydate.render({
                   elem: '#StartDate', format: 'yyyy-MM-dd',
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
           });
       </script>
    </form>
</body>
</html>