using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class AddChatRoomMemberRequest : WechatRequestBase
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

    public class AddChatRoomMemberResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponse baseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int memberCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MemberListItem> memberList { get; set; }
    }
}
