using Domain.BasicClass;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IAdminLoginService
    {
        ReturnClass Login(AdminQueryInput input);
    }
}
