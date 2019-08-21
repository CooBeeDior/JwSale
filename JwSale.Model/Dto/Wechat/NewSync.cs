using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class NewSyncRequest : WechatRequestBase
    {
        /// <summary>
        /// 3同步新消息，5同步通讯录，262151新消息和通讯录一起同步
        /// </summary>
        public string selector { get; set; }
    }
}
