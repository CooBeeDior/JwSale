using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    public class UnBindWechatUserRequest : RequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        [Required]
        public string WxOpenId { get; set; }
    }
}
