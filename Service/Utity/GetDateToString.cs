using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utity
{
    public class GetDateToString
    {
        /// <summary>
        /// 时间转换成年月日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ChangeToDateString(DateTime dt)
        {
           return dt.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 时间转换成时分秒
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ChangeToSecondString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// 获取选中当天的23点59分59秒
        /// </summary>
        public DateTime GetLastSecond(DateTime dt)
        {
            return Convert.ToDateTime(dt.AddDays(1).ToString("D").ToString()).AddSeconds(-1);
        }
    }
}
