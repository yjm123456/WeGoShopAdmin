using Domain.DTO;
using Domain.DTO.Input;
using Domain.DTO.Input.Finance;
using Domain.Enums.FinanceEnums;
using Newtonsoft.Json;
using Service.Finance;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WeGoShopAdmin.ApiControl.Finance
{
    /// <summary>
    /// 合伙人收款接口
    /// </summary>
    public class SaleManDepositInfo : IHttpHandler
    {
        private readonly SaleManDepositInfoService _saleManDepositInfoService = new SaleManDepositInfoService();
        private readonly GetFirstDayUtity _getFirstDayUtity = new GetFirstDayUtity();
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
                case ("DepositApplication"):
                    context.Response.Write(DepositApplication(context).ToString());
                    break;
                case ("UpdateDepositState"):
                    context.Response.Write(UpdateDepositState(context).ToString());
                    break;
            }
            #endregion
            context.Response.End();
        }
        public string Select(HttpContext context)
        {
            SaleManDepositInfoPagedInput input = new SaleManDepositInfoPagedInput();
            string startDate = context.Request["queryParams[StartDate]"].ToString().Trim();
            if (!string.IsNullOrEmpty(startDate))
            {
                input.StartDate = Convert.ToDateTime(startDate);
            }
            else
            {
                input.StartDate = _getFirstDayUtity.GetFirstDay();
            }
            string endDate = context.Request["queryParams[EndDate]"].ToString().Trim();
            if (!string.IsNullOrEmpty(endDate))
            {
                input.EndDate = Convert.ToDateTime(endDate);
            }
            else
            {
                input.EndDate = DateTime.Now;
            }
            try
            {
                var saleManId = context.Request["queryParams[SaleManId]"].ToString().Trim();
                input.SaleManId = Convert.ToInt64(saleManId);
            }
            catch(Exception err) {
                var salesManId = context.Request["queryParams[SalesManId]"].ToString().Trim();
                if (!string.IsNullOrEmpty(salesManId))
                {
                    input.SaleManId = Convert.ToInt64(salesManId);
                }
            }
            input.DepositState= Convert.ToInt32(context.Request["queryParams[DepositState]"].ToString().Trim());
            input.Page = Convert.ToInt32(context.Request["page"].ToString().Trim());
            input.Limit = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            //input.ShopName = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            var output = _saleManDepositInfoService.PagedList(input);
            LayuiPagedResult pagedRes = new LayuiPagedResult(output.Data, output.TotalRecords);
            var json = JsonConvert.SerializeObject(pagedRes);
            return json;
        }

        public string DepositApplication(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            InsertSaleManDepositInfoInput AddInput = new InsertSaleManDepositInfoInput();
            AddInput.SaleManId = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            AddInput.Balance = -Convert.ToDecimal(context.Request["NewBalance"].ToString().Trim());
            AddInput.DepositWay = (DepositWayEnum)Convert.ToInt32(context.Request["DepositWay"].ToString().Trim());
            AddInput.DepositAccount = context.Request["DepositAccount"].ToString().Trim();
            AddInput.PayeeName = context.Request["PayeeName"].ToString().Trim();
            result = _saleManDepositInfoService.Insert(AddInput);
            return JsonConvert.SerializeObject(result);
        }


        public string UpdateDepositState(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            UpdateDepositStateInput UpdateInput = new UpdateDepositStateInput();
            UpdateInput.Id = Convert.ToInt64(context.Request["Id"].ToString().Trim());
            UpdateInput.DepositState = Convert.ToInt32(context.Request["DepositState"].ToString().Trim());
            if (UpdateInput.DepositState == 1)
            {
                UpdateInput.ReceiptNo = context.Request["ReceiptNo"].ToString().Trim();
            }
            result = _saleManDepositInfoService.UpdateState(UpdateInput);
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