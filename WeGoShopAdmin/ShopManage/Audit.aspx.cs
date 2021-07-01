using Domain;
using Domain.DTO.Output.ShopManage;
using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.ShopManage
{
    public partial class Audit : System.Web.UI.Page
    {
        private readonly ShopManageService _shopManageService = new ShopManageService();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
                return;
            }
            var busId= Context.Request["id"].ToString();
            if (string.IsNullOrEmpty(busId))
            {
                Console.WriteLine("未获取到商户ID，请重试！");
                return;
            }
            ShopManagePagedOutput company = new ShopManagePagedOutput();
            company = _shopManageService.GetById(Convert.ToInt64(busId));
            this.UserId.Text = busId;
            this.CompanyName.Text = company.CompanyName;
            this.IdentityCard.Text = company.IdentityCard;
            this.InstitutionalType.SelectedIndex = company.InstitutionalType;
            this.OrganizingInstitution.Text = company.OrganizingInstitution;
            this.AccountName.Text = company.AccountName;
            this.DepositBank.Text = company.DepositBank;
            this.BankAccount.Text = company.BankAccount;
            this.Remark.Text = company.Remark;
            this.UserName.Text = company.UserName;
            this.UserNameChildren.Text = _shopManageService.GetChildrenByBusId(Convert.ToInt64(company.BusinessId)).Count().ToString() ;//动态赋值
            this.Version.Text = company.Version;
            this.DueTime.Text = company.DueTime.ToString("yyyy-MM-dd");
            this.Email.Text = company.Email;
            this.ContractNo.Text = company.ContractNo;
            this.SalesMan.Text = company.SaleManName;
            this.BusinessId.Text = company.BusinessId;
        }
    }
}