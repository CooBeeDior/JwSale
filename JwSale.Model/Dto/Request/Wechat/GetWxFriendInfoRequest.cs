using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Request.Wechat
{
    public class GetWxFriendInfoRequest
    {
        /// <summary>
        /// 所属微信Id
        /// </summary>
        public string BelongWxId { get; set; }

        /// <summary>
        /// 微信Id
        /// </summary>
        public string WxId { get; set; }
    }
}
