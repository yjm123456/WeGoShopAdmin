using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.FinanceEnums
{
    /// <summary>
    /// 提现状态枚举
    /// </summary>
    public enum DepositStateEnum
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        NotSet=0,
        /// <summary>
        /// 审核通过，待收款
        /// </summary>
        [Description("待收款")]
        Passed =1,
        /// <summary>
        /// 确认到账
        /// </summary>
        [Description("已到账")]
        ConfirmToAccount =2,
        /// <summary>
        /// 作废
        /// </summary>
        [Description("作废")]
        Cancel =3,
        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        NotPassed =999,
    }
}
