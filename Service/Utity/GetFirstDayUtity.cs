using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utity
{

    /// <summary>
    /// 获取当月第一天工具类
    /// </summary>
    public class GetFirstDayUtity
    {
        public DateTime GetFirstDay()
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            return Convert.ToDateTime(year + "-" + month + "-1");
        }
    }
}
