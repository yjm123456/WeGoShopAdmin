using Domain.BasicClass;
using Domain.DTO.Input.Finance;
using Domain.Enums.FinanceEnums;
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
    public class BalanceInfoResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "SaleManBalanceDetails";
        /// <summary>
        /// 分页查询合伙人信息 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<SaleManBalanceDetails> PagedResultBalance(SalesManBalanceDetailsInfoPagedInput input)
        {
            PagedResult<SaleManBalanceDetails> returnList = new PagedResult<SaleManBalanceDetails>();
            List<SaleManBalanceDetails> list = new List<SaleManBalanceDetails>();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";
            if (input.StartDate.HasValue)
            {
                queryCondition = " and CreateTime>=@StartDate";
            }
            if (input.EndDate.HasValue)
            {
                queryCondition += " and CreateTime<=@EndDate";
            }
            if (input.SaleManId.HasValue)
            {
                queryCondition += " and SaleManId=@SalesManId";
            }
            queryCondition += " and State=1";
            #endregion

            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = pagedUtity.PagedSql(tableName, input.Page, input.Limit, queryCondition, "CreateTime desc");
                    cmd.CommandText = pagedSql;
                    if (input.SaleManId.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@SalesManId", input.SaleManId.Value));
                    }
                    if (input.StartDate.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate.Value));
                    }
                    if (input.EndDate.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate.Value));
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SaleManBalanceDetails balanceDetails = new SaleManBalanceDetails();
                            balanceDetails.Id = Convert.ToInt64(reader["Id"].ToString());
                            balanceDetails.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            balanceDetails.ReceiptNo = reader["ReceiptNo"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                                balanceDetails.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            if (!string.IsNullOrEmpty(reader["InitBalance"].ToString()))
                                balanceDetails.InitBalance = Convert.ToDecimal(reader["InitBalance"].ToString());
                            if (!string.IsNullOrEmpty(reader["LastBalance"].ToString()))
                                balanceDetails.LastBalance = Convert.ToDecimal(reader["LastBalance"].ToString());
                            if (!string.IsNullOrEmpty(reader["thisOperateBalance"].ToString()))
                                balanceDetails.thisOperateBalance = Convert.ToDecimal(reader["thisOperateBalance"].ToString());
                            balanceDetails.Creator = reader["Creator"].ToString();
                            balanceDetails.OperationCardNo = reader["OperationCardNo"].ToString();
                            balanceDetails.Remark = reader["Remark"].ToString();
                            list.Add(balanceDetails);
                        }
                    }
                }
            }

            #endregion

            #region 读取总条数

            int totalCount = 0;
            var totalSql = "SELECT COUNT(*) FROM " + tableName + " where 1=1";
            if (!string.IsNullOrEmpty(queryCondition))
                totalSql += queryCondition;
            using (SqlConnection totalconn = new SqlConnection(connection))
            {
                using (SqlCommand totalcmd = totalconn.CreateCommand())
                {
                    totalconn.Open();
                    totalcmd.CommandText = totalSql;
                    if (input.SaleManId.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@SalesManId", input.SaleManId.Value));
                    }
                    if (input.StartDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate.Value));
                    }
                    if (input.EndDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate.Value));
                    }
                    using (SqlDataReader reader = totalcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalCount = Convert.ToInt32(reader[0].ToString());
                        }
                    }
                }
            }
            #endregion

            returnList.PageIndex = input.Page;
            returnList.PageSize = input.Limit;
            returnList.Data = list;
            returnList.TotalRecords = totalCount;
            return returnList;
        }


        /// <summary>
        /// 根据ID获取账单明细数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SaleManBalanceDetails GetById(long Id)
        {
            SaleManBalanceDetails balanceDetails = new SaleManBalanceDetails();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";

            queryCondition += " and Id=@Id";
            #endregion

            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @"SELECT TOP 1 * FROM " + tableName + " WHERE Id=@Id";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            balanceDetails.Id = Convert.ToInt64(reader["Id"].ToString());
                            balanceDetails.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            balanceDetails.ReceiptNo = reader["ReceiptNo"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                                balanceDetails.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            if (!string.IsNullOrEmpty(reader["InitBalance"].ToString()))
                                balanceDetails.InitBalance = Convert.ToDecimal(reader["InitBalance"].ToString());
                            if (!string.IsNullOrEmpty(reader["LastBalance"].ToString()))
                                balanceDetails.LastBalance = Convert.ToDecimal(reader["LastBalance"].ToString());
                            if (!string.IsNullOrEmpty(reader["thisOperateBalance"].ToString()))
                                balanceDetails.thisOperateBalance = Convert.ToDecimal(reader["thisOperateBalance"].ToString());
                            balanceDetails.Creator = reader["Creator"].ToString();
                            balanceDetails.OperationCardNo = reader["OperationCardNo"].ToString();
                            balanceDetails.Remark = reader["Remark"].ToString();
                        }
                    }
                }
            }

            #endregion
            return balanceDetails;
        }

        /// <summary>
        /// 验证是否存在重复单据
        /// </summary>
        /// <param name="ReceiptNo"></param>
        /// <returns></returns>
        public List<SaleManBalanceDetails> GetReceiptNo(string ReceiptNo)
        {
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";

            queryCondition += " and ReceiptNo=@ReceiptNo";
            #endregion

            #region 查询数据
            List<SaleManBalanceDetails> resultList = new List<SaleManBalanceDetails>();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @"SELECT TOP 1 * FROM " + tableName + " WHERE ReceiptNo=@ReceiptNo";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@ReceiptNo", ReceiptNo));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SaleManBalanceDetails balanceDetails = new SaleManBalanceDetails();
                            balanceDetails.Id = Convert.ToInt64(reader["Id"].ToString());
                            balanceDetails.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            balanceDetails.ReceiptNo = reader["ReceiptNo"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                                balanceDetails.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            if (!string.IsNullOrEmpty(reader["InitBalance"].ToString()))
                                balanceDetails.InitBalance = Convert.ToDecimal(reader["InitBalance"].ToString());
                            if (!string.IsNullOrEmpty(reader["LastBalance"].ToString()))
                                balanceDetails.LastBalance = Convert.ToDecimal(reader["LastBalance"].ToString());
                            if (!string.IsNullOrEmpty(reader["thisOperateBalance"].ToString()))
                                balanceDetails.thisOperateBalance = Convert.ToDecimal(reader["thisOperateBalance"].ToString());
                            balanceDetails.Creator = reader["Creator"].ToString();
                            balanceDetails.OperationCardNo = reader["OperationCardNo"].ToString();
                            balanceDetails.Remark = reader["Remark"].ToString();
                            resultList.Add(balanceDetails);
                        }
                    }
                }
            }

            #endregion
            return resultList;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="saleManBalanceDetails"></param>
        /// <returns></returns>
        public int Insert(SaleManBalanceDetails saleManBalanceDetails)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" INSERT INTO " + tableName + "([Id],[SaleManId] ,[ReceiptNo] ,[InitBalance],[LastBalance] ,[OperationCardNo] ,[thisOperateBalance],[Creator],[State],[Remark],[CreateTime]) VALUES (@Id,@SaleManId,@ReceiptNo,@InitBalance,@LastBalance,@OperationCardNo,@thisOperateBalance,@Creator,@State,@Remark,@CreateTime)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", saleManBalanceDetails.Id));
                    cmd.Parameters.Add(new SqlParameter("@SaleManId", saleManBalanceDetails.SaleManId));
                    cmd.Parameters.Add(new SqlParameter("@ReceiptNo", saleManBalanceDetails.ReceiptNo));
                    cmd.Parameters.Add(new SqlParameter("@InitBalance", saleManBalanceDetails.InitBalance));
                    cmd.Parameters.Add(new SqlParameter("@LastBalance", saleManBalanceDetails.LastBalance));
                    cmd.Parameters.Add(new SqlParameter("@OperationCardNo", saleManBalanceDetails.OperationCardNo));
                    cmd.Parameters.Add(new SqlParameter("@thisOperateBalance", saleManBalanceDetails.thisOperateBalance));
                    cmd.Parameters.Add(new SqlParameter("@Creator", saleManBalanceDetails.Creator));
                    cmd.Parameters.Add(new SqlParameter("@State", saleManBalanceDetails.State));
                    cmd.Parameters.Add(new SqlParameter("@Remark", saleManBalanceDetails.Remark));
                    cmd.Parameters.Add(new SqlParameter("@CreateTime", saleManBalanceDetails.CreateTime));
                    res = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return res;

        }

        public int UpdateState(long Id, SaleManBalanceDetailsStateEnum state)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " set State=@State  where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    cmd.Parameters.Add(new SqlParameter("@State", state));
                    result = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }

        public int UpdateReceiptNo(long Id, string ReceiptNo)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " set ReceiptNo=@ReceiptNo  where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    cmd.Parameters.Add(new SqlParameter("@ReceiptNo", ReceiptNo));
                    result = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return result;
        }
    }
}
