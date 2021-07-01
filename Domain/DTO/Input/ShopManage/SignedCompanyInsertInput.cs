using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.ShopManage
{
    public class BusinessInfoInsertInput
    {
        public string AccountName { get; set; }
        public string BankAccount { get; set; }
        public string CompanyName { get; set; }
        public string DepositBank { get; set; }
        public string Email { get; set; }
        public string FixPhone { get; set; }
        public string IdentityCard { get; set; }
        public int? InstitutionalType { get; set; }
        public string LiasonManName { get; set; }

        public int? Version { get; set; }
        public string OrganizingInstitution { get; set; }
        public string Phone { get; set; }
        public string SaleManId { get; set; }
        public int? Scope { get; set; }
        public string comments { get; set; }
        public string VerificationCode { get; set; }
    }
}
