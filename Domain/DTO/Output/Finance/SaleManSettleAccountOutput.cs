using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Output.Finance
{
    public class SaleManSettleAccountOutput:BaseOutput
    {

        public string SaleManName { get; set; }

        public string SettleAmount { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
