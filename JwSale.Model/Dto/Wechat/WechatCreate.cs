using JwSale.Model.Dto.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class WechatCreateRequest  
    {
        /// <summary>
        /// 二维码登录时为空，账号密码登录时必填
        /// </summary>
        public string user { get; set; }

        /// <summary>
        /// 二维码登录时为空，账号密码登录时必填
        /// </summary>
        public string pass { get; set; }

        /// <summary>
        /// 二维码登录时为空，账号密码登录时必填 同时支持62和A16
        /// </summary>
        public string deviceID { get; set; }


        public ProxyInfo proxyInfo { get; set; }
    }



}
