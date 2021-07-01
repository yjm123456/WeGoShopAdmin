using Domain.AdminEnum;
using Domain.Enums.InformationEnum;
using Service.Information;
using Service.ShopManage;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin
{
    public partial class IndexShop : System.Web.UI.Page
    {
        private readonly ShopManageService _shopManageService = new ShopManageService();
        private readonly InformationService _informationService = new InformationService();
        private readonly GetDateToString _getDataChange = new GetDateToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adminUser"] == null)
            {
                Context.Response.Redirect("~/ControlStation.aspx");
            }
            if (Session["adminRole"].ToString() != "Shop")
            {
                Context.Response.Redirect("~/NoPermission.aspx");
            }
            var Id = _shopManageService.GetByName(Session["adminUser"].ToString()).UserId;
            this.UserID.Text = Id.ToString();
            this.UserRole.Text = Session["adminRole"].ToString();
            var result = _shopManageService.GetById(Id);
            this.ContractNo.Text = string.IsNullOrEmpty(result.ContractNo)? "暂无" : result.ContractNo.ToString();
            this.LiansonMan.Text ="欢迎您,"+ result.LiasonManName.ToString();
            this.CompanyName.Text = result.CompanyName;
            this.InstitutionalType.Text = result.InstitutionalTypeDescription;
            this.Phone.Text = result.Phone;
            this.OrignaztionType.Text = result.OrganizingInstitution;
            if (result.ShopUserState == ShopUserStateEnum.Normal)
            {
                this.ShopUserState.Text = "<span style=color:green>" + result.ShopUserState.GetDescription() + "</span>";
            }
            else if(result.ShopUserState==ShopUserStateEnum.Forbidden)
            {
                this.ShopUserState.Text = "<span style=color:red>" + result.ShopUserState.GetDescription() + "</span>";
            }
            else 
            {
                this.ShopUserState.Text = "<span style=color:red>" + result.ShopUserState.GetDescription() + "</span>";
            }
            this.AuditState.Text = result.VerifyStateDescription;
            this.Email.Text = result.Email;
            this.CanUseTime.Text = result.HasUsedTime;
            this.DueTime.Text = _getDataChange.ChangeToDateString(result.DueTime);
            //上传信息
            string identityCardResult1 = result.IdentityCardImgUrl1;
            string identityCardResult2 = result.IdentityCardImgUrl2;
            string businessLicenseResult = result.BusinessLicenseImgUrl;
            string contractResult = result.ContractResultImgUrl;
            if (string.IsNullOrEmpty(identityCardResult1) || string.IsNullOrEmpty(identityCardResult2))
            {
                this.IdentityCardResult.Text = "<span style='color:red'>未上传</span>";
            }
            else
            {
                this.IdentityCardResult.Text = "<span style='color:green'>已上传</span>";
            }

            if (string.IsNullOrEmpty(businessLicenseResult))
            {
                this.BusinessLicenseResult.Text = "<span style='color:red'>未上传</span>";
            }
            else
            {
                this.BusinessLicenseResult.Text = "<span style='color:green'>已上传</span>";
            }

            if (string.IsNullOrEmpty(contractResult))
            {
                this.ContractResult.Text = "<span style='color:red'>未上传</span>";
            }
            else
            {
                this.ContractResult.Text = "<span style='color:green'>已上传</span>";
            }
            this.Version.Text = result.Version;
            this.Information.Text = _informationService.GetFirstBySendTarget(SendTargetEnum.Shop).Content;
            this.BusinessId.Text = result.BusinessId=="0"?"暂无商户号": result.BusinessId.ToString();
            this.BusInfo.Text = result.BusinessId;
        }
    }
}