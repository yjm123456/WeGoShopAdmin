
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BasicClass;
using Domain.DTO;
using Domain.DTO.Input.ShopManage;
using Responsitory.Admin;
using Service.Utity;

namespace Service.Admin
{
    public class AdminService 
    {

       private readonly  AdminResponsitory _adminResponsitory = new AdminResponsitory();
        /// <summary>
        /// 用户登陆模块
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ReturnClass Login(AdminQueryInput input)
        {
            var result = new ReturnClass();
            var userResult = _adminResponsitory.GetUser(input);
            if (userResult.UserName == null)
            {
                result.StatusCode = -1;
                result.Error = "您输入的账号不存在，请重新填写！";
                return result;
            }
            input.PassWord = CryptographerHelper.Md5Encrypt(input.PassWord);
            var PassWordResult = _adminResponsitory.CheckPassWord(input.PassWord, userResult.PassWord);
            if (PassWordResult == false)
            {
                result.StatusCode = 0;
                result.Error = "该账户的密码输入有误，请重新填写！";
                return result;
            }

            result.StatusCode = 1;
            result.Msg = userResult.Id.ToString();
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult UpdatePassWord(UpdateShopPassWordInput input)
        {
            ActionResult result = new ActionResult();
            //加密
            input.NewPassWord = CryptographerHelper.Md5Encrypt(input.NewPassWord);
            int updateRes = _adminResponsitory.UpdatePassWord(input);
            if (updateRes == 0)
            {
                result.StatusCode = 0;
                result.Error = "修改密码失败！";
                return result;
            }
            result.StatusCode = 1;
            result.Msg = "OK";
            return result;
        }
    }
}
