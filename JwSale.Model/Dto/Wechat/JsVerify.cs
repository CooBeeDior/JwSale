using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class JsVerifyRequest : WechatRequestBase
    {
        /// <summary>
        /// 小程序ID
        /// </summary>
        public string appID { get; set; }
    }
}
