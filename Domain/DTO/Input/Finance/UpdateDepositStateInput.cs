using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.Finance
{
    public class UpdateDepositStateInput
    {
        /// <summary>
        /// 对账单ID
        /// </summary>
        public long Id { get; set; }
        public int DepositState { get; set; }

        public string ReceiptNo { get; set; }
    }
}
