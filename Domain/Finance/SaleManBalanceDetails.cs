using Domain.BasicClass;
using Domain.Enums.FinanceEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Finance
{
    /// <summary>
    /// 合伙人账单明细类
    /// </summary>
    public class SaleManBalanceDetails:BaseClass
    {
        /// <summary>
        /// 合伙人ID
        /// </summary>
        public long SaleManId { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// 期初余额
        /// </summary>
        public decimal InitBalance { get; set; }
        /// <summary>
        /// 期末金额
        /// </summary>
        public decimal LastBalance { get; set; }
        /// <summary>
        /// 操作金额
        /// </summary>
        public decimal thisOperateBalance { get; set; }
        /// <summary>
        /// 操作账户
        /// </summary>
        public string OperationCardNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Creator { get; set; }

        public SaleManBalanceDetailsStateEnum State { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
