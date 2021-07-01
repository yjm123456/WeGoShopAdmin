using Domain.BasicClass;
using Domain.Enums.InformationEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Information
{
    /// <summary>
    /// 消息管理基础类
    /// </summary>
    public class InformationInfo:BaseClass
    {

        /// <summary>
        /// 发送目标
        /// </summary>
        public SendTargetEnum SendTarget { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        public InformationStateEnum InformationState { get; set; }
    }
}
