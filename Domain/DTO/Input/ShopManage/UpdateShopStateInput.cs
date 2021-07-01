using Domain.AdminEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.ShopManage
{
    /// <summary>
    /// 商户修改状态输入类
    /// </summary>
    public class UpdateShopStateInput
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 修改为目标状态
        /// </summary>
        public ShopUserStateEnum UserState { get; set; }
    }
}
