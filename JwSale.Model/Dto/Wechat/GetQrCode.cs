using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class GetQrCodeRequest : WechatRequestBase
    {
        /// <summary>
        /// wxid 登wxid 或 群聊ID
        /// </summary>
        public string userName { get; set; }
    }
}
