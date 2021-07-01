using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.SalesMan
{
    public class SalesManLevelPagedInput : BasePage
    {
        /// <summary>
        /// 内容（模糊查询）
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 等级名称（模糊查询）
        /// </summary>
        public string LevelName { get; set; }
    }
}
