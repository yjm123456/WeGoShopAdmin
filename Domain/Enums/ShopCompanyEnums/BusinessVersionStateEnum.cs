using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.ShopCompanyEnums
{
    public enum BusinessVersionStateEnum
    {
        /// <summary>
        /// 未启用
        /// </summary>
        [System.ComponentModel.Description("未启用")]
        NotUsed =0,
        /// <summary>
        /// 已启用
        /// </summary>
        [System.ComponentModel.Description("已启用")]
        Using =1
    }
}
