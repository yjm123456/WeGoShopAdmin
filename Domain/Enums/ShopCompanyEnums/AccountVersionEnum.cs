using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdminEnum
{
    /// <summary>
    /// 账户状态枚举类
    /// </summary>
    public enum AccountVersionEnum
    {
        /// <summary>
        /// 试用版
        /// </summary>
        [Description("试用版")]
        OnTrial=0,
        /// <summary>
        /// 基础版
        /// </summary>
        [Description("基础版")]
        BasicEdition =1,
        /// <summary>
        /// 标准版
        /// </summary>
        [Description("标准版")]
        StandardEdition =2,
        /// <summary>
        /// 旗舰版
        /// </summary
        [Description("旗舰版")]
        UltimateEdition =3,
        /// <summary>
        /// 商户自建
        /// </summary
        [Description("商户自建")]
        ShopUser = 4
    }
}
