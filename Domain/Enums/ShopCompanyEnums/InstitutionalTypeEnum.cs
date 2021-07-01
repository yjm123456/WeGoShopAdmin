using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AdminEnum
{
    /// <summary>
    /// 组织机构枚举类
    /// </summary>
    public enum InstitutionalTypeEnum
    {
        /// <summary>
        /// 企业法人
        /// </summary>
        [Description("企业法人")]
        EnterpriseLegalPerson = 1,
        /// <summary>
        /// 个体工商户
        /// </summary>
        [Description("个体工商户")]
        IndividualBusiness = 2,
        /// <summary>
        /// 企业媒体
        /// </summary>
        [Description("企业媒体")]
        CorporateMedia =3,
        /// <summary>
        /// 事业单位媒体
        /// </summary>
        [Description("事业单位媒体")]
        IndustrialMedia =4,
        /// <summary>
        /// 政府
        /// </summary>
        [Description("政府")]
        Goverment =5,
        /// <summary>
        /// 事业单位
        /// </summary>
        [Description("事业单位")]
        Industrial =6,
        /// <summary>
        /// 非营利组织(慈善基金会、大使馆、国外政府机构)
        /// </summary>
        [Description("非营利组织(慈善基金会、大使馆、国外政府机构)")]
        NonProfitOrganization = 7,
        /// <summary>
        /// 民办非企业单位
        /// </summary>
        [Description("民办非企业单位")]
        PrivateNonEnterpriseUnits =8,
        /// <summary>
        /// 社会团体
        /// </summary>
        [Description("社会团体")]
        SocialOrganization =9,
        /// <summary>
        /// 其他组织
        /// </summary>
        [Description("其他组织")]
        OtherOrganization =10
    }
}
