using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Response.User
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginResponse : IResponse
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }


    }
}
