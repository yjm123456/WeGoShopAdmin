using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin.ServerSide
{
    public partial class InsertDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
                return;
            }
            var serverPathId = Context.Request["id"].ToString();
            if (string.IsNullOrEmpty(serverPathId))
            {
                Console.WriteLine("未获取到服务器ID，请重试！");
                return;
            }
            this.ServerPathId.Text = serverPathId;
        }
    }
}