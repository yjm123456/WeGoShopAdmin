using Domain.BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Input.ServerPath
{
    public class UpdateServerPathInput : BaseClass
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string ServerIP { get; set; }

        /// <summary>
        /// 端口号，多个逗号隔开
        /// </summary>
        public string PingPort { get; set; }

        /// <summary>
        /// 服务器密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 带宽
        /// </summary>
        public string BandWidth { get; set; }

        /// <summary>
        /// cpu信息（例：i5 6500 3.2hz）
        /// </summary>
        public string CPUInfomation { get; set; }

        /// <summary>
        /// 运行内存信息（例：双通道，16G内存）
        /// </summary>
        public string RAMInformation { get; set; }
        /// <summary>
        /// 磁盘信息
        /// </summary>

        public string DiskInformation { get; set; }

        /// <summary>
        /// 服务器购买日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 服务器到期日期
        /// </summary>
        public DateTime EndDate { get; set; }

        public string Remark { get; set; }
    }
}
