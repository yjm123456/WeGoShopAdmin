using Domain.BasicClass;
using Domain.DTO.Input.ShopManage;
using Domain.Enums.ShopCompanyEnums;
using Domain.ShopCompany;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsitory.ShopManage
{
    public class BusinessVersionResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "BusinessVersion";
        /// <summary>
        /// 分页查询等级信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<BusinessVersion> PagedResult(BusinessVersionPagedInput input)
        {
            PagedResult<BusinessVersion> returnList = new PagedResult<BusinessVersion>();
            List<BusinessVersion> list = new List<BusinessVersion>();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";
            if (!string.IsNullOrEmpty(input.VersionName))
            {
                queryCondition += " and VersionName like @VersionName";
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
                  
                    if (!string.IsNullOrEmpty(input.VersionName))
                    {
                        cmd.Parameters.Add(new SqlParameter("@VersionName", "%" + input.VersionName + "%"));
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BusinessVersion salesMan = new BusinessVersion();
                            salesMan.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesMan.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesMan.Version = reader["Version"].ToString();
                            if (!string.IsNullOrEmpty(reader["VersionNum"].ToString()))
                                salesMan.VersionNum = Convert.ToInt32(reader["VersionNum"].ToString());
                            if (!string.IsNullOrEmpty(reader["Price"].ToString()))
                                salesMan.Price = Convert.ToDecimal(reader["Price"].ToString());
                            salesMan.Description = reader["Description"].ToString();
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesMan.State = (BusinessVersionStateEnum)Convert.ToInt32(reader["State"].ToString());
                            list.Add(salesMan);
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

                    if (!string.IsNullOrEmpty(input.VersionName))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@VersionName", "%" + input.VersionName + "%"));
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
        /// 获取ID和名字（下拉框使用）
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
                    string sql = @" SELECT  [VersionNum],[Version] FROM " + tableName+" WHERE State=1";
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(Convert.ToInt32(reader["VersionNum"].ToString()), reader["Version"].ToString());
                            resultList.Add(res);
                        }
                    }
                }
            }
            return res;
        }
    }
}
