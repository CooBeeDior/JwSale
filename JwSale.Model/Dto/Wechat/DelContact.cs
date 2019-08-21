using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class DelContactRequest : WechatRequestBase
    {
        /// <summary>
        /// 被删除帐号
        /// </summary>
        public string wxid { get; set; }
    }
}
