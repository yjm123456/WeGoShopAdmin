using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.FinanceEnums
{
    public enum SaleManBalanceDetailsStateEnum
    {
        /// <summary>
        /// 未启用
        /// </summary>
        [Description("未启用")]
        UnUsed = 0,
        /// <summary>
        /// 可用
        /// </summary>
        [Description("可用")]
        Using = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Forbidden = 2
    }
}
