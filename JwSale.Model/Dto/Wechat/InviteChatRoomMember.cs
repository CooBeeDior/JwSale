using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class InviteChatRoomMemberRequest : WechatRequestBase
    {
        /// <summary>
        /// 群id
        /// </summary>
        public string chatroom { get; set; }

        /// <summary>
        /// 好友列表 
        /// </summary>
        public IList<ChatRoomMember> list { get; set; }
    }
}
