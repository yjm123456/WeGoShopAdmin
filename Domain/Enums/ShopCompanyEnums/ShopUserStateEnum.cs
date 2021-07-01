using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdminEnum
{
    /// <summary>
    /// 商户状态枚举类
    /// </summary>
    public enum ShopUserStateEnum
    {/// <summary>
     /// 未启动
     /// </summary>
        [Description("未启动")]
        NotStarted = 0,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal=1,

        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        OutOfDate = 999,
        /// <summary>
        /// 已禁用
        /// </summary>
        [Description("已禁用")]
        Forbidden = 1000
    }
}
