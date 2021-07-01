using Domain.DTO;
using Domain.DTO.Input.Finance;
using Newtonsoft.Json;
using Service.Finance;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeGoShopAdmin.ApiControl.Balance
{
    /// <summary>
    /// BalanceInfo 的摘要说明
    /// </summary>
    public class BalanceInfo : IHttpHandler
    {
        private readonly BalanceInfoService _balanceInfoService = new BalanceInfoService();
        private readonly GetFirstDayUtity _getFirstDayUtity = new GetFirstDayUtity();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            #region 逻辑的部分
            string CheckParam = context.Request["CheckParam"].ToString().Trim();
            switch (CheckParam)
            {
                case ("SelectSaleManBalanceDetails"):
                    context.Response.Write(SelectSaleManBalanceDetails(context).ToString());
                    break;
            }
            #endregion
            context.Response.End();
        }
        public string SelectSaleManBalanceDetails(HttpContext context)
        {
            SalesManBalanceDetailsInfoPagedInput input = new SalesManBalanceDetailsInfoPagedInput();
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

                input.SaleManId = Convert.ToInt64(context.Request["queryParams[SalesManId]"].ToString());
            }
            catch (Exception er)
            {

                input.SaleManId = null;
            }
            input.Page = Convert.ToInt32(context.Request["page"].ToString().Trim());
            input.Limit = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            //input.ShopName = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            var output = _balanceInfoService.PagedListBalance(input);
            LayuiPagedResult pagedRes = new LayuiPagedResult(output.Data, output.TotalRecords);
            var json = JsonConvert.SerializeObject(pagedRes);
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