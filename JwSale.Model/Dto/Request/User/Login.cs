using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    /// <summary>
    /// 登录
    /// </summary>
    public class Login : RequestBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
