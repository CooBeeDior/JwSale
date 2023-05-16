using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ExtdeviceLoginconfirmGetRequest : WechatRequestBase
    {        
        /// <summary>
        /// 取扫码登录设备请求包接口返回
        /// </summary>
        public string url { get; set; }
    }
}
