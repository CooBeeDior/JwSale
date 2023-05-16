using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class WechatResponseBase
    {
        public string code { get; set; }

        public string name { get; set; }

        public string url { get; set; }

        public string packet { get; set; }

        public string token { get; set; }

        public string message { get; set; }

        public string describe { get; set; }
    }
}
