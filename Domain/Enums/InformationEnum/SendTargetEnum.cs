using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.InformationEnum
{
    /// <summary>
    /// 消息发送目标枚举类
    /// </summary>
    public enum SendTargetEnum
    {
        /// <summary>
        /// 不发送
        /// </summary>

        [Description("不发送")]
        NotSend =0,
        /// <summary>
        /// 商户
        /// </summary>
        [Description("商户")]
        Shop =1,
        /// <summary>
        /// 合伙人
        /// </summary>
        [Description("合伙人")]
        SalesMan =2
    }
}
