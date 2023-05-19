using JwSale.Model.Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Cache
{
    /// <summary>
    /// 用户缓存
    /// </summary>
    public class UserCache
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
        /// 登录设备
        /// </summary>
        public string LoginDevice { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 角色权限列表
        /// </summary>
        public IList<BriefInfo> Permissions { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }
    }


}