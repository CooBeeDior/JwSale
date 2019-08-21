using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class DelChatRoomMemberRequest : WechatRequestBase
    {
        /// <summary>
        /// 群号
        /// </summary>
        public string chatroom { get; set; }
        /// <summary>
        /// 待删除帐号列表 支持同时删除多人
        /// </summary>
        public IList<ChatRoomMember> list { get; set; }
    }
}
