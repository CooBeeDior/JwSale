using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class QuitChatRoomRequest:WechatRequestBase
    {
        /// <summary>
        /// 群号
        /// </summary>
        public string chatroom { get; set; }
    }
}
