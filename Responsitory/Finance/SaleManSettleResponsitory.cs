using Domain.Finance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsitory.Finance
{
    public class SaleManSettleResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "SaleManSettleAccount";
        /// <summary>
        /// 根据合伙人ID获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<SaleManSettleAccount> GetById(long Id)
        {
            List<SaleManSettleAccount> returnList = new List<SaleManSettleAccount>();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT * FROM "+tableName+" WHERE SaleManId=@Id";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SaleManSettleAccount result = new SaleManSettleAccount();
                            result.Id = Convert.ToInt64(reader["Id"].ToString());
                            result.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            if (!string.IsNullOrEmpty(reader["SettleAmount"].ToString()))
                            {
                                result.SettleAmount = Convert.ToDecimal(reader["SettleAmount"].ToString());
                            }
                            else
                            {
                                result.SettleAmount = 0;
                            }
                            if (!string.IsNullOrEmpty(reader["Month"].ToString()))
                                result.Month = Convert.ToInt32(reader["Month"].ToString());
                            if (!string.IsNullOrEmpty(reader["Year"].ToString()))
                                result.Year = Convert.ToInt32(reader["Year"].ToString());
                         
                            result.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            returnList.Add(result);
                        }
                    }
                }
            }
            #endregion
            return returnList;
        }
    }
}
