using Service.Finance;
using Service.SalesMan;
using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.Balance
{
    public partial class SaleManDeposit : System.Web.UI.Page
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
                Context.Response.Redirect("~/NoPermission.aspx");
            }
            if (Session["adminRole"].ToString() == "SalesMan")
            {
                var Id = Session["adminId"].ToString();
                this.UserID.Text = Id.ToString();
                this.UserRole.Text = Session["adminRole"].ToString();
                var result = _salesManInfoService.GetById(Convert.ToInt64(Id));
                this.UserName.Text = "欢迎您," + result.SaleManName.ToString();
                this.DepositState.Text = "1";
                this.SaleManId.Text = Id.ToString();
            }
            if (Session["adminRole"].ToString() == "admin")
            {
                var Id = Session["adminId"].ToString();
                this.UserID.Text = Id.ToString();
                this.UserRole.Text = Session["adminRole"].ToString();
                this.DepositState.Text = "0";
                this.UserName.Text = "欢迎您,admin";
            }
        }
    }
}