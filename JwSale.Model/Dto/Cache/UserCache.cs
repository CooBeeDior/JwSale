using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Cache
{
    /// <summary>
    /// 用户缓存
    /// </summary>
    public class UserCache
    {        /// <summary>
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
    }
}
