using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Model.Dto.Wechat
{
    public class ForkOfficialAccountRequest : WechatRequestBase
    {
        /// <summary>
        /// 公众号AppId
        /// </summary>
        public string AppId { get; set; }
    }
}
