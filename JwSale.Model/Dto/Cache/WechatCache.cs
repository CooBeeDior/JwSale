using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Wechat;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Cache
{
    public class WechatCache
    {
        public string Token { get; set; }

        public CheckLoginQrCodeResponse CheckLoginQrCode { get; set; }

        public ManualAuthResponse ManualAuth { get; set; }


        public ProxyInfo ProxyInfo { get; set; }
    }

 
}
