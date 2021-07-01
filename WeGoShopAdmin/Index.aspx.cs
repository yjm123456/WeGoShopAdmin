using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
            }
            if (Session["adminRole"].ToString() == "Shop")
            {
                Context.Response.Redirect("~/IndexShop.aspx");
            }
            if (Session["adminRole"].ToString() == "SalesMan")
            {
                Context.Response.Redirect("~/IndexSalesMan.aspx");
            }
            this.UserName.Text = "欢迎您," + Session["adminUser"].ToString(); 
            var Id = Session["adminId"].ToString();
            this.UserID.Text = Id.ToString();
            this.UserRole.Text = Session["adminRole"].ToString();
        }
    }
}