using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class GetChatRoomListRequest : RequestPageBase
    {
        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }
        /// <summary>
        /// 搜索关键词 群号和群Id
        /// </summary>
        public string KeyWords { get; set; }
    }
}
