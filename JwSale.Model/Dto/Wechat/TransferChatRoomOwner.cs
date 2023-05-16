using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class TransferChatRoomOwnerRequest : WechatRequestBase
    {
        /// <summary>
        /// 群号
        /// </summary>
        public string chatroom { get; set; }

        /// <summary>
        /// 新群主
        /// </summary>
        public string wxid { get; set; }
    }
}
