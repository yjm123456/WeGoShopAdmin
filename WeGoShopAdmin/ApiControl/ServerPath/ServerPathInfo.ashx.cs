using Domain.DTO;
using Domain.DTO.Input;
using Domain.DTO.Input.ServerPath;
using Domain.DTO.Output.ServerPath;
using Newtonsoft.Json;
using Service.ServerPath;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace WeGoShopAdmin.ApiControl
{
    /// <summary>
    /// ServerPathInfoApi 的摘要说明
    /// </summary>
    public class ServerPathInfo : IHttpHandler, IReadOnlySessionState
    {
        private readonly ServerPathInfoService _serverPathInfoService = new ServerPathInfoService();
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
                case ("SelectDetails"):
                    context.Response.Write(SelectDetails(context).ToString());
                    break;
                case ("InsertServerSide"):
                    context.Response.Write(InsertServerSide(context).ToString());
                    break;
                case ("InsertServerSideDetails"):
                    context.Response.Write(InsertServerSideDetails(context).ToString());
                    break;

                case ("UpdateServerSide"):
                    context.Response.Write(UpdateServerSide(context).ToString());
                    break;
                case ("DeleteServerSide"):
                    context.Response.Write(DeleteServerSide(context).ToString());
                    break;
                case ("DeleteServerSideDetails"):
                    context.Response.Write(DeleteServerSideDetails(context).ToString());
                    break;
            }
            #endregion
            context.Response.End();
        }


        public string Select(HttpContext context)
        {
            ServerPathInfoPagedInput input = new ServerPathInfoPagedInput();
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


            try
            {
                input.BusinessId = Convert.ToInt64(context.Request["queryParams[BusInfo]"].ToString().Trim());
            }
            catch
            {
                input.BusinessId = 0;
            }
            if (context.Session["adminRole"].ToString() == "Shop" && input.BusinessId == 0)
            {
                LayuiPagedResult res = new LayuiPagedResult(new List<ServerPathDetailsPagedOutput>(), 0);
                var jsonresult = JsonConvert.SerializeObject(res);
                return jsonresult;
            }
            input.IPPort = context.Request["queryParams[IPPort]"].ToString().Trim();
            input.SalesManId = context.Request["queryParams[SalesManId]"].ToString().Trim();
            input.Page = Convert.ToInt32(context.Request["page"].ToString().Trim());
            input.Limit = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            //input.ShopName = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            var output = _serverPathInfoService.PagedList(input);
            LayuiPagedResult pagedRes = new LayuiPagedResult(output.Data, output.TotalRecords);
            var json = JsonConvert.SerializeObject(pagedRes);
            return json;
        }
        public string SelectDetails(HttpContext context)
        {
            ServerPathInfoDetailsPagedInput input = new ServerPathInfoDetailsPagedInput();
            string serverPathId = context.Request["ServerPathId"].ToString().Trim();
            if (string.IsNullOrEmpty(serverPathId))
            {
                List<ServerPathDetailsPagedOutput> returnList = new List<ServerPathDetailsPagedOutput>();
                return JsonConvert.SerializeObject(new LayuiPagedResult(returnList, 0, 0));
            }
            input.ServerPathId = Convert.ToInt64(serverPathId);
            input.Page = Convert.ToInt32(context.Request["page"].ToString().Trim());
            input.Limit = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            //input.ShopName = Convert.ToInt32(context.Request["limit"].ToString().Trim());
            var output = _serverPathInfoService.PagedListDetails(input);
            LayuiPagedResult pagedRes = new LayuiPagedResult(output.Data, output.TotalRecords);
            var json = JsonConvert.SerializeObject(pagedRes);
            return json;
        }

        public string InsertServerSide(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            Stream reader = context.Request.InputStream;
            StreamReader sd = new StreamReader(reader);
            string json = sd.ReadToEnd();
            var input = JsonConvert.DeserializeObject<List<BaseJsonInput>>(json);
            InsertServerPathInput AddInput = new InsertServerPathInput();

            AddInput.CPUInfomation = input.Where(x => x.name == "CPUInfo").FirstOrDefault().value;
            AddInput.StartDate = Convert.ToDateTime(input.Where(x => x.name == "StartDate").FirstOrDefault().value);
            AddInput.EndDate = Convert.ToDateTime(input.Where(x => x.name == "EndDate").FirstOrDefault().value);
            AddInput.ServerIP = input.Where(x => x.name == "IPPort").FirstOrDefault().value;
            AddInput.PassWord = input.Where(x => x.name == "PassWord").FirstOrDefault().value;
            AddInput.PingPort = input.Where(x => x.name == "PingPort").FirstOrDefault().value;
            AddInput.BandWidth = input.Where(x => x.name == "BandWidth").FirstOrDefault().value;
            AddInput.RAMInformation = input.Where(x => x.name == "RAMInfo").FirstOrDefault().value;
            AddInput.DiskInformation = input.Where(x => x.name == "DiskInfo").FirstOrDefault().value;
            AddInput.Remark = input.Where(x => x.name == "Remark").FirstOrDefault().value;
            if (input.Where(x => x.name == "BusinessId").FirstOrDefault().value != "0")
            {
                AddInput.BusinessId = Convert.ToInt64(input.Where(x => x.name == "BusinessId").FirstOrDefault().value);
            }
            else
            {
                AddInput.BusinessId = 0;
            }
            result = _serverPathInfoService.Insert(AddInput);
            return JsonConvert.SerializeObject(result);
        }


        public string InsertServerSideDetails(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            Stream reader = context.Request.InputStream;
            StreamReader sd = new StreamReader(reader);
            string json = sd.ReadToEnd();
            var input = JsonConvert.DeserializeObject<List<BaseJsonInput>>(json);
            InsertServerPathDetailsInput AddInput = new InsertServerPathDetailsInput();
            AddInput.ServerPathId = Convert.ToInt64(input.Where(x => x.name == "ServerPathId").FirstOrDefault().value);
            AddInput.DomainName = input.Where(x => x.name == "DomainName").FirstOrDefault().value;
            AddInput.BindWechatStationName = input.Where(x => x.name == "BindWechatStationName").FirstOrDefault().value;
            AddInput.BindWechatStationType = Convert.ToInt16(input.Where(x => x.name == "BindWechatStationType").FirstOrDefault().value);
            AddInput.Phone = input.Where(x => x.name == "Phone").FirstOrDefault().value;
            AddInput.Email = input.Where(x => x.name == "Email").FirstOrDefault().value;
            AddInput.BindWechatStationUserName = input.Where(x => x.name == "BindWechatStationUserName").FirstOrDefault().value;
            AddInput.BindWechatStationPassWord = input.Where(x => x.name == "BindWechatStationPassWord").FirstOrDefault().value;
            AddInput.BindWechatStationAppId = input.Where(x => x.name == "BindWechatStationAppId").FirstOrDefault().value;
            AddInput.BindWechatStationAppSecret = input.Where(x => x.name == "BindWechatStationAppSecret").FirstOrDefault().value;
            AddInput.Remark = input.Where(x => x.name == "Remark").FirstOrDefault().value;
          
            result = _serverPathInfoService.InsertDetails(AddInput);
            return JsonConvert.SerializeObject(result);
        }

        public string UpdateServerSide(HttpContext context)
        {
            context.Response.ContentType = "application/x-www-form-urlencode";
            ActionResult result = new ActionResult();
            Stream reader = context.Request.InputStream;
            StreamReader sd = new StreamReader(reader);
            string json = sd.ReadToEnd();
            var input = JsonConvert.DeserializeObject<List<BaseJsonInput>>(json);
            UpdateServerPathInput updateInput = new UpdateServerPathInput();

            updateInput.CPUInfomation = input.Where(x => x.name == "CPUInfo").FirstOrDefault().value;
            updateInput.StartDate = Convert.ToDateTime(input.Where(x => x.name == "StartDate").FirstOrDefault().value);
            updateInput.EndDate = Convert.ToDateTime(input.Where(x => x.name == "EndDate").FirstOrDefault().value);
            updateInput.ServerIP = input.Where(x => x.name == "IPPort").FirstOrDefault().value;
            updateInput.PassWord = input.Where(x => x.name == "PassWord").FirstOrDefault().value;
            updateInput.PingPort = input.Where(x => x.name == "PingPort").FirstOrDefault().value;
            updateInput.BandWidth = input.Where(x => x.name == "BandWidth").FirstOrDefault().value;
            updateInput.RAMInformation = input.Where(x => x.name == "RAMInfo").FirstOrDefault().value;
            updateInput.DiskInformation = input.Where(x => x.name == "DiskInfo").FirstOrDefault().value;
            updateInput.Remark = input.Where(x => x.name == "Remark").FirstOrDefault().value;
            if (input.Where(x => x.name == "Id").FirstOrDefault().value == "0")
            {
                result.StatusCode = 0;
                result.Error = "未获取到修改编号数据！";
                return JsonConvert.SerializeObject(result);
            }
            updateInput.Id = Convert.ToInt64(input.Where(x => x.name == "Id").FirstOrDefault().value.ToString());
            result = _serverPathInfoService.Update(updateInput);
            return JsonConvert.SerializeObject(result);
        }

        public string DeleteServerSide(HttpContext context)
        {
            ActionResult result = new ActionResult();
            string serverPathId = context.Request["Id"].ToString().Trim();
            if (string.IsNullOrEmpty(serverPathId))
            {
                result.StatusCode = 0;
                result.Error = "未获取到删除编号数据！";
                return JsonConvert.SerializeObject(result);
            }
            result = _serverPathInfoService.Delete(Convert.ToInt64(serverPathId));
            return JsonConvert.SerializeObject(result);
        }

        public string DeleteServerSideDetails(HttpContext context)
        {
            ActionResult result = new ActionResult();
            string serverPathDetailId = context.Request["Id"].ToString().Trim();
            if (string.IsNullOrEmpty(serverPathDetailId))
            {
                result.StatusCode = 0;
                result.Error = "未获取到删除编号数据！";
                return JsonConvert.SerializeObject(result);
            }
            result = _serverPathInfoService.DeleteDetails(Convert.ToInt64(serverPathDetailId));
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