using Domain.AdminEnum;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.SalesMan;
using Domain.DTO.Input.ShopManage;
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
    public class SalesManInfoResponsitory
    {
        private string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private string tableName = "SalesManInfo";
        /// <summary>
        /// 分页查询合伙人信息 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<SalesManInfo> PagedResult(SalesManInfoPagedInput input)
        {
            PagedResult<SalesManInfo> returnList = new PagedResult<SalesManInfo>();
            List<SalesManInfo> list = new List<SalesManInfo>();
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
            if (!string.IsNullOrEmpty(input.SaleManName))
            {
                queryCondition += " and SaleManName like @SaleManName";
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
                        cmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate));
                    }
                    if (input.EndDate.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate));
                    }
                    if (!string.IsNullOrEmpty(input.SaleManName))
                    {
                        cmd.Parameters.Add(new SqlParameter("@SaleManName", "%"+ input.SaleManName + "%"));
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SalesManInfo salesMan = new SalesManInfo();
                            salesMan.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesMan.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesMan.SaleManName = reader["SaleManName"].ToString();
                            salesMan.Phone = reader["Phone"].ToString();
                            salesMan.Address = reader["Address"].ToString();
                            salesMan.Email = reader["Email"].ToString();
                            if (!string.IsNullOrEmpty(reader["LevelId"].ToString()))
                                salesMan.LevelId = Convert.ToInt64(reader["LevelId"].ToString());
                            salesMan.Balance = Convert.ToDecimal(reader["Balance"].ToString());
                            salesMan.LoginName = reader["LoginName"].ToString();
                            salesMan.PassWord = reader["PassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesMan.State = (ShopUserStateEnum)Convert.ToInt32(reader["State"].ToString());
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
                    if (input.StartDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@StartDate", input.StartDate));
                    }
                    if (input.EndDate.HasValue)
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@EndDate", input.EndDate));
                    }
                    if (!string.IsNullOrEmpty(input.SaleManName))
                    {
                        totalcmd.Parameters.Add(new SqlParameter("@SaleManName", "%" + input.SaleManName + "%"));
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
        /// 根据ID查询合伙人数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SalesManInfo GetById(long Id)
        {
            SalesManInfo salesMan = new SalesManInfo();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM " + tableName + " WHERE Id=@Id AND State=1";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesMan.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesMan.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesMan.SaleManName = reader["SaleManName"].ToString();
                            salesMan.Phone = reader["Phone"].ToString();
                            salesMan.Address = reader["Address"].ToString();
                            salesMan.Email = reader["Email"].ToString();
                            salesMan.Balance = Convert.ToDecimal(reader["Balance"].ToString());
                            if (!string.IsNullOrEmpty(reader["LevelId"].ToString()))
                                salesMan.LevelId = Convert.ToInt64(reader["LevelId"].ToString());
                            salesMan.LoginName = reader["LoginName"].ToString();
                            salesMan.PassWord = reader["PassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesMan.State = (ShopUserStateEnum)Convert.ToInt32(reader["State"].ToString());
                        }
                    }
                }
            }
            return salesMan;
        }

        /// <summary>
        /// 获取合伙人ID名称（下拉框使用）
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
                    string sql = @" SELECT  [Id],[SaleManName] FROM [dbo].[SalesManInfo] WHERE State=1";
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(Convert.ToInt64(reader["Id"].ToString()), reader["SaleManName"].ToString());
                            resultList.Add(res);
                        }
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 根据手机号获取数据
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public SalesManInfo GetByPhone(string Phone)
        {
            SalesManInfo salesMan = new SalesManInfo();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM " + tableName + " WHERE Phone=@Phone AND State=1";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Phone", Phone));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesMan.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesMan.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesMan.SaleManName = reader["SaleManName"].ToString();
                            salesMan.Phone = reader["Phone"].ToString();
                            salesMan.Address = reader["Address"].ToString();
                            salesMan.Email = reader["Email"].ToString();
                            salesMan.Balance = Convert.ToDecimal(reader["Balance"].ToString());
                            if (!string.IsNullOrEmpty(reader["LevelId"].ToString()))
                                salesMan.LevelId = Convert.ToInt64(reader["LevelId"].ToString());
                            salesMan.LoginName = reader["LoginName"].ToString();
                            salesMan.PassWord = reader["PassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesMan.State = (ShopUserStateEnum)Convert.ToInt32(reader["State"].ToString());
                        }
                    }
                }
            }
            return salesMan;
        }

        /// <summary>
        /// 根据邮箱获取数据
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public SalesManInfo GetByEmail(string Email)
        {
            SalesManInfo salesMan = new SalesManInfo();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM " + tableName + " WHERE Email=@Email AND State=1";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Email", Email));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesMan.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesMan.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesMan.SaleManName = reader["SaleManName"].ToString();
                            salesMan.Phone = reader["Phone"].ToString();
                            salesMan.Address = reader["Address"].ToString();
                            salesMan.Email = reader["Email"].ToString();
                            salesMan.Balance = Convert.ToDecimal(reader["Balance"].ToString());
                            if (!string.IsNullOrEmpty(reader["LevelId"].ToString()))
                                salesMan.LevelId = Convert.ToInt64(reader["LevelId"].ToString());
                            salesMan.LoginName = reader["LoginName"].ToString();
                            salesMan.PassWord = reader["PassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesMan.State = (ShopUserStateEnum)Convert.ToInt32(reader["State"].ToString());
                        }
                    }
                }
            }
            return salesMan;
        }

        public SalesManInfo GetByName(string Name)
        {
            SalesManInfo salesMan = new SalesManInfo();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string pagedSql = @" SELECT TOP 1 * FROM " + tableName + " WHERE SaleManName=@Name AND State=1";
                    cmd.CommandText = pagedSql;
                    cmd.Parameters.Add(new SqlParameter("@Name", Name));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesMan.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesMan.CreateTime = Convert.ToDateTime(reader["CreateTime"].ToString());
                            salesMan.SaleManName = reader["SaleManName"].ToString();
                            salesMan.Phone = reader["Phone"].ToString();
                            salesMan.Address = reader["Address"].ToString();
                            salesMan.Email = reader["Email"].ToString();
                            salesMan.Balance = Convert.ToDecimal(reader["Balance"].ToString());
                            if (!string.IsNullOrEmpty(reader["LevelId"].ToString()))
                                salesMan.LevelId = Convert.ToInt64(reader["LevelId"].ToString());
                            salesMan.LoginName = reader["LoginName"].ToString();
                            salesMan.PassWord = reader["PassWord"].ToString();
                            if (!string.IsNullOrEmpty(reader["State"].ToString()))
                                salesMan.State = (ShopUserStateEnum)Convert.ToInt32(reader["State"].ToString());
                        }
                    }
                }
            }
            return salesMan;
        }

        /// <summary>
        /// 根据用户名密码比对是否登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SalesManInfo SalesManInfoLogin(AdminQueryInput input)
        {
            SalesManInfo salesManInfo = new SalesManInfo();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT TOP 1 Id, LoginName,PassWord,State FROM SalesManInfo WHERE LoginName=@UserName";
                    cmd.Parameters.Add(new SqlParameter("@UserName", input.UserName));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesManInfo.Id = Convert.ToInt64(reader["Id"].ToString());
                            salesManInfo.LoginName = reader["LoginName"].ToString();
                            salesManInfo.PassWord = reader["PassWord"].ToString();
                            salesManInfo.State = (ShopUserStateEnum)Convert.ToInt64(reader["State"].ToString());
                        }
                    }
                }
            }
            return salesManInfo;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="saleManInfo"></param>
        /// <returns></returns>
        public int InsertSaleMan(SalesManInfo saleManInfo)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" INSERT INTO [dbo].[SalesManInfo]([Id],[SaleManName],[Phone],[Address],[Email],[LevelId],[LoginName] ,[PassWord] ,[State],[Balance] ,[CreateTime]) 
                                                               VALUES (@Id,@SaleManName,@Phone,@Address,@Email,@LevelId,@LoginName,@PassWord,@State, @Balance,@CreateTime)";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", saleManInfo.Id));
                    cmd.Parameters.Add(new SqlParameter("@SaleManName", saleManInfo.SaleManName));
                    cmd.Parameters.Add(new SqlParameter("@Phone", saleManInfo.Phone));
                    cmd.Parameters.Add(new SqlParameter("@Address", saleManInfo.Address));
                    cmd.Parameters.Add(new SqlParameter("@Email", saleManInfo.Email));
                    cmd.Parameters.Add(new SqlParameter("@LevelId", saleManInfo.LevelId));
                    cmd.Parameters.Add(new SqlParameter("@LoginName", saleManInfo.LoginName));
                    cmd.Parameters.Add(new SqlParameter("@PassWord", saleManInfo.PassWord));
                    cmd.Parameters.Add(new SqlParameter("@State", saleManInfo.State));
                    cmd.Parameters.Add(new SqlParameter("@Balance", saleManInfo.Balance));
                    cmd.Parameters.Add(new SqlParameter("@CreateTime", saleManInfo.CreateTime));
                    res = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }

            return res;

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="saleManInfo"></param>
        /// <returns></returns>
        public int UpdateSaleMan(SalesManInfo saleManInfo)
        {
            var res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" Update  [dbo].[SalesManInfo] set [SaleManName]=@SaleManName,[Phone]=@Phone,[Address]=@Address,[Email]=@Email,[LevelId]=@LevelId where id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Id", saleManInfo.Id));
                    cmd.Parameters.Add(new SqlParameter("@SaleManName", saleManInfo.SaleManName));
                    cmd.Parameters.Add(new SqlParameter("@Phone", saleManInfo.Phone));
                    cmd.Parameters.Add(new SqlParameter("@Address", saleManInfo.Address));
                    cmd.Parameters.Add(new SqlParameter("@Email", saleManInfo.Email));
                    cmd.Parameters.Add(new SqlParameter("@LevelId", saleManInfo.LevelId));
                    res = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }

            return res;

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
                    string sql = @" UPDATE " + tableName + " SET PassWord=@Password where Id=@UserId";
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

        /// <summary>
        /// 修改合伙人状态
        /// </summary>
        /// <returns></returns>
        public int UpdateState(UpdateShopStateInput input)
        {
            try
            {
                int num = 0;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " SET State=@State where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@State", input.UserState));
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

        public int UpdateName(UpdateSaleManNameInput input)
        {
            try
            {
                int num = 0;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " SET SaleManName=@Name where Id=@Id";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Name", input.NewSaleManName));
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
        /// 修改金额
        /// </summary>
        /// <returns></returns>
        public int UpdateBalance(long Id,decimal Balance)
        {
            try
            {
                int num = 0;

                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = @" UPDATE " + tableName + " SET Balance=@Balance where Id=@UserId";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("@Balance", Balance));
                    cmd.Parameters.Add(new SqlParameter("@UserId", Id));
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
