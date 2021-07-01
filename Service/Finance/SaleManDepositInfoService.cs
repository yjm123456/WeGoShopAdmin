using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.Finance;
using Domain.DTO.Input.SalesMan;
using Domain.DTO.Output.Finance;
using Domain.Enums.FinanceEnums;
using Domain.Finance;
using Responsitory.Finance;
using Service.SalesMan;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Finance
{
    /// <summary>
    /// 合伙人提现服务层
    /// </summary>
    public class SaleManDepositInfoService
    {
        private readonly SaleManDepositInfoResponsitory _saleManDepositInfoResponsitory = new SaleManDepositInfoResponsitory();
        private readonly SalesManInfoService _saleManInfoService = new SalesManInfoService();
        private readonly BalanceInfoService _balanceInfoService = new BalanceInfoService();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        private readonly SnowflakeIDcreator _snowFlake = new SnowflakeIDcreator();
        public PagedResult<SaleManDepositInfoOutput> PagedList(SaleManDepositInfoPagedInput input)
        {
            PagedResult<SaleManDepositInfoOutput> returnList = new PagedResult<SaleManDepositInfoOutput>();
            if (input.EndDate.HasValue)
            {
                input.EndDate = _dateTostring.GetLastSecond(input.EndDate.Value);
            }
            var SalesManInfoOutput = _saleManDepositInfoResponsitory.PagedResult(input);
            //获取的值转换
            List<SaleManDepositInfoOutput> ListData = new List<SaleManDepositInfoOutput>();
            foreach (var x in SalesManInfoOutput.Data)
            {
                var saleMan = _saleManInfoService.GetById(x.SaleManId);
                SaleManDepositInfoOutput data = new SaleManDepositInfoOutput();
                data.Id = x.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
                data.SaleManName = saleMan.SaleManName;
                data.Phone = saleMan.Phone;
                data.UpdateTime = x.UpdateTime;
                data.DepositMoney = x.DepositMoney.ToString("0.00");
                data.DepositCardNo = x.DepositCardNo.ToString();
                data.DepositWay = x.DepositWay.GetDescription();
                if (x.DepositState == Domain.Enums.FinanceEnums.DepositStateEnum.Passed)
                {
                    data.DepositState = "<span style='color:orange'>" + x.DepositState.GetDescription() + "</span>";
                    if (DateTime.Now.Day - data.UpdateTime.Day >= 3)
                    {
                        //自动到账
                        UpdateDepositStateInput updateStateInput = new UpdateDepositStateInput();
                        updateStateInput.DepositState = Convert.ToInt16(DepositStateEnum.ConfirmToAccount);
                        updateStateInput.Id = x.Id;
                        var updatestateRes= UpdateState(updateStateInput);
                        if (updatestateRes.StatusCode == 0)
                        {
                            returnList.Data = new List<SaleManDepositInfoOutput>() ;
                            returnList.TotalRecords = 0;
                            returnList.PageSize = 1;
                            returnList.PageIndex = 10;
                            return returnList;
                        }
                        continue;
                    }
                }
                else if (x.DepositState == Domain.Enums.FinanceEnums.DepositStateEnum.NotSet)
                {
                    data.DepositState = "<span style='color:blue'>" + x.DepositState.GetDescription() + "</span>";
                }
                else if (x.DepositState == Domain.Enums.FinanceEnums.DepositStateEnum.ConfirmToAccount)
                {
                    data.DepositState = "<span style='color:green'>" + x.DepositState.GetDescription() + "</span>";
                }
                else if (x.DepositState == Domain.Enums.FinanceEnums.DepositStateEnum.Cancel)
                {
                    data.DepositState = "<span style='color:gray'>" + x.DepositState.GetDescription() + "</span>";
                }
                else
                {
                    data.DepositState = "<span style='color:red'>" + x.DepositState.GetDescription() + "</span>";
                }
                data.DepositStateInt = Convert.ToInt32(x.DepositState);
                data.Remark = x.Remark;
                data.ReceivableName = x.ReceivableName;
                ListData.Add(data);
            }
            returnList.Data = ListData;
            returnList.TotalRecords = SalesManInfoOutput.TotalRecords;
            returnList.PageSize = SalesManInfoOutput.PageSize;
            returnList.PageIndex = SalesManInfoOutput.PageIndex;
            return returnList;
        }

        public ActionResult Insert(InsertSaleManDepositInfoInput input)
        {
            ActionResult result = new ActionResult();

            //验证合伙人是否启用
            var salemanInfo = _saleManInfoService.GetById(input.SaleManId);
            if ((ShopUserStateEnum)salemanInfo.State != ShopUserStateEnum.Normal)
            {
                result.StatusCode = 0;
                result.Error = "合伙人状态非正常，无法创建提现申请！";
                return result;
            }
            if (Convert.ToDecimal(salemanInfo.Balance) < input.Balance)
            {
                result.StatusCode = 0;
                result.Error = "提现金额不能大于" + salemanInfo.Balance + "，请重新输入！";
                return result;
            }

            //创建账单明细
            SaleManBalanceDetails details = new SaleManBalanceDetails();
            details.SaleManId = input.SaleManId;
            details.InitBalance = Convert.ToDecimal(_saleManInfoService.GetById(input.SaleManId).Balance);
            details.thisOperateBalance = input.Balance;
            details.LastBalance = details.InitBalance + details.thisOperateBalance;
            details.OperationCardNo = input.DepositAccount;
            details.Creator = "admin";
            details.Remark = "客户创建提现单据";
            details.State = 0;
            details.ReceiptNo = "";
            result = _balanceInfoService.Insert(details);
            if (result.StatusCode == 0)
            {
                return result;
            }
            //创建提现单
            //雪花算法生成ID
            SaleManDepositInfo insertInfo = new SaleManDepositInfo();
            _snowFlake.SetWorkerID(21);
            insertInfo.Id = _snowFlake.nextId();
            insertInfo.SaleManId = input.SaleManId;
            insertInfo.DepositMoney = input.Balance;
            insertInfo.DepositCardNo = input.DepositAccount;
            insertInfo.DepositWay = input.DepositWay;
            insertInfo.DepositState = DepositStateEnum.NotSet;
            insertInfo.ReceivableName = input.PayeeName;
            insertInfo.CreateTime = DateTime.Now;
            insertInfo.UpdateTime = DateTime.Now;
            insertInfo.SaleManBalanceDetailsId = Convert.ToInt64(result.Msg);
            insertInfo.Remark = "合伙人申请提现";
            var insertRes = _saleManDepositInfoResponsitory.Insert(insertInfo);
            if (insertRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "合伙人申请提现失败，请重试！";
                return result;
            }
            return result;
        }


        public ActionResult UpdateState(UpdateDepositStateInput input)
        {
            int updateRes = 0;
            ActionResult result = new ActionResult();
            var getResult = _saleManDepositInfoResponsitory.GetById(input.Id);
            if (getResult.Id != input.Id || getResult.Id == 0)
            {
                result.StatusCode = 0;
                result.Error = "未检索到该条提现单据，请重试！";
                return result;
            }
            updateRes = _saleManDepositInfoResponsitory.UpdateState(input);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改提现状态失败，请重试！";
                return result;
            }
            //审核通过加入单据号
            if ((DepositStateEnum)input.DepositState == DepositStateEnum.Passed)
            {

                result = _balanceInfoService.UpdateReceiptNo(getResult.SaleManBalanceDetailsId, input.ReceiptNo);
                if (result.StatusCode == 0)
                {
                    return result;
                }
            }
            if ((DepositStateEnum)input.DepositState == DepositStateEnum.ConfirmToAccount)
            {
                //启用单据明细并且扣款
                result = ConfirmDeposit(getResult.SaleManBalanceDetailsId, getResult.SaleManId);
            }
            return result;
        }

        /// <summary>
        /// 提现确认到账
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmDeposit(long SaleManBalanceId, long SaleManId)
        {
            ActionResult result = new ActionResult();
            //根据ID查询单据明细
            var saleManBalanceDetail = _balanceInfoService.GetById(SaleManBalanceId);
            if (string.IsNullOrEmpty(saleManBalanceDetail.Id))
            {
                result.StatusCode = 0;
                result.Error = "未检索到该条提现的的账单明细，请重试！";
                return result;
            }
            //启用单据明细
            result = _balanceInfoService.UpdateState(SaleManBalanceId, SaleManBalanceDetailsStateEnum.Using);
            if (result.StatusCode == 0)
            {
                return result;
            }
            //合伙人扣款
            UpdateSaleManBalanceInput uppdateBalanceInput = new UpdateSaleManBalanceInput();
            uppdateBalanceInput.Id = SaleManId;
            uppdateBalanceInput.InitBalance =Convert.ToDecimal(saleManBalanceDetail.InitBalance);
            uppdateBalanceInput.NewBalance = Convert.ToDecimal(saleManBalanceDetail.thisOperateBalance);
            uppdateBalanceInput.Remark = "提现确认到账";
            uppdateBalanceInput.Creator = saleManBalanceDetail.SaleManName;
            result = _saleManInfoService.UpdateBalance(uppdateBalanceInput);
            if (result.StatusCode == 0)
            {
                return result;
            }
            return result;
        }
    }
}
