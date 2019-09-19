using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class GetChatRoomInfoRequest
    {
        /// <summary>
        /// 所属微信Id
        /// </summary>
        public string BelongWxId { get; set; }

        /// <summary>
        /// 微信Id
        /// </summary>
        public string ChatRoomId { get; set; }
    }
}
