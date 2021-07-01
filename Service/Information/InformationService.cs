using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO.Input;
using Domain.DTO.Output.Information;
using Domain.Enums.InformationEnum;
using Responsitory.Information;
using Service.Utity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Information
{
    /// <summary>
    /// 消息管理服务层
    /// </summary>
    public class InformationService
    {
        private readonly InformationResponsitory _InformationResponsitory = new InformationResponsitory();
        private readonly GetDateToString _dateTostring = new Utity.GetDateToString();
        private readonly SnowflakeIDcreator _snowFlake = new SnowflakeIDcreator();
        public PagedResult<InformationInfoPagedOutput> PagedList(InformatioInfoPagedInput input)
        {
            PagedResult<InformationInfoPagedOutput> returnList = new PagedResult<InformationInfoPagedOutput>();

            var SalesManInfoOutput = _InformationResponsitory.PagedResult(input);
            //获取的值转换
            List<InformationInfoPagedOutput> ListData = new List<InformationInfoPagedOutput>();
            foreach (var x in SalesManInfoOutput.Data)
            {
                InformationInfoPagedOutput data = new InformationInfoPagedOutput();
                data.Id = x.Id.ToString();
                data.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
                data.Content = x.Content;
                data.SendTarget = x.SendTarget.GetDescription();
                data.InformationState = x.InformationState.GetDescription();
            }
            returnList.Data = ListData;
            returnList.TotalRecords = SalesManInfoOutput.TotalRecords;
            returnList.PageSize = SalesManInfoOutput.PageSize;
            returnList.PageIndex = SalesManInfoOutput.PageIndex;
            return returnList;
        }

        /// <summary>
        /// 获取根据发送方向获取最新一条通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InformationInfoPagedOutput GetFirstBySendTarget(SendTargetEnum input)
        {
           InformationInfoPagedOutput returnResult = new InformationInfoPagedOutput();

            var x = _InformationResponsitory.GetFirstBySendTarget(input);
            //获取的值转换
            returnResult.Id = x.Id.ToString();
            returnResult.CreateTime = _dateTostring.ChangeToDateString(x.CreateTime);
            returnResult.Content = x.Content;
            returnResult.SendTarget = x.SendTarget.GetDescription();
            returnResult.InformationState = x.InformationState.GetDescription();
            return returnResult;
        }
    }
}
