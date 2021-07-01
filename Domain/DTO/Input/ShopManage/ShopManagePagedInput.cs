using Domain.AdminEnum;
using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.ShopManage
{
    /// <summary>
    /// 商户信息查询输入类
    /// </summary>
    public class ShopManagePagedInput:BasePage
    {

        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long? SalesManId { get; set; }

        public long? BusinessId { get; set; }

        public AccountVersionEnum? Version { get; set; }

        public ShopUserStateEnum? ShopUserState { get; set; }

        public AuditStateEnum? AuditState { get; set; }
    }
}
