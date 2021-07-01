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
    public class SaleManDepositInfoResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "SaleManDepositInfo";
        /// <summary>
        /// 分页查询合伙人信息 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<SaleManDepositInfo> PagedResult(SaleManDepositInfoPagedInput input)
        {
            PagedResult<SaleManDepositInfo> returnList = new PagedResult<SaleManDepositInfo>();
            List<SaleManDepositInfo> list = new List<SaleManDepositInfo>();
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
                if (input.SaleManId.Value > 0)
                {
                    queryCondition += " and SaleManId=@SaleManId";
                }
            }
            if (input.DepositState.HasValue)
            {
                if (input.DepositState < 2)
                {
                    queryCondition += " and DepositState<=@Deposit";
                }
                else
                {
                    queryCondition += " and DepositState>=2";
                }
            }
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
                        if (input.SaleManId.Value > 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@SaleManId", input.SaleManId.Value));
                        }
                    }
                    if (input.StartDate.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate.Value));
                    }
                    if (input.EndDate.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate.Value));
                    }
                    if (input.DepositState < 2)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Deposit", input.DepositState.Value));
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SaleManDepositInfo depositInfo = new SaleManDepositInfo();
                            depositInfo.Id = Convert.ToInt64(reader["Id"].ToString());
                            depositInfo.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            depositInfo.UpdateTime = Convert.ToDateTime(reader["UpdateTime"].ToString());
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                                depositInfo.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            if (!string.IsNullOrEmpty(reader["DepositMoney"].ToString()))
                                depositInfo.DepositMoney = Convert.ToDecimal(reader["DepositMoney"].ToString());
                            depositInfo.DepositCardNo = reader["DepositCardNo"].ToString();
                            if (!string.IsNullOrEmpty(reader["DepositWay"].ToString()))
                                depositInfo.DepositWay = (DepositWayEnum)Convert.ToInt16(reader["DepositWay"].ToString());
                            if (!string.IsNullOrEmpty(reader["DepositState"].ToString()))
                                depositInfo.DepositState = (DepositStateEnum)Convert.ToInt16(reader["DepositState"].ToString());
                            depositInfo.Remark = reader["Remark"].ToString();
                            depositInfo.ReceivableName = reader["ReceivableName"].ToString();
                            list.Add(depositInfo);
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
                        if (input.SaleManId.Value > 0)
                        {
                            totalcmd.Parameters.Add(new SqlParameter("@SaleManId", input.SaleManId.Value));
                        }
                    }
                    if (input.StartDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate.Value));
                    }
                    if (input.EndDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate.Value));
                    }
                    if (input.DepositState < 2)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@Deposit", input.DepositState.Value));
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
        /// 新增
        /// </summary>
        /// <param name="salemanDepositInfo"></param>
        /// <returns></returns>
        public int Insert(SaleManDepositInfo salemanDepositInfo)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" INSERT INTO " + tableName + "([Id],[SaleManId] ,[DepositMoney] ,[DepositCardNo],[ReceivableName] ,[DepositWay] ,[DepositState] ,[SaleManBalanceDetailsId],[Remark],[CreateTime],[UpdateTime]) VALUES (@Id,@SaleManId,@DepositMoney,@DepositCardNo,@ReceivableName,@DepositWay,@DepositState,@SaleManBalanceDetailsId,@Remark,@CreateTime,@UpdateTime)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", salemanDepositInfo.Id));
                    cmd.Parameters.Add(new SqlParameter("@SaleManId", salemanDepositInfo.SaleManId));
                    cmd.Parameters.Add(new SqlParameter("@DepositMoney", salemanDepositInfo.DepositMoney));
                    cmd.Parameters.Add(new SqlParameter("@DepositCardNo", salemanDepositInfo.DepositCardNo));
                    cmd.Parameters.Add(new SqlParameter("@ReceivableName", salemanDepositInfo.ReceivableName));
                    cmd.Parameters.Add(new SqlParameter("@DepositWay", salemanDepositInfo.DepositWay));
                    cmd.Parameters.Add(new SqlParameter("@DepositState", salemanDepositInfo.DepositState));
                    cmd.Parameters.Add(new SqlParameter("@Remark", salemanDepositInfo.Remark));
                    cmd.Parameters.Add(new SqlParameter("@SaleManBalanceDetailsId", salemanDepositInfo.SaleManBalanceDetailsId));
                    cmd.Parameters.Add(new SqlParameter("@CreateTime", salemanDepositInfo.CreateTime));
                    cmd.Parameters.Add(new SqlParameter("@UpdateTime", salemanDepositInfo.UpdateTime));
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

        public SaleManDepositInfo GetById(long Id)
        {
            SaleManDepositInfo depositInfo = new SaleManDepositInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = "SELECT TOP 1 * FROM " + tableName + " WHERE Id=@Id";
                    cmd.CommandText = pagedSql;

                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            depositInfo.Id = Convert.ToInt64(reader["Id"].ToString());
                            depositInfo.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                                depositInfo.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            if (!string.IsNullOrEmpty(reader["DepositMoney"].ToString()))
                                depositInfo.DepositMoney = Convert.ToDecimal(reader["DepositMoney"].ToString());
                            depositInfo.DepositCardNo = reader["DepositCardNo"].ToString();
                            if (!string.IsNullOrEmpty(reader["DepositWay"].ToString()))
                                depositInfo.DepositWay = (DepositWayEnum)Convert.ToInt16(reader["DepositWay"].ToString());
                            if (!string.IsNullOrEmpty(reader["DepositState"].ToString()))
                                depositInfo.DepositState = (DepositStateEnum)Convert.ToInt16(reader["DepositState"].ToString());
                            depositInfo.Remark = reader["Remark"].ToString();
                            depositInfo.ReceivableName = reader["ReceivableName"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManBalanceDetailsId"].ToString()))
                                depositInfo.SaleManBalanceDetailsId = Convert.ToInt64(reader["SaleManBalanceDetailsId"].ToString());
                        }
                    }
                }
            }

            #endregion
            return depositInfo;
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int UpdateState(UpdateDepositStateInput input)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " set DepositState=@DepositState , UpdateTime=@UpdateTime where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", input.Id));
                    cmd.Parameters.Add(new SqlParameter("@DepositState", input.DepositState));
                    cmd.Parameters.Add(new SqlParameter("@UpdateTime", DateTime.Now));
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
    }
}
