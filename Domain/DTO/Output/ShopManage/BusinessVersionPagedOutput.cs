using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Output.ShopManage
{
    public class BusinessVersionPagedOutput:BaseOutput
    {
        public string Version { get; set; }

        public int VersionNum { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public int State { get; set; }
        public string StateDescriotion { get; set; }
    }
}
