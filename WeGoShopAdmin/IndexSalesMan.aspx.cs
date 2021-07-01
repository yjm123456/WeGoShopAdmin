using Domain.Enums.InformationEnum;
using Service.Information;
using Service.SalesMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin
{
    public partial class IndexSalesMan : System.Web.UI.Page
    {
        private readonly SalesManInfoService _salesManService = new SalesManInfoService();
        private readonly InformationService _informationService = new InformationService();
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
            var result = _salesManService.GetById(Convert.ToInt64(Id));
            var state = result.State;
            if (state == 0)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
            }
            if (!string.IsNullOrEmpty(result.SaleManName))
            {
                this.UserName.Text = result.SaleManName.ToString();
                this.SaleManName.Text = result.SaleManName.ToString();
            }
            else { this.UserName.Text = ""; this.SaleManName.Text = ""; }
            this.DistributionRate.Text = result.DistributionRate.ToString() + "%";
            if (!string.IsNullOrEmpty(result.Level))
                this.Level.Text = result.Level;
            else { this.Level.Text = ""; }
            if (!string.IsNullOrEmpty(result.Phone))
            this.Phone.Text = result.Phone.ToString();
            else { this.Phone.Text = ""; }
            if (!string.IsNullOrEmpty(result.Address))
            this.Address.Text = result.Address.ToString();
            else { this.Address.Text = ""; }
            if (!string.IsNullOrEmpty(result.Email))
            this.Email.Text = result.Email.ToString();
            else { this.Email.Text = ""; }
            //this.InstitutionalType.Text = result.InstitutionalTypeDescription;
            //this.Phone.Text = result.Phone;
            //this.OrignaztionType.Text = result.OrganizingInstitution;
            //if (result.ShopUserState == ShopUserStateEnum.Normal)
            //{
            //    this.ShopUserState.Text = "<span style=color:green>" + result.ShopUserState.GetDescription() + "</span>";
            //}
            //else if (result.ShopUserState == ShopUserStateEnum.Forbidden)
            //{
            //    this.ShopUserState.Text = "<span style=color:red>" + result.ShopUserState.GetDescription() + "</span>";
            //}
            //else
            //{
            //    this.ShopUserState.Text = "<span style=color:red>" + result.ShopUserState.GetDescription() + "</span>";
            //}

            this.Information.Text = _informationService.GetFirstBySendTarget(SendTargetEnum.SalesMan).Content;
        }
    }
}