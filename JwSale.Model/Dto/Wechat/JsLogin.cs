using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class JsLoginRequest : WechatRequestBase
    {
        /// <summary>
        /// 授权URL
        /// </summary>
        public string oauthUrl { get; set; }
    }
}
