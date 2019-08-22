using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ExtdeviceLoginconfirmOkRequest : WechatRequestBase
    {
        /// <summary>
        /// 网址 设备url地址
        /// </summary>
        public string url { get; set; }
    }
}
