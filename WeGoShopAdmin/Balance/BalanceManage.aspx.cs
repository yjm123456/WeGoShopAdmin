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
    public partial class BalanceManage : System.Web.UI.Page
    {

        private readonly ShopManageService _shopManageService = new ShopManageService();
        private readonly SalesManInfoService _salesManInfoService = new SalesManInfoService();
        private readonly SaleManSettleAccountService _saleManSettleAccountService = new SaleManSettleAccountService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
            }
            if (Session["adminRole"].ToString() != "SalesMan")
            {
                Context.Response.Redirect("~/NoPermission.aspx");
            }
            var Id = Session["adminId"].ToString();
            this.UserID.Text = Id.ToString();
            this.UserRole.Text = Session["adminRole"].ToString();
            var result = _salesManInfoService.GetById(Convert.ToInt64(Id));
            this.UserName.Text = "欢迎您," + result.SaleManName.ToString();
            this.SalesManId.Text = Id.ToString();
            var saleManRes = _salesManInfoService.GetById(Convert.ToInt64(Id));
            this.Balance.Text = saleManRes.Balance;
            this.SettleAmount.Text = _saleManSettleAccountService.GetSettleAmountById(Convert.ToInt64(Id)).ToString("0.00");
            this.DistributionRate.Text = saleManRes.DistributionRate.ToString();
        }
    }
}