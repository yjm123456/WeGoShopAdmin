using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.SalesMan;
using Domain.DTO.Input.ShopManage;
using Domain.DTO.Output.SalesMan;
using Domain.Enums.FinanceEnums;
using Domain.Finance;
using Domain.SalesMan;
using Responsitory.Finance;
using Responsitory.SalesMan;
using Service.Finance;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SalesMan
{
    public class SalesManInfoService
    {
        private readonly SalesManInfoResponsitory _salesManResponsitory = new SalesManInfoResponsitory();
        private readonly BalanceInfoService _balanceInfoService = new BalanceInfoService();
        private readonly SaleManLevelService _saleManLevelService = new SaleManLevelService();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        private readonly SnowflakeIDcreator _snowFlake = new SnowflakeIDcreator();

        public PagedResult<SalesManInfoPagedOutput> PagedList(SalesManInfoPagedInput input)
        {
            PagedResult<SalesManInfoPagedOutput> returnList = new PagedResult<SalesManInfoPagedOutput>();

            var SalesManInfoOutput = _salesManResponsitory.PagedResult(input);
            //获取的值转换
            List<SalesManInfoPagedOutput> ListData = new List<SalesManInfoPagedOutput>();
            foreach (var x in SalesManInfoOutput.Data)
            {
                SalesManInfoPagedOutput data = new SalesManInfoPagedOutput();
                data.Id = x.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
                data.SaleManName = x.SaleManName;
                data.Phone = x.Phone;
                data.Address = x.Address;
                data.Email = x.Email;
                data.Balance = x.Balance.ToString("0.00");
                if (x.LevelId > 0)
                {
                    var saleManLevel = _saleManLevelService.GetById(x.LevelId);
                    data.Level = saleManLevel.LevelName;
                    data.DistributionRate = saleManLevel.DistributionRate;
                }
                data.LoginName = x.LoginName;
                data.PassWord = x.PassWord;
                data.State = Convert.ToInt32(x.State);
                ListData.Add(data);
            }
            returnList.Data = ListData;
            returnList.TotalRecords = SalesManInfoOutput.TotalRecords;
            returnList.PageSize = SalesManInfoOutput.PageSize;
            returnList.PageIndex = SalesManInfoOutput.PageIndex;
            return returnList;
        }

        /// <summary>
        /// 获取合伙人（下拉框）接口
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, string> SelectIdAndName()
        {
            return _salesManResponsitory.GetIdAndName();
        }
        /// <summary>
        /// 获取合伙人等级（下拉框）接口
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, string> SelectLevelIdAndName() {
            return _saleManLevelService.SelectIdAndName();
        }

        /// <summary>
        /// 根据ID获取合伙人
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SalesManInfoPagedOutput GetById(long Id)
        {
            var x = _salesManResponsitory.GetById(Id);
            SalesManInfoPagedOutput data = new SalesManInfoPagedOutput();
            data.Id = x.Id.ToString();
            data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
            data.SaleManName = x.SaleManName;
            data.Phone = x.Phone;
            data.Address = x.Address;
            data.Email = x.Email;
            if (x.LevelId > 0)
            {
                var saleManLevel = _saleManLevelService.GetById(x.LevelId);
                data.Level = saleManLevel.LevelName;
                data.DistributionRate = saleManLevel.DistributionRate;
                data.LevelId = x.LevelId;
            }
            data.LoginName = x.LoginName;
            data.Balance = x.Balance.ToString("0.00");
            data.PassWord = x.PassWord;
            data.State = Convert.ToInt32(x.State);
            return data;
        }

        /// <summary>
        /// 合伙人登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ReturnClass Login(AdminQueryInput input)
        {
            var result = new ReturnClass();
            var userResult = _salesManResponsitory.SalesManInfoLogin(input);
            if (userResult.LoginName == null)
            {
                result.StatusCode = -1;
                result.Error = "您输入的账号不存在，请重新填写！";
                return result;
            }
            if (userResult.State != ShopUserStateEnum.Normal)
            {
                result.StatusCode = -1;
                result.Error = "登陆失败，该账户未启用！";
                return result;
            }
            input.PassWord = CryptographerHelper.Md5Encrypt(input.PassWord);
            var PassWordResult = _salesManResponsitory.CheckPassWord(input.PassWord, userResult.PassWord);
            if (PassWordResult == false)
            {
                result.StatusCode = 0;
                result.Error = "该账户的密码输入有误，请重新填写！";
                return result;
            }

            result.StatusCode = 1;
            result.Msg = userResult.Id.ToString();
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
            int updateRes = _salesManResponsitory.UpdatePassWord(input);
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

        /// <summary>
        /// 修改合伙人名字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult UpdateName(UpdateSaleManNameInput input)
        {
            ActionResult result = new ActionResult();

            var saleManInfo = _salesManResponsitory.GetByName(input.NewSaleManName);
            if (saleManInfo.Id!=0&&saleManInfo.Id != input.Id)
            {
                result.StatusCode = 0;
                result.Error = "昵称已存在，请重新填写！";
                return result;
            }

            int updateRes = _salesManResponsitory.UpdateName(input);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改昵称操作异常";
                return result;
            }
            result.StatusCode = 1;
            result.Msg = "OK";
            return result;
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult UpdateState(UpdateShopStateInput input)
        {
            ActionResult result = new ActionResult();

            int updateRes = _salesManResponsitory.UpdateState(input);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改状态操作异常";
                return result;
            }
            result.StatusCode = 1;
            result.Msg = "OK";
            return result;
        }

        /// <summary>
        /// 修改合伙人资金
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult UpdateBalance(UpdateSaleManBalanceInput input)
        {
            ActionResult result = new ActionResult();

            var salemanInfo = _salesManResponsitory.GetById(input.Id);
            if (salemanInfo.Id != input.Id)
            {
                result.StatusCode = 0;
                result.Error = "未检索到该合伙人数据！";
                return result;
            }
            decimal UpdateBalance = salemanInfo.Balance + input.NewBalance;
            int updateRes = _salesManResponsitory.UpdateBalance(input.Id, UpdateBalance);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改金额操作异常";
                return result;
            }
            result.StatusCode = 1;
            result.Msg = "OK";
            return result;
        }

        public ActionResult Insert(InsertSaleManInfoInput input)
        {
            ActionResult result = new ActionResult();

            //验证手机号重复
            var salemanInfo = _salesManResponsitory.GetByPhone(input.Phone);
            if (!String.IsNullOrEmpty(salemanInfo.Phone))
            {
                result.StatusCode = 0;
                result.Error = "存在相同手机号,请重新添加";
                return result;
            }
            //验证邮箱重复
            salemanInfo = _salesManResponsitory.GetByEmail(input.Email);
            if (!String.IsNullOrEmpty(salemanInfo.Email))
            {
                result.StatusCode = 0;
                result.Error = "存在相同邮箱,请重新添加";
                return result;
            }
            //雪花算法生成ID
            SalesManInfo insertInfo = new SalesManInfo();
            _snowFlake.SetWorkerID(32);
            insertInfo.Id = _snowFlake.nextId();
            insertInfo.SaleManName = input.SaleManName;
            insertInfo.Phone = input.Phone;
            insertInfo.Address = input.Address;
            insertInfo.State = ShopUserStateEnum.Normal;
            insertInfo.Email = input.Email;
            insertInfo.LevelId = input.LevelId;
            insertInfo.LoginName = input.Phone;
            insertInfo.PassWord = CryptographerHelper.Md5Encrypt("123456");
            insertInfo.Balance = input.Balance;
            var insertRes = _salesManResponsitory.InsertSaleMan(insertInfo);
            if (insertRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "添加合伙人信息失败，请重试！";
                return result;
            }
            return result;
        }

        public ActionResult UpdateInfo(UpdateSaleManInfoInput input)
        {
            ActionResult result = new ActionResult();

            //验证手机号重复
            var salemanInfo = _salesManResponsitory.GetByPhone(input.Phone);
            if (salemanInfo.Id != 0 && salemanInfo.Id != input.Id)
            {
                result.StatusCode = 0;
                result.Error = "昵称已存在，请重新填写！";
                return result;
            }

            if (!String.IsNullOrEmpty(salemanInfo.Phone) && salemanInfo.Id != input.Id)
            {
                result.StatusCode = 0;
                result.Error = "存在相同手机号,请重新填写";
                return result;
            }
            //验证邮箱重复
            salemanInfo = _salesManResponsitory.GetByEmail(input.Email);
            if (!String.IsNullOrEmpty(salemanInfo.Email) && salemanInfo.Id != input.Id)
            {
                result.StatusCode = 0;
                result.Error = "存在相同邮箱,请重新填写";
                return result;
            }
            SalesManInfo  updateInfo = new SalesManInfo();
            updateInfo.SaleManName = input.SaleManName;
            updateInfo.Phone = input.Phone;
            updateInfo.Address = input.Address;
            updateInfo.Email = input.Email;
            updateInfo.LevelId = input.LevelId;
            updateInfo.Id = input.Id;
            var insertRes = _salesManResponsitory.UpdateSaleMan(updateInfo);
            if (insertRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改合伙人信息失败，请重试！";
                return result;
            }
            return result;
        }
    }
}
