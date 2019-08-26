using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class CreateChatRoomRequest : WechatRequestBase
    {
        /// <summary>
        /// 群聊昵称
        /// </summary>
        public string nickName { get; set; }

        /// <summary>
        /// wxid_1(必须为创建人),wxid_2,wxid3(至少三个人)创建群聊wxid数据文本
        /// </summary>
        public IList<ChatRoomMember> list { get; set; }

    }

    public class CreateChatRoomResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseResponse baseResponse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Topic topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PyInitial pYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public QuanPin quanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int memberCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MemberListItem> memberList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ChatRoomName chatRoomName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ImgBuf imgBuf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bigHeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string smallHeadImgUrl { get; set; }
    }


}
