using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdminEnum
{
    /// <summary>
    /// 审核状态枚举类
    /// </summary>
    public enum AuditStateEnum
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [System.ComponentModel.Description("未审核")]
        NotAudit =0,


        /// <summary>
        /// 审核通过 
        /// </summary>
        [System.ComponentModel.Description("审核通过")]
        Passed =1,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [System.ComponentModel.Description("审核不通过")]
        NotPassed =2
    }
}
