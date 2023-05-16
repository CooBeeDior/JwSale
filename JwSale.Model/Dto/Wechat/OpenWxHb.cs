using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class OpenWxHbRequest : WechatRequestBase
    {
        
        /// <summary>
        /// 编码_URL编码
        /// </summary>
        public string nativeurl { get; set; }
        /// <summary>
        /// sendid
        /// </summary>
        public string sendid { get; set; }
        /// <summary>
        /// 来源群或人
        /// </summary>
        public string sessionUserName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string timingIdentifier { get; set; }

   
    }
}
