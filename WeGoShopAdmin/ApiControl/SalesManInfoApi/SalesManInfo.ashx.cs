using Domain.AdminEnum;
using Domain.DTO;
using Domain.DTO.Input;
using Domain.DTO.Input.SalesMan;
using Domain.DTO.Input.ShopManage;
using Domain.Finance;
using Newtonsoft.Json;
using Service.Finance;
using Service.SalesMan;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WeGoShopAdmin.ApiControl.SalesManInfoApi
{
    /// <summary>
    /// SalesManInfo 的摘要说明
    /// </summary>
    public class SalesManInfo : IHttpHandler
    {
        private readonly SalesManInfoService _salesManInfoService = new SalesManInfoService();
        private readonly GetFirstDayUtity _getFirstDayUtity = new GetFirstDayUtity();
        private readonly BalanceInfoService _balanceInfoService = new BalanceInfoService();
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
                case ("SelectIdAndName"):
                    context.Response.Write(SelectIdAndName(context).ToString());
                    break;
                case ("UpdateSalesManState"):
                    context.Response.Write(UpdateSalesManState(context).ToString());
                    break;
                case ("UpdateSalesManBalance"):
                    context.Response.Write(UpdateSalesManBalance(context).ToString());
                    break;
                case ("SelectLevelIdAndName"):
                    context.Response.Write(SelectLevelIdAndName(context).ToString());
                    break;
                case ("InsertSaleManInfo"):
                    context.Response.Write(InsertSaleManInfo(context).ToString());
                    break;
                case ("UpdateSaleManName"):
                    context.Response.Write(UpdateSalesManName(context).ToString());
                    break;
                case ("UpdatePhone"):
                    context.Response.Write(UpdateSalesManPhone(context).ToString());
                    break;

                case ("UpdateSaleManEmail"):
                    context.Response.Write(UpdateSalesManEmail(context).ToString());
                    break;

                case ("UpdateSaleManAddress"):
                    context.Response.Write(UpdateSalesManAddress(context).ToString());
                    break;
                case ("UpdateSaleManInfo"):
                    context.Response.Write(UpdateSalesManInfo(context).ToString());
                    break;

            }
            #endregion
            context.Response.End();
        }
        public string Select(HttpContext context)
        {
            SalesManInfoPagedInput input = new SalesManInfoPagedInput();
            string SaleManName = context.Request["queryParams[SaleManName]"].ToString().Trim();
            if (!string.IsNullOrEmpty(SaleManName))
            {
                input.SaleManName = SaleManName;
            }
            //string startDate = context.Request["queryParams[StartDate]"].ToString().Trim();
            //if (!string.IsNullOrEmpty(startDate))
            //{
            //    input.StartDate = Convert.ToDateTime(startDate);
            //}
            //else
            //{
            //    input.StartDate = _getFirstDayUtity.GetFirstDay();
            //}
            //string endDate = context.Request["queryParams[EndDate]"].ToString().Trim();
            //if (!string.IsNullOrEmpty(endDate))
            //{
            //    input.EndDate = Convert.ToDateTime(endDate);
            //}
            //else
            //{
            //    input.EndDate = DateTime.Now;
            //}
            input.Page = Convert.ToInt32(context.Request["page"].ToString().Trim());
            input.Limit = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            //input.ShopName = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            var output = _salesManInfoService.PagedList(input);
            LayuiPagedResult pagedRes = new LayuiPagedResult(output.Data, output.TotalRecords);
            var json = JsonConvert.SerializeObject(pagedRes);
            return json;
        }


        public string SelectIdAndName(HttpContext context)
        {
            var output = _salesManInfoService.SelectIdAndName();
            var json = JsonConvert.SerializeObject(output);
            return json;
        }
        public string SelectLevelIdAndName(HttpContext context)
        {
            var output = _salesManInfoService.SelectLevelIdAndName();
            var json = JsonConvert.SerializeObject(output);
            return json;
        }

        #region 修改合伙人方法
        public string UpdateSalesManName(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateSaleManNameInput input = new UpdateSaleManNameInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            input.NewSaleManName = context.Request["SaleManName"].ToString().Trim();
            returnResult = _salesManInfoService.UpdateName(input);
            return JsonConvert.SerializeObject(returnResult); 
        }

        public string UpdateSalesManPhone(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateSaleManInfoInput input = new UpdateSaleManInfoInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            var saleManInfo = _salesManInfoService.GetById(input.Id);
            input.SaleManName = saleManInfo.SaleManName;
            input.Email = saleManInfo.Email;
            input.LevelId = saleManInfo.LevelId;
            input.Address = saleManInfo.Address;
            input.Phone = context.Request["Phone"].ToString().Trim();
            returnResult = _salesManInfoService.UpdateInfo(input);
            return JsonConvert.SerializeObject(returnResult);
        }

        public string UpdateSalesManAddress(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateSaleManInfoInput input = new UpdateSaleManInfoInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            var saleManInfo = _salesManInfoService.GetById(input.Id);
            input.SaleManName = saleManInfo.SaleManName;
            input.Email = saleManInfo.Email;
            input.LevelId = saleManInfo.LevelId;
            input.Address = context.Request["Address"].ToString().Trim();
            input.Phone = saleManInfo.Phone;
            returnResult = _salesManInfoService.UpdateInfo(input);
            return JsonConvert.SerializeObject(returnResult);
        }

        public string UpdateSalesManBalance(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateSaleManBalanceInput input = new UpdateSaleManBalanceInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            input.InitBalance = Convert.ToDecimal(context.Request["InitBalance"].ToString().Trim());
            input.NewBalance = Convert.ToDecimal(context.Request["NewBalance"].ToString().Trim());
            input.Remark = context.Request["Remark"].ToString().Trim();
            input.Creator = "admin";
            returnResult = _salesManInfoService.UpdateBalance(input);
            SaleManBalanceDetails details = new SaleManBalanceDetails();
            details.SaleManId = input.Id;
            details.InitBalance = input.InitBalance;
            details.thisOperateBalance = input.NewBalance;
            details.LastBalance = details.InitBalance + details.thisOperateBalance;
            details.OperationCardNo = "";
            details.Creator = "admin";
            details.Remark = "管理员调整资金余额";
            details.State = Domain.Enums.FinanceEnums.SaleManBalanceDetailsStateEnum.Using;
            details.ReceiptNo = "";
            returnResult = _balanceInfoService.Insert(details);
            return JsonConvert.SerializeObject(returnResult); ;
        }
        public string UpdateSalesManEmail(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateSaleManInfoInput input = new UpdateSaleManInfoInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            var saleManInfo = _salesManInfoService.GetById(input.Id);
            input.SaleManName = saleManInfo.SaleManName;
            input.Email = context.Request["Email"].ToString().Trim();
            input.LevelId = saleManInfo.LevelId;
            input.Address = saleManInfo.Address;
            input.Phone = saleManInfo.Phone;
            returnResult = _salesManInfoService.UpdateInfo(input);
            return JsonConvert.SerializeObject(returnResult);
        }
        public string UpdateSalesManInfo(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            Stream reader = context.Request.InputStream;
            StreamReader sd = new StreamReader(reader);
            string json = sd.ReadToEnd();
            var input = JsonConvert.DeserializeObject<List<BaseJsonInput>>(json);
            UpdateSaleManInfoInput updateInput = new UpdateSaleManInfoInput();
            updateInput.Id = Convert.ToInt64(input.Where(x=>x.name=="SaleManId").FirstOrDefault().value);
            updateInput.SaleManName = input.Where(x => x.name == "SaleManName").FirstOrDefault().value;
            updateInput.Email = input.Where(x => x.name == "Email").FirstOrDefault().value;
            updateInput.Phone = input.Where(x => x.name == "Phone").FirstOrDefault().value;
            updateInput.LevelId = Convert.ToInt64(input.Where(x => x.name == "LevelId").FirstOrDefault().value);
            updateInput.Address = input.Where(x => x.name == "Address").FirstOrDefault().value;
            result = _salesManInfoService.UpdateInfo(updateInput);
            return JsonConvert.SerializeObject(result);
        }

        public string UpdateSalesManState(HttpContext context)
        {
            ActionResult returnResult = new ActionResult();
            UpdateShopStateInput input = new UpdateShopStateInput();
            input.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            input.UserState = (ShopUserStateEnum)Convert.ToInt16(context.Request["UserState"].ToString().Trim());
            returnResult = _salesManInfoService.UpdateState(input);
            return JsonConvert.SerializeObject(returnResult); ;
        }

        #endregion

        public string InsertSaleManInfo(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            Stream reader = context.Request.InputStream;
            StreamReader sd = new StreamReader(reader);
            string json = sd.ReadToEnd();
            var input = JsonConvert.DeserializeObject<List<BaseJsonInput>>(json);
            InsertSaleManInfoInput AddInput = new InsertSaleManInfoInput();
            AddInput.SaleManName = input.Where(x => x.name == "SaleManName").FirstOrDefault().value;
            AddInput.Email = input.Where(x => x.name == "Email").FirstOrDefault().value;
            AddInput.Phone = input.Where(x => x.name == "Phone").FirstOrDefault().value;
            AddInput.LevelId = Convert.ToInt64(input.Where(x => x.name == "Level").FirstOrDefault().value);
            AddInput.Balance = Convert.ToDecimal(input.Where(x => x.name == "Balance").FirstOrDefault().value);
            AddInput.Address = input.Where(x => x.name == "Address").FirstOrDefault().value;

            result = _salesManInfoService.Insert(AddInput);
            return JsonConvert.SerializeObject(result);
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