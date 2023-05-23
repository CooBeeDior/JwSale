using System;
using System.Collections.Generic;
using System.Text;
using JwSale.Model.Dto.Common;

namespace JwSale.Model.Dto.Response
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

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 角色权限列表
        /// </summary>
        public IList<BriefInfo> Permissions { get; set; }

    }

 
}
