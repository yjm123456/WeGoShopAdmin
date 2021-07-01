using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.ShopManage;
using Newtonsoft.Json;
using Service.Admin;
using Service.SalesMan;
using Service.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
namespace WeGoShopAdmin.ApiControl
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {

        private readonly AdminService _adminService = new AdminService();
        private readonly ShopManageService _shopManageService = new ShopManageService();
        private readonly SalesManInfoService _salesManService = new SalesManInfoService();
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            #region 这里是逻辑的部分
            string CheckParam = context.Request["CheckParam"].ToString().Trim();
            switch (CheckParam)
            {
                case ("SignIn"):
                    context.Response.Write(GetLoginData(context).ToString());
                    break;
                case ("SignOut"):
                    context.Response.Write(SignOut(context).ToString());
                    break;
                case ("UpdatePassWord"):
                    context.Response.Write(UpdatePassWord(context).ToString());
                    break;
            }
            #endregion
            context.Response.End();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetLoginData(HttpContext context)
        {
            AdminQueryInput input = new AdminQueryInput();
            input.UserName = context.Request["UserName"].ToString().Trim();
            input.PassWord = context.Request["PassWord"].ToString().Trim();
            var returnParam = _adminService.Login(input);
            if (returnParam.StatusCode == 1)
            {
                //加入缓存
                context.Session["adminUser"] = input.UserName;
                context.Session["adminId"] = returnParam.Msg.ToString();
                context.Session["adminRole"] = "admin";
            }
            else if (returnParam.StatusCode == -1)
            {
                //去商户表查询
                returnParam = _shopManageService.Login(input);
                if (returnParam.StatusCode == 1)
                {
                    context.Session["adminUser"] = input.UserName;
                    context.Session["adminId"] = returnParam.Msg.ToString();
                    context.Session["adminRole"] = "Shop";
                }
                else if (returnParam.StatusCode == -1)
                {
                    //去合伙人表查询
                    returnParam = _salesManService.Login(input);
                    if (returnParam.StatusCode == 1)
                    {
                        context.Session["adminUser"] = input.UserName;
                        context.Session["adminId"] = returnParam.Msg.ToString();
                        context.Session["adminRole"] = "SalesMan";
                    }
                }
            }
            var res = JsonConvert.SerializeObject(returnParam);
            return res;
        }

        public string SignOut(HttpContext context)
        {
            context.Session["adminUser"] =null;
            context.Session["adminRole"] = null;
            ReturnClass returnParam = new ReturnClass();
            returnParam.StatusCode = 1;
            returnParam.Msg = "OK";
            var res = JsonConvert.SerializeObject(returnParam);
            return res;
        }


        public string UpdatePassWord(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateShopPassWordInput input = new UpdateShopPassWordInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            input.NewPassWord = context.Request["NewPassword"].ToString().Trim();
            input.UserRole = context.Request["UserRole"].ToString().Trim();
            if (input.UserRole == "Shop")
            {
                returnResult = _shopManageService.UpdatePassWord(input);
            }
            if (input.UserRole == "admin")
            {
                returnResult = _adminService.UpdatePassWord(input);
            }
            if (input.UserRole == "SalesMan")
            {
                returnResult = _salesManService.UpdatePassWord(input);
            }
            return JsonConvert.SerializeObject(returnResult); ;
        }
        /// <summary>
        /// 退出
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}