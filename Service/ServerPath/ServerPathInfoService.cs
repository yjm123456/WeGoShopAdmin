using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.ServerPath;
using Domain.DTO.Output.ServerPath;
using Domain.Enums.WeChatStationEnum;
using Domain.ServerPath;
using Responsitory.ServerPath;
using Service.ShopManage;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServerPath
{
    public class ServerPathInfoService
    {
        private readonly ServerPathResponsitory _serverPathResponsitory = new ServerPathResponsitory();
        private readonly ShopManageService _shopManageService = new ShopManageService();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        private readonly SnowflakeIDcreator _snowFlake = new SnowflakeIDcreator();

        public PagedResult<ServerPathPagedOutput> PagedList(ServerPathInfoPagedInput input)
        {
            PagedResult<ServerPathPagedOutput> returnList = new PagedResult<ServerPathPagedOutput>();
            if (input.BusinessId < 1)
            {
                input.BusinessId = null;
            }
            List<long> businessId = new List<long>();
            if (!string.IsNullOrEmpty(input.SalesManId))
            {
                //商户表中检索推荐人为它并且version不等于4的businessId
                var shopResult = _shopManageService.GetBySalesManId(Convert.ToInt64(input.SalesManId));
                foreach (var z in shopResult)
                {
                    if (Convert.ToInt64(z.BusinessId) > 0)
                        businessId.Add(Convert.ToInt64(z.BusinessId));
                }
            }
            var serverPathOutput = _serverPathResponsitory.PagedResult(input);
            //获取的值转换
            List<ServerPathPagedOutput> ListData = new List<ServerPathPagedOutput>();
            foreach (var x in serverPathOutput.Data)
            {
                ServerPathPagedOutput data = new ServerPathPagedOutput();
                data.Id = x.Id.ToString();
                data.ServerIP = x.ServerIP;
                data.PingPort = x.PingPort;
                data.PassWord = x.PassWord;
                data.BandWidth = x.BandWidth;
                data.CPUInfomation = x.CPUInfomation;
                data.RAMInformation = x.RAMInformation;
                data.DiskInformation = x.DiskInformation;
                data.StartDate = _dateTostring.ChangeToDateString(x.StartDate);
                data.EndDate = _dateTostring.ChangeToDateString(x.EndDate);
                data.Remark = x.Remark;
                var busId = Convert.ToInt64(x.BusinessId);
                if (busId != 0)
                {
                    data.ShopUserName = _shopManageService.GetNameByBusId(busId);
                }
                else
                {
                    data.ShopUserName = "<span style='color:red'>超管添加</span>";
                }
                if (!string.IsNullOrEmpty(input.SalesManId))
                {
                    var findres = businessId.Where(k => k.Equals(x.BusinessId));
                    if (findres.Count() > 0)
                        ListData.Add(data);
                }
                else {
                    ListData.Add(data);
                }
            }
            returnList.Data = ListData;
            returnList.TotalRecords = serverPathOutput.TotalRecords;
            returnList.PageSize = serverPathOutput.PageSize;
            returnList.PageIndex = serverPathOutput.PageIndex;
            return returnList;
        }

        public PagedResult<ServerPathDetailsPagedOutput> PagedListDetails(ServerPathInfoDetailsPagedInput input)
        {
            PagedResult<ServerPathDetailsPagedOutput> returnList = new PagedResult<ServerPathDetailsPagedOutput>();

            var serverPathDetailsOutput = _serverPathResponsitory.PagedResultDetails(input);
            //获取的值转换
            List<ServerPathDetailsPagedOutput> ListData = new List<ServerPathDetailsPagedOutput>();
            foreach (var x in serverPathDetailsOutput.Data)
            {
                ServerPathDetailsPagedOutput data = new ServerPathDetailsPagedOutput();
                data.Id = x.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
                data.ServerPathId = x.ServerPathId;
                data.Email = x.Email;
                data.Phone = x.Phone;
                data.DomainName = x.DomainName;
                data.BindWechatStationName = x.BindWechatStationName;
                data.BindWechatStationUserName = x.BindWechatStationUserName;
                data.BindWechatStationPassWord = x.BindWechatStationPassWord;
                data.BindWechatStationType = x.BindWechatStationType.GetDescription();
                data.BindWechatStationAppId = x.BindWechatStationAppId;
                data.BindWechatStationAppSecret = x.BindWechatStationAppSecret;
                data.Remark = x.Remark;
                ListData.Add(data);
            }
            returnList.Data = ListData;
            returnList.TotalRecords = serverPathDetailsOutput.TotalRecords;
            returnList.PageSize = serverPathDetailsOutput.PageSize;
            returnList.PageIndex = serverPathDetailsOutput.PageIndex;
            return returnList;
        }

        public ActionResult Insert(InsertServerPathInput input)
        {
            ActionResult result = new ActionResult();

            //同个businessId下验证IP重复
            var serverPath = _serverPathResponsitory.GetByIP(input.ServerIP, input.BusinessId);
            if (!String.IsNullOrEmpty(serverPath.ServerIP))
            {
                result.StatusCode = 0;
                result.Error = "存在相同IP,请重新添加";
                return result;
            }
            //雪花算法生成ID
            ServerPathInfo insertInfo = new ServerPathInfo();
            _snowFlake.SetWorkerID(30);
            insertInfo.Id = _snowFlake.nextId();
            insertInfo.BusinessId = input.BusinessId;
            insertInfo.CPUInfomation = input.CPUInfomation;
            insertInfo.RAMInformation = input.RAMInformation;
            insertInfo.DiskInformation = input.DiskInformation;
            insertInfo.ServerIP = input.ServerIP;
            insertInfo.PingPort = input.PingPort;
            insertInfo.PassWord = input.PassWord;
            insertInfo.BandWidth = input.BandWidth;
            insertInfo.StartDate = input.StartDate;
            insertInfo.EndDate = input.EndDate;
            insertInfo.Remark = input.Remark;
            var insertRes = _serverPathResponsitory.InsertServerPath(insertInfo);
            if (insertRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "添加服务器信息失败，请重试！";
                return result;
            }
            return result;
        }


        public ActionResult Update(UpdateServerPathInput input)
        {
            ActionResult result = new ActionResult();
            //根據ID获取修改数据
            var serverPathInfoSelectRes = _serverPathResponsitory.GetById(input.Id);
            //同个businessId下验证IP重复
            var serverPath = _serverPathResponsitory.GetByIP(input.ServerIP, serverPathInfoSelectRes.BusinessId);
            if (!String.IsNullOrEmpty(serverPath.ServerIP))
            {
                if (serverPath.Id != serverPathInfoSelectRes.Id)
                {
                    result.StatusCode = 0;
                    result.Error = "存在相同IP,请重新添加";
                    return result;
                }
            }
            var updateRes = _serverPathResponsitory.UpdateServerPath(input);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改服务器信息失败，请重试！";
                return result;
            }
            return result;
        }

        public ActionResult Delete(long Id)
        {
            ActionResult result = new ActionResult();
            //验证其中是否还有子集参数
            ServerPathInfoDetailsPagedInput input = new ServerPathInfoDetailsPagedInput();
            input.ServerPathId = Id;
            var detailRes = _serverPathResponsitory.PagedResultDetails(input);
            if (detailRes.TotalRecords > 0)
            {
                result.StatusCode = 0;
                result.Error = "存在绑定信息";
                return result;
            }

            var updateRes = _serverPathResponsitory.DeleteServerPath(Id);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "删除服务器信息失败，请重试！";
                return result;
            }
            return result;
        }
        public ActionResult DeleteDetails(long Id)
        {
            ActionResult result = new ActionResult();
            var updateRes = _serverPathResponsitory.DeleteServerPathDetails(Id);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "删除服务器详细信息失败，请重试！";
                return result;
            }
            return result;
        }
        public ActionResult InsertDetails(InsertServerPathDetailsInput input)
        {
            ActionResult result = new ActionResult();

            //同个服务器下验证域名和公众号名称重复
            var serverPathCheckDomain = _serverPathResponsitory.GetDetailByDomainName(input.DomainName, input.ServerPathId);
            if (!String.IsNullOrEmpty(serverPathCheckDomain.DomainName))
            {
                result.StatusCode = 0;
                result.Error = "存在相同域名,请重新添加";
                return result;
            }
            var serverPathCheckWechatStationName = _serverPathResponsitory.GetDetailByWechatStationName(input.BindWechatStationUserName, input.BindWechatStationName, input.ServerPathId);
            if (!String.IsNullOrEmpty(serverPathCheckWechatStationName.BindWechatStationName))
            {
                result.StatusCode = 0;
                result.Error = "存在相同公众号名称,请重新添加";
                return result;
            }
            //雪花算法生成ID
            ServerPathDetails insertInfo = new ServerPathDetails();
            _snowFlake.SetWorkerID(30);
            insertInfo.Id = _snowFlake.nextId();
            insertInfo.ServerPathId = input.ServerPathId;
            insertInfo.DomainName = input.DomainName;
            insertInfo.Email = input.Email;
            insertInfo.Phone = input.Phone;
            insertInfo.BindWechatStationName = input.BindWechatStationName;
            insertInfo.BindWechatStationUserName = input.BindWechatStationUserName;
            insertInfo.BindWechatStationPassWord = input.BindWechatStationPassWord;
            insertInfo.BindWechatStationAppId = input.BindWechatStationAppId;
            insertInfo.BindWechatStationAppSecret = input.BindWechatStationAppSecret;
            insertInfo.BindWechatStationType = (WechatStationTypeEnum)input.BindWechatStationType;
            insertInfo.Remark = input.Remark;
            var insertRes = _serverPathResponsitory.InsertServerPathDetails(insertInfo);
            if (insertRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "添加服务器详细信息失败，请重试！";
                return result;
            }
            return result;
        }

        public ServerPathPagedOutput GetById(long Id)
        {
            ServerPathPagedOutput output = new ServerPathPagedOutput();
            var result = _serverPathResponsitory.GetById(Id);
            output.Id = result.Id.ToString();
            output.ServerIP = result.ServerIP;
            output.PingPort = result.PingPort;
            output.PassWord = result.PassWord;
            output.BandWidth = result.BandWidth;
            output.CPUInfomation = result.CPUInfomation;
            output.RAMInformation = result.RAMInformation;
            output.DiskInformation = result.DiskInformation;
            output.StartDate = _dateTostring.ChangeToDateString(result.StartDate);
            output.EndDate = _dateTostring.ChangeToDateString(result.EndDate);
            output.Remark = result.Remark;
            var busId = Convert.ToInt64(result.BusinessId);
            output.ShopUserName = _shopManageService.GetNameByBusId(busId);
            return output;
        }


        public List<ServerPathDetailsPagedOutput> GetDetailById(long ServerPathId)
        {
            var serverPathDetailsOutput = _serverPathResponsitory.GetDetailByServerPathId(ServerPathId);
            //获取的值转换
            List<ServerPathDetailsPagedOutput> ListData = new List<ServerPathDetailsPagedOutput>();
            foreach (var x in serverPathDetailsOutput)
            {
                ServerPathDetailsPagedOutput data = new ServerPathDetailsPagedOutput();
                data.Id = x.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
                data.ServerPathId = x.ServerPathId;
                data.Email = x.Email;
                data.Phone = x.Phone;
                data.DomainName = x.DomainName;
                data.BindWechatStationName = x.BindWechatStationName;
                data.BindWechatStationUserName = x.BindWechatStationUserName;
                data.BindWechatStationPassWord = x.BindWechatStationPassWord;
                data.BindWechatStationType = x.BindWechatStationType.GetDescription();
                data.BindWechatStationAppId = x.BindWechatStationAppId;
                data.BindWechatStationAppSecret = x.BindWechatStationAppSecret;
                data.Remark = x.Remark;
                ListData.Add(data);
            }
            return ListData;
        }

        /// <summary>
        /// 获取商户最新的服务器信息
        /// </summary>
        /// <param name="BusinessId"></param>
        /// <returns></returns>
        public ServerPathPagedOutput GetFirstSaleManId(long BusinessId)
        {
            ServerPathPagedOutput output = new ServerPathPagedOutput();
            var result = _serverPathResponsitory.GetFirstByBusinessId(BusinessId);
            output.Id = result.Id.ToString();
            output.ServerIP = result.ServerIP;
            output.PingPort = result.PingPort;
            output.PassWord = result.PassWord;
            output.BandWidth = result.BandWidth;
            output.CPUInfomation = result.CPUInfomation;
            output.RAMInformation = result.RAMInformation;
            output.DiskInformation = result.DiskInformation;
            output.StartDate = _dateTostring.ChangeToDateString(result.StartDate);
            output.EndDate = _dateTostring.ChangeToDateString(result.EndDate);
            output.Remark = result.Remark;
            var busId = Convert.ToInt64(result.BusinessId);
            output.ShopUserName = _shopManageService.GetNameByBusId(busId);
            return output;
        }
    }
}
