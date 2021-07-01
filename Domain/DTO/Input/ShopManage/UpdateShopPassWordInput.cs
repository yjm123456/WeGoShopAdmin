using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.ShopManage
{
    /// <summary>
    /// 商户修改密码输入类
    /// </summary>
    public class UpdateShopPassWordInput
    {
        public long Id { get; set; }

        public string NewPassWord { get; set; }

        public string UserRole { get; set; }
    }
}
