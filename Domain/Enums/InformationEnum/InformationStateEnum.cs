using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums.InformationEnum
{
    /// <summary>
    /// 消息状态枚举值
    /// </summary>
    public enum InformationStateEnum
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disabled=0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable =1
    }
}
