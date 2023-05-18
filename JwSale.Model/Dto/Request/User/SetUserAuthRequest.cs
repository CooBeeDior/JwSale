using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    /// <summary>
    /// 设置用户授权
    /// </summary>
    public class SetUserAuthRequest : RequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string UserId { get; set; }
        /// <summary>
        /// 类型 1:端口授权  2:月租授权
        /// </summary>
        public int Type { get; set; }

   
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredTime { get; set; }
    }
}
