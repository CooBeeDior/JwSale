using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class DelSafeDeviceRequest : WechatRequestBase
    {
        /// <summary>
        /// uuid
        /// </summary>
        public string uuid { get; set; }
    }
}
