using Domain.DTO;
using Domain.DTO.Input.ShopManage;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsitory.Admin
{
    /// <summary>
    /// 管理员仓储层
    /// </summary>
    public class AdminResponsitory
    {
        private readonly string connection = ConfigurationManager.ConnectionStrings["WeGoShopAdminSqlServer"].ToString();
        private readonly string tableName = "AdminUSER";
        /// <summary>
        /// 根据用户名密码比对是否登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public User GetUser(AdminQueryInput input)
        {
            User adminUser = new User();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT * FROM "+ tableName + " WHERE USERNAME=@UserName";
                    cmd.Parameters.Add(new SqlParameter("@UserName", input.UserName));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            adminUser.Id = Convert.ToInt64(reader["Id"].ToString());
                            adminUser.UserName = reader["UserName"].ToString();
                            adminUser.PassWord = reader["PassWord"].ToString();
                        }
                    }
                }
            }
            return adminUser;
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
    }
}
