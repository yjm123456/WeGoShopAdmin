using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.SalesManManage
{
    public partial class InsertSalesMan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
                return;
            }
            if(Session["adminRole"].ToString()!= "admin") {
                Context.Response.Redirect("~/NoPermission.aspx");
                return;
            }
        }
    }
}