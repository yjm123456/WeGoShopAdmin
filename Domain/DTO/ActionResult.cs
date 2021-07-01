using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    /// <summary>
    /// 普通返回结果
    /// </summary>
    public class ActionResult
    {
        public int StatusCode { get; set; } = 1;

        public string Msg { get; set; } = "OK";

        public string Error { get; set; }
        
    }
}
