using Domain.BasicClass;
using Domain.DTO.Input.ServerPath;
using Domain.Enums.WeChatStationEnum;
using Domain.ServerPath;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsitory.ServerPath
{
    public class ServerPathResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "ServerPathInfo";
        private string tableNameDetails = "ServerPathDetails";

        /// <summary>
        /// 分页查询服务器信息 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<ServerPathInfo> PagedResult(ServerPathInfoPagedInput input)
        {
            PagedResult<ServerPathInfo> returnList = new PagedResult<ServerPathInfo>();
            List<ServerPathInfo> list = new List<ServerPathInfo>();
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
            if (input.BusinessId.HasValue)
            {
                queryCondition += " and BusinessId =@BusinessId";
            }
            if (!string.IsNullOrEmpty(input.IPPort))
            {
                queryCondition += " and ServerIP=@ServerIP";
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
                    if (input.StartDate.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate.Value));
                    }
                    if (input.EndDate.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate.Value));
                    }
                    if (input.BusinessId.HasValue)
                    {

                        cmd.Parameters.Add(new SqlParameter("@BusinessId",input.BusinessId.Value));
                    }
                    if (!string.IsNullOrEmpty(input.IPPort))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ServerIP", input.IPPort));
                    }
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ServerPathInfo serverPath = new ServerPathInfo();
                                serverPath.Id = Convert.ToInt64(reader["Id"].ToString());
                                serverPath.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                                serverPath.ServerIP = reader["ServerIP"].ToString();
                                serverPath.PingPort = reader["PingPort"].ToString();
                                serverPath.PassWord = reader["PassWord"].ToString();
                                serverPath.BandWidth = reader["BandWidth"].ToString();
                                serverPath.CPUInfomation = reader["CPUInfomation"].ToString();
                                serverPath.RAMInformation = reader["RAMInformation"].ToString();
                                serverPath.DiskInformation = reader["DiskInformation"].ToString();
                                if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                                    serverPath.StartDate = Convert.ToDateTime(reader["StartDate"].ToString());
                                if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                                    serverPath.EndDate = Convert.ToDateTime(reader["EndDate"].ToString());
                                if (!string.IsNullOrEmpty(reader["BusinessId"].ToString()))
                                    serverPath.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                                serverPath.Remark = reader["Remark"].ToString();
                                list.Add(serverPath);
                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        return returnList;
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
                    if (input.StartDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate.Value));
                    }
                    if (input.EndDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate.Value));
                    }
                    if (input.BusinessId.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@BusinessId",input.BusinessId));
                    }
                    if (!string.IsNullOrEmpty(input.IPPort))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@ServerIP", input.IPPort));
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
        /// 根据IP获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ServerPathInfo GetByIP(string IP, long BusinessId)
        {
            ServerPathInfo result = new ServerPathInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT * FROM (SELECT* FROM [ServerPathInfo] WHERE SERVERIP=@ServerIP ) AS [ServerPathInfo]   WHERE  BUSINESSiD=@BusinessId";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@ServerIP", IP));
                    cmd.Parameters.Add(new SqlParameter("@BusinessId", BusinessId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt64(reader["Id"].ToString());
                            result.ServerIP = reader["ServerIP"].ToString();
                            result.PingPort = reader["PingPort"].ToString();
                            result.PassWord = reader["PassWord"].ToString();
                            result.BandWidth = reader["BandWidth"].ToString();
                            result.CPUInfomation = reader["CPUInfomation"].ToString();
                            result.RAMInformation = reader["RAMInformation"].ToString();
                            result.DiskInformation = reader["DiskInformation"].ToString();
                            result.StartDate = Convert.ToDateTime(reader["StartDate"].ToString());
                            result.EndDate = Convert.ToDateTime(reader["EndDate"].ToString());
                            result.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            if (!string.IsNullOrEmpty(reader["BusinessId"].ToString()))
                            {
                                result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            }
                            else
                            {
                                result.BusinessId = 0;
                            }
                            result.Remark = reader["Remark"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }


        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ServerPathInfo GetById(long Id)
        {
            ServerPathInfo result = new ServerPathInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM  [ServerPathInfo]   WHERE  Id=@Id";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt64(reader["Id"].ToString());
                            result.ServerIP = reader["ServerIP"].ToString();
                            result.PingPort = reader["PingPort"].ToString();
                            result.PassWord = reader["PassWord"].ToString();
                            result.BandWidth = reader["BandWidth"].ToString();
                            result.CPUInfomation = reader["CPUInfomation"].ToString();
                            result.RAMInformation = reader["RAMInformation"].ToString();
                            result.DiskInformation = reader["DiskInformation"].ToString();
                            result.StartDate = Convert.ToDateTime(reader["StartDate"].ToString());
                            result.EndDate = Convert.ToDateTime(reader["EndDate"].ToString());
                            result.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            if (!string.IsNullOrEmpty(reader["BusinessId"].ToString()))
                            {
                                result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            }
                            else
                            {
                                result.BusinessId = 0;
                            }
                            result.Remark = reader["Remark"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }


        /// <summary>
        /// 根据商户ID获取最新一条数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ServerPathInfo GetFirstByBusinessId(long BusinessId)
        {
            ServerPathInfo result = new ServerPathInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM  [ServerPathInfo]   WHERE  BusinessId=@BusinessId order by createTime desc";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@BusinessId", BusinessId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt64(reader["Id"].ToString());
                            result.ServerIP = reader["ServerIP"].ToString();
                            result.PingPort = reader["PingPort"].ToString();
                            result.PassWord = reader["PassWord"].ToString();
                            result.BandWidth = reader["BandWidth"].ToString();
                            result.CPUInfomation = reader["CPUInfomation"].ToString();
                            result.RAMInformation = reader["RAMInformation"].ToString();
                            result.DiskInformation = reader["DiskInformation"].ToString();
                            result.StartDate = Convert.ToDateTime(reader["StartDate"].ToString());
                            result.EndDate = Convert.ToDateTime(reader["EndDate"].ToString());
                            result.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            if (!string.IsNullOrEmpty(reader["BusinessId"].ToString()))
                            {
                                result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            }
                            else
                            {
                                result.BusinessId = 0;
                            }
                            result.Remark = reader["Remark"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="serverPathInfo"></param>
        /// <returns></returns>
        public int InsertServerPath(ServerPathInfo serverPathInfo)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" INSERT INTO [dbo].[ServerPathInfo]([Id],[BusinessId],[CPUInfomation],[RAMInformation],[DiskInformation],[ServerIP],[PingPort] ,[PassWord] ,[BandWidth],[StartDate] ,[EndDate] ,[Remark],[CreateTime]) VALUES (@Id,@BusinessId,@CPUInfomation,@RAMInformation,@DiskInformation,@ServerIP,@PingPort,@PassWord,@BandWidth, @StartDate,@EndDate,@Remark,@CreateTime)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", serverPathInfo.Id));
                    cmd.Parameters.Add(new SqlParameter("@BusinessId", serverPathInfo.BusinessId));
                    cmd.Parameters.Add(new SqlParameter("@CPUInfomation", serverPathInfo.CPUInfomation));
                    cmd.Parameters.Add(new SqlParameter("@RAMInformation", serverPathInfo.RAMInformation));
                    cmd.Parameters.Add(new SqlParameter("@DiskInformation", serverPathInfo.DiskInformation));
                    cmd.Parameters.Add(new SqlParameter("@ServerIP", serverPathInfo.ServerIP));
                    cmd.Parameters.Add(new SqlParameter("@PingPort", serverPathInfo.PingPort));
                    cmd.Parameters.Add(new SqlParameter("@PassWord", serverPathInfo.PassWord));
                    cmd.Parameters.Add(new SqlParameter("@BandWidth", serverPathInfo.BandWidth));
                    cmd.Parameters.Add(new SqlParameter("@StartDate", serverPathInfo.StartDate));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", serverPathInfo.EndDate));
                    cmd.Parameters.Add(new SqlParameter("@Remark", serverPathInfo.Remark));
                    cmd.Parameters.Add(new SqlParameter("@CreateTime", serverPathInfo.CreateTime));
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


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="serverPathInfo"></param>
        /// <returns></returns>
        public int UpdateServerPath(UpdateServerPathInput serverPathInfo)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE [dbo].[ServerPathInfo] set CPUInfomation=@CPUInfomation,RAMInformation=@RAMInformation,DiskInformation=@DiskInformation,ServerIP=@ServerIP,PingPort=@PingPort,PassWord=@PassWord,BandWidth=@BandWidth, StartDate=@StartDate,EndDate=@EndDate,Remark=@Remark where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", serverPathInfo.Id));
                    cmd.Parameters.Add(new SqlParameter("@CPUInfomation", serverPathInfo.CPUInfomation));
                    cmd.Parameters.Add(new SqlParameter("@RAMInformation", serverPathInfo.RAMInformation));
                    cmd.Parameters.Add(new SqlParameter("@DiskInformation", serverPathInfo.DiskInformation));
                    cmd.Parameters.Add(new SqlParameter("@ServerIP", serverPathInfo.ServerIP));
                    cmd.Parameters.Add(new SqlParameter("@PingPort", serverPathInfo.PingPort));
                    cmd.Parameters.Add(new SqlParameter("@PassWord", serverPathInfo.PassWord));
                    cmd.Parameters.Add(new SqlParameter("@BandWidth", serverPathInfo.BandWidth));
                    cmd.Parameters.Add(new SqlParameter("@StartDate", serverPathInfo.StartDate));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", serverPathInfo.EndDate));
                    cmd.Parameters.Add(new SqlParameter("@Remark", serverPathInfo.Remark));
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
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteServerPath(long Id)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" Delete from [dbo]." + tableName + "  where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
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

        #region 服务器明细部分
        /// <summary>
        /// 根据服务器ID查询明细
        /// </summary>
        /// <param name="ServerPathId"></param>
        /// <returns></returns>
        public PagedResult<ServerPathDetails> PagedResultDetails(ServerPathInfoDetailsPagedInput input)
        {
            PagedResult<ServerPathDetails> returnList = new PagedResult<ServerPathDetails>();
            List<ServerPathDetails> list = new List<ServerPathDetails>();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";
            queryCondition = " and ServerPathId=@ServerPathId";
            #endregion

            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = pagedUtity.PagedSql(tableNameDetails, input.Page, input.Limit, queryCondition, "CreateTime desc");
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@ServerPathId", input.ServerPathId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ServerPathDetails details = new ServerPathDetails();
                            details.Id = Convert.ToInt64(reader["Id"].ToString());
                            details.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            details.ServerPathId = Convert.ToInt64(reader["ServerPathId"].ToString());
                            details.DomainName = reader["DomainName"].ToString();
                            details.Email = reader["Email"].ToString();
                            details.Phone = reader["Phone"].ToString();
                            details.BindWechatStationName = reader["BindWechatStationName"].ToString();
                            details.BindWechatStationUserName = reader["BindWechatStationUserName"].ToString();
                            details.BindWechatStationPassWord = reader["BindWechatStationPassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["BindWechatStationType"].ToString()))
                                details.BindWechatStationType = (WechatStationTypeEnum)Convert.ToInt16(reader["BindWechatStationType"].ToString());
                            details.BindWechatStationAppId = reader["BindWechatStationAppId"].ToString();
                            details.BindWechatStationAppSecret = reader["BindWechatStationAppSecret"].ToString();
                            details.Remark = reader["Remark"].ToString();
                            list.Add(details);
                        }
                    }
                }
            }

            #endregion

            #region 读取总条数

            int totalCount = 0;
            var totalSql = "SELECT COUNT(*) FROM " + tableNameDetails + " where 1=1";
            if (!string.IsNullOrEmpty(queryCondition))
                totalSql += queryCondition;
            using (SqlConnection totalconn = new SqlConnection(connection))
            {
                using (SqlCommand totalcmd = totalconn.CreateCommand())
                {
                    totalconn.Open();
                    totalcmd.CommandText = totalSql;
                    totalcmd.Parameters.Add(new SqlParameter("@ServerPathId", input.ServerPathId));
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
        /// 根据域名获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ServerPathDetails GetDetailByDomainName(string DomainName, long ServerPathId)
        {
            ServerPathDetails result = new ServerPathDetails();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @"SELECT TOP 1 * FROM " + tableNameDetails + " WHERE DomainName=@DomainName and ServerPathId=@ServerPathId";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@DomainName", DomainName));
                    cmd.Parameters.Add(new SqlParameter("@ServerPathId", ServerPathId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt64(reader["Id"].ToString());
                            result.ServerPathId = Convert.ToInt64(reader["ServerPathId"].ToString());
                            result.DomainName = reader["DomainName"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.BindWechatStationName = reader["BindWechatStationName"].ToString();
                            result.BindWechatStationPassWord = reader["BindWechatStationPassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["BindWechatStationPassWord"].ToString()))
                                result.BindWechatStationType = (WechatStationTypeEnum)Convert.ToUInt16(reader["BindWechatStationPassWord"].ToString());
                            result.BindWechatStationUserName = reader["BindWechatStationUserName"].ToString();
                            result.BindWechatStationAppId = reader["BindWechatStationAppId"].ToString();
                            result.BindWechatStationAppSecret = reader["BindWechatStationAppSecret"].ToString();
                            result.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 根据绑定公众号名称获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ServerPathDetails GetDetailByWechatStationName(string BindWechatStationUserName, string WeChatStationName, long ServerPathId)
        {
            ServerPathDetails result = new ServerPathDetails();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @"SELECT TOP 1 * FROM " + tableNameDetails + " WHERE BindWechatStationUserName=@BindWechatStationUserName  AND BindWechatStationName=@WeChatStationName and ServerPathId=@ServerPathId";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@BindWechatStationUserName", BindWechatStationUserName));
                    cmd.Parameters.Add(new SqlParameter("@WeChatStationName", WeChatStationName));
                    cmd.Parameters.Add(new SqlParameter("@ServerPathId", ServerPathId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Id = Convert.ToInt64(reader["Id"].ToString());
                            result.ServerPathId = Convert.ToInt64(reader["ServerPathId"].ToString());
                            result.DomainName = reader["DomainName"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.BindWechatStationName = reader["BindWechatStationName"].ToString();
                            result.BindWechatStationPassWord = reader["BindWechatStationPassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["BindWechatStationPassWord"].ToString()))
                                result.BindWechatStationType = (WechatStationTypeEnum)Convert.ToUInt16(reader["BindWechatStationPassWord"].ToString());
                            result.BindWechatStationUserName = reader["BindWechatStationUserName"].ToString();
                            result.BindWechatStationAppId = reader["BindWechatStationAppId"].ToString();
                            result.BindWechatStationAppSecret = reader["BindWechatStationAppSecret"].ToString();
                            result.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }
        
        /// <summary>
        /// 根据服务器ID获取详细信息
        /// </summary>
        /// <param name="ServerPathId"></param>
        /// <returns></returns>
        public List<ServerPathDetails> GetDetailByServerPathId(long ServerPathId)
        {
            List<ServerPathDetails> list = new List<ServerPathDetails>();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string Sql = "SELECT * FROM " + tableNameDetails + " WHERE 1=1 and ServerPathId=@ServerPathId order by CreateTime desc";
            #endregion

            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = Sql;
                    cmd.Parameters.Add(new SqlParameter("@ServerPathId", ServerPathId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ServerPathDetails details = new ServerPathDetails();
                            details.Id = Convert.ToInt64(reader["Id"].ToString());
                            details.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            details.ServerPathId = Convert.ToInt64(reader["ServerPathId"].ToString());
                            details.DomainName = reader["DomainName"].ToString();
                            details.Email = reader["Email"].ToString();
                            details.Phone = reader["Phone"].ToString();
                            details.BindWechatStationName = reader["BindWechatStationName"].ToString();
                            details.BindWechatStationUserName = reader["BindWechatStationUserName"].ToString();
                            details.BindWechatStationPassWord = reader["BindWechatStationPassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["BindWechatStationType"].ToString()))
                                details.BindWechatStationType = (WechatStationTypeEnum)Convert.ToInt16(reader["BindWechatStationType"].ToString());
                            details.BindWechatStationAppId = reader["BindWechatStationAppId"].ToString();
                            details.BindWechatStationAppSecret = reader["BindWechatStationAppSecret"].ToString();
                            details.Remark = reader["Remark"].ToString();
                            list.Add(details);
                        }
                    }
                }
            }

            #endregion


            return list;
        }

        /// <summary>
        /// 新增详细信息
        /// </summary>
        /// <param name="serverPathInfo"></param>
        /// <returns></returns>
        public int InsertServerPathDetails(ServerPathDetails serverPathInfo)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" INSERT INTO [dbo].[ServerPathDetails]([Id],[ServerPathId],[DomainName],[Email],[Phone],[BindWechatStationName],[BindWechatStationUserName] ,[BindWechatStationPassWord] ,[BindWechatStationType],[BindWechatStationAppId] ,[BindWechatStationAppSecret] ,[Remark],[CreateTime]) VALUES (@Id,@ServerPathId,@DomainName,@Email,@Phone,@BindWechatStationName,@BindWechatStationUserName,@BindWechatStationPassWord,@BindWechatStationType, @BindWechatStationAppId,@BindWechatStationAppSecret,@Remark,@CreateTime)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", serverPathInfo.Id));
                    cmd.Parameters.Add(new SqlParameter("@ServerPathId", serverPathInfo.ServerPathId));
                    cmd.Parameters.Add(new SqlParameter("@DomainName", serverPathInfo.DomainName));
                    cmd.Parameters.Add(new SqlParameter("@Email", serverPathInfo.Email));
                    cmd.Parameters.Add(new SqlParameter("@Phone", serverPathInfo.Phone));
                    cmd.Parameters.Add(new SqlParameter("@BindWechatStationName", serverPathInfo.BindWechatStationName));
                    cmd.Parameters.Add(new SqlParameter("@BindWechatStationUserName", serverPathInfo.BindWechatStationUserName));
                    cmd.Parameters.Add(new SqlParameter("@BindWechatStationPassWord", serverPathInfo.BindWechatStationPassWord));
                    cmd.Parameters.Add(new SqlParameter("@BindWechatStationType", serverPathInfo.BindWechatStationType));
                    cmd.Parameters.Add(new SqlParameter("@BindWechatStationAppId", serverPathInfo.BindWechatStationAppId));
                    cmd.Parameters.Add(new SqlParameter("@BindWechatStationAppSecret", serverPathInfo.BindWechatStationAppSecret));
                    cmd.Parameters.Add(new SqlParameter("@Remark", serverPathInfo.Remark));
                    cmd.Parameters.Add(new SqlParameter("@CreateTime", serverPathInfo.CreateTime));
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


        public int DeleteServerPathDetails(long Id)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" Delete from [dbo]." + tableNameDetails + "  where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
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
        #endregion
    }
}
