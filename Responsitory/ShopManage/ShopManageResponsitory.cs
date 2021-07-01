using Domain;
using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.ShopManage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsitory.ShopManage
{
    /// <summary>
    /// 商户信息仓储层
    /// </summary>
    public class ShopManageResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "BusinessInfo";

        /// <summary>
        /// 分页查询商户信息 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<BusinessInfo> PagedResult(ShopManagePagedInput input)
        {
            PagedResult<BusinessInfo> returnList = new PagedResult<BusinessInfo>();
            List<BusinessInfo> list = new List<BusinessInfo>();
            #region 配置条件
            PagedUtity pagedUtity = new PagedUtity();
            string queryCondition = "";
            if (!string.IsNullOrEmpty(input.CompanyName))
            {
                queryCondition = " and CompanyName like @CompanyName";
            }
            if (!string.IsNullOrEmpty(input.UserName))
            {
                queryCondition = " and UserName like @UserName";
            }
            if (input.StartDate.HasValue)
            {
                queryCondition = " and CreateDate>=@StartDate";
            }

            if (input.EndDate.HasValue)
            {
                queryCondition += " and CreateDate<=@EndDate";
            }
            if (input.SalesManId.HasValue)
            {
                if (input.SalesManId.Value == 1)
                {
                    queryCondition += " and SaleManId=0";
                }
                else
                {
                    queryCondition += " and SaleManId=@SalesManId";
                }
            }
            if (input.Version.HasValue)
            {
                queryCondition += " and Version=@Version";
            }
            else {
                queryCondition += " and Version<4";
            }
            if (input.ShopUserState.HasValue)
            {
                if (input.ShopUserState == ShopUserStateEnum.OutOfDate)
                {
                    queryCondition += " and ShopUserState>=@shopUserState";
                }
                else {
                    queryCondition += " and ShopUserState=@ShopUserState";
                }
            }
            if (input.AuditState.HasValue)
            {
                queryCondition += " and VerifyState=@AuditState";
            }
            if (input.BusinessId.HasValue)
            {
                queryCondition += " and BusinessId=@BusinessId";
            }
            #endregion
            try
            {
                #region 查询数据
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        string pagedSql = pagedUtity.PagedSql(tableName, input.Page, input.Limit, queryCondition, "CreateDate desc");
                        cmd.CommandText = pagedSql;
                        if (!string.IsNullOrEmpty(input.CompanyName))
                        {
                            cmd.Parameters.Add(new SqlParameter("@CompanyName", "%"+ input.CompanyName+"%"));
                        }
                        if (!string.IsNullOrEmpty(input.UserName))
                        {
                            cmd.Parameters.Add(new SqlParameter("@UserName", "%" + input.UserName + "%"));
                        }
                        if (input.StartDate.HasValue)
                        {
                            cmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate));
                        }
                        if (input.Version.HasValue)
                        {
                            cmd.Parameters.Add(new SqlParameter("@Version", input.Version));
                        }
                        if (input.ShopUserState.HasValue)
                        {
                            cmd.Parameters.Add(new SqlParameter("@ShopUserState", input.ShopUserState));
                        }
                        if (input.AuditState.HasValue)
                        {
                            cmd.Parameters.Add(new SqlParameter("@AuditState", input.AuditState));
                        }
                        if (input.EndDate.HasValue)
                        {
                            cmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate));
                        }
                        if (input.SalesManId.HasValue)
                        {
                            if (input.SalesManId.Value != 1)
                            {
                                cmd.Parameters.Add(new SqlParameter("@SalesManId", input.SalesManId.Value));
                            }
                        }
                        if (input.BusinessId.HasValue)
                        {
                            cmd.Parameters.Add(new SqlParameter("@BusinessId", input.BusinessId.Value));
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BusinessInfo company = new BusinessInfo();
                                company.UserId = Convert.ToInt32(reader["UserId"].ToString());
                                company.ContractNo = reader["CompanyName"].ToString();
                                company.CompanyName = reader["CompanyName"].ToString();
                                company.InstitutionalType = (InstitutionalTypeEnum)Convert.ToInt64(reader["InstitutionalType"].ToString());
                                company.OrganizingInstitution = reader["OrganizingInstitution"].ToString();
                                company.AccountName = reader["AccountName"].ToString();
                                company.DepositBank = reader["DepositBank"].ToString();
                                company.BankAccount = reader["BankAccount"].ToString();
                                company.UserName = reader["UserName"].ToString();
                                company.LiasonManName = reader["LiasonManName"].ToString();
                                company.Phone = reader["Phone"].ToString();
                                company.FixPhone = reader["FixPhone"].ToString();
                                company.Email = reader["Email"].ToString();
                                company.IdentityCard = reader["Email"].ToString();
                                if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                                {
                                    company.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                                }
                                else
                                {
                                    company.SaleManId = 0;
                                }
                                company.SaleManName = reader["SaleManName"].ToString();
                                if (!string.IsNullOrEmpty(reader["ApplicationRange"].ToString()))
                                    company.ApplicationRange = (ShopUsingTypeEnum)Convert.ToInt64(reader["ApplicationRange"].ToString());
                                if (!string.IsNullOrEmpty(reader["VerifyState"].ToString()))
                                    company.VerifyState = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                                if (!string.IsNullOrEmpty(reader["Version"].ToString()))
                                    company.Version = (AccountVersionEnum)Convert.ToInt64(reader["Version"].ToString());
                                if (!string.IsNullOrEmpty(reader["ShopUserState"].ToString()))
                                    company.ShopUserState = (ShopUserStateEnum)Convert.ToInt64(reader["ShopUserState"].ToString());
                                company.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                                company.DueTime = Convert.ToDateTime(reader["DueTime"].ToString());
                                company.Remark = reader["Remark"].ToString();
                                company.IdentityCardImgUrl1 = reader["IdentityCardImgUrl1"].ToString();
                                company.IdentityCardImgUrl2 = reader["IdentityCardImgUrl2"].ToString();
                                company.BusinessLicenseImgUrl = reader["BusinessLicenseImgUrl"].ToString();
                                company.ContractResultImgUrl = reader["ContractResultImgUrl"].ToString();
                                list.Add(company);
                            }
                        }
                    }
                }

                #endregion
            }

            catch (Exception err)
            {

            }
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
                    if (!string.IsNullOrEmpty(input.CompanyName))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@CompanyName", "%" + input.CompanyName + "%"));
                    }
                    if (!string.IsNullOrEmpty(input.UserName))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@UserName", "%" + input.UserName + "%"));
                    }
                    if (input.StartDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate));
                    }
                    if (input.Version.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@Version", input.Version));
                    }
                    if (input.ShopUserState.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@ShopUserState", input.ShopUserState));
                    }
                    if (input.AuditState.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@AuditState", input.AuditState));
                    }
                    if (input.EndDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate));
                    }
                    if (input.SalesManId.HasValue)
                    {
                        if (input.SalesManId.Value != 1)
                        {
                            totalcmd.Parameters.Add(new SqlParameter("@SalesManId", input.SalesManId.Value));
                        }
                    }
                    if (input.BusinessId.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@BusinessId", input.BusinessId.Value));
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
        /// 修改商户状态
        /// </summary>
        /// <returns></returns>
        public int UpdateState(UpdateShopStateInput input)
        {
            try
            {
                int num = 0;
                string BusinessId = "";
                //查询该商户对应的所有businessId
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        string sql = @" SELECT TOP 1 [BusinessId] FROM [dbo].[BusinessInfo] WHERE UserId=@ID";
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@ID", input.Id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BusinessId = reader["BusinessId"].ToString();
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(BusinessId))
                {
                    return 0;
                }
                if (BusinessId == "0" && input.UserState == ShopUserStateEnum.Normal)
                {
                    return 0;
                }
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " SET ShopUserState=@State where BusinessId=@BusinessId";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@BusinessId", BusinessId));
                    cmd.Parameters.Add(new SqlParameter("@State", input.UserState));
                    num = cmd.ExecuteNonQuery();
                    con.Close();
                    return num;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int UpdateVerifyState(UpdateVerifyStateInput input)
        {
            try
            {
                int num = 0;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " SET VerifyState=@State";
                    if (input.VerifyState == AuditStateEnum.Passed) {
                        sql += @" ,DueTime=@DueTime ";
                    }
                    sql += @"  where UserId=@UserId";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@UserId", input.Id));
                    if (input.VerifyState == AuditStateEnum.Passed)
                    {
                        cmd.Parameters.Add(new SqlParameter("@DueTime", DateTime.Now.AddMonths(1)));
                    }
                    cmd.Parameters.Add(new SqlParameter("@State", input.VerifyState));
                    num = cmd.ExecuteNonQuery();
                    con.Close();
                    return num;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据ID获取商户审核状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public AuditStateEnum GetAuditStateById(long Id)
        {
            AuditStateEnum result = new AuditStateEnum();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string sql = @" SELECT TOP 1 [VerifyState] FROM [dbo].[BusinessInfo] WHERE UserId=@ID";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                        }
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 根据ID获取商户版本
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public AccountVersionEnum GetVersionById(long Id)
        {
            AccountVersionEnum result = new AccountVersionEnum();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string sql = @" SELECT TOP 1 [Version] FROM [dbo].[BusinessInfo] WHERE UserId=@ID";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = (AccountVersionEnum)Convert.ToInt16(reader["Version"].ToString());
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据ID获取商户id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public long GetBusinessIDById(long Id)
        {
            try
            {
                long result = 0;
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        string sql = @" SELECT TOP 1 [BusinessId] FROM [dbo].[BusinessInfo] WHERE UserId=@ID";
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@ID", Id));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result= Convert.ToInt64(reader["BusinessId"].ToString());
                            }
                        }
                    }
                }
                return result;
            }
            catch {
                return 0;
            }
          
        }

        /// <summary>
        /// 根据ID获取商户当前启用状态
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ShopUserStateEnum GetShopUserStateById(long Id)
        {
            ShopUserStateEnum result = new ShopUserStateEnum();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string sql = @" SELECT TOP 1 [ShopUserState] FROM [dbo].[BusinessInfo] WHERE UserId=@ID";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = (ShopUserStateEnum)Convert.ToInt32(reader["ShopUserState"].ToString());
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public BusinessInfo GetById(long Id)
        {
            BusinessInfo result = new BusinessInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1* FROM BusinessInfo WHERE USERID=@Id";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            result.ContractNo = reader["ContractNo"].ToString();
                            result.CompanyName = reader["CompanyName"].ToString();
                            result.InstitutionalType = (InstitutionalTypeEnum)Convert.ToInt64(reader["InstitutionalType"].ToString());
                            result.OrganizingInstitution = reader["OrganizingInstitution"].ToString();
                            result.AccountName = reader["AccountName"].ToString();
                            result.DepositBank = reader["DepositBank"].ToString();
                            result.BankAccount = reader["BankAccount"].ToString();
                            result.LiasonManName = reader["LiasonManName"].ToString();
                            result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            result.UserName = reader["UserName"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.FixPhone = reader["FixPhone"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.IdentityCard = reader["IdentityCard"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                            {
                                result.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            }
                            else
                            {
                                result.SaleManId = 0;
                            }
                            result.SaleManName = reader["SaleManName"].ToString();
                            if (!string.IsNullOrEmpty(reader["ApplicationRange"].ToString()))
                                result.ApplicationRange = (ShopUsingTypeEnum)Convert.ToInt64(reader["ApplicationRange"].ToString());
                            if (!string.IsNullOrEmpty(reader["VerifyState"].ToString()))
                                result.VerifyState = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                            if (!string.IsNullOrEmpty(reader["Version"].ToString()))
                                result.Version = (AccountVersionEnum)Convert.ToInt64(reader["Version"].ToString());
                            if (!string.IsNullOrEmpty(reader["ShopUserState"].ToString()))
                                result.ShopUserState = (ShopUserStateEnum)Convert.ToInt64(reader["ShopUserState"].ToString());
                            result.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            result.DueTime = Convert.ToDateTime(reader["DueTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                            result.IdentityCardImgUrl1 = reader["IdentityCardImgUrl1"].ToString();
                            result.IdentityCardImgUrl2 = reader["IdentityCardImgUrl2"].ToString();
                            result.BusinessLicenseImgUrl = reader["BusinessLicenseImgUrl"].ToString();
                            result.ContractResultImgUrl = reader["ContractResultImgUrl"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 更新商户号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int UpdateBusinessId(UpdateBusinessIdInput input)
        {
            try
            {
                int num = 0;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " SET BusinessId=@BusinessId where UserId=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@BusinessId", input.BusinessId));
                    cmd.Parameters.Add(new SqlParameter("@Id", input.Id));
                    num = cmd.ExecuteNonQuery();
                    con.Close();
                    return num;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据商户号获取数据
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public BusinessInfo GetByBusId(long businessId)
        {
            BusinessInfo result = new BusinessInfo();
            if (businessId == 0) {
                return result;
            }
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1* FROM BusinessInfo WHERE BusinessId=@busId and version<>4";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@busId", businessId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            result.ContractNo = reader["CompanyName"].ToString();
                            result.CompanyName = reader["CompanyName"].ToString();
                            result.InstitutionalType = (InstitutionalTypeEnum)Convert.ToInt64(reader["InstitutionalType"].ToString());
                            result.OrganizingInstitution = reader["OrganizingInstitution"].ToString();
                            result.AccountName = reader["AccountName"].ToString();
                            result.DepositBank = reader["DepositBank"].ToString();
                            result.BankAccount = reader["BankAccount"].ToString();
                            result.LiasonManName = reader["LiasonManName"].ToString();
                            result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            result.UserName = reader["UserName"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.FixPhone = reader["FixPhone"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.IdentityCard = reader["IdentityCard"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                            {
                                result.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            }
                            else
                            {
                                result.SaleManId = 0;
                            }
                            result.SaleManName = reader["SaleManName"].ToString();
                            if (!string.IsNullOrEmpty(reader["ApplicationRange"].ToString()))
                                result.ApplicationRange = (ShopUsingTypeEnum)Convert.ToInt64(reader["ApplicationRange"].ToString());
                            if (!string.IsNullOrEmpty(reader["VerifyState"].ToString()))
                                result.VerifyState = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                            if (!string.IsNullOrEmpty(reader["Version"].ToString()))
                                result.Version = (AccountVersionEnum)Convert.ToInt64(reader["Version"].ToString());
                            if (!string.IsNullOrEmpty(reader["ShopUserState"].ToString()))
                                result.ShopUserState = (ShopUserStateEnum)Convert.ToInt64(reader["ShopUserState"].ToString());
                            result.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            result.DueTime = Convert.ToDateTime(reader["DueTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                            result.IdentityCardImgUrl1 = reader["IdentityCardImgUrl1"].ToString();
                            result.IdentityCardImgUrl2 = reader["IdentityCardImgUrl2"].ToString();
                            result.BusinessLicenseImgUrl = reader["BusinessLicenseImgUrl"].ToString();
                            result.ContractResultImgUrl = reader["ContractResultImgUrl"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 根据登陆名（Email）获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BusinessInfo GetByLoginName(string name)
        {
            BusinessInfo result = new BusinessInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1* FROM BusinessInfo WHERE UserName=@UserName and version<>4";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@UserName", name));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            result.ContractNo = reader["CompanyName"].ToString();
                            result.CompanyName = reader["CompanyName"].ToString();
                            result.InstitutionalType = (InstitutionalTypeEnum)Convert.ToInt64(reader["InstitutionalType"].ToString());
                            result.OrganizingInstitution = reader["OrganizingInstitution"].ToString();
                            result.AccountName = reader["AccountName"].ToString();
                            result.DepositBank = reader["DepositBank"].ToString();
                            result.BankAccount = reader["BankAccount"].ToString();
                            result.LiasonManName = reader["LiasonManName"].ToString();
                            result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            result.UserName = reader["UserName"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.FixPhone = reader["FixPhone"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.IdentityCard = reader["IdentityCard"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                            {
                                result.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            }
                            else
                            {
                                result.SaleManId = 0;
                            }
                            result.SaleManName = reader["SaleManName"].ToString();
                            if (!string.IsNullOrEmpty(reader["ApplicationRange"].ToString()))
                                result.ApplicationRange = (ShopUsingTypeEnum)Convert.ToInt64(reader["ApplicationRange"].ToString());
                            if (!string.IsNullOrEmpty(reader["VerifyState"].ToString()))
                                result.VerifyState = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                            if (!string.IsNullOrEmpty(reader["Version"].ToString()))
                                result.Version = (AccountVersionEnum)Convert.ToInt64(reader["Version"].ToString());
                            if (!string.IsNullOrEmpty(reader["ShopUserState"].ToString()))
                                result.ShopUserState = (ShopUserStateEnum)Convert.ToInt64(reader["ShopUserState"].ToString());
                            result.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            result.DueTime = Convert.ToDateTime(reader["DueTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                            result.IdentityCardImgUrl1 = reader["IdentityCardImgUrl1"].ToString();
                            result.IdentityCardImgUrl2 = reader["IdentityCardImgUrl2"].ToString();
                            result.BusinessLicenseImgUrl = reader["BusinessLicenseImgUrl"].ToString();
                            result.ContractResultImgUrl = reader["ContractResultImgUrl"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 根据公司名称获取数据
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public BusinessInfo GetByCompanyName(string companyName)
        {
            BusinessInfo result = new BusinessInfo();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1* FROM BusinessInfo WHERE CompanyName=@CompanyName and version<>4";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@CompanyName", companyName));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            result.ContractNo = reader["CompanyName"].ToString();
                            result.CompanyName = reader["CompanyName"].ToString();
                            result.InstitutionalType = (InstitutionalTypeEnum)Convert.ToInt64(reader["InstitutionalType"].ToString());
                            result.OrganizingInstitution = reader["OrganizingInstitution"].ToString();
                            result.AccountName = reader["AccountName"].ToString();
                            result.DepositBank = reader["DepositBank"].ToString();
                            result.BankAccount = reader["BankAccount"].ToString();
                            result.LiasonManName = reader["LiasonManName"].ToString();
                            result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            result.UserName = reader["UserName"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.FixPhone = reader["FixPhone"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.IdentityCard = reader["IdentityCard"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                            {
                                result.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            }
                            else
                            {
                                result.SaleManId = 0;
                            }
                            result.SaleManName = reader["SaleManName"].ToString();
                            if (!string.IsNullOrEmpty(reader["ApplicationRange"].ToString()))
                                result.ApplicationRange = (ShopUsingTypeEnum)Convert.ToInt64(reader["ApplicationRange"].ToString());
                            if (!string.IsNullOrEmpty(reader["VerifyState"].ToString()))
                                result.VerifyState = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                            if (!string.IsNullOrEmpty(reader["Version"].ToString()))
                                result.Version = (AccountVersionEnum)Convert.ToInt64(reader["Version"].ToString());
                            if (!string.IsNullOrEmpty(reader["ShopUserState"].ToString()))
                                result.ShopUserState = (ShopUserStateEnum)Convert.ToInt64(reader["ShopUserState"].ToString());
                            result.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            result.DueTime = Convert.ToDateTime(reader["DueTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                            result.IdentityCardImgUrl1 = reader["IdentityCardImgUrl1"].ToString();
                            result.IdentityCardImgUrl2 = reader["IdentityCardImgUrl2"].ToString();
                            result.BusinessLicenseImgUrl = reader["BusinessLicenseImgUrl"].ToString();
                            result.ContractResultImgUrl = reader["ContractResultImgUrl"].ToString();
                        }
                    }
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 获取商户自建的商户号
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetChildrenByBusId(long businessId)
        {
            List<BusinessInfo> resultList = new List<BusinessInfo>();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT * FROM BusinessInfo WHERE BusinessId=@busId and version=4";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@busId", businessId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BusinessInfo result = new BusinessInfo();
                            result.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            result.ContractNo = reader["CompanyName"].ToString();
                            result.CompanyName = reader["CompanyName"].ToString();
                            result.InstitutionalType = (InstitutionalTypeEnum)Convert.ToInt64(reader["InstitutionalType"].ToString());
                            result.OrganizingInstitution = reader["OrganizingInstitution"].ToString();
                            result.AccountName = reader["AccountName"].ToString();
                            result.DepositBank = reader["DepositBank"].ToString();
                            result.BankAccount = reader["BankAccount"].ToString();
                            result.LiasonManName = reader["LiasonManName"].ToString();
                            result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            result.UserName = reader["UserName"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.FixPhone = reader["FixPhone"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.IdentityCard = reader["IdentityCard"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                            {
                                result.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            }
                            else
                            {
                                result.SaleManId = 0;
                            }
                            result.SaleManName = reader["SaleManName"].ToString();
                            if (!string.IsNullOrEmpty(reader["ApplicationRange"].ToString()))
                                result.ApplicationRange = (ShopUsingTypeEnum)Convert.ToInt64(reader["ApplicationRange"].ToString());
                            if (!string.IsNullOrEmpty(reader["VerifyState"].ToString()))
                                result.VerifyState = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                            if (!string.IsNullOrEmpty(reader["Version"].ToString()))
                                result.Version = (AccountVersionEnum)Convert.ToInt64(reader["Version"].ToString());
                            if (!string.IsNullOrEmpty(reader["ShopUserState"].ToString()))
                                result.ShopUserState = (ShopUserStateEnum)Convert.ToInt64(reader["ShopUserState"].ToString());
                            result.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            result.DueTime = Convert.ToDateTime(reader["DueTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                            resultList.Add(result);
                        }
                    }
                }
            }
            #endregion
            return resultList;
        }

        /// <summary>
        /// 根据合伙人获取商户
        /// </summary>
        /// <param name="salesManId"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetBySalesManId(long salesManId)
        {
            List<BusinessInfo> resultList = new List<BusinessInfo>();
            #region 查询数据
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT * FROM BusinessInfo WHERE SaleManId=@SalesManId and version<>4";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@SalesManId", salesManId));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BusinessInfo result = new BusinessInfo();
                            result.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            result.ContractNo = reader["CompanyName"].ToString();
                            result.CompanyName = reader["CompanyName"].ToString();
                            result.InstitutionalType = (InstitutionalTypeEnum)Convert.ToInt64(reader["InstitutionalType"].ToString());
                            result.OrganizingInstitution = reader["OrganizingInstitution"].ToString();
                            result.AccountName = reader["AccountName"].ToString();
                            result.DepositBank = reader["DepositBank"].ToString();
                            result.BankAccount = reader["BankAccount"].ToString();
                            result.LiasonManName = reader["LiasonManName"].ToString();
                            result.BusinessId = Convert.ToInt64(reader["BusinessId"].ToString());
                            result.UserName = reader["UserName"].ToString();
                            result.Phone = reader["Phone"].ToString();
                            result.FixPhone = reader["FixPhone"].ToString();
                            result.Email = reader["Email"].ToString();
                            result.IdentityCard = reader["IdentityCard"].ToString();
                            if (!string.IsNullOrEmpty(reader["SaleManId"].ToString()))
                            {
                                result.SaleManId = Convert.ToInt64(reader["SaleManId"].ToString());
                            }
                            else
                            {
                                result.SaleManId = 0;
                            }
                            result.SaleManName = reader["SaleManName"].ToString();
                            if (!string.IsNullOrEmpty(reader["ApplicationRange"].ToString()))
                                result.ApplicationRange = (ShopUsingTypeEnum)Convert.ToInt64(reader["ApplicationRange"].ToString());
                            if (!string.IsNullOrEmpty(reader["VerifyState"].ToString()))
                                result.VerifyState = (AuditStateEnum)Convert.ToInt64(reader["VerifyState"].ToString());
                            if (!string.IsNullOrEmpty(reader["Version"].ToString()))
                                result.Version = (AccountVersionEnum)Convert.ToInt64(reader["Version"].ToString());
                            if (!string.IsNullOrEmpty(reader["ShopUserState"].ToString()))
                                result.ShopUserState = (ShopUserStateEnum)Convert.ToInt64(reader["ShopUserState"].ToString());
                            result.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            result.DueTime = Convert.ToDateTime(reader["DueTime"].ToString());
                            result.Remark = reader["Remark"].ToString();
                            resultList.Add(result);
                        }
                    }
                }
            }
            #endregion
            return resultList;
        }

        /// <summary>
        /// 获取最大的用户ID
        /// </summary>
        /// <returns></returns>
        public int getMaxUserId()
        {
            int UserId = 0;
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string sql = @" select Max(UserId) as UserId from BusinessInfo ";
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserId=  Convert.ToInt32(reader["UserId"].ToString());
                        }
                    }
                }
            }
            return UserId;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="businessInfo"></param>
        /// <returns></returns>
        public bool AddBusiness(BusinessInfo businessInfo)
        {
            var res = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" INSERT INTO [dbo].[BusinessInfo]([UserId],[RoleId],[UserName],[Password],[Email],[CreateDate],[CompanyName] ,[InstitutionalType]
           ,[OrganizingInstitution],[AccountName] ,[DepositBank] ,[BankAccount],[LiasonManName] ,[Phone],[FixPhone] ,[IdentityCard],[SaleManId] ,[SaleManName],[ApplicationRange] ,[VerifyState]
           ,[Version] ,[ShopUserState]  ,[DueTime]  ,[Remark] ,[BusinessId]) VALUES (@UserId,@RoleId,@UserName,@Password,@Email,@CreateDate,@CompanyName,@InstitutionalType,
            @OrganizingInstitution, @AccountName,@DepositBank,@BankAccount,@LiasonManName,@Phone,@FixPhone,@IdentityCard ,@SaleManId,@SaleManName,@ApplicationRange,@VerifyState,@Version,
            @ShopUserState,@DueTime,@Remark,@BusinessId)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@AccountName", businessInfo.AccountName));
                    cmd.Parameters.Add(new SqlParameter("@UserId", businessInfo.UserId));
                    cmd.Parameters.Add(new SqlParameter("@UserName", businessInfo.UserName));
                    cmd.Parameters.Add(new SqlParameter("@ShopUserState", businessInfo.ShopUserState));
                    cmd.Parameters.Add(new SqlParameter("@DueTime", businessInfo.DueTime));
                    cmd.Parameters.Add(new SqlParameter("@Version", businessInfo.Version));
                    cmd.Parameters.Add(new SqlParameter("@VerifyState", businessInfo.VerifyState));
                    cmd.Parameters.Add(new SqlParameter("@RoleId", businessInfo.RoleId));
                    cmd.Parameters.Add(new SqlParameter("@Phone", businessInfo.Phone));
                    cmd.Parameters.Add(new SqlParameter("@OrganizingInstitution", businessInfo.OrganizingInstitution));
                    cmd.Parameters.Add(new SqlParameter("@LiasonManName", businessInfo.LiasonManName));
                    cmd.Parameters.Add(new SqlParameter("@InstitutionalType", businessInfo.InstitutionalType));
                    cmd.Parameters.Add(new SqlParameter("@IdentityCard", businessInfo.IdentityCard));
                    cmd.Parameters.Add(new SqlParameter("@FixPhone", businessInfo.FixPhone));
                    cmd.Parameters.Add(new SqlParameter("@CreateDate", businessInfo.CreateDate));
                    cmd.Parameters.Add(new SqlParameter("@CompanyName", businessInfo.CompanyName));
                    cmd.Parameters.Add(new SqlParameter("@BusinessId", businessInfo.BusinessId));
                    cmd.Parameters.Add(new SqlParameter("@ApplicationRange", businessInfo.ApplicationRange));
                    cmd.Parameters.Add(new SqlParameter("@BankAccount", businessInfo.BankAccount));
                    cmd.Parameters.Add(new SqlParameter("@DepositBank", businessInfo.DepositBank));
                    cmd.Parameters.Add(new SqlParameter("@Email", businessInfo.Email));
                    cmd.Parameters.Add(new SqlParameter("@Password", businessInfo.Password));
                    cmd.Parameters.Add(new SqlParameter("@Remark", businessInfo.Remark));
                    cmd.Parameters.Add(new SqlParameter("@SaleManId", businessInfo.SaleManId));
                    cmd.Parameters.Add(new SqlParameter("@SaleManName", businessInfo.SaleManName));
                    res = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return res > 0;

        }

        /// <summary>
        /// 根据用户名密码比对是否登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public BusinessInfo BusinessInfoLogin(AdminQueryInput input)
        {
            BusinessInfo busInfo = new BusinessInfo();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT TOP 1 UserId, UserName,PassWord,Version FROM BusinessInfo WHERE USERNAME=@UserName ";
                    cmd.Parameters.Add(new SqlParameter("@UserName", input.UserName));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            busInfo.UserId = Convert.ToInt32(reader["UserId"].ToString());
                            busInfo.UserName = reader["UserName"].ToString();
                            busInfo.Password = reader["PassWord"].ToString();
                            busInfo.Version = (AccountVersionEnum)Convert.ToInt16(reader["Version"].ToString());
                        }
                    }
                }
            }
            return busInfo;
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="inputPassWord">输入密码</param>
        /// <param name="PassWord">数据库存储密码</param>
        /// <returns></returns>
        public bool CheckPassWord(string inputPassWord, string PassWord)
        {
            if (inputPassWord == PassWord)
            { return true; }
            else { return false; }
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public int UpdatePassWord(UpdateShopPassWordInput input)
        {
            try
            {
                int num = 0;
               
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " SET Password=@Password where UserId=@UserId";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Password", input.NewPassWord));
                    cmd.Parameters.Add(new SqlParameter("@UserId", input.Id));
                    num = cmd.ExecuteNonQuery();
                    con.Close();
                    return num;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
