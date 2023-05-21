using JwSale.Model.Dto.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Request.User
{
    public class BindWechatUserRequest : RequestBase
    {
        
        /// <summary>
        /// 微信号
        /// </summary>
        public string WxNo { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImageUrl { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string PhoneNumer { get; set; }
    }
}
