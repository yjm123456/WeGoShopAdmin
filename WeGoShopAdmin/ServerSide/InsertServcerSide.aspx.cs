using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.ServerSide
{
    public partial class InsertServcerSide : System.Web.UI.Page
    {
        private readonly ShopManageService _shopManageService = new ShopManageService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
                return;
            }

            var Id = _shopManageService.GetByName(Session["adminUser"].ToString()).UserId;
            var result = _shopManageService.GetById(Id);
            this.BusinessId.Text = result.BusinessId == null ? "0" : result.BusinessId.ToString();

        }
    }
}