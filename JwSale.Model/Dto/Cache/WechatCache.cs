using JwSale.Model.Dto.Common;
using JwSale.Model.Dto.Wechat;
using JwSale.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Cache
{
    public class WechatCache
    {
        public string Token { get; set; }

        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录类型 1：扫码 2:62或a16
        /// </summary>
        public LoginType LoginType { get; set; }

        public CheckLoginQrCodeResponse CheckLoginQrCode { get; set; }

        public ManualAuthResponse ManualAuth { get; set; }


        public ProxyInfo ProxyInfo { get; set; }
    }

 
}
