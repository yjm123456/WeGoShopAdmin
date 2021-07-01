using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.SalesManManage
{
    public partial class SalesManManageInfo : System.Web.UI.Page
    {
        private readonly ShopManageService _shopManageService = new ShopManageService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
            }
            if (Session["adminRole"].ToString() != "admin")
            {
                Context.Response.Redirect("~/NoPermission.aspx");
            }
            else
            {
                this.UserName.Text = "欢迎您,admin";
            }
            var Id = Session["adminId"].ToString();
            this.UserID.Text = Id.ToString();
            this.UserRole.Text = Session["adminRole"].ToString();
        }
    }
}