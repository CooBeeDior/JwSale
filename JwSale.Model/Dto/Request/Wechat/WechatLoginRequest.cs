using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class WechatLoginRequest
    {
        /// <summary>
        /// 登录Code
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
