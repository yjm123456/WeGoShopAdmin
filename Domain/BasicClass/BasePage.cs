using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BasicClass
{
    /// <summary>
    /// 分页基础类
    /// </summary>
    public class BasePage
    {

        public int Page { get; set; } = 10;

        public int Limit { get; set; } = 1;
    }
}
