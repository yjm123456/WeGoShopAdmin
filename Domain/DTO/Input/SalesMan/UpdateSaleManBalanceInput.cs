using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.SalesMan
{
    public class UpdateSaleManBalanceInput 
    {
        public long Id { get; set; }

        public decimal InitBalance { get; set; }

        public decimal NewBalance { get; set; }

        public string Remark { get; set; }
        public string Creator { get; set; }
    }
}
