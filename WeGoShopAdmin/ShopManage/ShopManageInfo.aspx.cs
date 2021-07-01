using Service.SalesMan;
using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.ShopManage
{
    public partial class ShopManageInfo : System.Web.UI.Page
    {
        private readonly ShopManageService _shopManageService = new ShopManageService();
        private readonly SalesManInfoService _salesManInfoService = new SalesManInfoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");

            }
            if (Session["adminRole"].ToString() == "Shop")
            {
                this.BusInfo.Text = _shopManageService.GetByName(Session["adminUser"].ToString()).BusinessId.ToString();
                var Id = _shopManageService.GetByName(Session["adminUser"].ToString()).UserId;
                var result = _shopManageService.GetById(Id);
                this.UserName.Text = "欢迎您," + result.LiasonManName.ToString();
                this.UserID.Text = Id.ToString();
                this.UserRole.Text = Session["adminRole"].ToString();
            }
            if (Session["adminRole"].ToString() == "admin")
            {
                try
                {
                    this.Version.SelectedIndex = Convert.ToInt32(HttpContext.Current.Request["Version"].ToString());
                }
                catch (Exception err)
                {
                    this.Version.SelectedIndex = 0;
                }
                this.UserName.Text = "欢迎您,admin";
                this.BusInfo.Text = "-1";
                var Id = Session["adminId"].ToString();
                this.UserID.Text = Id.ToString();
                this.UserRole.Text = Session["adminRole"].ToString();
            }
            if (Session["adminRole"].ToString() == "SalesMan")
            {
                var Id = Session["adminId"].ToString();
                var result = _salesManInfoService.GetById(Convert.ToInt64(Id));
                this.UserName.Text = "欢迎您," + result.SaleManName.ToString();
                this.UserID.Text = Id.ToString();
                this.UserRole.Text = Session["adminRole"].ToString();
                //this.BusInfo.Text = "-1";
                try
                {
                    this.Version.SelectedIndex =Convert.ToInt32(HttpContext.Current.Request["Version"].ToString());
                }
                catch (Exception err)
                {
                    this.Version.SelectedIndex = 0;
                }
            }
        }
    }
}