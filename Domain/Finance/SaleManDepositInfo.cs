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
    /// 客户提现记录基础类
    /// </summary>
    public class SaleManDepositInfo:BaseClass
    {
        public long SaleManId { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal DepositMoney { get; set; }

        /// <summary>
        /// 提现账户
        /// </summary>
        public string DepositCardNo { get; set; }

        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public DepositWayEnum DepositWay { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public DepositStateEnum DepositState { get; set; }

        /// <summary>
        /// 收款人
        /// </summary>
        public string ReceivableName { get; set; }

        public long SaleManBalanceDetailsId { get; set; }
        public string Remark { get; set; }

    }
}
