using Domain.DTO.Output.Finance;
using Responsitory.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Finance
{
    /// <summary>
    /// 合伙人结算服务层
    /// </summary>
    public class SaleManSettleAccountService
    {
        private readonly SaleManSettleResponsitory _saleManSettleResponsitory = new SaleManSettleResponsitory();
        /// <summary>
        /// 根据合伙人ID获取当月结算金额
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public decimal GetSettleAmountById(long Id)
        {
            SaleManSettleAccountOutput output = new SaleManSettleAccountOutput();
            var result = _saleManSettleResponsitory.GetById(Id);
            if (result.Count == 0)
                return 0;
            var thisMonth = DateTime.Now.Month;
            var salemanSettleAccount= result.Where(z => z.Month == thisMonth).FirstOrDefault();
            if (salemanSettleAccount != null)
            {
                return salemanSettleAccount.SettleAmount;
            }
            else { return 0; }
        }
    }
}
