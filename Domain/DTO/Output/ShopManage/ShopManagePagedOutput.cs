using Domain.AdminEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Output.ShopManage
{
    /// <summary>
    /// 商户信息分页输出类
    /// </summary>
    public class ShopManagePagedOutput: BaseOutput
    {
        public string CompanyName { get; set; }

        public string LiasonManName { get; set; }

        public string Phone { get; set; }

        public string SaleManName { get; set; }

        public string Version { get; set; }

        public string ApplicationRange { get; set; }

        public int VerifyState { get; set; }
        public string VerifyStateDescription { get; set; }

        public ShopUserStateEnum ShopUserState { get; set; }


        public string  HasUsedTime { get; set; }

        public string Remark { get; set; }

        //详细信息
        public string IdentityCard { get; set; }
        public int InstitutionalType { get; set; }
        public string InstitutionalTypeDescription { get; set; }
        public string OrganizingInstitution { get; set; }
        public string AccountName { get; set; }
        public string BankAccount { get; set; }
        public string DepositBank { get; set; }
        public string UserName { get; set; }
        public DateTime DueTime{ get; set; }
        public string Email { get; set; }

        public string ContractNo { get; set; }
        public string BusinessId { get; set; }

        //证件信息
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
