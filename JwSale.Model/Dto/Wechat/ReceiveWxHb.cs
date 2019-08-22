using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ReceiveWxHbRequest : WechatRequestBase
    {
        /// <summary>
        /// URL编码
        /// </summary>
        public string nativeurl { get; set; }
        
        /// <summary>
        /// sendid
        /// </summary>
        public string sendid { get; set; }
    }
}
