using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BasicClass
{
    /// <summary>
    /// json返回类
    /// </summary>
    public class ReturnClass
    {

        public int StatusCode { get; set; } = 1;

        public string Msg { get; set; } = "OK";

        public string Error { get; set; }

    }
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">分页结果返回数据的数据类型</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// 分页页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 数据分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}
