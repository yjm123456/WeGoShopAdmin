using Domain;
using Domain.AdminEnum;
using Domain.DTO;
using Service.SalesMan;
using Service.ShopManage;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeGoShopAdmin
{
    public partial class Register : System.Web.UI.Page
    {
        private static ShopManageService shopMange = new ShopManageService();
        private static SnowflakeIDcreator unity = new SnowflakeIDcreator();
        private static SalesManInfoService salse = new SalesManInfoService();
        private static readonly SendMail sendMails = new SendMail();
        private static string sendCode;
        private static DateTime sendtime;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //[WebMethod]

        //public static string AddBusinessInfo(string AccountName, string BankAccount, string CompanyName, string DepositBank, string Email, string FixPhone, string IdentityCard,
        //    int InstitutionalType, string LiasonManName, int Version, string OrganizingInstitution, string Phone, string SaleManId, int? Scope, string comments, string VerificationCode)
        //{
        //    ActionResult result = new ActionResult();
        //    string res = "";
        //    TimeSpan ts = DateTime.Now - sendtime;
        //    int n = ts.Minutes;//分差
        //    //大于提交五分钟验证码失效
        //    if (ts.Minutes <= 5)
        //    {
        //        if (VerificationCode == sendCode)
        //        {
        //            var salseUser = salse.SelectIdAndName();
        //            BusinessInfo businessInfo = new BusinessInfo();
        //            businessInfo.AccountName = AccountName;
        //            businessInfo.BankAccount = BankAccount;
        //            businessInfo.CompanyName = CompanyName;
        //            businessInfo.DepositBank = DepositBank;
        //            businessInfo.Email = Email;
        //            businessInfo.Phone = Phone;
        //            businessInfo.FixPhone = FixPhone;
        //            businessInfo.IdentityCard = IdentityCard;
        //            businessInfo.InstitutionalType = (InstitutionalTypeEnum)InstitutionalType;
        //            businessInfo.LiasonManName = LiasonManName;
        //            businessInfo.Version = (AccountVersionEnum)Version;
        //            businessInfo.OrganizingInstitution = OrganizingInstitution;
        //            businessInfo.SaleManId = string.IsNullOrEmpty(SaleManId) ? 0 : long.Parse(SaleManId);
        //            businessInfo.Remark = comments;
        //            businessInfo.CreateDate = DateTime.Now;
        //            businessInfo.UserName = Email;
        //            businessInfo.Password = "NGnUxlvQFuQ=";
        //            businessInfo.RoleId = 12;
        //            businessInfo.ApplicationRange = (ShopUsingTypeEnum)Scope;
        //            businessInfo.SaleManName = string.IsNullOrEmpty(SaleManId) ? "" : salseUser.FirstOrDefault(x => x.Key == long.Parse(SaleManId)).Value;
        //            result = shopMange.AddBusiness(businessInfo);
                    
        //        }
        //        else
        //        {
        //            result.StatusCode = 0;
        //            result.Error = "验证码有误！";
        //        }
        //    }
        //    else
        //    {
        //        result.StatusCode = 0;
        //        result.Error = "验证码已过期请重新发送！";
        //    }
        //    if (result.StatusCode == 0)
        //    {
        //        res = result.Error;
        //    }
        //    return res;
        //}

        //public static string AddBusinessInfo(string AccountName, string BankAccount, string CompanyName, string DepositBank, string Email, string FixPhone, string IdentityCard,
        //    int InstitutionalType, string LiasonManName, int Version, string OrganizingInstitution, string Phone, string SaleManId, int Scope, string comments, string VerificationCode)
        //{
        //    ActionResult result = new ActionResult();
        //    string res = "";
        //    TimeSpan ts = DateTime.Now - sendtime;
        //    int n = ts.Minutes;//分差
        //    //大于提交五分钟验证码失效
        //    if (ts.Minutes <= 5)
        //    {
        //        if (VerificationCode == sendCode)
        //        {
        //            var salseUser = salse.SelectIdAndName();
        //            BusinessInfo businessInfo = new BusinessInfo();
        //            businessInfo.AccountName = AccountName;
        //            businessInfo.BankAccount = BankAccount;
        //            businessInfo.CompanyName = CompanyName;
        //            businessInfo.DepositBank = DepositBank;
        //            businessInfo.Email = Email;
        //            businessInfo.Phone = Phone;
        //            businessInfo.FixPhone = FixPhone;
        //            businessInfo.IdentityCard = IdentityCard;
        //            businessInfo.InstitutionalType = (InstitutionalTypeEnum)InstitutionalType;
        //            businessInfo.LiasonManName = LiasonManName;
        //            businessInfo.Version = (AccountVersionEnum)Version;
        //            businessInfo.OrganizingInstitution = OrganizingInstitution;
        //            businessInfo.SaleManId = string.IsNullOrEmpty(SaleManId) ? 0 : long.Parse(SaleManId);
        //            businessInfo.Remark = comments;
        //            businessInfo.CreateDate = DateTime.Now;
        //            businessInfo.UserName = Email;
        //            businessInfo.Password = "NGnUxlvQFuQ=";
        //            businessInfo.RoleId = 12;
        //            businessInfo.ApplicationRange = (ShopUsingTypeEnum)Scope;
        //            businessInfo.SaleManName = string.IsNullOrEmpty(SaleManId) ? "" : salseUser.FirstOrDefault(x => x.Key == long.Parse(SaleManId)).Value;
        //            result = shopMange.AddBusiness(businessInfo);
        //        }
        //        else
        //        {
        //            result.StatusCode = 0;
        //            result.Error = "验证码有误！";
        //        }
        //    }
        //    else
        //    {
        //        result.StatusCode = 0;
        //        result.Error = "验证码已过期请重新发送！";
        //    }

        //    return res;
        //}

        //[WebMethod]
        //public static void SendMail(string Email)
        //{

        //    Service.Utity.SendMail sendMails = new Service.Utity.SendMail();
        //    Random rand1 = new Random();
        //    sendCode = rand1.Next(1000, 9999).ToString();
        //    var sub = "本次注册验证码为：" + sendCode + " 有效时间五分钟";
        //    sendMails.sendMail("smtp.163.com", "18827526577@163.com", "HIWYSPUYPQMPKIUK", "微购", "18827526577@163.com", Email, "商家注册验证码码", sub);
        //    //记录发送时间
        //    sendtime = DateTime.Now;

        //}
    }
}