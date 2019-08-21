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


  
}
