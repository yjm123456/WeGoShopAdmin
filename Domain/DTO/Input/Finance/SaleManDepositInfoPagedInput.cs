using Domain.BasicClass;
using Domain.Enums.FinanceEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.Finance
{
    public class SaleManDepositInfoPagedInput:BasePage
    {
        public long? SaleManId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? DepositState { get; set; }
    }
}
