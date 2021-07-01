using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.SalesMan
{
    public class SalesManInfoPagedInput:BasePage
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string SaleManName { get; set; }
    }
}
