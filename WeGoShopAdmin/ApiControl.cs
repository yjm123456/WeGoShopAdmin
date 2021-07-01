using Domain.BasicClass;
using Domain.DTO;
using IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeGoShopAdmin
{
    public class ApiControl : ApiController
    {
        public readonly IAdminLoginService _adminLoginService;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="adminLoginService"></param>
        public ApiControl(IAdminLoginService adminLoginService)
        {
            _adminLoginService = adminLoginService;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string Login(AdminQueryInput input)
        {
            Service.Utity.MD5EncryptHelper MD5Helper = new Service.Utity.MD5EncryptHelper();
            input. PassWord = MD5Helper.MD5Encrypt(input.PassWord);
            var returnParam = _adminLoginService.Login(input);
            var res = JsonConvert.SerializeObject(returnParam);
            return res;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}