using Service.SalesMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.SalesManManage
{
    public partial class UpdateSalesMan : System.Web.UI.Page
    {
        private readonly SalesManInfoService _salemanInfoService = new SalesManInfoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
                return;
            }
            if (Session["adminRole"].ToString() != "admin")
            {
                Context.Response.Redirect("~/NoPermission.aspx");
                return;
            }
            var SaleManId = Context.Request["id"].ToString();
            if (string.IsNullOrEmpty(SaleManId))
            {
                Console.WriteLine("未获取到合伙人ID，请重试！");
                return;
            }
            this.SaleManId.Text = SaleManId;
            var result = _salemanInfoService.GetById(Convert.ToInt64(SaleManId));
            if (string.IsNullOrEmpty(SaleManId))
            {
                Console.WriteLine("未获取到合伙人ID，请重试！");
                return;
            }
            if (!string.IsNullOrEmpty(result.SaleManName))
            {
                this.SaleManName.Text = result.SaleManName.ToString();
            }
            else {

                this.SaleManName.Text = "";
            }
            if (!string.IsNullOrEmpty(result.Email))
            {
                this.Email.Text = result.Email.ToString();
            }
            else
            {

                this.Email.Text = "";
            }

            if (!string.IsNullOrEmpty(result.Phone))
            {
                this.Phone.Text = result.Phone.ToString();
            }
            else
            {

                this.Phone.Text = "";
            }
            if (!string.IsNullOrEmpty(result.Level))
            {
                this.LevelId.Text = result.LevelId.ToString();
            }
            if (!string.IsNullOrEmpty(result.Address))
            {
                this.Address.Text = result.Address.ToString();
            }
            else
            {

                this.Address.Text = "";
            }
        }
    }
}