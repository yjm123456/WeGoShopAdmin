using Domain.BasicClass;
using Domain.Enums.SaleManEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SalesMan
{
    public class SaleManLevel:BaseClass
    {
        /// <summary>
        /// 等级名称
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 分成比例
        /// </summary>
        public int DistributionRate { get; set; }

        /// <summary>
        /// 等级描述(备注)
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public SaleManLevelStateEnum State { get; set; }

        #region 达到该等级的条件
        /// <summary>
        /// 标准版客户量
        /// </summary>
        public int StandardEditionNum { get; set; }

        /// <summary>
        /// 基础版客户量
        /// </summary>
        public int BasicEditionNum { get; set; }
        /// <summary>
        /// 旗舰版客户量
        /// </summary>
        public int UltimateEdition { get; set; }
        #endregion
    }
}
