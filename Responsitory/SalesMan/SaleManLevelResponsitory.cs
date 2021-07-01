using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO.Input.SalesMan;
using Domain.Enums.SaleManEnums;
using Domain.SalesMan;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsitory.SalesMan
{
    public class SaleManLevelResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "SaleManLevel";


        public PagedResult<SaleManLevel> PagedResult(SalesManLevelPagedInput input)
        {
            PagedResult<SaleManLevel> returnList = new PagedResult<SaleManLevel>();
            List<SaleManLevel> list = new List<SaleManLevel>();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";
            if (!string.IsNullOrEmpty(input.LevelName))
            {
                queryCondition += " and LevelName like @LevelName";
            }
            if (!string.IsNullOrEmpty(input.Condition))
            {
                queryCondition += " and Condition like @Condition";
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
                    
                    if (!string.IsNullOrEmpty(input.LevelName))
                    {
                        cmd.Parameters.Add(new SqlParameter("@LevelName", "%" + input.LevelName + "%"));
                    }
                    if (!string.IsNullOrEmpty(input.Condition))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Condition", "%" + input.Condition + "%"));
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SaleManLevel salesManlevel = new SaleManLevel();
                            salesManlevel.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesManlevel.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesManlevel.Level = Convert.ToInt16(reader["Level"].ToString());
                            salesManlevel.LevelName = reader["LevelName"].ToString();
                            salesManlevel.Remark = reader["Remark"].ToString();
                            if (!string.IsNullOrEmpty(reader["DistributionRate"].ToString()))
                                salesManlevel.DistributionRate = Convert.ToInt16(reader["DistributionRate"].ToString());
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesManlevel.State = (SaleManLevelStateEnum)Convert.ToInt32(reader["State"].ToString());
                            salesManlevel.StandardEditionNum = Convert.ToInt16(reader["StandardEditionNum"].ToString());
                            salesManlevel.BasicEditionNum = Convert.ToInt16(reader["BasicEditionNum"].ToString());
                            salesManlevel.UltimateEdition = Convert.ToInt16(reader["UltimateEdition"].ToString());
                            list.Add(salesManlevel);

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
                    if (!string.IsNullOrEmpty(input.LevelName))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@LevelName", "%" + input.LevelName + "%"));
                    }
                    if (!string.IsNullOrEmpty(input.Condition))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@Condition", "%" + input.Condition + "%"));
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

        public SaleManLevel GetByLevel(int Level)
        {
            SaleManLevel salesManlevel = new SaleManLevel();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM " + tableName + " WHERE Level=@Level AND State=1";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Level", Level));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesManlevel.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesManlevel.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesManlevel.LevelName = reader["LevelName"].ToString();
                            salesManlevel.Level = Convert.ToInt16(reader["Level"].ToString());
                            salesManlevel.Remark = reader["Remark"].ToString();
                            if (!string.IsNullOrEmpty(reader["DistributionRate"].ToString()))
                                salesManlevel.DistributionRate = Convert.ToInt16(reader["DistributionRate"].ToString());
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesManlevel.State = (SaleManLevelStateEnum)Convert.ToInt32(reader["State"].ToString());
                            salesManlevel.StandardEditionNum = Convert.ToInt16(reader["StandardEditionNum"].ToString());
                            salesManlevel.BasicEditionNum = Convert.ToInt16(reader["BasicEditionNum"].ToString());
                            salesManlevel.UltimateEdition = Convert.ToInt16(reader["UltimateEdition"].ToString());
                        }
                    }
                }
            }
            return salesManlevel;
        }

        /// <summary>
        /// 获取等级ID名称（展示使用）
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, string> GetIdAndName()
        {
            List<Dictionary<long, string>> resultList = new List<Dictionary<long, string>>();

            Dictionary<long, string> res = new Dictionary<long, string>();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string sql = @" SELECT  [Level],[LevelName],[DistributionRate] FROM " + tableName+" WHERE State=1 order by createtime desc";
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(Convert.ToInt64(reader["Level"].ToString()), reader["LevelName"].ToString()+"("+reader["DistributionRate"].ToString()+"%)");
                            resultList.Add(res);
                        }
                    }
                }
            }
            return res;
        }
    }
}
