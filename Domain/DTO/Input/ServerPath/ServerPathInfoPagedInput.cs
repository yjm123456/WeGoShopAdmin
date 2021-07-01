using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.ServerPath
{
    public class ServerPathInfoPagedInput: BasePage
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long? BusinessId { get; set; }

        public string IPPort { get; set; }
        public string SalesManId { get; set; }
    }
}
