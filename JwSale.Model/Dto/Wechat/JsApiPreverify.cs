using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class JsApiPreverifyRequest : WechatRequestBase
    {
        public string url { get; set; }

        public string appid { get; set; }

        public string timestamp { get; set; }

        public string noncestr { get; set; }

        public string signature { get; set; }

 
    }
}
