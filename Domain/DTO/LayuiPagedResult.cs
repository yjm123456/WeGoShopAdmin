using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.DTO
{
    /// <summary>
    /// layui分页结果
    /// </summary>
    public class LayuiPagedResult
    {
        /// <summary>
        /// 初始化分页成功的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="count">数据条数</param>
        public LayuiPagedResult(object Data, int Count, dynamic TotalRow = null)
        {
            code = 0;
            data = Data;
            count = Count;
            totalRow = TotalRow;
        }

        /// <summary>
        /// 初始化分页失败的数据
        /// </summary>
        /// <param name="errorMsg">错误消息</param>
        public LayuiPagedResult(string errorMsg)
        {
            code = -1;
            msg = errorMsg;
        }

        /// <summary>
        /// 代码，0：成功，-1：失败
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 数据总数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 统计列
        /// </summary>
        public dynamic totalRow { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int? StatusCode { get; set; }

       
    }
}
