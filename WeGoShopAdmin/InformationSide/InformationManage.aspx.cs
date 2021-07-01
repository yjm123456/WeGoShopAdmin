using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.InformationSide
{
    public partial class InformationManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
            }
            if (Session["adminRole"].ToString() == "admin")
            {
                var Id = Session["adminId"].ToString();
                this.UserID.Text = Id.ToString();
                this.UserRole.Text = Session["adminRole"].ToString();
                this.UserName.Text = "欢迎您,admin";
                this.BusInfo.Text = "-1";
            }
            else {
                Context.Response.Redirect("~/NoPermission.aspx");
            }
        }
    }
}