using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    /// <summary>
    /// 用户基础类
    /// </summary>
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }
    }
}
