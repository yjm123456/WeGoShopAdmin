using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO.Input.SalesMan;
using Domain.DTO.Output.SalesMan;
using Responsitory.SalesMan;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SalesMan
{
    public class SaleManLevelService
    {
        private readonly SaleManLevelResponsitory _saleManLevelResponsitory = new SaleManLevelResponsitory();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        public PagedResult<SaleManLevelPagedOutput> PagedList(SalesManLevelPagedInput input)
        {
            PagedResult<SaleManLevelPagedOutput> returnList = new PagedResult<SaleManLevelPagedOutput>();

            var SalesManInfoOutput = _saleManLevelResponsitory.PagedResult(input);
            //获取的值转换
            List<SaleManLevelPagedOutput> ListData = new List<SaleManLevelPagedOutput>();
            foreach (var x in SalesManInfoOutput.Data)
            {
                SaleManLevelPagedOutput data = new SaleManLevelPagedOutput();
                data.Id = x.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
                data.LevelName = x.LevelName;
                data.Remark = x.Remark;
                data.DistributionRate = x.DistributionRate;
                data.State = Convert.ToInt32(x.State);
                data.Level = x.Level;
                data.StandardEditionNum = x.StandardEditionNum;
                data.BasicEditionNum = x.BasicEditionNum;
                data.UltimateEdition = x.UltimateEdition;
                data.StateDescription = x.State.GetDescription();
                ListData.Add(data);
            }
            returnList.Data = ListData;
            returnList.TotalRecords = SalesManInfoOutput.TotalRecords;
            returnList.PageSize = SalesManInfoOutput.PageSize;
            returnList.PageIndex = SalesManInfoOutput.PageIndex;
            return returnList;
        }

        public Dictionary<long, string> SelectIdAndName()
        {
            List<Dictionary<long, string>> resultList = new List<Dictionary<long, string>>();
            return _saleManLevelResponsitory.GetIdAndName();
        }
      
        public GetSaleManLevelByIdOutPut GetById(long Level)
        {
            var x = _saleManLevelResponsitory.GetByLevel(Convert.ToInt32(Level));
            GetSaleManLevelByIdOutPut data = new GetSaleManLevelByIdOutPut();
            data.Id = x.Id.ToString();
            data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
            data.LevelName = x.LevelName;
            data.Level = x.Level;
            data.DistributionRate = x.DistributionRate;
            data.Remark = x.Remark;
            data.State = Convert.ToInt16(x.State);
            data.StateDescription = x.State.GetDescription();
            data.StandardEditionNum = x.StandardEditionNum;
            data.BasicEditionNum = x.BasicEditionNum;
            data.UltimateEdition = x.UltimateEdition;
            return data;
        }
    }
}
