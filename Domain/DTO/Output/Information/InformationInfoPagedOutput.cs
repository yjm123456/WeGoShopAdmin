using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Output.Information
{
    public class InformationInfoPagedOutput:BaseOutput
    {
        /// <summary>
        /// 发送目标
        /// </summary>
        public string SendTarget { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        public string InformationState { get; set; }
    }
}
