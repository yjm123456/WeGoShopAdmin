using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.Finance;
using Domain.DTO.Output.Finance;
using Domain.Enums.FinanceEnums;
using Domain.Finance;
using Responsitory.Finance;
using Responsitory.SalesMan;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Finance
{
    /// <summary>
    /// 合伙人资金明细服务层
    /// </summary>
    public class BalanceInfoService
    {
        private readonly BalanceInfoResponsitory _balanceInfoResponsitory = new BalanceInfoResponsitory();
        private readonly SalesManInfoResponsitory _saleManInfoResponsitory = new SalesManInfoResponsitory();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        private readonly SnowflakeIDcreator _snowFlake = new SnowflakeIDcreator();
        public PagedResult<SalesManBalanceDetailsInfoOutput> PagedListBalance(SalesManBalanceDetailsInfoPagedInput input)
        {
            PagedResult<SalesManBalanceDetailsInfoOutput> returnList = new PagedResult<SalesManBalanceDetailsInfoOutput>();
            if (input.EndDate.HasValue)
            {
                input.EndDate = _dateTostring.GetLastSecond(input.EndDate.Value);
            }
            var SalesManInfoOutput = _balanceInfoResponsitory.PagedResultBalance(input);
            //获取的值转换
            List<SalesManBalanceDetailsInfoOutput> ListData = new List<SalesManBalanceDetailsInfoOutput>();
            foreach (var x in SalesManInfoOutput.Data)
            {
                SalesManBalanceDetailsInfoOutput data = new SalesManBalanceDetailsInfoOutput();
                data.Id = x.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
                data.SaleManName = _saleManInfoResponsitory.GetById(x.SaleManId).SaleManName;
                data.ReceiptNo = x.ReceiptNo;
                data.InitBalance = x.InitBalance.ToString("0.00");
                data.OperationCardNo = x.OperationCardNo;
                data.LastBalance = x.LastBalance.ToString("0.00");
                data.thisOperateBalance = x.thisOperateBalance.ToString("0.00");
                data.Creator = x.Creator;
                data.Remark = x.Remark;
                ListData.Add(data);
            }
            returnList.Data = ListData;
            returnList.TotalRecords = SalesManInfoOutput.TotalRecords;
            returnList.PageSize = SalesManInfoOutput.PageSize;
            returnList.PageIndex = SalesManInfoOutput.PageIndex;
            return returnList;
        }

        public SalesManBalanceDetailsInfoOutput GetById(long Id)
        {
            var SalesManInfoOutput = _balanceInfoResponsitory.GetById(Id);
            //获取的值转换
                SalesManBalanceDetailsInfoOutput data = new SalesManBalanceDetailsInfoOutput();
                data.Id = SalesManInfoOutput.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(SalesManInfoOutput.CreateTime);
                data.SaleManName = _saleManInfoResponsitory.GetById(SalesManInfoOutput.SaleManId).SaleManName;
                data.ReceiptNo = SalesManInfoOutput.ReceiptNo;
                data.InitBalance = SalesManInfoOutput.InitBalance.ToString("0.00");
                data.OperationCardNo = SalesManInfoOutput.OperationCardNo;
                data.LastBalance = SalesManInfoOutput.LastBalance.ToString("0.00");
                data.thisOperateBalance = SalesManInfoOutput.thisOperateBalance.ToString("0.00");
                data.Creator = SalesManInfoOutput.Creator;
                data.Remark = SalesManInfoOutput.Remark;
            return data;
        }

        public ActionResult Insert(SaleManBalanceDetails details)
        {
            ActionResult result = new ActionResult();
            _snowFlake.SetWorkerID(22);
            long detailId = _snowFlake.nextId();
            details.Id = detailId;
            int InsertDetails = _balanceInfoResponsitory.Insert(details);
            if (InsertDetails == 0)
            {
                result.StatusCode = 0;
                result.Error = "添加账单明细失败，请重试！";
                return result;
            }
            result.Msg = detailId.ToString();
            return result;
        }

        public ActionResult UpdateState(long Id, SaleManBalanceDetailsStateEnum enumState)
        {
            ActionResult result = new ActionResult();
            var getResult = _balanceInfoResponsitory.GetById(Id);
            if (getResult.Id != Id)
            {
                result.StatusCode = 0;
                result.Error = "未检索到该账单明细数据，请重试！";
                return result;
            }
            int Updateresult = _balanceInfoResponsitory.UpdateState(Id, enumState);
            return result;
        }

        public ActionResult UpdateReceiptNo( long DetailsId,string ReceiptNo)
        {
            ActionResult result = new ActionResult();
            var getResult = _balanceInfoResponsitory.GetById(DetailsId);
            if (getResult.Id!=DetailsId)
            {
                result.StatusCode = 0;
                result.Error = "未检索到该账单明细数据，请重试！";
                return result;
            }
            var getReceiptNo = _balanceInfoResponsitory.GetReceiptNo(ReceiptNo);
            var findRes = getReceiptNo.Where(z => z.ReceiptNo == ReceiptNo && z.Id != DetailsId).ToList();
            if (findRes.Count > 0)
            {
                result.StatusCode = 0;
                result.Error = "已存在重复单据号，请修改后重试！";
                return result;
            }
            int Updateresult= _balanceInfoResponsitory.UpdateReceiptNo(DetailsId,ReceiptNo);
            return result;
        }
    }
}
