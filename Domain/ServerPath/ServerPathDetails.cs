using Domain.BasicClass;
using Domain.Enums.WeChatStationEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServerPath
{
    public class ServerPathDetails:BaseClass
    {
        /// <summary>
        /// 所属服务器ID
        /// </summary>
        public long ServerPathId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 绑定微信公众号名称
        /// </summary>
        public string BindWechatStationName { get; set; }
        /// <summary>
        /// 绑定微信公众号登陆名
        /// </summary>
        public string BindWechatStationUserName { get; set; }
        /// <summary>
        /// 绑定微信公众号密码
        /// </summary>
        public string BindWechatStationPassWord { get; set; }
        /// <summary>
        /// 绑定微信公众号类型
        /// </summary>
        public WechatStationTypeEnum BindWechatStationType { get; set; }
        /// <summary>
        /// 绑定微信公众号appId
        /// </summary>
        public string BindWechatStationAppId { get; set; }
        /// <summary>
        /// 绑定微信公众号AppSecret
        /// </summary>
        public string BindWechatStationAppSecret { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
