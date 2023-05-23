using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request
{
    public class WechatLoginRequest: RequestBase
    {
        /// <summary>
        /// 登录Code
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
