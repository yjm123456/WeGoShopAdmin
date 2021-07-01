using Domain.AdminEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UpdateVerifyStateInput
    {
        public long Id { get; set; }

        public AuditStateEnum VerifyState { get; set; }
    }
}
