using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.WeChatStationEnum
{
    /// <summary>
    /// 公众号类型枚举类
    /// </summary>
    public enum WechatStationTypeEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>

        [Description("未设置")]
        NotSet =0,
        /// <summary>
        /// 订阅号
        /// </summary>

        [Description("订阅号")]
        SubscriptionNumber =1,
        /// <summary>
        /// 服务号
        /// </summary>

        [Description("服务号")]
        ServiceNumber =2,
        /// <summary>
        /// 小程序
        /// </summary>
        [Description("小程序")]
        TinyProcedure =3,

        /// <summary>
        /// 企业微信
        /// </summary>
        [Description("企业微信")]
        CompanyWeChat =4
    }
}
