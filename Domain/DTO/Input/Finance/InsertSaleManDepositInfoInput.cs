using Domain.Enums.FinanceEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.Finance
{
    public class InsertSaleManDepositInfoInput
    {

        public long SaleManId { get; set; }


        public decimal Balance { get; set; }


        public DepositWayEnum DepositWay { get; set; }

        public string DepositAccount { get; set; }

        public string PayeeName { get; set; }
    }

}
