using Domain;
using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.ShopManage;
using Domain.DTO.Output.ShopManage;
using Newtonsoft.Json;
using System.Web.SessionState;
using Service.SalesMan;
using Service.ShopManage;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WeGoShopAdmin.ApiControl.ShopManageApi
{
    /// <summary>
    /// 商户信息接口层
    /// </summary>
    public class ShopManageInfo : IHttpHandler, IReadOnlySessionState
    {
        private static ShopManageService _shopManageService = new ShopManageService();
        private static GetDateToString _changeDate = new GetDateToString();
        private static string sendCode;
        private static DateTime sendtime;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            #region 逻辑的部分
            string CheckParam = context.Request["CheckParam"].ToString().Trim();
            switch (CheckParam)
            {
                case ("Select"):
                    context.Response.Write(Select(context).ToString());
                    break;
                case ("Insert"):
                    context.Response.Write(Insert(context).ToString());
                    break;
                case ("UpdateShopUserState"):
                    context.Response.Write(UpdateShopUserState(context).ToString());
                    break;
                case ("UpdateVerifyState"):
                    context.Response.Write(UpdateVerifyState(context).ToString());
                    break;
                case ("CreateBusinessId"):
                    context.Response.Write(CreateBusinessId(context).ToString());
                    break;
                case ("SendMail"):
                    context.Response.Write(SendMail(context).ToString());
                    break;
                case ("GetImgUrl"):
                    context.Response.Write(GetImgUrl(context).ToString());
                    break;
                case ("ResetPwd"):
                    context.Response.Write(ResetPwd(context).ToString());
                    break;
                case ("SelectVersion"):
                    context.Response.Write(SelectVersion(context).ToString());
                    break;
                case ("GetLoginUrl"):
                    context.Response.Write(GetLoginUrl(context).ToString());
                    break;
            }
            #endregion
            context.Response.End();
        }

        public string Select(HttpContext context)
        {
            ShopManagePagedInput input = new ShopManagePagedInput();
            #region 时间查询（已注释）
            //string startDate = context.Request["queryParams[StartDate]"].ToString().Trim();
            //if (!string.IsNullOrEmpty(startDate))
            //{
            //    input.StartDate = Convert.ToDateTime(startDate);
            //}
            //else {
            //    GetFirstDayUtity _utity = new GetFirstDayUtity();
            //    input.StartDate = _utity.GetFirstDay();
            //}
            //string endDate = context.Request["queryParams[EndDate]"].ToString().Trim();
            //if (!string.IsNullOrEmpty(endDate))
            //{
            //    input.EndDate = Convert.ToDateTime(endDate);
            //}
            //else {
            //    input.EndDate = DateTime.Now;
            //}
            #endregion
            input.CompanyName = context.Request["queryParams[CompanyName]"].ToString().Trim();
            input.UserName = context.Request["queryParams[LoginUserName]"].ToString().Trim();
            input.Page = Convert.ToInt32(context.Request["page"].ToString().Trim());
            input.Limit = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            #region 组装合伙人信息
            string salesManId = "";
            if (context.Session["adminRole"].ToString() == "SalesMan")
            {
                salesManId = context.Session["adminId"].ToString().Trim();
            }
            else if (context.Session["adminRole"].ToString() == "Shop")
            {
                salesManId = "0";
                input.BusinessId = Convert.ToInt64(context.Request["queryParams[BusInfo]"].ToString().Trim());
                input.Version = AccountVersionEnum.ShopUser;
            }
            else
            {
                salesManId = context.Request["queryParams[SalesManId]"].ToString().Trim();
            }
            if (!string.IsNullOrEmpty(salesManId))
            {
                var saleManid = Convert.ToInt64(salesManId);
                if (saleManid > 0)
                {
                    input.SalesManId = saleManid;
                }
            }
            #endregion

            input.ShopUserState = (ShopUserStateEnum)Convert.ToInt32(context.Request["queryParams[ShopUserState]"].ToString().Trim());
            input.AuditState = (AuditStateEnum)Convert.ToInt32(context.Request["queryParams[AuditState]"].ToString().Trim());
            
            string version = context.Request["queryParams[Version]"].ToString().Trim();
            if (!string.IsNullOrEmpty(version))
            {
                var Version = Convert.ToInt64(version);
                if (Version > 0)
                {
                    input.Version = (AccountVersionEnum)Version;
                }
            }
            var output = _shopManageService.PagedList(input);
            LayuiPagedResult pagedRes = new LayuiPagedResult(output.Data, output.TotalRecords);
            var json = JsonConvert.SerializeObject(pagedRes);
            return json;
        }


        public string Insert(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            Stream reader=  context.Request.InputStream;
            StreamReader sd = new StreamReader(reader);
            string json = sd.ReadToEnd();
            BusinessInfoInsertInput input = new BusinessInfoInsertInput();
            input = JsonConvert.DeserializeObject<BusinessInfoInsertInput>(json);
            TimeSpan ts = DateTime.Now - sendtime;
            int n = ts.Minutes;//分差
            //大于提交五分钟验证码失效
            if (ts.Minutes <= 5)
            {
                if (input.VerificationCode != sendCode)
                {
                    result.StatusCode = 0;
                    result.Error = "验证码有误！";
                    return JsonConvert.SerializeObject(result);
                }
                BusinessInfo businessInfo = new BusinessInfo();
                businessInfo.AccountName = input.AccountName;
                businessInfo.BankAccount = input.BankAccount;
                businessInfo.CompanyName = input.CompanyName;
                businessInfo.DepositBank = input.DepositBank;
                businessInfo.Email = input.Email;
                businessInfo.Phone = input.Phone;
                businessInfo.FixPhone = input.FixPhone;
                businessInfo.IdentityCard = input.IdentityCard;
                businessInfo.InstitutionalType = (InstitutionalTypeEnum)input.InstitutionalType.Value;
                businessInfo.LiasonManName = input.LiasonManName;
                businessInfo.Version = (AccountVersionEnum)input.Version.Value;
                businessInfo.OrganizingInstitution = input.OrganizingInstitution;
                businessInfo.SaleManId = string.IsNullOrEmpty(input.SaleManId) ? 0 : long.Parse(input.SaleManId);
                businessInfo.Remark = input.comments;
                businessInfo.CreateDate = DateTime.Now;
                businessInfo.UserName = input.Email;
                businessInfo.Password = "NGnUxlvQFuQ=";
                businessInfo.RoleId = 12;
                businessInfo.ApplicationRange = (ShopUsingTypeEnum)input.Scope.Value;
                result = _shopManageService.AddBusiness(businessInfo);
            }
            else
            {
                result.StatusCode = 0;
                result.Error = "验证码已过期请重新发送！";
            }

            return JsonConvert.SerializeObject(result);
        }


        public string UpdateShopUserState(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateShopStateInput input = new UpdateShopStateInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            input.UserState = (ShopUserStateEnum)Convert.ToInt16(context.Request["UserState"].ToString().Trim());
            returnResult = _shopManageService.UpdateState(input);
            return JsonConvert.SerializeObject(returnResult); ;
        }

        public string UpdateVerifyState(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateVerifyStateInput input = new UpdateVerifyStateInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            input.VerifyState = (AuditStateEnum)Convert.ToInt16(context.Request["VerifyState"].ToString().Trim());
            returnResult = _shopManageService.UpdateVerifyState(input);
            return JsonConvert.SerializeObject(returnResult); ;
        }

        public string CreateBusinessId(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateBusinessIdInput input = new UpdateBusinessIdInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            returnResult = _shopManageService.UpdateBusinessId(input);
            return JsonConvert.SerializeObject(returnResult);
        }

        public string SendMail(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            try
            {
                string Email = context.Request["Email"].ToString().Trim();
                Service.Utity.SendMail sendMails = new Service.Utity.SendMail();
                Random rand1 = new Random();
                sendCode = rand1.Next(1000, 9999).ToString();
                var sub = "本次注册验证码为：" + sendCode + " 有效时间五分钟";
                //sendMails.sendMail("smtp.qq.com", "1315292348@qq.com", "zdwyvhawrztecbc", "微购", "1315292348@qq.com", Email, "商家注册验证码码", sub);

                sendMails.sendMail("smtp.qq.com", "1315292348@qq.com", "zdwyvhawrztegcbc", "微购", "1315292348@qq.com", Email, "商家注册验证码码", sub);
                //记录发送时间
                sendtime = DateTime.Now;
            }
            catch (Exception err)
            {
                returnResult.StatusCode = 0;
                returnResult.Error = err.ToString();
            }

            returnResult.Msg = _changeDate.ChangeToSecondString(sendtime);
            return JsonConvert.SerializeObject(returnResult);
        }
        
        public string GetImgUrl(HttpContext context)
        {
            ActionResult result = new ActionResult();
            string UserName = context.Request["UserName"].ToString().Trim();

            if (string.IsNullOrEmpty(UserName))
            {
                result.StatusCode = 0;
                result.Error = "未获取到登陆账户！";
                return JsonConvert.SerializeObject(result);
            }
            var output = _shopManageService.GetImgUrl(UserName);
           
            var json = JsonConvert.SerializeObject(output);
            return json;
        }

        public string ResetPwd(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateShopPassWordInput input = new UpdateShopPassWordInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            input.NewPassWord = "123456";
            returnResult = _shopManageService.UpdatePassWord(input);
            return JsonConvert.SerializeObject(returnResult); ;
        }

        public string SelectVersion(HttpContext context)
        {
            var output = _shopManageService.SelectVersion();
            var json = JsonConvert.SerializeObject(output);
            return json;
        }
        public string GetLoginUrl(HttpContext context)
        {
            long Id = 0;
            try
            {
                 Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            }
            catch {
                ActionResult res= new ActionResult();
                res.StatusCode = 0;
                res.Error = "未配置商户ID,无法访问控制台！";
                return JsonConvert.SerializeObject(res);
            }
            if (Id == 0)
            {
                ActionResult res = new ActionResult();
                res.StatusCode = 0;
                res.Error = "未配置商户ID,无法访问控制台！";
                return JsonConvert.SerializeObject(res);
            }
            var output = _shopManageService.GetLoginUrl(Id);
            var json = JsonConvert.SerializeObject(output);
            return json;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}