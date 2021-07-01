using Service.ServerPath;
using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.ServerSide
{
    public partial class UpdateServcerSide : System.Web.UI.Page
    {
        private readonly ServerPathInfoService _serverPathInfoService = new ServerPathInfoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
                return;
            }

            var Id = Context.Request["id"].ToString();
            if (string.IsNullOrEmpty(Id))
            {
                Console.WriteLine("未获取到服务器ID，请重试！");
                return;
            }
            var res = _serverPathInfoService.GetById(Convert.ToInt64(Id));
            this.Id.Text = Id;
            this.CPUInfo.Text = res.CPUInfomation;
            this.StartDate.Value = res.StartDate;
            this.EndDate.Text = res.EndDate;
            this.IPPort.Text = res.ServerIP;
            this.PassWord.Text = res.PassWord;
            this.PingPort.Text = res.PingPort;
            this.BandWidth.Text = res.BandWidth;
            this.RAMInfo.Text = res.RAMInformation;
            this.DiskInfo.Text = res.DiskInformation;
            this.Remark.Text = res.Remark;

        }
    }
}