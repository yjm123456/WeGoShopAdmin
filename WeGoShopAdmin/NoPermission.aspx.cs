using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin
{
    public partial class NoPermission : System.Web.UI.Page
    {
        private readonly ShopManageService _shopManageService = new ShopManageService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
            }
            var Id = _shopManageService.GetByName(Session["adminUser"].ToString()).UserId;
            var result = _shopManageService.GetById(Id);
            if (result.LiasonManName == null)
            {
                this.UserName.Text = "欢迎您,admin" ;
            }
            else {
                this.UserName.Text = "欢迎您," + result.LiasonManName.ToString();
            }
        }
    }
}