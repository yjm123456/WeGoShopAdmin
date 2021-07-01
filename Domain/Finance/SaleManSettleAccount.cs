using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Finance
{
    /// <summary>
    /// 合伙人账户结算基础类
    /// </summary>
    public class SaleManSettleAccount : BaseClass
    {
        public long SaleManId { get; set; }

        public decimal SettleAmount { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
