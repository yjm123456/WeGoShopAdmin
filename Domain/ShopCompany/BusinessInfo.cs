using Domain.AdminEnum;
using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 商户信息基础类
    /// </summary>
    public class BusinessInfo 
    {
        /// <summary>
        /// ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 登陆名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo{ get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public InstitutionalTypeEnum InstitutionalType { get; set; }
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrganizingInstitution { get; set; }

        /// <summary>
        /// 开户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string DepositBank { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LiasonManName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string FixPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 推荐人ID
        /// </summary>
        public long SaleManId { get; set; }
        /// <summary>
        /// 推荐人
        /// </summary>
        public string SaleManName { get; set; }
        /// <summary>
        /// 选择版本
        /// </summary>
        public AccountVersionEnum Version { get; set; }

        /// <summary>
        /// 使用范围
        /// </summary>
        public ShopUsingTypeEnum ApplicationRange { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStateEnum VerifyState { get; set; }
        /// <summary>
        /// 商户状态
        /// </summary>
        public ShopUserStateEnum ShopUserState { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime DueTime { get; set; }

        public string Remark { get; set; }

        public long BusinessId { get; set; }
        /// <summary>
        /// 身份证人像上传地址
        /// </summary>
        public string IdentityCardImgUrl1 { get; set; }
        /// <summary>
        /// 身份证国徽上传地址
        /// </summary>
        public string IdentityCardImgUrl2 { get; set; }
        /// <summary>
        /// 营业执照上传地址
        /// </summary>
        public string BusinessLicenseImgUrl { get; set; }
        /// <summary>
        /// 合同回执上传地址
        /// </summary>
        public string ContractResultImgUrl { get; set; }
    }
}
