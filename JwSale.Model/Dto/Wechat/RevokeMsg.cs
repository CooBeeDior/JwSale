using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class RevokeMsgRequest:WechatRequestBase
    {
        /// <summary>
        /// 接收对象wxid
        /// </summary>
        public string toUserName { get; set; }

        /// <summary>
        /// 撤回的消息 ID
        /// </summary>
        public string newMsgId { get; set; }
    }
}
