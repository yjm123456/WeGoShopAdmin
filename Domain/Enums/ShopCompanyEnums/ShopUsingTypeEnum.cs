using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdminEnum
{
    /// <summary>
    /// 商户注册使用范围枚举类
    /// </summary>
    public enum ShopUsingTypeEnum
    {
        /// <summary>
        /// 电商
        /// </summary>
        [Description("电商")]
        ECommerce=1,

        /// <summary>
        /// 线下店铺
        /// </summary>
        [Description("线下店铺")]
        OfflineStore =2,
        /// <summary>
        /// 运营团队
        /// </summary>
        [Description("运营团队")]
        OperationTeam =3
    }
}
