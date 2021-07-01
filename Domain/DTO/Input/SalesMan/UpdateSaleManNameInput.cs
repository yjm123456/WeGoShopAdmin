using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.SalesMan
{
    public class UpdateSaleManNameInput
    {
        public long Id { get; set; }
        public string NewSaleManName { get; set; }

    }
}
