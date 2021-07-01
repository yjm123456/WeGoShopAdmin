using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Output.Finance
{
    public class SalesManBalanceDetailsInfoOutput : BaseOutput
    {

        /// <summary>
        /// 合伙人
        /// </summary>
        public string SaleManName { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// 期初余额
        /// </summary>
        public string InitBalance { get; set; }
        /// <summary>
        /// 期末金额
        /// </summary>
        public string LastBalance { get; set; }
        /// <summary>
        /// 操作金额
        /// </summary>
        public string thisOperateBalance { get; set; }
        /// <summary>
        /// 操作账户
        /// </summary>
        public string OperationCardNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
