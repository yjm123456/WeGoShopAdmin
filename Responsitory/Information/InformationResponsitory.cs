using Domain.BasicClass;
using Domain.DTO.Input;
using Domain.Enums.InformationEnum;
using Domain.Information;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsitory.Information
{
    public class InformationResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "InformationInfo";
        /// <summary>
        /// 分页查询合伙人信息 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<InformationInfo> PagedResult(InformatioInfoPagedInput input)
        {
            PagedResult<InformationInfo> returnList = new PagedResult<InformationInfo>();
            List<InformationInfo> list = new List<InformationInfo>();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";
            if (!string.IsNullOrEmpty(input.Content))
            {
                queryCondition = " and Content like @Content";
            }
            if (input.SendTarget.HasValue)
            {
                queryCondition += " and SendTarget=@SendTarget";
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
                    if (!string.IsNullOrEmpty(input.Content))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Content", "%" + input.Content + "%"));
                    }
                    if (input.SendTarget.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@SendTarget", input.SendTarget.Value));
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InformationInfo information = new InformationInfo();
                            information.Id = Convert.ToInt64(reader["Id"].ToString());
                            information.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            information.Content = reader["Content"].ToString();
                            if (!string.IsNullOrEmpty(reader["SendTarget"].ToString()))
                                information.SendTarget = (SendTargetEnum)Convert.ToInt32(reader["SendTarget"].ToString());
                            if (!string.IsNullOrEmpty(reader["InformationState"].ToString()))
                                information.InformationState = (InformationStateEnum)Convert.ToInt32(reader["InformationState"].ToString());
                            list.Add(information);
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
                    if (!string.IsNullOrEmpty(input.Content))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@Content", "%" + input.Content + "%"));
                    }
                    if (input.SendTarget.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@SendTarget", input.SendTarget.Value));
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

        public InformationInfo GetFirstBySendTarget(SendTargetEnum input)
        {
            InformationInfo information = new InformationInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM " + tableName + " WHERE SendTarget =@SendTarget ORDER BY CREATETIME DESC";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@SendTarget", input));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            information.Id = Convert.ToInt64(reader["Id"].ToString());
                            information.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            information.Content = reader["Content"].ToString();
                            if (!string.IsNullOrEmpty(reader["SendTarget"].ToString()))
                                information.SendTarget = (SendTargetEnum)Convert.ToInt32(reader["SendTarget"].ToString());
                            if (!string.IsNullOrEmpty(reader["InformationState"].ToString()))
                                information.InformationState = (InformationStateEnum)Convert.ToInt32(reader["InformationState"].ToString());
                        }
                    }
                }
            }

            #endregion
            return information;
        }
    }
}
