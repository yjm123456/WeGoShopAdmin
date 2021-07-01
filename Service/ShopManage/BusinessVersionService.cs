using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO.Input.ShopManage;
using Domain.DTO.Output.ShopManage;
using Responsitory.ShopManage;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ShopManage
{
    public class BusinessVersionService
    {
        private readonly BusinessVersionResponsitory _businessVersionResponsitory = new BusinessVersionResponsitory();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        private readonly SnowflakeIDcreator _snowFlake = new SnowflakeIDcreator();

        public PagedResult<BusinessVersionPagedOutput> PagedList(BusinessVersionPagedInput input)
        {
            PagedResult<BusinessVersionPagedOutput> returnList = new PagedResult<BusinessVersionPagedOutput>();

            var shopManageOutput = _businessVersionResponsitory.PagedResult(input);
            //获取的值转换
            List<BusinessVersionPagedOutput> ListData = new List<BusinessVersionPagedOutput>();
            foreach (var x in shopManageOutput.Data)
            {
                BusinessVersionPagedOutput data = new BusinessVersionPagedOutput();
                data.Id = x.Id.ToString();
                data.CreateTime =_dateTostring.ChangeToDateString(x.CreateTime);
                data.Version = x.Version;
                data.VersionNum = x.VersionNum;
                data.Price = x.Price.ToString("0.00");
                data.Description = x.Description;
                data.StateDescriotion = x.State.GetDescription();
                data.State = Convert.ToInt32(x.State);
                ListData.Add(data);
            }
            returnList.Data = ListData;
            returnList.TotalRecords = shopManageOutput.TotalRecords;
            returnList.PageSize = shopManageOutput.PageSize;
            returnList.PageIndex = shopManageOutput.PageIndex;
            return returnList;
        }


        public Dictionary<long, string> GetIdAndName()
        {
            return _businessVersionResponsitory.GetIdAndName();
        }
    }
}
