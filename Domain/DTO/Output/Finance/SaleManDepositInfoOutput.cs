using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Output.Finance
{
    public class SaleManDepositInfoOutput:BaseOutput
    {

        public string SaleManName { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public string DepositMoney { get; set; }

        /// <summary>
        /// 提现账号
        /// </summary>
        public string DepositCardNo { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } 

        /// <summary>
        /// 账号类型
        /// </summary>
        public string DepositWay { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public string DepositState { get; set; }
        public int DepositStateInt { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        public string ReceivableName { get; set; }

        public string Remark { get; set; }
    }
}
