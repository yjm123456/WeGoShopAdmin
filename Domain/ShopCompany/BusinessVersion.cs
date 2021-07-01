using Domain.BasicClass;
using Domain.Enums.ShopCompanyEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ShopCompany
{
    public class BusinessVersion : BaseClass
    {
        public string Version { get; set; }

        public int VersionNum { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public BusinessVersionStateEnum State { get; set; }
    }
}
