using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Output.ShopManage
{
    /// <summary>
    /// 证件保存地址返回类
    /// </summary>
    public class ImgUrlOutput
    {
        public string IdCard1Url { get; set; }
        public string IdCardUrl2 { get; set; }
        public string BusinessLicenseUrl { get; set; }
        public string ContractResultUrl { get; set; }
    }
}
