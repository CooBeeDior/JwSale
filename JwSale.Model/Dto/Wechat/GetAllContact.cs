using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class GetAllContactRequest : WechatRequestBase
    {
        /// <summary>
        /// 默认传0
        /// </summary>
        public string currentWxcontactSeq { get; set; }
        /// <summary>
        /// 默认传0
        /// </summary>
        public string currentChatRoomContactSeq { get; set; }


    }
}
