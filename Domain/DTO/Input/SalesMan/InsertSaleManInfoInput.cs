using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.SalesMan
{
    public class InsertSaleManInfoInput
    {
        public string SaleManName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public long LevelId { get; set; }
        public decimal Balance { get; set; }
        public string Address { get; set; }
    }
}
