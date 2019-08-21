using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class NewVerifyPasswdRequest : WechatRequestBase
    {
        /// <summary>
        /// 原始密码
        /// </summary>
        public string pass { get; set; }
    }
}
