using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.FinanceEnums
{
    /// <summary>
    /// 提现渠道
    /// </summary>
    public enum DepositWayEnum
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        AliPay=1,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat =2,
        /// <summary>
        /// 银行卡
        /// </summary>
        [Description("银行卡")]
        BankCard =3
    }
}
