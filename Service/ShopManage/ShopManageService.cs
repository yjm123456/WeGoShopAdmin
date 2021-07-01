using Domain;
using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.ShopManage;
using Domain.DTO.Output.ShopManage;
using Responsitory.ShopManage;
using Service.SalesMan;
using Service.ServerPath;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ShopManage
{
    /// <summary>
    /// 商户信息业务层
    /// </summary>
    public class ShopManageService
    {
        private readonly ShopManageResponsitory _shopManageResponsitory = new ShopManageResponsitory();
        private readonly BusinessVersionService _businessVersionService = new BusinessVersionService();
        private readonly SalesManInfoService _salesManInfoService = new SalesManInfoService();
        private static ServerPathInfoService _serverPathInfoService = new ServerPathInfoService();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        private readonly SnowflakeIDcreator _snowFlake = new SnowflakeIDcreator();
        public PagedResult<ShopManagePagedOutput> PagedList(ShopManagePagedInput input)
        {
            PagedResult<ShopManagePagedOutput> returnList = new PagedResult<ShopManagePagedOutput>();
            if (input.EndDate != null)
            {
                input.EndDate = input.EndDate.Value.AddDays(1);
            }
            if (input.BusinessId < 0)
            {
                input.BusinessId = null;
                input.Version = null;
            }
            if (Convert.ToInt32(input.ShopUserState) < 0)
            {
                input.ShopUserState = null;
            }
            if (Convert.ToInt32(input.AuditState) < 0)
            {
                input.AuditState = null;
            }
            var shopManageOutput = _shopManageResponsitory.PagedResult(input);
            //获取的值转换
            List<ShopManagePagedOutput> ListData = new List<ShopManagePagedOutput>();
            foreach (var x in shopManageOutput.Data)
            {
                ShopManagePagedOutput data = new ShopManagePagedOutput();
                data.Id = x.UserId.ToString();
                data.UserName = x.UserName;
                data.CompanyName = x.CompanyName;
                data.LiasonManName = x.LiasonManName;
                data.Phone = x.Phone;
                data.SaleManName = x.SaleManName;
                data.Version = x.Version.GetDescription();
                data.ApplicationRange = x.ApplicationRange.GetDescription();
                data.VerifyState = Convert.ToInt32(x.VerifyState);
                if (x.VerifyState == AuditStateEnum.Passed)
                {
                    data.VerifyStateDescription = "<span style=color:green>" + x.VerifyState.GetDescription() + "</span>";
                }
                else
                {
                    data.VerifyStateDescription = "<span style=color:red>" + x.VerifyState.GetDescription() + "</span>";
                    if (x.ShopUserState == ShopUserStateEnum.Normal)
                    {
                        //修改商户状态为禁用
                        UpdateShopStateInput updateInput = new UpdateShopStateInput();
                        updateInput.Id = Convert.ToInt64(data.Id);
                        updateInput.UserState = ShopUserStateEnum.Forbidden;
                        var updateStateResult = _shopManageResponsitory.UpdateState(updateInput);
                        if (updateStateResult == 0)
                        {
                            returnList.Data = new List<ShopManagePagedOutput>();
                            returnList.TotalRecords = 0;
                            returnList.PageSize = shopManageOutput.PageSize;
                            returnList.PageIndex = shopManageOutput.PageIndex;
                            return returnList;
                        }
                    }
                }
                data.ShopUserState = x.ShopUserState;
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateDate);
                var dateResultDay = (x.DueTime - DateTime.Now).Days;
                var dateResultHours = (x.DueTime - DateTime.Now).Hours;
                var dateResultMin = (x.DueTime - DateTime.Now).Minutes;
                var dateResultSecond = (x.DueTime - DateTime.Now).Seconds;
                if (dateResultDay > 0)
                {
                    if (dateResultDay < 10)
                    {
                        data.HasUsedTime = "<span style=color:red>" + dateResultDay + "天</span>";
                    }
                    else
                    {
                        data.HasUsedTime = "<span style=color:green>" + dateResultDay + "天</span>";
                    }
                }
                else if (dateResultHours > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultHours + "小时</span>";
                }

                else if (dateResultMin > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultMin + "分</span>";
                }
                else if (dateResultSecond > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultSecond + "秒</span>";
                }

                else
                {

                    data.HasUsedTime = "<span style=color:gray>已过期</span>";
                    //验证是否需要修改为已过期
                    if (x.ShopUserState != ShopUserStateEnum.OutOfDate)
                    {
                        //修改商户状态为已过期
                        UpdateShopStateInput updateInput = new UpdateShopStateInput();
                        updateInput.Id = Convert.ToInt64(data.Id);
                        updateInput.UserState = ShopUserStateEnum.OutOfDate;
                        var updateStateResult = _shopManageResponsitory.UpdateState(updateInput);
                        if (updateStateResult == 0)
                        {
                            returnList.Data = new List<ShopManagePagedOutput>();
                            returnList.TotalRecords = 0;
                            returnList.PageSize = shopManageOutput.PageSize;
                            returnList.PageIndex = shopManageOutput.PageIndex;
                            return returnList;
                        }
                    }
                }
                data.IdentityCardImgUrl1 = x.IdentityCardImgUrl1;
                data.IdentityCardImgUrl2 = x.IdentityCardImgUrl2;
                data.BusinessLicenseImgUrl = x.BusinessLicenseImgUrl;
                data.ContractResultImgUrl = x.ContractResultImgUrl;
                data.Remark = x.Remark;
                ListData.Add(data);
            }
            returnList.Data = ListData;
            returnList.TotalRecords = shopManageOutput.TotalRecords;
            returnList.PageSize = shopManageOutput.PageSize;
            returnList.PageIndex = shopManageOutput.PageIndex;
            return returnList;
        }

        public ActionResult UpdateState(UpdateShopStateInput input)
        {
            ActionResult result = new ActionResult();
            //验证是否审核通过
            var verifyResult = _shopManageResponsitory.GetAuditStateById(input.Id);
            if (verifyResult == AuditStateEnum.NotAudit)
            {
                result.StatusCode = 0;
                result.Error = "请先审核商户信息！";
                return result;
            }
            var version = _shopManageResponsitory.GetVersionById(input.Id);
            if (version == AccountVersionEnum.ShopUser)
            {
                var businessId = _shopManageResponsitory.GetBusinessIDById(input.Id);
                if (businessId == 0)
                {
                    result.StatusCode = 0;
                    result.Error = "未检索到该账户所属商户信息，无法修改状态！";
                    return result;
                }
                //查询母账号是否被禁用
                var busInfo = _shopManageResponsitory.GetByBusId(businessId);
                if(busInfo.BusinessId!=businessId)
                {
                    result.StatusCode = 0;
                    result.Error = "未检索到该账户所属商户信息，无法修改状态！";
                    return result;
                }
                if (busInfo.ShopUserState != ShopUserStateEnum.Normal)
                {
                    result.StatusCode = 0;
                    result.Error = "该商户未启用，无法修改子账户状态！";
                    return result;
                }
            }
            if (verifyResult == AuditStateEnum.NotPassed)
            {
                result.StatusCode = 0;
                result.Error = "该商户审核信息未通过，无法修改状态！";
                return result;
            }
            int updateRes = _shopManageResponsitory.UpdateState(input);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "商户号未生成！";
                return result;
            }
            result.StatusCode = 1;
            result.Msg = "OK";
            return result;
        }

        public ActionResult UpdateVerifyState(UpdateVerifyStateInput input)
        {
            ActionResult result = new ActionResult();
            //验证是否审核通过
            var verifyResult = _shopManageResponsitory.GetAuditStateById(input.Id);
            if (verifyResult == AuditStateEnum.Passed)
            {
                result.StatusCode = 0;
                result.Error = "该商户已审核通过，无需审核！";
                return result;
            }
            int updateRes = _shopManageResponsitory.UpdateVerifyState(input);
            if (input.VerifyState == AuditStateEnum.NotPassed)
            {
                UpdateShopStateInput updateShopStateInput = new UpdateShopStateInput();
                updateShopStateInput.Id = input.Id;
                updateShopStateInput.UserState = ShopUserStateEnum.Forbidden;
                updateRes += _shopManageResponsitory.UpdateState(updateShopStateInput);
            }
            if (updateRes > 0)
            {
                result.StatusCode = 1;
                result.Msg = "OK";
            }
            return result;
        }

        public ShopManagePagedOutput GetById(long id)
        {

            ShopManagePagedOutput data = new ShopManagePagedOutput();
            var companyResult = _shopManageResponsitory.GetById(id);
            data.Id = companyResult.UserId.ToString();
            data.CompanyName = companyResult.CompanyName;
            data.BankAccount = companyResult.BankAccount;
            data.UserName = companyResult.UserName;
            data.DueTime = companyResult.DueTime;
            data.LiasonManName = companyResult.LiasonManName;
            data.InstitutionalType = Convert.ToInt16(companyResult.InstitutionalType);
            data.OrganizingInstitution = companyResult.OrganizingInstitution;
            data.AccountName = companyResult.AccountName;
            data.DepositBank = companyResult.DepositBank;
            data.Email = companyResult.Email;
            data.BusinessId = companyResult.BusinessId.ToString();
            data.Phone = companyResult.Phone;
            data.IdentityCard = companyResult.IdentityCard;
            data.ContractNo = companyResult.ContractNo;
            data.SaleManName = companyResult.SaleManName;
            data.InstitutionalTypeDescription = companyResult.InstitutionalType.GetDescription();
            data.Version = companyResult.Version.GetDescription();
            data.ApplicationRange = companyResult.ApplicationRange.GetDescription();
            if (companyResult.VerifyState == AuditStateEnum.Passed)
            {
                data.VerifyStateDescription = "<span style=color:green>" + companyResult.VerifyState.GetDescription() + "</span>";
            }
            else
            {
                data.VerifyStateDescription = "<span style=color:red>" + companyResult.VerifyState.GetDescription() + "</span>";
            }
            data.ShopUserState = companyResult.ShopUserState;
            data.CreateTime = _dateTostring.ChangeToDateString(companyResult.CreateDate);
            var dateResultDay = (companyResult.DueTime - DateTime.Now).Days;
            var dateResultHours = (companyResult.DueTime - DateTime.Now).Hours;
            var dateResultMin = (companyResult.DueTime - DateTime.Now).Minutes;
            var dateResultSecond = (companyResult.DueTime - DateTime.Now).Seconds;
            if (dateResultDay > 0)
            {
                if (dateResultDay < 10)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultDay + "天</span>";
                }
                else
                {
                    data.HasUsedTime = "<span style=color:green>" + dateResultDay + "天</span>";
                }
            }
            else if (dateResultHours > 0)
            {
                data.HasUsedTime = "<span style=color:red>" + dateResultHours + "小时</span>";
            }

            else if (dateResultMin > 0)
            {
                data.HasUsedTime = "<span style=color:red>" + dateResultMin + "分</span>";
            }
            else if (dateResultSecond > 0)
            {
                data.HasUsedTime = "<span style=color:red>" + dateResultSecond + "秒</span>";
            }
            else
            {

                data.HasUsedTime = "<span style=color:gray>已过期</span>";
                //验证是否需要修改为已过期
                if (companyResult.ShopUserState != ShopUserStateEnum.OutOfDate)
                {
                    //修改商户状态为已过期
                    UpdateShopStateInput updateInput = new UpdateShopStateInput();
                    updateInput.Id = Convert.ToInt64(data.Id);
                    updateInput.UserState = ShopUserStateEnum.OutOfDate;
                    var updateStateResult = _shopManageResponsitory.UpdateState(updateInput);
                    if (updateStateResult == 0)
                    {
                        data = new ShopManagePagedOutput();
                        return data;
                    }
                }
            }
            data.IdentityCardImgUrl1 = companyResult.IdentityCardImgUrl1;
            data.IdentityCardImgUrl2 = companyResult.IdentityCardImgUrl2;
            data.BusinessLicenseImgUrl = companyResult.BusinessLicenseImgUrl;
            data.ContractResultImgUrl = companyResult.ContractResultImgUrl;
            data.Remark = companyResult.Remark;
            return data;
        }

        public List<ShopManagePagedOutput> GetChildrenByBusId(long businessId)
        {
            List<ShopManagePagedOutput> listResult = new List<ShopManagePagedOutput>();
            ShopManagePagedOutput data = new ShopManagePagedOutput();
            var companyResultList = _shopManageResponsitory.GetChildrenByBusId(businessId);
            foreach (var companyResult in companyResultList)
            {
                data.Id = companyResult.UserId.ToString();
                data.CompanyName = companyResult.CompanyName;
                data.BankAccount = companyResult.BankAccount;
                data.UserName = companyResult.UserName;
                data.DueTime = companyResult.DueTime;
                data.LiasonManName = companyResult.LiasonManName;
                data.InstitutionalType = Convert.ToInt16(companyResult.InstitutionalType);
                data.OrganizingInstitution = companyResult.OrganizingInstitution;
                data.AccountName = companyResult.AccountName;
                data.DepositBank = companyResult.DepositBank;
                data.Email = companyResult.Email;
                data.BusinessId = companyResult.BusinessId.ToString();
                data.Phone = companyResult.Phone;
                data.IdentityCard = companyResult.IdentityCard;
                data.ContractNo = companyResult.ContractNo;
                data.SaleManName = companyResult.SaleManName;
                data.Version = companyResult.Version.GetDescription();
                data.ApplicationRange = companyResult.ApplicationRange.GetDescription();
                if (companyResult.VerifyState == AuditStateEnum.Passed)
                {
                    data.VerifyStateDescription = "<span style=color:green>" + companyResult.VerifyState.GetDescription() + "</span>";
                }
                else
                {
                    data.VerifyStateDescription = "<span style=color:red>" + companyResult.VerifyState.GetDescription() + "</span>";
                }
                data.ShopUserState = companyResult.ShopUserState;
                data.Remark = companyResult.Remark;
                data.CreateTime = _dateTostring.ChangeToDateString(companyResult.CreateDate);
                var dateResultDay = (companyResult.DueTime - DateTime.Now).Days;
                var dateResultHours = (companyResult.DueTime - DateTime.Now).Hours;
                var dateResultMin = (companyResult.DueTime - DateTime.Now).Minutes;
                var dateResultSecond = (companyResult.DueTime - DateTime.Now).Seconds;
                if (dateResultDay > 0)
                {
                    if (dateResultDay < 10)
                    {
                        data.HasUsedTime = "<span style=color:red>" + dateResultDay + "天</span>";
                    }
                    else
                    {
                        data.HasUsedTime = "<span style=color:green>" + dateResultDay + "天</span>";
                    }
                }
                else if (dateResultHours > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultHours + "小时</span>";
                }

                else if (dateResultMin > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultMin + "分</span>";
                }
                else if (dateResultSecond > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultSecond + "秒</span>";
                }
                else
                {

                    data.HasUsedTime = "<span style=color:gray>已过期</span>";
                    //验证是否需要修改为已过期
                    if (companyResult.ShopUserState != ShopUserStateEnum.OutOfDate)
                    {
                        //修改商户状态为已过期
                        UpdateShopStateInput updateInput = new UpdateShopStateInput();
                        updateInput.Id = Convert.ToInt64(data.Id);
                        updateInput.UserState = ShopUserStateEnum.OutOfDate;
                        var updateStateResult = _shopManageResponsitory.UpdateState(updateInput);
                        if (updateStateResult == 0)
                        {
                            data = new ShopManagePagedOutput();
                        }
                    }
                }
                listResult.Add(data);
            }
            return listResult;
        }

        public string GetNameByBusId(long BusinessId)
        {
            return _shopManageResponsitory.GetByBusId(BusinessId).CompanyName;
        }

        public List<ShopManagePagedOutput> GetBySalesManId(long SalesManId)
        {
            List<ShopManagePagedOutput> listResult = new List<ShopManagePagedOutput>();
            var companyResultList = _shopManageResponsitory.GetBySalesManId(SalesManId);
            foreach (var companyResult in companyResultList)
            {
                ShopManagePagedOutput data = new ShopManagePagedOutput();
                data.Id = companyResult.UserId.ToString();
                data.CompanyName = companyResult.CompanyName;
                data.BankAccount = companyResult.BankAccount;
                data.UserName = companyResult.UserName;
                data.DueTime = companyResult.DueTime;
                data.LiasonManName = companyResult.LiasonManName;
                data.InstitutionalType = Convert.ToInt16(companyResult.InstitutionalType);
                data.OrganizingInstitution = companyResult.OrganizingInstitution;
                data.AccountName = companyResult.AccountName;
                data.DepositBank = companyResult.DepositBank;
                data.Email = companyResult.Email;
                data.BusinessId = companyResult.BusinessId.ToString();
                data.Phone = companyResult.Phone;
                data.IdentityCard = companyResult.IdentityCard;
                data.ContractNo = companyResult.ContractNo;
                data.SaleManName = companyResult.SaleManName;
                data.Version = companyResult.Version.GetDescription();
                data.ApplicationRange = companyResult.ApplicationRange.GetDescription();
                if (companyResult.VerifyState == AuditStateEnum.Passed)
                {
                    data.VerifyStateDescription = "<span style=color:green>" + companyResult.VerifyState.GetDescription() + "</span>";
                }
                else
                {
                    data.VerifyStateDescription = "<span style=color:red>" + companyResult.VerifyState.GetDescription() + "</span>";
                }
                data.ShopUserState = companyResult.ShopUserState;
                data.Remark = companyResult.Remark;
                data.CreateTime = _dateTostring.ChangeToDateString(companyResult.CreateDate);
                var dateResultDay = (companyResult.DueTime - DateTime.Now).Days;
                var dateResultHours = (companyResult.DueTime - DateTime.Now).Hours;
                var dateResultMin = (companyResult.DueTime - DateTime.Now).Minutes;
                var dateResultSecond = (companyResult.DueTime - DateTime.Now).Seconds;
                if (dateResultDay > 0)
                {
                    if (dateResultDay < 10)
                    {
                        data.HasUsedTime = "<span style=color:red>" + dateResultDay + "天</span>";
                    }
                    else
                    {
                        data.HasUsedTime = "<span style=color:green>" + dateResultDay + "天</span>";
                    }
                }
                else if (dateResultHours > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultHours + "小时</span>";
                }

                else if (dateResultMin > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultMin + "分</span>";
                }
                else if (dateResultSecond > 0)
                {
                    data.HasUsedTime = "<span style=color:red>" + dateResultSecond + "秒</span>";
                }
                else
                {

                    data.HasUsedTime = "<span style=color:gray>已过期</span>";
                    //验证是否需要修改为已过期
                    if (companyResult.ShopUserState != ShopUserStateEnum.OutOfDate)
                    {
                        //修改商户状态为已过期
                        UpdateShopStateInput updateInput = new UpdateShopStateInput();
                        updateInput.Id = Convert.ToInt64(data.Id);
                        updateInput.UserState = ShopUserStateEnum.OutOfDate;
                        var updateStateResult = _shopManageResponsitory.UpdateState(updateInput);
                        if (updateStateResult == 0)
                        {
                            data = new ShopManagePagedOutput();
                        }
                    }
                }
                listResult.Add(data);
            }
            return listResult;
        }
        public BusinessInfo GetByName(string Name)
        {
            return _shopManageResponsitory.GetByLoginName(Name);
        }
        public ActionResult UpdateBusinessId(UpdateBusinessIdInput input)
        {
            ActionResult result = new ActionResult();
            var auditState = _shopManageResponsitory.GetAuditStateById(input.Id);
            if (auditState != AuditStateEnum.Passed)
            {
                result.StatusCode = 0;
                result.Error = "该商户" + auditState.GetDescription() + "，无法生成商户号！";
                return result;
            }
            //var shopUserState = _shopManageResponsitory.GetShopUserStateById(input.Id);
            //if (shopUserState == ShopUserStateEnum.OutOfDate)
            //{
            //    result.StatusCode = 0;
            //    result.Error = "该商户"+shopUserState.GetDescription()+ "，无法生成商户号！";
            //    return result;
            //}
            _snowFlake.SetWorkerID(83);
            long BusinessId = _snowFlake.nextId();
            string BusinessIdParam = BusinessId.ToString();
            input.BusinessId = BusinessIdParam;
            //验证是否审核通过
            var updateRes = _shopManageResponsitory.UpdateBusinessId(input);
            if (updateRes > 0)
            {
                result.StatusCode = 1;
                result.Msg = BusinessIdParam;
            }
            return result;
        }
        /// <summary>
        ///  商家注册
        /// </summary>
        /// <param name="businessInfo"></param>
        /// <returns></returns>
        public ActionResult AddBusiness(BusinessInfo businessInfo)
        {
            ActionResult result = new ActionResult();
            #region 验证
            //验证邮箱是否被重复注册
            var companyRes = GetByName(businessInfo.Email);
            if (companyRes.Email != null)
            {
                result.StatusCode = 0;
                result.Error = "该邮箱已被重复注册，请更换邮箱！";
                return result;
            }

            //验证公司名称是否被重复注册
            companyRes = _shopManageResponsitory.GetByCompanyName(businessInfo.CompanyName);
            if (companyRes.CompanyName != null)
            {
                result.StatusCode = 0;
                result.Error = "该公司名称已被重复注册，请更换公司名称！";
                return result;
            }
            #endregion


            var salseUser = _salesManInfoService.SelectIdAndName();
            if (businessInfo.SaleManId > 0)
            {
                businessInfo.SaleManName = salseUser.FirstOrDefault(x => x.Key == businessInfo.SaleManId).Value;
            }
            else
            {
                businessInfo.SaleManName = "";
            }
            businessInfo.DueTime = DateTime.Now.AddDays(7);

            //获取最大位数UserID并且加1生成新的userId
            var maxUserId = _shopManageResponsitory.getMaxUserId();
            if (maxUserId == 0)
            {
                result.StatusCode = 0;
                result.Error = "生成新ID失败！";
                return result;
            }
            businessInfo.UserId = maxUserId + 1;
            var addres = _shopManageResponsitory.AddBusiness(businessInfo);
            if (addres == false)
            {
                result.StatusCode = 0;
                result.Error = "生成商户号失败！";
                return result;
            }
            return result;
        }
        /// <summary>
        /// 商家登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ReturnClass Login(AdminQueryInput input)
        {
            var result = new ReturnClass();
            var userResult = _shopManageResponsitory.BusinessInfoLogin(input);
            if (userResult.UserName == null)
            {
                result.StatusCode = -1;
                result.Error = "您输入的账号不存在，请重新填写！";
                return result;
            }
            input.PassWord = CryptographerHelper.Md5Encrypt(input.PassWord);
            var PassWordResult = _shopManageResponsitory.CheckPassWord(input.PassWord, userResult.Password);
            if (PassWordResult == false)
            {
                result.StatusCode = 0;
                result.Error = "该账户的密码输入有误，请重新填写！";
                return result;
            }
            if ((AccountVersionEnum)Convert.ToInt16(userResult.Version) == AccountVersionEnum.ShopUser)
            {
                result.StatusCode = 0;
                result.Error = "该账户为商户自建账户，无法登陆!";
                return result;
            }
            result.StatusCode = 1;
            result.Msg = userResult.UserId.ToString();
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult UpdatePassWord(UpdateShopPassWordInput input)
        {
            ActionResult result = new ActionResult();
            //加密
            input.NewPassWord = CryptographerHelper.Md5Encrypt(input.NewPassWord);
            int updateRes = _shopManageResponsitory.UpdatePassWord(input);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改密码失败！";
                return result;
            }
            result.StatusCode = 1;
            result.Msg = "OK";
            return result;
        }

        public ImgUrlOutput GetImgUrl(string LoginName)
        {
            ImgUrlOutput output = new ImgUrlOutput();
            var res = GetByName(LoginName);
            output.IdCard1Url = res.IdentityCardImgUrl1;
            output.IdCardUrl2 = res.IdentityCardImgUrl2;
            output.BusinessLicenseUrl = res.BusinessLicenseImgUrl;
            output.ContractResultUrl = res.ContractResultImgUrl;
            return output;
        }


        public Dictionary<long, string> SelectVersion()
        {
            return _businessVersionService.GetIdAndName();
        }
        public ActionResult GetLoginUrl(long Id)
        {
            ActionResult result = new ActionResult();
            var serverPathResult = _serverPathInfoService.GetFirstSaleManId(Id);
            if (serverPathResult.Id == null)
            {
                result.StatusCode = 0;
                result.Error = "未找到商户对应的服务器信息";
                return result;
            }
            var serverPathDetailFirst = _serverPathInfoService.GetDetailById(Convert.ToInt64(serverPathResult.Id));
            if (serverPathDetailFirst.Count == 0)
            {
                result.StatusCode = 0;
                result.Error = "未绑定公众号和域名信息，无法拼接链接地址！";
                return result;
            }
            result.Msg = "http://" + serverPathDetailFirst.FirstOrDefault().DomainName + ":" + serverPathResult.PingPort + "/admin/login.aspx";
            return result;
        }
    }
}
