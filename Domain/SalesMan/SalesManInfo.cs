using Domain.AdminEnum;
using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SalesMan
{
    public class SalesManInfo:BaseClass
    {
        /// <summary>
        /// 合伙人名称
        /// </summary>
        public string SaleManName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public long LevelId { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ShopUserStateEnum State { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal Balance { get; set; }
    }
}
